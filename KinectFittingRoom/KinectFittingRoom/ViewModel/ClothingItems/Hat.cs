using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class Hat : ClothingItemBase
    {
        /// <summary>
        /// Constructor of Hat object
        /// </summary>
        /// <param name="image">Image of item</param>
        /// <param name="significantWidth">Significant width of hat</param>
        public Hat(Bitmap image, double imageWidthToItemWidth)
        {
            Image = image;
            ImageWidthToItemWidth = imageWidthToItemWidth;
            Category = 0;

            for (int i = ClothingManager.Instance.ChosenClothes.Count - 1; i >= 0; i--)
                if (ClothingManager.Instance.ChosenClothes[i].Category == 0)
                    ClothingManager.Instance.ChosenClothes.RemoveAt(i);
        }

        /// <summary>
        ///Set position for hat
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        public override void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            System.Windows.Point head = KinectService.GetJointPoint(skeleton.Joints[JointType.Head], sensor, width, height);
            System.Windows.Point shoulderLeft = KinectService.GetJointPoint(skeleton.Joints[JointType.ShoulderLeft], sensor, width, height);
            System.Windows.Point shoulderRight = KinectService.GetJointPoint(skeleton.Joints[JointType.ShoulderRight], sensor, width, height);

            double heightToWidth = Height / Width;
            double newWidth = (shoulderRight.X - shoulderLeft.X) * 0.5;
            Width = ImageWidthToItemWidth * newWidth;
            Height = heightToWidth * Width;
            Top = head.Y - 10;
            Left = head.X - Width / 2;
        }
    }
}
