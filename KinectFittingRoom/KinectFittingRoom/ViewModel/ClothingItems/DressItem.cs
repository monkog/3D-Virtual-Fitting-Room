using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class DressItem : ClothingItemBase
    {
        #region .ctor
        /// <summary>
        /// Constructor of Dress object
        /// </summary>
        /// <param name="model">3D model</param>
        public DressItem(Model3DGroup model)
            : base(model)
        {
            JointToTrackPosition = JointType.HipLeft;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
        }
        #endregion .ctor
    }
}
