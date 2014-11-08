using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KinectFittingRoom.UI.Debug;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel
{
    public class KinectService : ViewModelBase, IKinectService
    {
        #region Variables
        /// <summary>
        /// Captured skeletons
        /// </summary>
        private Skeleton[] _skeletons;
        /// <summary>
        /// Current KinectSensor
        /// </summary>
        private KinectSensor _kinectSensor;
        /// <summary>
        /// WritableBitmap that source from Kinect camera is written to
        /// </summary>
        private WriteableBitmap _kinectCameraImage;
        /// <summary>
        /// Bounds of camera source
        /// </summary>
        private Int32Rect _cameraSourceBounds;
        /// <summary>
        /// Number of bytes per line
        /// </summary>
        private int _colorStride;
        /// <summary>
        /// Models of all of the skeletons
        /// </summary>
        private ObservableCollection<Polyline> _skeletonModels;
        #endregion
        #region Public Properties
        /// <summary>
        /// Current KinectSensor
        /// </summary>
        public KinectSensor Kinect
        {
            get { return _kinectSensor; }
            set
            {
                if (_kinectSensor != value)
                {
                    if (_kinectSensor != null)
                    {
                        UninitializeKinectSensor(_kinectSensor);
                        _kinectSensor = null;
                    }
                    if (value != null && value.Status == KinectStatus.Connected)
                    {
                        _kinectSensor = value;
                        InitializeKinectSensor(_kinectSensor);
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets the Kinect camera image.
        /// </summary>
        /// <value>
        /// The Kinect camera image.
        /// </value>
        public WriteableBitmap KinectCameraImage
        {
            get { return _kinectCameraImage; }
            set
            {
                if (Equals(_kinectCameraImage, value))
                    return;
                _kinectCameraImage = value;
                OnPropertyChanged("KinectCameraImage");
            }
        }
        /// <summary>
        /// Gets or sets the skeleton parts.
        /// </summary>
        /// <value>
        /// The skeleton parts.
        /// </value>
        public ObservableCollection<Polyline> SkeletonParts
        {
            get { return _skeletonModels; }
            set
            {
                if (_skeletonModels == value)
                    return;
                if (value.Count > 0)
                {
                    _skeletonModels = value;
                    OnPropertyChanged("SkeletonParts");
                }
            }
        }
        /// <summary>
        /// Parent canvas for all buttons
        /// </summary>
        //public Canvas ParentButtonCanvas { get { return ButtonCanvas; } }
        #endregion
        #region Private Methods
        /// <summary>
        /// Enables ColorStream from newly detected KinectSensor and sets output image
        /// </summary>
        /// <param name="sensor">Detected KinectSensor</param>
        private void InitializeKinectSensor(KinectSensor sensor/*, ref WriteableBitmap kinectCameraImage*/)
        {
            if (sensor != null)
            {
                ColorImageStream colorStream = sensor.ColorStream;
                colorStream.Enable();

                KinectCameraImage = new WriteableBitmap(colorStream.FrameWidth, colorStream.FrameHeight
                    , 96, 96, PixelFormats.Bgr32, null);
                _cameraSourceBounds = new Int32Rect(0, 0, colorStream.FrameWidth, colorStream.FrameHeight);
                _colorStride = colorStream.FrameWidth * colorStream.FrameBytesPerPixel;

                sensor.ColorFrameReady += KinectSensor_ColorFrameReady;

                sensor.SkeletonStream.AppChoosesSkeletons = false;
                sensor.SkeletonStream.Enable();
                _skeletons = new Skeleton[sensor.SkeletonStream.FrameSkeletonArrayLength];
                sensor.SkeletonFrameReady += KinectSensor_SkeletonFrameReady;
                try
                {
                    sensor.Start();
                }
                catch (Exception exc)
                {
                    // TODO: Handle IOException when Kinect is being used by another process
                    MessageBox.Show(exc.Message);
                }
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
                _skeletons = null;
            }
        }
        /// <summary>
        /// Handles SkeletonFrameReady event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments containing SkeletonFrame</param>
        private void KinectSensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame == null || frame.SkeletonArrayLength == 0)
                    return;
                frame.CopySkeletonDataTo(_skeletons);

                var skeleton = GetPrimarySkeleton(_skeletons);
                //DrawHandCursor(skeleton);

#if DEBUG
                Brush brush = Brushes.Coral;
                try
                {
                    SkeletonParts = SkeletonModel.DrawSkeleton(_skeletons, brush, _kinectSensor
                        , Application.Current.MainWindow.ActualWidth, Application.Current.MainWindow.ActualHeight);
                }
                catch (Exception)
                {
                }
#endif
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
                    var pixels = new byte[colorFrame.PixelDataLength];
                    colorFrame.CopyPixelDataTo(pixels);

                    KinectCameraImage.WritePixels(_cameraSourceBounds, pixels, _colorStride, 0);
                    OnPropertyChanged("KinectCameraImage");
                }
            }
        }
        /// <summary>
        /// Subscribes for StatusChanged event and initializes KinectSensor
        /// </summary>
        private void DiscoverKinectSensors()
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
        #endregion Private Methods
        #region Public Methods
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            DiscoverKinectSensors();
        }
        /// <summary>
        /// Looks for the closest skeleton
        /// </summary>
        /// <param name="skeletons">All skeletons recognised by Kinect</param>
        /// <returns>The skeleton closestto the sensor</returns>
        public static Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;

            if (skeletons != null)
                foreach (Skeleton skelet in skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked))
                    if (skeleton == null || skelet.Position.Z < skeleton.Position.Z)
                        skeleton = skelet;

            return skeleton;
        }
        /// <summary>
        /// Mapps a point from Kinect space to canvas space
        /// </summary>
        /// <param name="joint">Joint to map</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="width">Width of the canvas.</param>
        /// <param name="height">Height of the canvas.</param>
        /// <returns>Mapped point</returns>
        public static Point GetJointPoint(Joint joint, KinectSensor sensor, double width, double height)
        {
            var point = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, sensor.DepthStream.Format);

            return new Point(point.X * (width / sensor.DepthStream.FrameWidth)
                , point.Y * (height / sensor.DepthStream.FrameHeight));
        }
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            Kinect = null;
        }
        #endregion Public Methods
        #region Protected Methods
        #endregion Protected Methods
    }
}
