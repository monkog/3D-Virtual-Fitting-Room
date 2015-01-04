using System;
using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    public abstract class ClothingItemBase : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// The clothing model
        /// </summary>
        private Model3DGroup _model;
        /// <summary>
        /// the height scale
        /// </summary>
        private double _heightScale;
        /// <summary>
        /// The width scale
        /// </summary>
        private double _widthScale;
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
        public Transform3D BaseTransformation { get; protected set; }
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
                SetBaseTransformation();
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
        protected ClothingItemBase(Model3DGroup model)
        {
            Model = model;
            HeightScale = WidthScale = 1;
        }
        #endregion
        #region Protected Methods
        /// <summary>
        /// Tracks the rotation angle between two joints.
        /// </summary>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="joint1">The first joint.</param>
        /// <param name="joint2">The second joint.</param>
        /// <returns>Rotation angle between two joints or NaN if at least one of the joints isn't tracked</returns>
        protected double TrackJointsRotation(KinectSensor sensor, Joint joint1, Joint joint2)
        {
            if (joint1.TrackingState == JointTrackingState.NotTracked
                || joint2.TrackingState == JointTrackingState.NotTracked)
                return double.NaN;

            var rightHip = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint1.Position, sensor.DepthStream.Format);
            var leftHip = sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint2.Position, sensor.DepthStream.Format);

            return -(Math.Atan(((double)rightHip.Depth - leftHip.Depth) / ((double)leftHip.X - rightHip.X)) * 180.0 / Math.PI);
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
        ///Set position for part of set
        /// </summary>
        /// <param name="skeleton">Recognised skeleton</param>
        /// <param name="sensor">Kinect sensor</param>
        /// <param name="width">Kinect image width</param>
        /// <param name="height">Kinect image height</param>
        public void TrackSkeletonParts(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            Angle = TrackJointsRotation(sensor, skeleton.Joints[LeftJointToTrackAngle], skeleton.Joints[RightJointToTrackAngle]);

            var joint = KinectService.MapJointPointTo3DSpace(
                KinectService.GetJointPoint(skeleton.Joints[JointToTrackPosition], sensor, width, height), width / 2, height / 2);

            var transform = new Transform3DGroup();
            transform.Children.Add(BaseTransformation);
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Angle)));
            transform.Children.Add(new TranslateTransform3D(joint.X, joint.Y, 0));
            Model.Transform = transform;
        }
        #endregion Public Methods
        #region Private Methods
        /// <summary>
        /// Sets the base transformation.
        /// </summary>
        private void SetBaseTransformation()
        {
            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new ScaleTransform3D(_widthScale, 1, _widthScale));
            transform.Children.Add(new ScaleTransform3D(1, _heightScale, 1));
            BaseTransformation = transform;
        }
        #endregion Private Methods
        #region Enums
        public enum ClothingType
        {
            HatItem,
            SkirtItem,
            GlassesItem
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
