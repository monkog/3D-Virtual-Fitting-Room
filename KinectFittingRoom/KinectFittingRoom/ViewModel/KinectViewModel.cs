using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using KinectFittingRoom.ViewModel.ButtonItems;

namespace KinectFittingRoom.ViewModel
{
    /// <summary>
    /// View model for MainWindow
    /// </summary>
    public class KinectViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        /// <summary>
        /// Height of the camera image
        /// </summary>
        private static double _cameraImageHeight;
        /// <summary>
        /// Width of the camera image
        /// </summary>
        private static double _cameraImageWidth;
        /// <summary>
        /// Image from the Kinect 
        /// </summary>
        private WriteableBitmap _kinectCameraImage;
        /// <summary>
        /// The Kinect service
        /// </summary>
        private KinectService _kinectService;
        /// <summary>
        /// The clothing category collection
        /// </summary>
        private ObservableCollection<ClothingCategory> _clothingCategory;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets the width of the view model.
        /// </summary>
        /// <value>
        /// The width of the view model.
        /// </value>
        public static double Width
        { get { return _cameraImageWidth; } }
        /// <summary>
        /// Gets the height of the view model.
        /// </summary>
        /// <value>
        /// The height of the view model.
        /// </value>
        public static double Height
        { get { return _cameraImageHeight; } }
        /// <summary>
        /// Gets or sets the width of the camera image.
        /// </summary>
        public double CameraImageWidth
        {
            get { return _cameraImageWidth; }
            set
            {
                if (_cameraImageWidth == value)
                    return;
                _cameraImageWidth = value;
                OnPropertyChanged("CameraImageWidth");
            }
        }
        /// <summary>
        /// Gets or sets the height of the camera image.
        /// </summary>
        public double CameraImageHeight
        {
            get { return _cameraImageHeight; }
            set
            {
                if (_cameraImageHeight == value)
                    return;
                _cameraImageHeight = value;
                OnPropertyChanged("CameraImageHeight");
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
                if (_kinectCameraImage == value)
                    return;
                _kinectCameraImage = value;
                OnPropertyChanged("KinectCameraImage");
            }
        }
        /// <summary>
        /// Gets or sets the clothing categories collection.
        /// </summary>
        /// <value>
        /// The clothing categories collection.
        /// </value>
        public ObservableCollection<ClothingCategory> ClothingCategories
        {
            get { return _clothingCategory; }
            set
            {
                if (_clothingCategory == value)
                    return;
                _clothingCategory = value;
                OnPropertyChanged("ClothingCategory");
            }
        }

        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="KinectViewModel"/> class.
        /// </summary>
        /// <param name="kinectService">The kinect service.</param>
        public KinectViewModel(KinectService kinectService)
        {
            _kinectService = kinectService;
            _kinectService.PropertyChanged += _kinectService_PropertyChanged;
            _kinectService.Initialize();
        }
        #endregion .ctor
        #region Private Methods
        /// <summary>
        /// Handles PropertyChanged event on _kinectService
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _kinectService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "KinectCameraImage":
                    KinectCameraImage = _kinectService.KinectCameraImage;
                    break;
            }
        }
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
        }
        #endregion Private Methods
        #region Event Handlers
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Event Handlers
        #region Protected Methods
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="property">The property.</param>
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion Protected Methods
    }
}
