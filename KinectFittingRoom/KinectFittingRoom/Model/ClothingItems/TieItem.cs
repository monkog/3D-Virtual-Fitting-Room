using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class TieItem : ClothingItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TieItem"/> class.
        /// </summary>
        /// <param name="model">3D model</param>
        /// <param name="ratio">Size ratio</param>
        /// <param name="deltaY">Default Y position</param>
        public TieItem(Model3DGroup model, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.ShoulderCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = JointType.Spine;
        }
    }
}
