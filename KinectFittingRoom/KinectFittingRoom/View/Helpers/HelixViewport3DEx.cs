using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace KinectFittingRoom.View.Helpers
{
    public class HelixViewport3DEx : HelixViewport3D
    {
        #region Dependency Properties
        /// <summary>
        /// The transform matrix property
        /// </summary>
        public static readonly DependencyProperty TransformMatrixProperty = DependencyProperty.RegisterAttached(
            "TransformMatrix", typeof(Matrix3D), typeof(HelixViewport3DEx), new FrameworkPropertyMetadata(null));
        #endregion Dependency Properties
        #region Public Properties
        /// <summary>
        /// Gets or sets the transform matrix.
        /// </summary>
        /// <value>
        /// The transform matrix.
        /// </value>
        public Matrix3D TransformMatrix
        {
            get { return (Matrix3D)GetValue(TransformMatrixProperty); }
            set { SetValue(TransformMatrixProperty, value); }
        }
        #endregion Public Properties
        #region Public Methods
        /// <summary>
        /// Sets the transform matrix.
        /// </summary>
        internal void SetTransformMatrix()
        {
            var matrix = Viewport.GetTotalTransform();
            matrix.Invert();
            SetCurrentValue(TransformMatrixProperty, matrix);
        }
        #endregion Public Methods
    }
}
