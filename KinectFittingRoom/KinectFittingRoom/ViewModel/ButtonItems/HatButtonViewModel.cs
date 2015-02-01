using KinectFittingRoom.Model.ClothingItems;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class HatButtonViewModel : ClothingButtonViewModel
    {
        #region .ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="HatButtonViewModel"/> class.
        /// </summary>
        /// <param name="type">The type of the clothing.</param>
        /// <param name="pathToModel">The path to model.</param>
        public HatButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Male, pathToModel)
        {
            Ratio = 0.8;
            DeltaY = 1.2;
        }
        #endregion .ctor
        #region Commands
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.AddClothingItem<HatItem>(Category, ModelPath, Ratio, DeltaY);
        }
        #endregion Commands
    }
}
