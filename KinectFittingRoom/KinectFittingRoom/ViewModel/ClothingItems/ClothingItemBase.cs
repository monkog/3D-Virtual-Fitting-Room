using System;
using System.Windows;
using System.Windows.Media.Media3D;
using Microsoft.Kinect;
using Petzold.Media3D;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    public abstract class ClothingItemBase : ViewModelBase
    {
        #region Protected Fields
        /// <summary>
        /// The height scale
        /// </summary>
        protected double _heightScale;
        /// <summary>
        /// Tolerance of the width
        /// </summary>
        protected double Tolerance;
        #endregion Protected Fields
        #region Private Fields
        /// <summary>
        /// The width scale
        /// </summary>
        private double _widthScale;
        /// <summary>
        /// The clothing model
        /// </summary>
        private Model3DGroup _model;
        /// <summary>
        /// The basic bounds of the model
        /// </summary>
        private Rect3D _basicBounds;
        /// <summary>
        /// The delta between positions
        /// </summary>
        private double _deltaPosition;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the rotation angle.
        /// </summary>
        /// <value>
        /// The rotation angle.
        /// </value>
        public double Angle { get; set; }
        /// <summary>
        /// Gets the base transformation for all other transformations.
        /// </summary>
        /// <value>
        /// The base transformation.
        /// </value>
        public Transform3D ScaleTransformation { get; protected set; }
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public Model3DGroup Model
        {
            get { return _model; }
            set
            {
                if (_model == value)
                    return;
                _model = value;
                OnPropertyChanged("Model");
            }
        }
        /// <summary>
        /// Gets or sets the height scale.
        /// </summary>
        /// <value>
        /// The height scale.
        /// </value>
        public double HeightScale
        {
            get { return _heightScale; }
            set
            {
                if (_heightScale == value)
                    return;
                _heightScale = value;
                SetScaleTransformation();
            }
        }
        /// <summary>
        /// Gets or sets the width scale.
        /// </summary>
        /// <value>
        /// The width scale.
        /// </value>
        public double WidthScale
        {
            get { return _widthScale; }
            set
            {
                if (_widthScale == value)
                    return;
                _widthScale = value;
                SetScaleTransformation();
            }
        }
        /// <summary>
        /// Gets or sets the delta value.
        /// </summary>
        /// <value>
        /// The delta value.
        /// </value>
        public double DeltaPosition
        {
            get { return _deltaPosition; }
            set
            {
                if (_deltaPosition == value)
                    return;
                _deltaPosition = value;
            }
        }
        /// <summary>
        /// Gets or sets the joint to track position.
        /// </summary>
        /// <value>
        /// The joint to track position.
        /// </value>
        public JointType JointToTrackPosition { get; protected set; }
        /// <summary>
        /// Gets or sets the left joint to track angle.
        /// </summary>
        /// <value>
        /// The left joint to track angle.
        /// </value>
        public JointType LeftJointToTrackAngle { get; protected set; }
        /// <summary>
        /// Gets or sets the right joint to track angle.
        /// </summary>
        /// <value>
        /// The right joint to track angle.
        /// </value>
        public JointType RightJointToTrackAngle { get; protected set; }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClothingItemBase"/> class.
        /// </summary>
        /// <param name="model">3D model</param>
        /// <param name="tolerance">Tolerance of the model scale</param>
        protected ClothingItemBase(Model3DGroup model, double tolerance)
        {
            Model = model;
            _basicBounds = model.Bounds;
            _deltaPosition = 0;
            Tolerance = tolerance;
        }
        #endregion
        #region Protected Methods
        /// <summary>
        /// Tracks the rotation angle between two joints.
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="leftJoint">The first joint.</param>
        /// <param name="rightJoint">The second joint.</param>
        /// <returns>Rotation angle between two joints</returns>
        protected double TrackJointsRotation(KinectSensor sensor, DepthImagePoint leftJoint, DepthImagePoint rightJoint)
        {
            return -(Math.Atan(((double)leftJoint.Depth - rightJoint.Depth)
                / ((double)rightJoint.X - leftJoint.X)) * 180.0 / Math.PI);
        }
        #endregion Protected Methods
        #region Public Methods
        /// <summary>
        /// Invokes setting the item's position
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        public void UpdateItemPosition(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            if (skeleton == null || skeleton.Joints[LeftJointToTrackAngle].TrackingState == JointTrackingState.NotTracked
                || skeleton.Joints[RightJointToTrackAngle].TrackingState == JointTrackingState.NotTracked
                || skeleton.Joints[JointToTrackPosition].TrackingState == JointTrackingState.NotTracked)
                return;

            TrackSkeletonParts(skeleton, sensor, width, height);
        }
        /// <summary>
        /// Set position for part of set
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        public void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            var leftJoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skeleton.Joints[LeftJointToTrackAngle].Position, sensor.DepthStream.Format);
            var rightJoint = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skeleton.Joints[RightJointToTrackAngle].Position, sensor.DepthStream.Format);

            Angle = TrackJointsRotation(sensor, leftJoint, rightJoint);

            var joint = KinectService.GetJointPoint(skeleton.Joints[JointToTrackPosition], sensor, width, height);

            LineRange range;
            Point2DtoPoint3D(new Point(joint.X, joint.Y + _deltaPosition), out range);
            var point3D = range.PointFromZ(0);

            FitModelToBody(leftJoint, rightJoint, sensor, width, height);

            var transform = new Transform3DGroup();
            transform.Children.Add(ScaleTransformation);
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Angle)));
            transform.Children.Add(new TranslateTransform3D(point3D.X, point3D.Y, point3D.Z));
            Model.Transform = transform;
        }
        /// <summary>
        /// Maps the Point in 2D to point in 3D.
        /// </summary>
        /// <param name="point2D">The 2D point.</param>
        /// <param name="range">The range.</param>
        private void Point2DtoPoint3D(Point point2D, out LineRange range)
        {
            Point3D point = new Point3D(point2D.X, point2D.Y, 0);
            Matrix3D matxViewport = ClothingManager.Instance.ViewportTransform;
            Matrix3D matxCamera = ClothingManager.Instance.CameraTransform;

            matxViewport.Invert();
            matxCamera.Invert();

            Point3D pointNormalized = matxViewport.Transform(point);
            pointNormalized.Z = 0.01;
            Point3D pointNear = matxCamera.Transform(pointNormalized);
            pointNormalized.Z = 0.99;
            Point3D pointFar = matxCamera.Transform(pointNormalized);

            range = new LineRange(pointNear, pointFar);
        }
        /// <summary>
        /// Maps the Point in 3D to point in 2D.
        /// </summary>
        /// <param name="point">The 3D point.</param>
        /// <returns></returns>
        private Point Point3DtoPoint2D(Point3D point)
        {
            Matrix3D matrix = ClothingManager.Instance.CameraTransform;
            matrix.Append(ClothingManager.Instance.ViewportTransform);

            Point3D pointTransformed = matrix.Transform(point);
            return new Point(pointTransformed.X, pointTransformed.Y);
        }
        /// <summary>
        /// Fit width of model to width of body
        /// </summary>
        /// <param name="leftJoint">The left joint.</param>
        /// <param name="rightJoint">The right joint.</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void FitModelToBody(DepthImagePoint leftJoint, DepthImagePoint rightJoint, KinectSensor sensor, double width, double height)
        {
            var location = _basicBounds.Location;
            Point leftBound = Point3DtoPoint2D(location);
            Point rightBound =
                Point3DtoPoint2D(new Point3D(location.X + _basicBounds.SizeX, location.Y + _basicBounds.SizeY
                    , location.Z + _basicBounds.SizeZ));
            var ratio = (Math.Abs(leftJoint.X - rightJoint.X) / Math.Abs(leftBound.X - rightBound.X));
            WidthScale = _heightScale = ratio * Tolerance;
        }
        #endregion Public Methods
        #region Private Methods
        /// <summary>
        /// Sets the base transformation.
        /// </summary>
        private void SetScaleTransformation()
        {
            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new ScaleTransform3D(_widthScale, 1, _widthScale));
            transform.Children.Add(new ScaleTransform3D(1, _heightScale, 1));
            ScaleTransformation = transform;
        }
        #endregion Private Methods
        #region Enums
        public enum ClothingType
        {
            HatItem,
            SkirtItem,
            GlassesItem,
            DressItem,
            TieItem
        }

        public enum MaleFemaleType
        {
            Male,
            Female,
            Both
        }
        #endregion Enums
    }
}
