using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    class SkirtItem : ClothingItemBase
    {
        /// <summary>
        /// Constructor of Skirt object
        /// </summary>
        /// <param name="image">Image of item</param>
        /// <param name="imageWidthToItemWidth">Proportion image width to significant width of item</param>
        public SkirtItem(Bitmap image, double imageWidthToItemWidth)
            : base(image, imageWidthToItemWidth)
        {
        }

        /// <summary>
        ///Set position for skirt
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        public override void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            System.Windows.Point head = KinectService.GetJointPoint(skeleton.Joints[JointType.Head], sensor, width, height); ;
            System.Windows.Point footLeft = KinectService.GetJointPoint(skeleton.Joints[JointType.FootLeft], sensor, width, height);
            System.Windows.Point spine = KinectService.GetJointPoint(skeleton.Joints[JointType.Spine], sensor, width, height);

            double heightToWidth = Height / Width;
            double newWidth = (footLeft.Y - head.Y) * 0.18;
            Width = ImageWidthToItemWidth * newWidth;
            Height = heightToWidth * Width;
            Top = spine.Y + 20;
            Left = spine.X - Width / 2;
        }
    }
}
