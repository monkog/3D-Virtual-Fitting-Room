using KinectFittingRoom.ViewModel.ClothingItems;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ClearSetButton : TopMenuButtonViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClearSetButton"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public ClearSetButton(TopMenuButtonViewModel.Functionality function, Bitmap image)
            : base(function, image)
        { }
        /// <summary>
        /// Clears chosen set
        /// </summary>
        public override void DoTheFunctionality()
        {
            ClothingManager.Instance.ChosenClothes = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
            TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Collapsed;
        }
    }
}
