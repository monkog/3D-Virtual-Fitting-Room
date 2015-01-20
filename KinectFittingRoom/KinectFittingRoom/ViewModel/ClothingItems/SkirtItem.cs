using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class SkirtItem : ClothingItemBase
    {
        /// <summary>
        /// Hips width with margins
        /// </summary>
        private const double Margins = 1.8;
        #region .ctor
        /// <summary>
        /// Constructor of Skirt object
        /// </summary>
        /// <param name="model">3D model</param>
        public SkirtItem(Model3DGroup model)
            : base(model)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.HipLeft;
            RightJointToTrackAngle = JointType.HipRight;
        }
        #endregion .ctor
        /// <summary>
        /// Fit width of model to width of body
        /// </summary>
        /// <param name="skeleton">The skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image heigh</param>
        public override void GetBasicWidth(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            var leftHip = KinectService.GetJointPoint(skeleton.Joints[JointType.HipLeft], sensor, width, height);
            var rightHip = KinectService.GetJointPoint(skeleton.Joints[JointType.HipRight], sensor, width, height);
            var t = Model.Bounds.SizeX * ModelSizeRatio;
            _heightScale = WidthScale = (rightHip.X - leftHip.X) * Margins / t;
        }
    }
}
