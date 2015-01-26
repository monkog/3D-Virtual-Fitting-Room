using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class TopItem : ClothingItemBase
    {
        /// <summary>
        /// Hips width with margins
        /// </summary>
        private const double Ratio = 1.2;
        /// <summary>
        /// The factor to move model in Y coordinate
        /// </summary>
        private const double DeltaY = 1.05;
        #region .ctor
        /// <summary>
        /// Constructor of Top object
        /// </summary>
        /// <param name="model">3D model</param>
        /// <param name="bottomJoint">Bottom joint to track size</param>
        public TopItem(Model3DGroup model, JointType bottomJoint)
            : base(model, Ratio, DeltaY)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = bottomJoint;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TopItem"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public TopItem(Model3DGroup model)
            : this(model, JointType.Spine)
        { }
        #endregion .ctor
    }
}
