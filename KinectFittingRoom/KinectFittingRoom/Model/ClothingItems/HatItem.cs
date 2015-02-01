using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class HatItem : ClothingItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HatItem"/> class.
        /// </summary>
        /// <param name="model">3D model of the hat</param>
        /// <param name="ratio">Size ratio</param>
        /// <param name="deltaY">Default Y position</param>
        public HatItem(Model3DGroup model, double ratio, double deltaY)
            : base(model, ratio, deltaY)
        {
            JointToTrackPosition = JointType.Head;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.Head;
            RightJointToTrackScale = JointType.ShoulderCenter;
        }
    }
}
