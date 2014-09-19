using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Kinect;

namespace KinectFittingRoom
{
    partial class MainWindow
    {
        /// <summary>
        /// Sets hand cursor on the screen
        /// </summary>
        /// <param name="leftHand"></param>
        /// <param name="rightHand"></param>
        private void SetHandCursor(Joint leftHand, Joint rightHand)
        {
            Joint hand = GetPrimaryHand(leftHand, rightHand);
            TrackHand(hand);
        }

        /// <summary>
        /// Gets the primary hand
        /// </summary>
        /// <param name="leftHand"></param>
        /// <param name="rightHand"></param>
        /// <returns>Hand closest to Kinect</returns>
        private Joint GetPrimaryHand(Joint leftHand, Joint rightHand)
        {
            Joint hand = new Joint();

            hand = leftHand;

            if (rightHand.TrackingState != JointTrackingState.NotTracked)
                if (hand.TrackingState == JointTrackingState.NotTracked
                    || hand.Position.Z > rightHand.Position.Z)
                    hand = rightHand;

            return hand;
        }

        /// <summary>
        /// Keeps track of the hand
        /// </summary>
        /// <param name="hand">Primary hand</param>
        private void TrackHand(Joint hand)
        {
            if (hand.TrackingState == JointTrackingState.NotTracked)
            {
                // TODO: Why doesn't it work????
                //HandCursor.Visibility = Visibility.Collapsed;
                return;
            }

            // TODO: Why doesn't it work????
            //HandCursor.Visibility = Visibility.Visible;

            DepthImagePoint point = Kinect.CoordinateMapper.MapSkeletonPointToDepthPoint(hand.Position
                , Kinect.DepthStream.Format);
            int x = (int)((point.X * KinectCameraImage.ActualWidth / Kinect.DepthStream.FrameWidth));
            int y = (int)((point.Y * KinectCameraImage.ActualHeight / Kinect.DepthStream.FrameHeight));

            Canvas.SetLeft(HandCursor, x - (HandCursor.ActualWidth / 2.0));
            Canvas.SetTop(HandCursor, y - (HandCursor.Height / 2.0));
        }
    }
}
