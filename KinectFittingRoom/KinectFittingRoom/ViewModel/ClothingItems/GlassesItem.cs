using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class GlassesItem : ClothingItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlassesItem"/> class.
        /// </summary>
        /// <param name="model">3D model</param>
        public GlassesItem(Model3DGroup model)
            : base(model)
        {
            JointToTrackPosition = JointType.Head;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
        }
    }
}