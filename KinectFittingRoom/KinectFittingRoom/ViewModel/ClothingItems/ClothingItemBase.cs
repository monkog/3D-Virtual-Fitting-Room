using KinectFittingRoom.ViewModel.Debug;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
namespace KinectFittingRoom.ViewModel.ClothingItems
{
    public abstract class ClothingItemBase : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// Proportion image width to significant width of item
        /// </summary>
        private double _imageWidthToItemWidth;
        /// <summary>
        /// Path to original image of item
        /// </summary>
        private string _pathToImage;
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
        /// Gets the proportion of image width to significant width of item
        /// </summary>
        public double ImageWidthToItemWidth
        {
            get
            {
                return _imageWidthToItemWidth;
            }
        }
        /// <summary>
        /// Gets the path to original image of item
        /// </summary>
        public string PathToImage 
        {
            get
            {
                return _pathToImage;
            }
        }

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
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClothingItemBase"/> class.
        /// </summary>
        /// <param name="pathToImage">Path to original image of item</param>
        /// <param name="imageWidthToItemWidth">Proportion image width to significant width of item</param>
        public ClothingItemBase(string pathToImage, double imageWidthToItemWidth)
        {
            Image = new Bitmap(Bitmap.FromFile(pathToImage));
            _pathToImage = pathToImage;
            _imageWidthToItemWidth = imageWidthToItemWidth;
        }
        #endregion
        #region Methods
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
        public abstract void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height);
        #endregion Methods
        public enum ClothingType
        {
            HatItem,
            SkirtItem,
            GlassesItem
        }  
    }
}
