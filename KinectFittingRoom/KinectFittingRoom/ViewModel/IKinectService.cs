namespace KinectFittingRoom.ViewModel
{
    /// <summary>
    /// Kinect service interface
    /// </summary>
    public interface IKinectService
    {
        #region Methods
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        void Cleanup();
        #endregion Methods
    }
}
