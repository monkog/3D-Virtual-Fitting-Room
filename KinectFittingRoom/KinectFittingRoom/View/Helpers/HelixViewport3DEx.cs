using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Petzold.Media3D;

namespace KinectFittingRoom.View.Helpers
{
    public class HelixViewport3DEx : HelixViewport3D
    {
        #region Dependency Properties
        /// <summary>
        /// The ViewportTransform property
        /// </summary>
        public static readonly DependencyProperty ViewportTransformProperty = DependencyProperty.RegisterAttached(
            "ViewportTransform", typeof(Matrix3D), typeof(HelixViewport3DEx), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// The CameraTransform property
        /// </summary>
        public static readonly DependencyProperty CameraTransformProperty = DependencyProperty.RegisterAttached(
            "CameraTransform", typeof(Matrix3D), typeof(HelixViewport3DEx), new FrameworkPropertyMetadata(null));
        #endregion Dependency Properties
        #region Public Properties
        /// <summary>
        /// Gets or sets the camera transform.
        /// </summary>
        /// <value>
        /// The camera transform.
        /// </value>
        public Matrix3D CameraTransform
        {
            get { return (Matrix3D)GetValue(CameraTransformProperty); }
            set { SetValue(CameraTransformProperty, value); }
        }
        /// <summary>
        /// Gets or sets the viewport transform.
        /// </summary>
        /// <value>
        /// The viewport transform.
        /// </value>
        public Matrix3D ViewportTransform
        {
            get { return (Matrix3D)GetValue(ViewportTransformProperty); }
            set { SetValue(ViewportTransformProperty, value); }
        }
        #endregion Public Properties
        #region Public Methods
        /// <summary>
        /// Sets the ViewportTransform matrix and CameraTransform matrix.
        /// </summary>
        public void SetTransformMatrix()
        {
            SetCurrentValue(ViewportTransformProperty, ViewportInfo.GetViewportTransform(Viewport));
            SetCurrentValue(CameraTransformProperty, ViewportInfo.GetCameraTransform(Viewport));
        }
        #endregion Public Methods
    }
}
