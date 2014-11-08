using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using KinectFittingRoom.ViewModel.ButtonItems;

namespace KinectFittingRoom.ViewModel
{
    /// <summary>
    /// View model for MainWindow
    /// </summary>
    public class KinectViewModel : ViewModelBase
    {
        #region Private Fields
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
        /// <summary>
        /// Gets the kinect service.
        /// </summary>
        /// <value>
        /// The kinect service.
        /// </value>
        public KinectService KinectService
        {
            get { return _kinectService; }
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
        #endregion Event Handlers
        #region Protected Methods
        #endregion Protected Methods
    }
}
