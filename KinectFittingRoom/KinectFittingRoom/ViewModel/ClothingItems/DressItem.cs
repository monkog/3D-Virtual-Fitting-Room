using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class DressItem : ClothingItemBase
    {
        /// <summary>
        /// Hips width with margins
        /// </summary>
        private const double Ratio = 1.5;
        #region .ctor
        /// <summary>
        /// Constructor of Dress object
        /// </summary>
        /// <param name="model">3D model</param>
        public DressItem(Model3DGroup model)
            : base(model, Ratio)
        {
            JointToTrackPosition = JointType.HipCenter;
            LeftJointToTrackAngle = JointType.ShoulderLeft;
            RightJointToTrackAngle = JointType.ShoulderRight;
        }
        #endregion .ctor
    }
}
