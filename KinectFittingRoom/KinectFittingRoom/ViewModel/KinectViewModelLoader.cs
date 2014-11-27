namespace KinectFittingRoom.ViewModel
{
    /// <summary>
    /// Loads Kinect view model
    /// </summary>
    public class KinectViewModelLoader
    {
        #region Private Fields
        /// <summary>
        /// Kinect view model
        /// </summary>
        static KinectViewModel _kinectViewModel;
        /// <summary>
        /// Kinect service
        /// </summary>
        static KinectService _kinectService;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets the view model.
        /// </summary>
        public KinectViewModel KinectViewModel
        {
            get { return _kinectViewModel ?? (_kinectViewModel = new KinectViewModel(_kinectService)); }
        }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="KinectViewModelLoader"/> class.
        /// </summary>
        public KinectViewModelLoader()
        {
            _kinectService = new KinectService();
            _kinectService.Initialize();
        }
        #endregion .ctor
        #region Public Methods        
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public static void Cleanup()
        {
            if (_kinectViewModel != null)
                _kinectViewModel.Cleanup();

            _kinectService.Cleanup();
        }
        #endregion Public Methods
    }
}
