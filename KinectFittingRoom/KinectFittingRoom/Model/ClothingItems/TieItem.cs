using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class TieItem : ClothingItemBase
    {
        /// <summary>
        /// The ratio
        /// </summary>
        private const double Ratio = 1;
        /// <summary>
        /// The factor to move model in Y coordinate
        /// </summary>
        private const double DeltaY = 1.05;
        /// <summary>
        /// Initializes a new instance of the <see cref="TieItem"/> class.
        /// </summary>
        /// <param name="model">3D model</param>
        public TieItem(Model3DGroup model)
            : base(model, Ratio, DeltaY)
        {
            JointToTrackPosition = JointType.ShoulderCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = JointType.Spine;
        }
    }
}
