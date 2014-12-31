using KinectFittingRoom.ViewModel.ClothingItems;
using System.Collections.Generic;
using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ClearSetButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClearSetButtonViewModel"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public ClearSetButtonViewModel(Functionality function, Bitmap image)
            : base(function, image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Clears chosen set
        /// </summary>
        public override void ClickEventExecuted()
        {
            ClothingManager.Instance.ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
            ClearMenu();
        }
        #endregion
    }
}
