using KinectFittingRoom.ViewModel.ClothingItems;

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
        public override void ClickExecuted(object parameter)
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.ContainsKey(ClothingItemBase.ClothingType.SkirtItem))
                ClothingManager.Instance.ChosenClothesModels.Remove(ClothingItemBase.ClothingType.SkirtItem);
            ClothingManager.Instance.AddClothingItem<DressItem>(Category, ModelPath);
        }
        #endregion Commands
    }
}
