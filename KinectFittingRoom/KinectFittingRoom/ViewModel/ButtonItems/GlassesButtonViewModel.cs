using KinectFittingRoom.ViewModel.ClothingItems;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    class GlassesButtonViewModel : ClothingButtonViewModel
    {
        #region .ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="GlassesButtonViewModel"/> class.
        /// </summary>
        /// <param name="type">The type of the clothing.</param>
        /// <param name="pathToModel">The path to model.</param>
        public GlassesButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Both, pathToModel)
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
            ClothingManager.Instance.AddClothingItem<GlassesItem>(Category, ModelPath);
        }
        #endregion Commands
    }
}
