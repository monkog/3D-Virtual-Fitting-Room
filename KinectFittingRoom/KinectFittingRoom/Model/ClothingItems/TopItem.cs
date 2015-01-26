using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class TopItem : ClothingItemBase
    {
        /// <summary>
        /// Hips width with margins
        /// </summary>
        private const double Ratio = 1.5;
        #region .ctor
        /// <summary>
        /// Constructor of Top object
        /// </summary>
        /// <param name="model">3D model</param>
        /// <param name="bottomJoint">Bottom joint to track size</param>
        public TopItem(Model3DGroup model, JointType bottomJoint)
            : base(model, Ratio)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = bottomJoint;
            DeltaPosition = 0;
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
