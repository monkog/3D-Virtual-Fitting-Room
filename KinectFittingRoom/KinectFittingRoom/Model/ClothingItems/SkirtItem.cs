using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class SkirtItem : ClothingItemBase
    {
        #region .ctor
        /// <summary>
        /// Constructor of Skirt object
        /// </summary>
        /// <param name="model">3D model</param>
        /// <param name="bottomJoint">Bottom joint to track size</param>
        /// <param name="ratio">Size ratio</param>
        /// <param name="deltaY">Default Y position</param>
        public SkirtItem(Model3DGroup model, JointType bottomJoint, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.HipLeft;
            RightJointToTrackAngle = JointType.HipRight;
            LeftJointToTrackScale = JointType.HipCenter;
            RightJointToTrackScale = bottomJoint;
        }
        #endregion .ctor
    }
}
