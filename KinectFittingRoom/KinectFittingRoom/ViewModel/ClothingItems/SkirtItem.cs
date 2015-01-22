using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class SkirtItem : ClothingItemBase
    {
        /// <summary>
        /// Hips width with margins
        /// </summary>
        private const double Ratio = 1.2;
        #region .ctor
        /// <summary>
        /// Constructor of Skirt object
        /// </summary>
        /// <param name="model">3D model</param>
        public SkirtItem(Model3DGroup model)
            : base(model, Ratio)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.HipLeft;
            RightJointToTrackAngle = JointType.HipRight;
            LeftJointToTrackScale = JointType.HipLeft;
            RightJointToTrackScale = JointType.HipRight;
        }
        #endregion .ctor
    }
}
