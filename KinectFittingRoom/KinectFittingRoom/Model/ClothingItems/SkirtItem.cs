using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class SkirtItem : ClothingItemBase
    {
        /// <summary>
        /// Hips width with margins
        /// </summary>
        private const double Ratio = 0.9;
        #region .ctor
        /// <summary>
        /// Constructor of Skirt object
        /// </summary>
        /// <param name="model">3D model</param>
        /// <param name="bottomJoint">Bottom joint to track size</param>
        public SkirtItem(Model3DGroup model, JointType bottomJoint)
            : base(model, Ratio)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.HipLeft;
            RightJointToTrackAngle = JointType.HipRight;
            LeftJointToTrackScale = JointType.HipCenter;
            RightJointToTrackScale = bottomJoint;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SkirtItem"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SkirtItem(Model3DGroup model)
            : this(model, JointType.KneeRight)
        { }
        #endregion .ctor
    }
}
