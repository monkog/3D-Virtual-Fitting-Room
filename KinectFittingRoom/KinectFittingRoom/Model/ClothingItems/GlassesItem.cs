using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.Model.ClothingItems
{
    class GlassesItem : ClothingItemBase
    {
        /// <summary>
        /// The ratio
        /// </summary>
        private const double Ratio = 0.3;
        /// <summary>
        /// Initializes a new instance of the <see cref="GlassesItem"/> class.
        /// </summary>
        /// <param name="model">3D model</param>
        public GlassesItem(Model3DGroup model)
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