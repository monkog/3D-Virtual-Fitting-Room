using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class SkirtItem : ClothingItemBase
    {
        #region .ctor
        /// <summary>
        /// Constructor of Skirt object
        /// </summary>
        /// <param name="pathToImage">Path to original image of item</param>
        /// <param name="imageWidthToItemWidth">Proportion image width to significant width of item</param>
        public SkirtItem(string pathToImage, double imageWidthToItemWidth)
            : base(pathToImage, imageWidthToItemWidth)
        { }
        #endregion .ctor
        #region Public Methods
        /// <summary>
        ///Set position for skirt
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        public override void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            double rotationAngle = TrackJointsRotation(sensor, skeleton.Joints[JointType.HipRight], skeleton.Joints[JointType.HipLeft]);
            
            var head = KinectService.GetJointPoint(skeleton.Joints[JointType.Head], sensor, width, height);
            var footLeft = KinectService.GetJointPoint(skeleton.Joints[JointType.FootLeft], sensor, width, height);
            var spine = KinectService.GetJointPoint(skeleton.Joints[JointType.Spine], sensor, width, height);

            double heightToWidth = Height / Width;
            double newWidth = (footLeft.Y - head.Y) * 0.18;
            Width = ImageWidthToItemWidth * newWidth;
            Height = heightToWidth * Width;
            Top = spine.Y + 20;
            Left = spine.X - Width / 2;
        }
        #endregion Public Methods
    }
}
