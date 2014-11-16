using System.Windows;
using System.Windows.Media.Media3D;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    /// <summary>
    /// Base class for all models
    /// </summary>
    class ModelBase : UIElement3D
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBase"/> class.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        public ModelBase(string resourceKey)
        {
            Visual3DModel = Application.Current.Resources[resourceKey] as Model3DGroup;
        }
        #endregion .ctor
        #region Public Methods        
        /// <summary>
        /// Moves the model.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        /// <param name="angle">The angle.</param>
        public void MoveModel(double x, double y, double z, double angle)
        {
            var rotate = new RotateTransform3D { Rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle) };
            var translate = new TranslateTransform3D(x, y, z);

            var transform = new Transform3DGroup();
            transform.Children.Add(rotate);
            transform.Children.Add(translate);
            Transform = transform;
        }
        #endregion Public Methods
    }
}
