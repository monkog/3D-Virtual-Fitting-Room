using KinectFittingRoom.ViewModel.ClothingItems;

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
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.ContainsKey(ClothingItemBase.ClothingType.DressItem))
                ClothingManager.Instance.ChosenClothesModels.Remove(ClothingItemBase.ClothingType.DressItem);
            ClothingManager.Instance.AddClothingItem<SkirtItem>(Category, ModelPath);
        }
        #endregion Commands
    }
}
