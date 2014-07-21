using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Showroom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        /// <summary>
        /// Current KinectSensor
        /// </summary>
        private KinectSensor m_kinectSensor;
        /// <summary>
        /// WritableBitmap that source from Kinect camera is written to
        /// </summary>
        private WriteableBitmap m_cameraSourceBitmap;
        /// <summary>
        /// Bounds of camera source
        /// </summary>
        private Int32Rect m_cameraSourceBounds;
        /// <summary>
        /// Number of bytes per line
        /// </summary>
        private int m_colorStride;
        /// <summary>
        /// Captured skeletons
        /// </summary>
        private Skeleton[] m_skeletons;
        #endregion

        #region Properties
        /// <summary>
        /// Current KinectSensor
        /// </summary>
        public KinectSensor Kinect
        {
            get { return m_kinectSensor; }
            set
            {
                if (m_kinectSensor != value)
                {
                    if (m_kinectSensor != null)
                    {
                        UninitializeKinectSensor(m_kinectSensor);
                        m_kinectSensor = null;
                    }
                    if (value != null && value.Status == KinectStatus.Connected)
                    {
                        m_kinectSensor = value;
                        InitializeKinectSensor(m_kinectSensor);
                    }
                }
            }
        }
        #endregion

        #region Methods
        public MainWindow()
        {
            InitializeComponent();
            Loaded += DiscoverKinectSensors;
            Unloaded += (sender, e) => { Kinect = null; };
        }

        /// <summary>
        /// Enables ColorStream from newly detected KinectSensor and sets output image
        /// </summary>
        /// <param name="sensor">Detected KinectSensor</param>
        private void InitializeKinectSensor(KinectSensor sensor)
        {
            if (sensor != null)
            {
                ColorImageStream colorStream = sensor.ColorStream;
                colorStream.Enable();

                m_cameraSourceBitmap = new WriteableBitmap(colorStream.FrameWidth, colorStream.FrameHeight
                    , 96, 96, PixelFormats.Bgr32, null);
                m_cameraSourceBounds = new Int32Rect(0, 0, colorStream.FrameWidth, colorStream.FrameHeight);
                m_colorStride = colorStream.FrameWidth * colorStream.FrameBytesPerPixel;
                KinectCameraImage.Source = m_cameraSourceBitmap;

                sensor.ColorFrameReady += KinectSensor_ColorFrameReady;

                sensor.SkeletonStream.Enable();
                m_skeletons = new Skeleton[sensor.SkeletonStream.FrameSkeletonArrayLength];
                sensor.SkeletonFrameReady += KinectSensor_SkeletonFrameReady;
                sensor.Start();
            }
        }

        /// <summary>
        /// Disables ColorStream from disconnected KinectSensor
        /// </summary>
        /// <param name="sensor">Disconnected KinectSensor</param>
        private void UninitializeKinectSensor(KinectSensor sensor)
        {
            if (sensor != null)
            {
                sensor.Stop();
                sensor.ColorFrameReady -= KinectSensor_ColorFrameReady;
                sensor.SkeletonFrameReady -= KinectSensor_SkeletonFrameReady;
                sensor.SkeletonStream.Disable();
                m_skeletons = null;
            }
        }

        /// <summary>
        /// Handles ColorFrameReady event
        /// </summary>
        /// <remarks>
        /// Views image from the camera in KinectCameraImage
        /// </remarks>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments containing ImageFrame</param>
        private void KinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame != null)
                {
                    byte[] pixels = new byte[colorFrame.PixelDataLength];
                    colorFrame.CopyPixelDataTo(pixels);

                    m_cameraSourceBitmap.WritePixels(m_cameraSourceBounds, pixels, m_colorStride, 0);
                }
            }
        }

        private void KinectSensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame == null)
                    return;

                Polyline figure;
                Brush brush = Brushes.Coral;
                Skeleton skeleton;

                SkeletonCanvas.Children.Clear();
                frame.CopySkeletonDataTo(m_skeletons);

                for (int i = 0; i < m_skeletons.Length; i++)
                {
                    skeleton = m_skeletons[i];
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        JointType[] joints = new[]
                        {
                            JointType.Head
                            , JointType.ShoulderCenter
                            , JointType.ShoulderLeft
                            , JointType.Spine
                            , JointType.ShoulderRight
                            , JointType.ShoulderCenter
                            , JointType.HipCenter
                            , JointType.HipLeft
                            , JointType.Spine
                            , JointType.HipRight
                            , JointType.HipCenter
                        };

                        SkeletonCanvas.Children.Add(CreateFigure(skeleton, brush, joints));
                    }
                }
            }
        }

        private Polyline CreateFigure(Skeleton skeleton, Brush brush, JointType[] joints)
        {
            Polyline figure = new Polyline();
            figure.StrokeThickness = 8;
            figure.Stroke = brush;

            for (int i = 0; i < joints.Length; i++)
                figure.Points.Add(GetJointPoint(skeleton.Joints[joints[i]]));

            return figure;
        }

        private Point GetJointPoint(Joint joint)
        {
            DepthImagePoint point = Kinect.MapSkeletonPointToDepth(joint.Position, Kinect.DepthStream.Format);

           // point.X *= (int)SkeletonCanvas.ActualWidth / Kinect.DepthStream.FrameWidth;
           // point.Y *= (int)SkeletonCanvas.ActualHeight / Kinect.DepthStream.FrameHeight;

            return new Point(point.X, point.Y);
        }

        /// <summary>
        /// Subscribes for StatusChanged event and initializes KinectSensor
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void DiscoverKinectSensors(object sender, RoutedEventArgs e)
        {
            KinectSensor.KinectSensors.StatusChanged += KinectSensor_StatusChanged;
            Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
        }

        /// <summary>
        /// Updates KinectSensor
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void KinectSensor_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Initializing:
                case KinectStatus.Connected:
                    if (Kinect == null)
                        Kinect = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    if (Kinect == e.Sensor)
                    {
                        Kinect = null;
                        Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                        if (Kinect == null)
                            //TODO: Notify about no sensors connected
                            throw new NotImplementedException();
                    }
                    break;
                default:
                    //TODO: Notify about error
                    throw new NotImplementedException();
            }
        }
        #endregion
    }
}
