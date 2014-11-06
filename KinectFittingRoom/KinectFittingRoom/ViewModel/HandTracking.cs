using System.Windows;
using System.Windows.Media.Media3D;
using KinectFittingRoom.UI.Buttons.Events;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel
{
    class HandTracking
    {
        #region Methods
        /// <summary>
        /// Invokes setting the hand's position if skeleton is not null
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        private void DrawHandCursor(Skeleton skeleton)
        {
            if (skeleton == null)
            {
                //HandCursor.Visibility = Visibility.Collapsed;
                return;
            }

            TrackHand(skeleton.Joints[JointType.HandLeft], skeleton.Joints[JointType.HandRight]);
        }
        /// <summary>
        /// Mapps left and right hand cooridinates to the proper space
        /// </summary>
        /// <param name="leftHand">Left hand joint</param>
        /// <param name="rightHand">Right hand joint</param>
        private void TrackHand(Joint leftHand, Joint rightHand)
        {
            if (leftHand.TrackingState == JointTrackingState.NotTracked && rightHand.TrackingState == JointTrackingState.NotTracked)
                return;

            //DepthImagePoint leftPoint = Kinect.CoordinateMapper.MapSkeletonPointToDepthPoint(leftHand.Position
            //    , Kinect.DepthStream.Format);
            //int lx = (int)((leftPoint.X * KinectCameraImage.ActualWidth / Kinect.DepthStream.FrameWidth));
            //int ly = (int)((leftPoint.Y * KinectCameraImage.ActualHeight / Kinect.DepthStream.FrameHeight));

            //DepthImagePoint rightPoint = Kinect.CoordinateMapper.MapSkeletonPointToDepthPoint(rightHand.Position
            //    , Kinect.DepthStream.Format);
            //int rx = (int)((rightPoint.X * KinectCameraImage.ActualWidth / Kinect.DepthStream.FrameWidth));
            //int ry = (int)((rightPoint.Y * KinectCameraImage.ActualHeight / Kinect.DepthStream.FrameHeight));

            //Point lp = KinectCameraImage.TranslatePoint(new Point(lx, ly), MainGrid);
            //Point rp = KinectCameraImage.TranslatePoint(new Point(rx, ry), MainGrid);
            //HandCursorManager.Instance.HandleHandCursorEvents(HandCursor, new Vector3D(lp.X, lp.Y, leftPoint.Depth), new Point(lx, ly)
            //    , new Vector3D(rp.X, rp.Y, rightPoint.Depth), new Point(rx, ry));
        }
        #endregion Methods
    }
}
