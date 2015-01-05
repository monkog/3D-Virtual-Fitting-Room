using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class HatItem : ClothingItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HatItem"/> class.
        /// </summary>
        /// <param name="model">3D model of the hat</param>
        public HatItem(Model3DGroup model)
            : base(model)
        {
            JointToTrackPosition = JointType.ShoulderCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
        }
    }
}
