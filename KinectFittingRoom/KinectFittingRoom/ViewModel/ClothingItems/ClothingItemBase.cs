using KinectFittingRoom.ViewModel.Debug;
using Microsoft.Kinect;
using System;
using System.Drawing;
using System.Windows;
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
        /// Constructor of ClothingItemBase object, removes clothes from the same category
        /// </summary>
        /// <param name="image">Image of item</param>
        /// <param name="proportion">Proportion image width to significant width </param>
        /// <param name="part">The part of set</param>
        public ClothingItemBase(Bitmap image, double proportion, double part)
        {
            _image = image;
            _proportion = proportion;
            _part = part;

            for (int i = ClothingManager.Instance.ChosenClothes.Count - 1; i >= 0; i--)
                if (ClothingManager.Instance.ChosenClothes[i]._part == part)
                    ClothingManager.Instance.ChosenClothes.RemoveAt(i);
        }
        /// <summary>
        /// Invokes setting the item's position
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        public void UpdateItemPosition(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            if (skeleton == null) return;

            TrackSkeletonParts(skeleton, sensor, width, height);
        }
        /// <summary>
        ///Set position for part of set
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        private void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            ///_part=0 => hat
            ///_part=1 => skirt
            ///_part=2 => glasses

            double newWidth, shoulderWidth;
            double heightWidth = Height / Width;
            System.Windows.Point shoulderLeft, shoulderRight, head = KinectService.GetJointPoint(skeleton.Joints[JointType.Head], sensor, width, height); ;
            switch ((int)_part)
            {
                case 0:
                    shoulderLeft = KinectService.GetJointPoint(skeleton.Joints[JointType.ShoulderLeft], sensor, width, height);
                    shoulderRight = KinectService.GetJointPoint(skeleton.Joints[JointType.ShoulderRight], sensor, width, height);

                    shoulderWidth = shoulderRight.X - shoulderLeft.X;
                    newWidth = 0.5 * shoulderWidth;
                    Width = _proportion * newWidth;
                    Height = Width * heightWidth;
                    Top = head.Y - 10;
                    Left = head.X - Width / 2;
                    break;
                case 1:
                    System.Windows.Point footLeft = KinectService.GetJointPoint(skeleton.Joints[JointType.FootLeft], sensor, width, height);
                    System.Windows.Point spine = KinectService.GetJointPoint(skeleton.Joints[JointType.Spine], sensor, width, height);

                    double theHeight = footLeft.Y - head.Y;
                    newWidth = theHeight * 0.18;
                    Width = _proportion * newWidth;
                    Height = Width * heightWidth;
                    Top = spine.Y + 20;
                    Left = spine.X - Width / 2;
                    break;
                case 2:
                    shoulderLeft = KinectService.GetJointPoint(skeleton.Joints[JointType.ShoulderLeft], sensor, width, height);
                    shoulderRight = KinectService.GetJointPoint(skeleton.Joints[JointType.ShoulderRight], sensor, width, height);

                    shoulderWidth = shoulderRight.X - shoulderLeft.X;
                    newWidth = 0.5 * shoulderWidth;
                    Width = _proportion * newWidth;
                    Height = Width * heightWidth;
                    Top = head.Y + 50;
                    Left = head.X - Width / 2;
                    break;
            }
        }
        #endregion Methods
    }
}
