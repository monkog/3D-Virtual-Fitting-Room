using Microsoft.Kinect;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KinectFittingRoom
{
    public partial class MainWindow
    {
        #region Variables
        /// <summary>
        /// Captured skeletons
        /// </summary>
        private Skeleton[] m_skeletons;
        #endregion

        #region Methods
        /// <summary>
        /// Handles SkeletonFrameReady event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments containing SkeletonFrame</param>
        private void KinectSensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame == null || frame.SkeletonArrayLength == 0)
                    return;
# if DEBUG
                Brush brush = Brushes.Coral;

                SkeletonCanvas.Children.Clear();
#endif
                frame.CopySkeletonDataTo(m_skeletons);
                Skeleton skeleton = GetPrimarySkeleton(m_skeletons);
                DrawHandCursor(skeleton);

#if DEBUG
                foreach (Skeleton skelet in m_skeletons)
                    if (skelet.TrackingState != SkeletonTrackingState.NotTracked)
                        DrawSkeleton(skelet, brush);
#endif
            }
        }

        /// <summary>
        /// Looks for the closest skeleton
        /// </summary>
        /// <param name="skeletons">All skeletons recognised by Kinect</param>
        /// <returns>The skeleton closestto the sensor</returns>
        private Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;

            if (skeletons != null)
                foreach (Skeleton skelet in skeletons)
                    if (skelet.TrackingState == SkeletonTrackingState.Tracked)
                        if (skeleton == null || skelet.Position.Z < skeleton.Position.Z)
                            skeleton = skelet;

            return skeleton;
        }

        /// <summary>
        /// Draws a skeleton on a canvas
        /// </summary>
        /// <param name="skeleton">Skeleton to draw</param>
        /// <param name="brush">Color of the skeleton</param>
        private void DrawSkeleton(Skeleton skeleton, Brush brush)
        {
            SkeletonCanvas.Children.Add(CreateFigure(skeleton, brush, CreateBody()));
            SkeletonCanvas.Children.Add(CreateFigure(skeleton, brush, CreateLeftHand()));
            SkeletonCanvas.Children.Add(CreateFigure(skeleton, brush, CreateRightHand()));
            SkeletonCanvas.Children.Add(CreateFigure(skeleton, brush, CreateLeftLeg()));
            SkeletonCanvas.Children.Add(CreateFigure(skeleton, brush, CreateRightLeg()));
        }

        /// <summary>
        /// Creates a body for skeleton
        /// </summary>
        /// <returns>Array of joint that refer to the body</returns>
        private JointType[] CreateBody()
        {
            return new[]
                        {
                            JointType.Head
                            , JointType.ShoulderCenter
                            , JointType.ShoulderLeft
                            , JointType.Spine
                            , JointType.ShoulderRight
                            , JointType.ShoulderCenter
                            , JointType.HipCenter
                            , JointType.HipLeft
                            , JointType.Spine
                            , JointType.HipRight
                            , JointType.HipCenter
                        };
        }

        /// <summary>
        /// Creates a right hand for skeleton
        /// </summary>
        /// <returns>Array of joint that refer to the right hand</returns>
        private JointType[] CreateRightHand()
        {
            return new[]
                        {
                            JointType.ShoulderRight
                            , JointType.ElbowRight
                            , JointType.WristRight
                            , JointType.HandRight
                        };
        }

        /// <summary>
        /// Creates a left hand for skeleton
        /// </summary>
        /// <returns>Array of joint that refer to the left hand</returns>
        private JointType[] CreateLeftHand()
        {
            return new[]
                        {
                            JointType.ShoulderLeft
                            , JointType.ElbowLeft
                            , JointType.WristLeft
                            , JointType.HandLeft
                        };
        }

        /// <summary>
        /// Creates a right leg for skeleton
        /// </summary>
        /// <returns>Array of joint that refer to the right leg</returns>
        private JointType[] CreateRightLeg()
        {
            return new[]
                        {
                            JointType.HipRight
                            , JointType.KneeRight
                            , JointType.AnkleRight
                            , JointType.FootRight
                        };
        }

        /// <summary>
        /// Creates a left leg for skeleton
        /// </summary>
        /// <returns>Array of joint that refer to the left leg</returns>
        private JointType[] CreateLeftLeg()
        {
            return new[]
                        {
                            JointType.HipLeft
                            , JointType.KneeLeft
                            , JointType.AnkleLeft
                            , JointType.FootLeft
                        };
        }

        /// <summary>
        /// Creates user skeleton
        /// </summary>
        /// <param name="skeleton"></param>
        /// <param name="brush"></param>
        /// <param name="joints"></param>
        /// <returns></returns>
        private Polyline CreateFigure(Skeleton skeleton, Brush brush, JointType[] joints)
        {
            Polyline figure = new Polyline();
            figure.StrokeThickness = 8;
            figure.Stroke = brush;

            for (int i = 0; i < joints.Length; i++)
                figure.Points.Add(GetJointPoint(skeleton.Joints[joints[i]]));

            return figure;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="joint"></param>
        /// <returns></returns>
        private Point GetJointPoint(Joint joint)
        {
            DepthImagePoint point = Kinect.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, Kinect.DepthStream.Format);

            return new Point(point.X * (SkeletonCanvas.ActualWidth / Kinect.DepthStream.FrameWidth)
                , point.Y * (SkeletonCanvas.ActualHeight / Kinect.DepthStream.FrameHeight));
        }
        #endregion
    }
}
