using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class SkirtItem : ClothingItemBase
    {
        #region .ctor

        /// <summary>
        /// Constructor of Skirt object
        /// </summary>
        /// <param name="pathToTexture">Path to texture</param>
        /// <param name="imageWidthToItemWidth">Proportion image width to significant width of item</param>
        /// <param name="model">3D model</param>
        public SkirtItem(string pathToTexture, double imageWidthToItemWidth
            , GeometryModel3D model)
            : base(pathToTexture, imageWidthToItemWidth, model)
        {
            JointToTrack = JointType.HipLeft;
        }
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
            Angle = TrackJointsRotation(sensor, skeleton.Joints[JointType.HipRight], skeleton.Joints[JointType.HipLeft]);

            var transform = new Transform3DGroup();
            transform.Children.Add(BaseTransformation);
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), Angle)));
            Model.Transform = transform;

            transform.Children.Add(new TranslateTransform3D());
            //var head = KinectService.GetJointPoint(skeleton.Joints[JointType.Head], sensor, width, height);
            //var footLeft = KinectService.GetJointPoint(skeleton.Joints[JointType.FootLeft], sensor, width, height);
            //var spine = KinectService.GetJointPoint(skeleton.Joints[JointType.Spine], sensor, width, height);

            //double heightToWidth = Height / Width;
            //double newWidth = (footLeft.Y - head.Y) * 0.18;
            //Width = ImageWidthToItemWidth * newWidth;
            //Height = heightToWidth * Width;
            //Top = spine.Y + 20;
            //Left = spine.X - Width / 2;
            // = TrackJointsRotation(sensor, skeleton.Joints[JointType.HipRight], skeleton.Joints[JointType.HipLeft]);

            
        }
        #endregion Public Methods
    }
}
