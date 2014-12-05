using KinectFittingRoom.ViewModel.Debug;
using Microsoft.Kinect;
using System;
using System.Drawing;
namespace KinectFittingRoom.ViewModel.ClothingItems
{
    public class ClothingItemBase : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// Proportion image width to significant width 
        /// </summary>
        private double _proportion;
        /// <summary>
        /// The part of set
        /// </summary>
        private double _part;
        /// <summary>
        /// The image
        /// </summary>
        private Bitmap _image;
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
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The height.
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

        #region Methods
        /// <summary>
        /// Constructor of ClothingItemBase object
        /// </summary>
        /// <param name="image">Image of item</param>
        /// <param name="proportion">Proportion image width to significant width </param>
        /// <param name="part">The part of set</param>
        /// <param name="skeleton">Recognised skeleton</param>
        public ClothingItemBase(Bitmap image, double proportion, double part)
        {
            _image = image;
            _proportion = proportion;
            _part = part;
            MatchItemToSkeleton();
        }
        
        /// <summary>
        /// Match part os set to the proper space
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        private void MatchItemToSkeleton()
        {
            ///_part=0 => hat
            ///_part=1 => skirt
            ///_part=2 => glasses
            Skeleton skeleton = KinectService.GetPrimarySkeleton(KinectService.Skeletons);
            double newWidth,shoulderWidth;
            double heightWidth = Height / Width;
            switch ((int)this._part)
            {
                case 0:
                    shoulderWidth = skeleton.Joints[JointType.ShoulderRight].Position.X - skeleton.Joints[JointType.ShoulderLeft].Position.X;
                    newWidth = 0.5 * shoulderWidth;
                   // Width = _proportion * newWidth;
                    Width = 300;
                    Height = Width*heightWidth;
                    Top = skeleton.Joints[JointType.Head].Position.Y;
                    Left = skeleton.Joints[JointType.Head].Position.X - Width / 2;
                    break;
                case 1:
                    double height = skeleton.Joints[JointType.Head].Position.Y - skeleton.Joints[JointType.FootLeft].Position.Y;
                    newWidth = height * 0.36;
                    Width = 200;
                    Height = Width * heightWidth;
                   // Width = _proportion * newWidth;
                    Top = skeleton.Joints[JointType.Spine].Position.Y;
                    Left = skeleton.Joints[JointType.Spine].Position.X - Width / 2;
                    break;
                case 2:
                    shoulderWidth = skeleton.Joints[JointType.ShoulderRight].Position.X - skeleton.Joints[JointType.ShoulderLeft].Position.X;
                    newWidth = 0.5 * shoulderWidth;
                    // Width = _proportion * newWidth;
                    Width = 300;
                    Height = Width * heightWidth;
                    Top = skeleton.Joints[JointType.Head].Position.Y+50;
                    Left = skeleton.Joints[JointType.Head].Position.X - Width / 2;
                    break;
            }
        }
        #endregion Methods
    }
}
