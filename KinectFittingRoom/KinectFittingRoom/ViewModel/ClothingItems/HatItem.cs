using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class HatItem : ClothingItemBase
    {
        private const double RegularProportionShoulderWidthToHead = 0.6;
        /// <summary>
        /// Initializes a new instance of the <see cref="HatItem"/> class.
        /// </summary>
        /// <param name="model">3D model of the hat</param>
        public HatItem(Model3DGroup model)
            : base(model)
        {
            JointToTrackPosition = JointType.Head;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
        }
        /// <summary>
        /// Fit width of model to width of body
        /// </summary>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image heigh</param>
        public override void GetBasicWidth(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            var leftShoulder = KinectService.GetJointPoint(skeleton.Joints[JointType.ShoulderLeft], sensor, width, height); ;
            var rightShoulder = KinectService.GetJointPoint(skeleton.Joints[JointType.ShoulderRight], sensor, width, height);
            var t = Model.Bounds.SizeX * ModelSizeRatio;
            HeightScale = WidthScale = (rightShoulder.X - leftShoulder.X) * (RegularProportionShoulderWidthToHead / t);
        }
    }
}
