#if DEBUG
using KinectFittingRoom.ViewModel.Debug;
#endif
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Microsoft.Kinect;
using KinectFittingRoom.ViewModel.ClothingItems;

namespace KinectFittingRoom.ViewModel
{
    public class KinectService : ViewModelBase, IKinectService
    {
        #region Constants
        /// <summary>
        /// Base width of user screen
        /// </summary>
        private double BaseScreenWidth = 1366;
        #endregion Constants
        #region Private Fields
        /// <summary>
        /// Captured skeletons
        /// </summary>
        private Skeleton[] _skeletons;
        /// <summary>
        /// Height of skeleton before zooming
        /// </summary>
        private double _skeletonHeight;
        /// <summary>
        /// Current KinectSensor
        /// </summary>
        private KinectSensor _kinectSensor;
        /// <summary>
        /// WritableBitmap that source from Kinect camera is written to
        /// </summary>
        private WriteableBitmap _kinectCameraImage;
        /// <summary>
        /// The width of orthographic camera
        /// </summary>
        private double _cameraWidth;
        /// <summary>
        /// Width of orthographic camera before scaling
        /// </summary>
        private double _originalCameraWidth;
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
        private HandTracking _hand;
#if DEBUG
        /// <summary>
        /// The skeleton manager
        /// </summary>
        private SkeletonManager _skeletonManager;
#endif
        /// <summary>
        /// Visibility of ErrorGrid 
        /// </summary>
        private Visibility _errorGridVisibility;
        /// <summary>
        /// Visibility of ClothesArea 
        /// </summary>
        private Visibility _clothesAreaVisibility;
        /// <summary>
        /// The image width
        /// </summary>
        private double _imageWidth;
        /// <summary>
        /// The image height
        /// </summary>
        private double _imageHeight;
        /// <summary>
        /// The error grid message
        /// </summary>
        private string _errorGridMessage;
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
                //if (_kinectSensor != value)
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
        /// Gets or sets the width of camera
        /// </summary>
        public double CameraWidth
        {
            get { return _cameraWidth; }
            set
            {
                if (_cameraWidth == value)
                    return;
                _cameraWidth = value;
                OnPropertyChanged("CameraWidth");
            }
        }
        /// <summary>
        /// Gets or sets the hand.
        /// </summary>
        /// <value>
        /// The hand.
        /// </value>z
        public HandTracking Hand
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
#if DEBUG
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
#endif
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
                ClothingManager.Instance.EmptySpace = SystemParameters.PrimaryScreenWidth - value;
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
        /// Gets or sets visibility of ClothesArea
        /// </summary>
        /// <value>
        /// The visibility of ClothesArea
        /// </value>
        public Visibility ClothesAreaVisibility
        {
            get { return _clothesAreaVisibility; }
            set
            {
                if (_clothesAreaVisibility == value)
                    return;
                _clothesAreaVisibility = value;
                OnPropertyChanged("ClothesAreaVisibility");
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
        /// <summary>
        /// Gets or sets the error grid message.
        /// </summary>
        /// <value>
        /// The error grid message.
        /// </value>
        public string ErrorGridMessage
        {
            get { return _errorGridMessage; }
            set
            {
                if (_errorGridMessage == value)
                    return;
                _errorGridMessage = value;
                OnPropertyChanged("ErrorGridMessage");
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
                catch (Exception)
                {
                    UninitializeKinectSensor(sensor);
                    Kinect = null;
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Kinect jest używany przez inny proces." + Environment.NewLine +
                        "Spróbuj odłączyć i ponownie podłączyć urządzenie do komputera." + Environment.NewLine +
                        "Upewnij się, że wszystkie programy używajace Kinecta zostały wyłączone.";
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
                if (skeleton == null)
                {
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Nie wykryto szkieletu lub utracono jego położenie." + Environment.NewLine +
                        "Poczekaj chwilę i sprawdź, czy stoisz w odpowiedniej odległości od urządzenia.";
                    ClothesAreaVisibility = Visibility.Hidden;
                    return;
                }
                if (ClothesAreaVisibility == Visibility.Hidden)
                {
                    ErrorGridVisibility = Visibility.Collapsed;
                    ClothesAreaVisibility = Visibility.Visible;
                }
                Hand.UpdateHandCursor(skeleton, Kinect, Width, Height);

                SetCameraWidth(skeleton, Kinect, Width, Height);
                ClothingManager.Instance.UpdateItemPosition(skeleton, Kinect, Width, Height);
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
            {
                ErrorGridVisibility = Visibility.Visible;
                ErrorGridMessage = "Proszę podłączyć Kinect";
            }
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
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Inicjalizacja Kinecta...";
                    break;
                case KinectStatus.Connected:
                    ErrorGridVisibility = Visibility.Hidden;
                    if (Kinect == null)
                        Kinect = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    if (Kinect == e.Sensor)
                    {
                        Kinect = null;
                        Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                        if (Kinect == null)
                        {
                            ErrorGridVisibility = Visibility.Visible;
                            ErrorGridMessage = "Podłącz Kinect do komputera.";
                        }
                    }
                    break;
                case KinectStatus.NotPowered:
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Podłącz kabel zasilający do gniazdka.";
                    break;
                default:
                    ErrorGridVisibility = Visibility.Visible;
                    ErrorGridMessage = "Kinect nie może być uruchomiony. Poczekaj chwilę lub uruchom program ponownie.";
                    break;
            }
        }
        /// <summary>
        /// Sets new width of camera after skeleton update
        /// </summary>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="sensor">The sensor</param>
        /// <param name="width">The width</param>
        /// <param name="height">The Height</param>
        private void SetCameraWidth(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            if (ClothingManager.Instance.ChosenClothesModels.Count == 0)
                return;

            if (_skeletonHeight == 0)
                _skeletonHeight = KinectService.GetJointPoint(skeleton.Joints[JointType.FootRight], Kinect, Width, Height).Y - KinectService.GetJointPoint(skeleton.Joints[JointType.Head], Kinect, Width, Height).Y;

            var head = KinectService.GetJointPoint(skeleton.Joints[JointType.Head], Kinect, Width, Height);
            var footRight = KinectService.GetJointPoint(skeleton.Joints[JointType.FootRight], Kinect, Width, Height);

            CameraWidth = _originalCameraWidth * _skeletonHeight / (footRight.Y - head.Y);
        }
        #endregion Private Methods
        #region Public Methods
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            Hand = new HandTracking();
#if DEBUG
            SkeletonManager = new SkeletonManager();
#endif
            ErrorGridVisibility = Visibility.Hidden;
            ClothesAreaVisibility = Visibility.Visible;
            DiscoverKinectSensors();
            CameraWidth = _originalCameraWidth = SystemParameters.PrimaryScreenWidth / BaseScreenWidth;
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
        public static Point3D GetJointPoint(Joint joint, KinectSensor sensor, double width, double height)
        {
            var point = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, sensor.DepthStream.Format);

            return new Point3D(point.X * (width / sensor.DepthStream.FrameWidth)
                , point.Y * (height / sensor.DepthStream.FrameHeight), point.Depth);
        }
        /// <summary>
        /// Maps the joint point to 3D space.
        /// </summary>
        /// <param name="joint">The joint coordiates in the screen resolution space.</param>
        /// <param name="width">Half of the Kinect image width</param>
        /// <param name="height">Half of the Kinect image height</param>
        /// <returns>Mapped joint point in 3D space</returns>
        public static Point MapJointPointTo3DSpace(Point joint, double width, double height)
        {
            return new Point((joint.X - width) / width, -(joint.Y - height) / height);
        }
        /// <summary>
        /// Calculates the distance between joints.
        /// </summary>
        /// <param name="joint1">The 1st joint.</param>
        /// <param name="joint2">The 2nd joint.</param>
        /// <returns></returns>
        public static Point CalculateDistanceBetweenJoints(Point joint1, Point joint2)
        {
            return new Point(Math.Abs(joint1.X - joint2.X), Math.Abs(joint1.Y - joint2.Y));
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
