using System.Windows;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel
{
    public class HandTracking : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// The position of the left hand
        /// </summary>
        private Point _leftPosition;
        /// <summary>
        /// The position of the right hand
        /// </summary>
        private Point _rightPosition;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the Position of the left hand.
        /// </summary>
        /// <value>
        /// The Position of the left hand.
        /// </value>
        public Point LeftPosition
        {
            get { return _leftPosition; }
            set
            {
                if (_leftPosition == value)
                    return;
                _leftPosition = value;
                OnPropertyChanged("LeftPosition");
            }
        }
        /// <summary>
        /// Gets or sets the Position of the right hand.
        /// </summary>
        /// <value>
        /// The Position of the right hand.
        /// </value>
        public Point RightPosition
        {
            get { return _rightPosition; }
            set
            {
                if (_rightPosition == value)
                    return;
                _rightPosition = value;
                OnPropertyChanged("RightPosition");
            }
        }
        #endregion Public Properties
        #region Methods
        /// <summary>
        /// Invokes setting the hand's position if skeleton is not null
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        public void UpdateHandCursor(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            if (skeleton == null) return;

            TrackHand(skeleton.Joints[JointType.HandLeft], skeleton.Joints[JointType.HandRight], sensor, width, height);
        }
        /// <summary>
        /// Mapps left and right hand cooridinates to the proper space
        /// </summary>
        /// <param name="leftHand">Left hand joint</param>
        /// <param name="rightHand">Right hand joint</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        private void TrackHand(Joint leftHand, Joint rightHand, KinectSensor sensor, double width, double height)
        {
            if (leftHand.TrackingState == JointTrackingState.NotTracked && rightHand.TrackingState == JointTrackingState.NotTracked)
                return;

            DepthImagePoint leftPoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(leftHand.Position
                , sensor.DepthStream.Format);
            int lx = (int)((leftPoint.X * width / sensor.DepthStream.FrameWidth));
            int ly = (int)((leftPoint.Y * height / sensor.DepthStream.FrameHeight));

            DepthImagePoint rightPoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(rightHand.Position
                , sensor.DepthStream.Format);
            int rx = (int)((rightPoint.X * width / sensor.DepthStream.FrameWidth));
            int ry = (int)((rightPoint.Y * height / sensor.DepthStream.FrameHeight));

            LeftPosition = new Point(lx, ly);
            RightPosition = new Point(rx, ry);
        }
        #endregion Methods
    }
}
