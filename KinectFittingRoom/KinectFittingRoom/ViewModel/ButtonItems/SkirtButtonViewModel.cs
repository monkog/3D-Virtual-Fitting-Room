using KinectFittingRoom.ViewModel.ClothingItems;
using System.Collections.Generic;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class SkirtButtonViewModel : ClothingButtonViewModel
    {
        #region .ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="SkirtButtonViewModel"/> class.
        /// </summary>
        /// <param name="type">The type of the clothing.</param>
        /// <param name="pathToModel">The path to model.</param>
        public SkirtButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Female, pathToModel)
        { }
        #endregion .ctor
        #region Commands
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void CategoryExecuted(object parameter)
        {
            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ClothingManager.Instance.ChosenClothesModels;
            if (tmp.ContainsKey(ClothingItemBase.ClothingType.DressItem))
                tmp.Remove(ClothingItemBase.ClothingType.DressItem);
            ClothingManager.Instance.ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
            AddClothingItem<SkirtItem>();
        }
        #endregion Commands
    }
}
