using System;
using System.Windows.Media.Media3D;
using Microsoft.Kinect;
using System.Drawing;
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
        /// Path to the texture of item
        /// </summary>
        private string _pathToTexture;
        /// <summary>
        /// The texture
        /// </summary>
        private Bitmap _texture;
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
        /// The clothing model
        /// </summary>
        private GeometryModel3D _model;
        /// <summary>
        /// The Canvas.Top
        /// </summary>
        private double _top;
        /// <summary>
        /// the height scale
        /// </summary>
        private double _heightScale;
        /// <summary>
        /// The width scale
        /// </summary>
        private double _widthScale;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the rotation angle.
        /// </summary>
        /// <value>
        /// The rotation angle.
        /// </value>
        public double Angle { get; set; }
        /// <summary>
        /// Gets the base transformation for all other transformations.
        /// </summary>
        /// <value>
        /// The base transformation.
        /// </value>
        public Transform3D BaseTransformation { get; protected set; }
        /// <summary>
        /// Gets or sets the default transformation of the model (unscaled).
        /// </summary>
        /// <value>
        /// The default transformation.
        /// </value>
        public Transform3D DefaultTransformation { get; protected set; }
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
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public GeometryModel3D Model
        {
            get { return _model; }
            set
            {
                if (_model == value)
                    return;
                _model = value;
                OnPropertyChanged("Model");
            }
        }
        /// <summary>
        /// Gets or sets the height scale.
        /// </summary>
        /// <value>
        /// The height scale.
        /// </value>
        public double HeightScale
        {
            get { return _widthScale; }
            set
            {
                if (_heightScale == value)
                    return;
                _heightScale = value;
                Transform3DGroup transform = new Transform3DGroup();
                transform.Children.Add(DefaultTransformation);
                transform.Children.Add(new ScaleTransform3D(1, _heightScale, 1));
                BaseTransformation = transform;
            }
        }
        /// <summary>
        /// Gets or sets the width scale.
        /// </summary>
        /// <value>
        /// The width scale.
        /// </value>
        public double WidthScale
        {
            get { return _widthScale; }
            set
            {
                if (_widthScale == value)
                    return;
                _widthScale = value;
                Transform3DGroup transform = new Transform3DGroup();
                transform.Children.Add(DefaultTransformation);
                transform.Children.Add(new ScaleTransform3D(_widthScale, 1, 1));
                BaseTransformation = transform;
            }
        }
        /// <summary>
        /// Gets or sets the joint to track.
        /// </summary>
        /// <value>
        /// The joint to track.
        /// </value>
        public JointType JointToTrack { get; protected set; }

#warning Probably delete those.
        /// <summary>
        /// Gets the path to texture of item
        /// </summary>
        public string PathToTexture
        {
            get
            {
                return _pathToTexture;
            }
        }
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public Bitmap Texture
        {
            get { return _texture; }
            set
            {
                if (_texture == value)
                    return;
                _texture = value;
                OnPropertyChanged("Texture");
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
        /// <param name="pathToTexture">Path to original image of item</param>
        /// <param name="imageWidthToItemWidth">Proportion image width to significant width of item</param>
        /// <param name="model">3D model</param>
        protected ClothingItemBase(string pathToTexture, double imageWidthToItemWidth
            , GeometryModel3D model)
        {
            Texture = new Bitmap(Bitmap.FromFile(pathToTexture));
            _pathToTexture = pathToTexture;
            _imageWidthToItemWidth = imageWidthToItemWidth;
            Model = model;
            DefaultTransformation = BaseTransformation = model.Transform;
            HeightScale = WidthScale = 1;
        }
        #endregion
        #region Protected Methods
        /// <summary>
        /// Tracks the rotation angle between two joints.
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="joint1">The first joint.</param>
        /// <param name="joint2">The second joint.</param>
        /// <returns>Rotation angle between two joints or NaN if at least one of the joints isn't tracked</returns>
        protected double TrackJointsRotation(KinectSensor sensor, Joint joint1, Joint joint2)
        {
            if (joint1.TrackingState == JointTrackingState.NotTracked
                || joint2.TrackingState == JointTrackingState.NotTracked)
                return double.NaN;

            var rightHip = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint1.Position, sensor.DepthStream.Format);
            var leftHip = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint2.Position, sensor.DepthStream.Format);

            return -(Math.Atan(((double)rightHip.Depth - leftHip.Depth) / ((double)leftHip.X - rightHip.X)) * 180.0 / Math.PI);
        }
        #endregion Protected Methods
        #region Public Methods
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
        #endregion Public Methods
        #region Enums
        public enum ClothingType
        {
            HatItem,
            SkirtItem,
            GlassesItem
        }

        public enum MaleFemaleType
        {
            Male,
            Female,
            Both
        }
        #endregion Enums
    }
}
