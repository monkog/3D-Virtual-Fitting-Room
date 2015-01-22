using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class BagItem : ClothingItemBase
    {
        /// <summary>
        /// The ratio
        /// </summary>
        private const double Ratio = 1;
        /// <summary>
        /// Initializes a new instance of the <see cref="BagItem"/> class.
        /// </summary>
        /// <param name="model">3D model of the bag</param>
        public BagItem(Model3DGroup model)
            : base(model, Ratio)
        {
            JointToTrackPosition = JointType.HandLeft;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.ShoulderCenter;
            RightJointToTrackScale = JointType.HipCenter;
        }
    }
}