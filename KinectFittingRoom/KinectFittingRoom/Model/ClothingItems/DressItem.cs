using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class DressItem : ClothingItemBase
    {
        #region .ctor
        /// <summary>
        /// Constructor of Dress object
        /// </summary>
        /// <param name="model">3D model</param>
        /// <param name="bottomJoint">Bottom joint to track size</param>
        /// <param name="ratio">Hips width with margins</param>
        /// <param name="deltaY">Default Y position</param>
        public DressItem(Model3DGroup model, JointType bottomJoint, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = bottomJoint;
        }
        #endregion .ctor
    }
}
