using KinectFittingRoom.Model.ClothingItems;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    class TieButtonViewModel : ClothingButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="TieButtonViewModel"/> class.
        /// </summary>
        /// <param name="type">The type of the clothing.</param>
        /// <param name="pathToModel">The path to model.</param>
        public TieButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Male, pathToModel)
        {
            Ratio = 1;
            DeltaY = 1.05;
        }
        #endregion .ctor
        #region Commands
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.AddClothingItem<TieItem>(Category, ModelPath, Ratio, DeltaY);
        }
        #endregion Commands
    }
}
