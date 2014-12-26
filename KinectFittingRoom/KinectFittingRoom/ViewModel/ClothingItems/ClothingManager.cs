using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using KinectFittingRoom.ViewModel.ButtonItems;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    public sealed class ClothingManager : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// Only instance of ClothingManager
        /// </summary>
        private static ClothingManager _instance;
        /// <summary>
        /// The chosen clothing models collection
        /// </summary>
        private Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> _chosenClothesModels;
        /// <summary>
        /// The clothing collection
        /// </summary>
        private ObservableCollection<ClothingButtonViewModel> _clothing;
        /// <summary>
        /// The last added item to ChosenClothes collection
        /// </summary>
        private ClothingItemBase _lastAddedItem;
        /// <summary>
        /// The last chosen category
        /// </summary>
        private ClothingCategoryButtonViewModel _lastChosenCategory;
        /// <summary>
        /// Chosen type of clothes
        /// </summary>
        private ClothingItemBase.MaleFemaleType _chosenType;
        /// <summary>
        /// Position of the spine joint
        /// </summary>
        private Vector3D _spinePosition;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the chosen type of clothes
        /// </summary>
        public ClothingItemBase.MaleFemaleType ChosenType
        {
            get { return _chosenType; }
            set
            {
                if (_chosenType == value)
                    return;
                _chosenType = value;
            }
        }
        /// <summary>
        /// Gets or sets last chosen category
        /// </summary>
        public ClothingCategoryButtonViewModel LastChosenCategory
        {
            get { return _lastChosenCategory; }
            set
            {
                if (_lastChosenCategory == value)
                    return;
                _lastChosenCategory = value;
            }
        }
        /// <summary>
        /// Gets or sets the last added item to ChosenClothes collection
        /// </summary>
        public ClothingItemBase LastAddedItem
        {
            get { return _lastAddedItem; }
            set
            {
                if (_lastAddedItem == value)
                    return;
                _lastAddedItem = value;
            }
        }
        /// <summary>
        /// Gets or sets the chosen clothing models collection.
        /// </summary>
        /// <value>
        /// The chosen clothing models collection.
        /// </value>
        public Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> ChosenClothesModels
        {
            get { return _chosenClothesModels; }
            set
            {
                if (_chosenClothesModels == value)
                    return;
                _chosenClothesModels = value;
                OnPropertyChanged("ChosenClothesModels");
            }
        }
        /// <summary>
        /// Gets or sets the available clothing collection.
        /// </summary>
        /// <value>
        /// The available clothing collection.
        /// </value>
        public ObservableCollection<ClothingButtonViewModel> Clothing
        {
            get { return _clothing; }
            set
            {
                if (_clothing == value)
                    return;
                _clothing = value;
                OnPropertyChanged("Clothing");
            }
        }
        /// <summary>
        /// Method with access to only instance of ClothingManager
        /// </summary>
        public static ClothingManager Instance
        {
            get { return _instance ?? (_instance = new ClothingManager()); }
        }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Private constructor of ClothingManager. 
        /// </summary>
        private ClothingManager()
        {
            _chosenType = ClothingItemBase.MaleFemaleType.Female;
            ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
        }
        #endregion .ctor
        #region Protected Methods        

        /// <summary>
        /// Scales images of clothes
        /// </summary>
        /// <param name="ratio">The ratio of scaling</param>
        public void ScaleImage(double ratio)
        {
#warning TODO
            //ClothingItemBase lastItem = LastAddedItem;
            //lastItem.PathToImage = new Bitmap(lastItem.Image, (int) lastItem.Image.Width, (int) (ratio*lastItem.Image.Height));

            //Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ChosenClothes;
            //tmp[tmp.FirstOrDefault(a => a.Value.PathToImage == lastItem.PathToImage).Key] = lastItem;
            //ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }

        /// <summary>
        /// Tracks the joints rotation.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="joint1">The joint1.</param>
        /// <param name="joint2">The joint2.</param>
        /// <returns>Angle between two joints</returns>
        private double TrackJointsRotation(KinectSensor sensor, Joint joint1, Joint joint2)
        {
            if (joint1.TrackingState == JointTrackingState.NotTracked
                || joint2.TrackingState == JointTrackingState.NotTracked)
                return double.NaN;

            var rightHip = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint1.Position, sensor.DepthStream.Format);
            var leftHip = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint2.Position, sensor.DepthStream.Format);

            return (Math.Atan(((double)rightHip.Depth - leftHip.Depth) / ((double)leftHip.X - rightHip.X)) * 180.0 / Math.PI);
        }
        #endregion Protected Methods
        #region Public Methods
        /// <summary>
        /// Sets the spine position
        /// </summary>
        /// <param name="spinePosition">Spine position</param>
        public void SetSpinePosition(Vector3D spinePosition)
        {
            _spinePosition = spinePosition;
        }
        /// <summary>
        /// Updates the item position.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void UpdateItemPosition(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            double rotationAngle = TrackJointsRotation(sensor, skeleton.Joints[JointType.HipRight], skeleton.Joints[JointType.HipLeft]);

            var head = KinectService.GetJointPoint(skeleton.Joints[JointType.Head], sensor, width, height);
            var footLeft = KinectService.GetJointPoint(skeleton.Joints[JointType.FootLeft], sensor, width, height);
            var spine = KinectService.GetJointPoint(skeleton.Joints[JointType.Spine], sensor, width, height);

            foreach (var model in ChosenClothesModels.Values)
            {
                //// Create the animations.
                //DoubleAnimation frontAnimation, backAnimation;
                //this.PrepareForRotation(out frontAnimation, out backAnimation);
                //Point3DAnimation cameraZoomAnim = this.CreateCameraAnimation();

                //// Start the animations.
                //_frontRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, frontAnimation);
                //_backRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, backAnimation);
                //camera.BeginAnimation(PerspectiveCamera.PositionProperty, cameraZoomAnim);
            }
        }
        #endregion Public Methods
    }
}
