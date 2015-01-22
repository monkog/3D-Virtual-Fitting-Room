using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class HatItem : ClothingItemBase
    {
        /// <summary>
        /// The ratio
        /// </summary>
        private const double Ratio = 1.1;
        /// <summary>
        /// Initializes a new instance of the <see cref="HatItem"/> class.
        /// </summary>
        /// <param name="model">3D model of the hat</param>
        public HatItem(Model3DGroup model)
            : base(model, Ratio, verticalTrack: true)
        {
            JointToTrackPosition = JointType.Head;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
            LeftJointToTrackScale = JointType.Head;
            RightJointToTrackScale = JointType.ShoulderCenter;
        }
    }
}
