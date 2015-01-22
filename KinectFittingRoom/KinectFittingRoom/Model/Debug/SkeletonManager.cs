#if DEBUG
using KinectFittingRoom.ViewModel;
using Microsoft.Kinect;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KinectFittingRoom.Model.Debug
{
    /// <summary>
    /// Manages Skeleton parts when debugging
    /// </summary>
    public class SkeletonManager : ViewModelBase
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the skeleton parts.
        /// </summary>
        /// <value>
        /// The skeleton parts.
        /// </value>
        public ObservableCollection<Polyline> SkeletonParts
        {
            get { return _skeletonModels; }
            set
            {
                if (_skeletonModels == value)
                    return;
                if (value.Count > 0)
                {
                    _skeletonModels = value;
                    OnPropertyChanged("SkeletonParts");
                }
            }
        }
        #endregion Public Properties
        #region Private Fields
        /// <summary>
        /// Models of all of the skeletons
        /// </summary>
        private ObservableCollection<Polyline> _skeletonModels;
        #endregion Private Fields
        #region Public Methods
        /// <summary>
        /// Creates skeleton models for each of the tracked skeleton in the array
        /// </summary>
        /// <param name="skeletons">Array of all found skeletons</param>
        /// <param name="brush">Color of the skeleton</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="width">The width of the canvas.</param>
        /// <param name="height">The height of the canvas.</param>
        public void DrawSkeleton(Skeleton[] skeletons, Brush brush, KinectSensor sensor, double width, double height)
        {
            var skeletonModels = new ObservableCollection<Polyline>();
            foreach (var skeleton in skeletons.Where(skeleton => skeleton.TrackingState != SkeletonTrackingState.NotTracked))
            {
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateBody(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateLeftHand(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateRightHand(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateLeftLeg(), sensor, width, height));
                skeletonModels.Add(CreateFigure(skeleton, brush, CreateRightLeg(), sensor, width, height));
            }
            SkeletonParts = skeletonModels;
        }
        #endregion Public Methods
        #region Private Methods
        /// <summary>
        /// Creates a body for skeleton
        /// </summary>
        /// <returns>Array of joint that refer to the body</returns>
        private IEnumerable<JointType> CreateBody()
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
        private IEnumerable<JointType> CreateRightHand()
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
        private IEnumerable<JointType> CreateLeftHand()
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
        private IEnumerable<JointType> CreateRightLeg()
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
        private IEnumerable<JointType> CreateLeftLeg()
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
        /// Creates the skeleton model.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <param name="brush">Brush.</param>
        /// <param name="joints">Joints to create skeleton model from.</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="width">The width of the canvas.</param>
        /// <param name="height">The height of the canvas.</param>
        /// <returns>Skeleton model as a polyline</returns>
        private Polyline CreateFigure(Skeleton skeleton, Brush brush, IEnumerable<JointType> joints
            , KinectSensor sensor, double width, double height)
        {
            var figure = new Polyline { StrokeThickness = 8, Stroke = brush };

            foreach (var joint in joints)
            {
                var jointPoint = KinectService.GetJointPoint(skeleton.Joints[joint], sensor, width, height);
                figure.Points.Add(new Point(jointPoint.X, jointPoint.Y));
            }

            return figure;
        }
        #endregion Private Methods
    }
}
#endif
