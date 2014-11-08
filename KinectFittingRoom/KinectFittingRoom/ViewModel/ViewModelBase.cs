using System.ComponentModel;

namespace KinectFittingRoom.ViewModel
{
    /// <summary>
    /// Base class for View Models
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Protected Methods
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="property">The property.</param>
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion Protected Methods
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
