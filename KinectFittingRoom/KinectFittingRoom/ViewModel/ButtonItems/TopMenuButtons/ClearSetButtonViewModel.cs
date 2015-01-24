using KinectFittingRoom.Model.ClothingItems;
using System.Drawing;
using KinectFittingRoom.ViewModel.Helpers;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ClearSetButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClearSetButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public ClearSetButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Clears chosen set
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
            ClearMenu();
        }
        #endregion
    }
}
