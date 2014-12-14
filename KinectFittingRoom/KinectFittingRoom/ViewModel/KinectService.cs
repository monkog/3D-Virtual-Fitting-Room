using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KinectFittingRoom.ViewModel.Debug;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel
{
    public class KinectService : ViewModelBase, IKinectService
    {
        #region Private Fields
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
        /// User's Hand
        /// </summary>
        private Hand _hand;
        /// <summary>
        /// The skeleton manager
        /// </summary>
        private SkeletonManager _skeletonManager;
        /// <summary>
        /// Visibility of ErrorGrid 
        /// </summary>
        private Visibility _errorGridVisibility;
        /// <summary>
        /// The image width
        /// </summary>
        private double _imageWidth;
        /// <summary>
        /// The image height
        /// </summary>
        private double _imageHeight;
        #endregion Private Fields
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
        /// Gets or sets the hand.
        /// </summary>
        /// <value>
        /// The hand.
        /// </value>z
        public Hand Hand
        {
            get { return _hand; }
            set
            {
                if (_hand == value)
                    return;
                _hand = value;
                OnPropertyChanged("Hand");
            }
        }
        /// <summary>
        /// Gets or sets the skeleton manager.
        /// </summary>
        /// <value>
        /// The skeleton manager.
        /// </value>
        public SkeletonManager SkeletonManager
        {
            get { return _skeletonManager; }
            set
            {
                if (_skeletonManager == value)
                    return;
                _skeletonManager = value;
                OnPropertyChanged("SkeletonManager");
            }
        }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width
        {
            get { return _imageWidth; }
            set
            {
                if (_imageWidth == value)
                    return;
                _imageWidth = value;
                OnPropertyChanged("Width");
            }
        }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height
        {
            get { return _imageHeight; }
            set
            {
                if (_imageHeight == value)
                    return;
                _imageHeight = value;
                OnPropertyChanged("Height");
            }
        }
        /// <summary>
        /// Gets or sets visibility of ErrorGrid
        /// </summary>
        /// <value>
        /// The visibility of ErrorGrid
        /// </value>
        public Visibility ErrorGridVisibility
        {
            get { return _errorGridVisibility; }
            set 
            {
                if (_errorGridVisibility == value)
                    return;
                _errorGridVisibility = value;
                OnPropertyChanged("ErrorGridVisibility");
            }
        }
        #endregion
        #region Private Methods
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
            if (sensor == null) return;
            sensor.Stop();
            sensor.ColorFrameReady -= KinectSensor_ColorFrameReady;
            sensor.SkeletonFrameReady -= KinectSensor_SkeletonFrameReady;
            sensor.SkeletonStream.Disable();
            _skeletons = null;
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
                Hand.UpdateHandCursor(skeleton, Kinect, Width, Height);
                foreach (var c in ClothingItems.ClothingManager.Instance.ChosenClothes)
                    c.Value.UpdateItemPosition(skeleton, Kinect, Width, Height);               
#if DEBUG
                Brush brush = Brushes.Coral;
                SkeletonManager.DrawSkeleton(_skeletons, brush, _kinectSensor, Width, Height);
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
                if (colorFrame == null) return;
                var pixels = new byte[colorFrame.PixelDataLength];
                colorFrame.CopyPixelDataTo(pixels);

                KinectCameraImage.WritePixels(_cameraSourceBounds, pixels, _colorStride, 0);
                OnPropertyChanged("KinectCameraImage");
            }
        }
        /// <summary>
        /// Subscribes for StatusChanged event and initializes KinectSensor
        /// </summary>
        private void DiscoverKinectSensors()
        {
            KinectSensor.KinectSensors.StatusChanged += KinectSensor_StatusChanged;
            Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
            if (Kinect == null)
                ErrorGridVisibility=Visibility.Visible;
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
                    ErrorGridVisibility = Visibility.Hidden;
                    break;
                case KinectStatus.Disconnected:
                    if (Kinect == e.Sensor)
                    {
                        Kinect = null;
                        Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                        if (Kinect == null)
                            ErrorGridVisibility = Visibility.Visible;
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
            Hand = new Hand();
            SkeletonManager = new SkeletonManager();
            ErrorGridVisibility = Visibility.Hidden;
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
    }
}
