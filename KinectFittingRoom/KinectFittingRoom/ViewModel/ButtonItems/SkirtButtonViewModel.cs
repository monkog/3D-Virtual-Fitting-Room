using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using KinectFittingRoom.ViewModel.ClothingItems;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class SkirtButtonViewModel : ClothingButtonViewModel
    {
        #region .ctor
        public SkirtButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel, string pathToTexture)
            : base(type, pathToModel, pathToTexture)
        { }
        #endregion .ctor
        #region Commands
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void CategoryExecuted(object parameter)
        {
            ClothingButtonViewModel clickedButton = (ClothingButtonViewModel)parameter;

            Dictionary<ClothingItemBase.ClothingType, GeometryModel3D> tmp = ClothingManager.Instance.ChosenClothes;
            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmpModels = ClothingManager.Instance.ChosenClothesModels;

            Model3DGroup group = Importer.Load(ModelPath);
            var modelGroup = (GeometryModel3D)group.Children.First();
            var model = new GeometryModel3D(modelGroup.Geometry, MaterialHelper.CreateImageMaterial(TexturePath));
            model.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));

            tmp[clickedButton.Category] = model;
            try
            {
                tmpModels[clickedButton.Category].Model = model;
            }
            catch (Exception)
            {
                tmpModels[clickedButton.Category] = new SkirtItem(TexturePath, 2);
                tmpModels[clickedButton.Category].Model = model;
            }
            ClothingManager.Instance.ChosenClothes = new Dictionary<ClothingItemBase.ClothingType, GeometryModel3D>(tmp);
            ClothingManager.Instance.ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmpModels);
            ClothingManager.Instance.Cloth = model;
        }
        #endregion Commands
    }
}
