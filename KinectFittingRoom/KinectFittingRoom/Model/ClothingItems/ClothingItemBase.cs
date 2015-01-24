using System;
using System.Windows;
using System.Windows.Media.Media3D;
using KinectFittingRoom.ViewModel;
using Microsoft.Kinect;
using Petzold.Media3D;

namespace KinectFittingRoom.Model.ClothingItems
{
    public abstract class ClothingItemBase : ViewModelBase
    {
        #region Protected Fields
        /// <summary>
        /// Tolerance of the width
        /// </summary>
        protected double Tolerance;
        #endregion Protected Fields
        #region Private Fields
        /// <summary>
        /// The height scale
        /// </summary>
        private double _heightScale;
        /// <summary>
        /// The width scale
        /// </summary>
        private double _widthScale;
        /// <summary>
        /// The height model scale. Determined by the measurement of model.
        /// </summary>
        private double _heightModelScale;
        /// <summary>
        /// The width model scale. Determined by the measurement of model.
        /// </summary>
        private double _widthModelScale;
        /// <summary>
        /// The clothing model
        /// </summary>
        private Model3DGroup _model;
        /// <summary>
        /// The basic bounds of the model
        /// </summary>
        private Rect3D _basicBounds;
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
        /// Gets the matrix for scale transform.
        /// </summary>
        /// <value>
        /// The matrix for scale transform.
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
        /// Gets or sets the value to move the model in Y coordinate.
        /// </summary>
        /// <value>
        /// The value to move the model in Y coordinate.
        /// </value>
        public double DeltaPosition { get; set; }
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
        /// <summary>
        /// Gets or sets the left joint to track scale.
        /// </summary>
        /// <value>
        /// The left joint to track scale.
        /// </value>
        public JointType LeftJointToTrackScale { get; protected set; }
        /// <summary>
        /// Gets or sets the right joint to track scale.
        /// </summary>
        /// <value>
        /// The right joint to track scale.
        /// </value>
        public JointType RightJointToTrackScale { get; protected set; }
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
            DeltaPosition = 0;
            Tolerance = tolerance;
            _widthScale = _heightScale = 1;
        }
        #endregion
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
        #endregion Public Methods
        #region Private Methods
        /// <summary>
        /// Set position for part of set
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        private void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            Angle = TrackJointsRotation(sensor, skeleton.Joints[LeftJointToTrackAngle], skeleton.Joints[RightJointToTrackAngle]);

            var joint = KinectService.GetJointPoint(skeleton.Joints[JointToTrackPosition], sensor, width, height);

            LineRange range;
            Point2DtoPoint3D(new Point(joint.X, joint.Y + DeltaPosition), out range);
            var point3D = range.PointFromZ(0);

            FitModelToBody(skeleton.Joints[LeftJointToTrackScale], skeleton.Joints[RightJointToTrackScale], sensor, width, height, Angle);

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

            try
            {
                matxViewport.Invert();
                matxCamera.Invert();

                Point3D pointNormalized = matxViewport.Transform(point);
                pointNormalized.Z = 0.01;
                Point3D pointNear = matxCamera.Transform(pointNormalized);
                pointNormalized.Z = 0.99;
                Point3D pointFar = matxCamera.Transform(pointNormalized);

                range = new LineRange(pointNear, pointFar);

            }
            catch (Exception)
            {
                range = new LineRange();
            }
        }
        /// <summary>
        /// Sets the base transformation.
        /// </summary>
        private void SetScaleTransformation()
        {
            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new ScaleTransform3D(_widthScale * _widthModelScale, 1, _widthScale * _widthModelScale));
            transform.Children.Add(new ScaleTransform3D(1, _heightScale * _heightModelScale, 1));
            ScaleTransformation = transform;
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
        /// Tracks the rotation angle between two joints.
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="leftJoint">The first joint.</param>
        /// <param name="rightJoint">The second joint.</param>
        /// <returns>Rotation angle between two joints</returns>
        private double TrackJointsRotation(KinectSensor sensor, Joint leftJoint, Joint rightJoint)
        {
            if (leftJoint.TrackingState == JointTrackingState.NotTracked
               || rightJoint.TrackingState == JointTrackingState.NotTracked)
                return double.NaN;

            var jointLeftPosition = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(leftJoint.Position, sensor.DepthStream.Format);
            var jointRightPosition = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(rightJoint.Position, sensor.DepthStream.Format);

            return -(Math.Atan(((double)jointLeftPosition.Depth - jointRightPosition.Depth)
                / ((double)jointRightPosition.X - jointLeftPosition.X)) * 180.0 / Math.PI);
        }
        /// <summary>
        /// Fit width of model to width of body
        /// </summary>
        /// <param name="joint1">The first joint.</param>
        /// <param name="joint2">The second joint.</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private void FitModelToBody(Joint joint1, Joint joint2, KinectSensor sensor, double width, double height, double angle)
        {
            if (joint1.TrackingState == JointTrackingState.NotTracked
                || joint2.TrackingState == JointTrackingState.NotTracked)
                WidthScale = HeightScale = 0.1;

            var joint1Position = KinectService.GetJointPoint(joint1, sensor, width, height);
            var joint2Position = KinectService.GetJointPoint(joint2, sensor, width, height);

            var location = _basicBounds.Location;
            Point leftBound = Point3DtoPoint2D(location);
            Point rightBound =
                Point3DtoPoint2D(new Point3D(location.X + _basicBounds.SizeX, location.Y + _basicBounds.SizeY
                    , location.Z + _basicBounds.SizeZ));

            double ratio = (Math.Abs(joint1Position.Y - joint2Position.Y) / Math.Abs(leftBound.Y - rightBound.Y));
            _widthModelScale = _heightModelScale = ratio * Tolerance;
            SetScaleTransformation();
        }
        #endregion Private Methods
        #region Enums
        public enum ClothingType
        {
            HatItem,
            SkirtItem,
            GlassesItem,
            DressItem,
            TieItem,
            BagItem
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
