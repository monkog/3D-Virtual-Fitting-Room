namespace KinectFittingRoom.ViewModel.ClothingItems
{
    public class ClothingItemBase : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// The image width
        /// </summary>
        private double _imageWidth;
        /// <summary>
        /// The image height
        /// </summary>
        private double _imageHeight;
        /// <summary>
        /// The Canvas.Left
        /// </summary>
        private double _left;
        /// <summary>
        /// The Canvas.Top
        /// </summary>
        private double _top;
        #endregion Private Fields
        #region Public Properties
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
        /// Gets or sets the Canvas.Left.
        /// </summary>
        /// <value>
        /// The Canvas.Left.
        /// </value>
        public double Left
        {
            get { return _left; }
            set
            {
                if (_left == value)
                    return;
                _left = value;
                OnPropertyChanged("Left");
            }
        }
        /// <summary>
        /// Gets or sets the Canvas.Top.
        /// </summary>
        /// <value>
        /// The Canvas.Top.
        /// </value>
        public double Top
        {
            get { return _top; }
            set
            {
                if (_top == value)
                    return;
                _top = value;
                OnPropertyChanged("Top");
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
        #endregion Public Properties
    }
}
