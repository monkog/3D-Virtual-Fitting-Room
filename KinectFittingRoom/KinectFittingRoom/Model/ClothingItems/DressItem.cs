using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class DressItem : ClothingItemBase
    {
        /// <summary>
        /// Hips width with margins
        /// </summary>
        private const double Ratio = 1.7;
        #region .ctor
        /// <summary>
        /// Constructor of Dress object
        /// </summary>
        /// <param name="model">3D model</param>
        /// <param name="bottomJoint">Bottom joint to track size</param>
        public DressItem(Model3DGroup model, JointType bottomJoint)
            : base(model, Ratio)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = bottomJoint;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DressItem"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public DressItem(Model3DGroup model)
            : this(model, JointType.HipCenter)
        { }
        #endregion .ctor
    }
}
