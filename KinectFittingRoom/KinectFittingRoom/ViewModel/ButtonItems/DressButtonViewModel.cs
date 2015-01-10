using KinectFittingRoom.ViewModel.ClothingItems;
using System.Collections.Generic;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class DressButtonViewModel : ClothingButtonViewModel
    {
        #region .ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="DressButtonViewModel"/> class.
        /// </summary>
        /// <param name="type">The type of the clothing.</param>
        /// <param name="pathToModel">The path to model.</param>
        public DressButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
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
            if (tmp.ContainsKey(ClothingItemBase.ClothingType.SkirtItem))
                tmp.Remove(ClothingItemBase.ClothingType.SkirtItem);
            ClothingManager.Instance.ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
            AddClothingItem<DressItem>();
        }
        #endregion Commands
    }
}
