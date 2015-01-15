using Microsoft.Kinect;
using System.Windows.Media.Media3D;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class TieItem : ClothingItemBase
    {
         private const double RegularProportionShoulderWidthToHead = 0.2;
        /// <summary>
         /// Initializes a new instance of the <see cref="TieItem"/> class.
        /// </summary>
        /// <param name="model">3D model</param>
         public TieItem(Model3DGroup model)
            : base(model)
        {
            JointToTrackPosition = JointType.ShoulderCenter;
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
