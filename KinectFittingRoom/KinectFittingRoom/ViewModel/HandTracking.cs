using System.Windows;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel
{
    public class Hand : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// The position of the hand cursor
        /// </summary>
        private Point _position;
        /// <summary>
        /// Visibility of the hand cursor
        /// </summary>
        private Visibility _visibility;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the Position of the hand cursor.
        /// </summary>
        /// <value>
        /// The Position of the hand cursor.
        /// </value>
        public Point Position
        {
            get { return _position; }
            set
            {
                if (_position == value)
                    return;
                _position = value;
                OnPropertyChanged("Position");
            }
        }
        /// <summary>
        /// Gets or sets the visibility of the hand cursor.
        /// </summary>
        /// <value>
        /// The visibility of the hand cursor.
        /// </value>
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                if (_visibility == value)
                    return;
                _visibility = value;
                OnPropertyChanged("Visibility");
            }
        }
        #endregion Public Properties
        #region Methods
        /// <summary>
        /// Invokes setting the hand's position if skeleton is not null
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        public void UpdateHandCursor(Skeleton skeleton, KinectSensor sensor)
        {
            if (skeleton == null)
            {
                Visibility = Visibility.Collapsed;
                return;
            }
            Visibility=Visibility.Visible;

            TrackHand(skeleton.Joints[JointType.HandLeft], skeleton.Joints[JointType.HandRight], sensor);
        }
        /// <summary>
        /// Mapps left and right hand cooridinates to the proper space
        /// </summary>
        /// <param name="leftHand">Left hand joint</param>
        /// <param name="rightHand">Right hand joint</param>
        /// <param name="sensor">Kinect sensor</param>
        private void TrackHand(Joint leftHand, Joint rightHand, KinectSensor sensor)
        {
            if (leftHand.TrackingState == JointTrackingState.NotTracked && rightHand.TrackingState == JointTrackingState.NotTracked)
                return;

            //DepthImagePoint leftPoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(leftHand.Position
            //    , sensor.DepthStream.Format);
            //int lx = (int)((leftPoint.X * KinectCameraImage.ActualWidth / sensor.DepthStream.FrameWidth));
            //int ly = (int)((leftPoint.Y * KinectCameraImage.ActualHeight / sensor.DepthStream.FrameHeight));

            //DepthImagePoint rightPoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(rightHand.Position
            //    , sensor.DepthStream.Format);
            //int rx = (int)((rightPoint.X * KinectCameraImage.ActualWidth / sensor.DepthStream.FrameWidth));
            //int ry = (int)((rightPoint.Y * KinectCameraImage.ActualHeight / sensor.DepthStream.FrameHeight));

            //Point lp = KinectCameraImage.TranslatePoint(new Point(lx, ly), MainGrid);
            //Point rp = KinectCameraImage.TranslatePoint(new Point(rx, ry), MainGrid);
            //HandCursorManager.Instance.HandleHandCursorEvents(HandCursor, new Vector3D(lp.X, lp.Y, leftPoint.Depth), new Point(lx, ly)
            //    , new Vector3D(rp.X, rp.Y, rightPoint.Depth), new Point(rx, ry));
        }
        #endregion Methods
    }
}
