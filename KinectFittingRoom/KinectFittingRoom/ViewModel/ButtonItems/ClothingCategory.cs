using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class ClothingCategory : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// The button's image
        /// </summary>
        private Bitmap _image;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the button's image.
        /// </summary>
        /// <value>
        /// The button's image.
        /// </value>
        public Bitmap Image
        {
            get { return _image; }
            set
            {
                if (_image == value)
                    return;
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        #endregion Public Properties
    }
}
