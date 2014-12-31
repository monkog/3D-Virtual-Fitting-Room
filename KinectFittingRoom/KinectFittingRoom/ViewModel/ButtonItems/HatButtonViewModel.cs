using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using KinectFittingRoom.ViewModel.ClothingItems;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class HatButtonViewModel : ClothingButtonViewModel
    {
        #region .ctor
        public HatButtonViewModel(ClothingItemBase.ClothingType type, ClothingItemBase.MaleFemaleType maleFemaleType, string pathToModel, string pathToTexture)
            : base(type, maleFemaleType, pathToModel, pathToTexture)
        { }
        #endregion .ctor
        #region Commands
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void CategoryExecuted(object parameter)
        {
            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmpModels = ClothingManager.Instance.ChosenClothesModels;

            Model3DGroup group = Importer.Load(ModelPath);
            var modelGroup = (GeometryModel3D)group.Children.First();
            var model = new GeometryModel3D(modelGroup.Geometry, MaterialHelper.CreateImageMaterial(TexturePath));
            //model.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
            model.Transform = new ScaleTransform3D(0.2, 0.2, 0.2);

            try
            {
                tmpModels[Category].Model = model;
            }
            catch (Exception)
            {
                tmpModels[Category] = new HatItem(TexturePath, 2) { Model = model };
            }
            ClothingManager.Instance.ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmpModels);
        }
        #endregion Commands
    }
}
