using KinectFittingRoom.ViewModel.ClothingItems;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ClearLastItemButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClearLastItemButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public ClearLastItemButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Clears last chosen item
        /// </summary>
        public override void ClickExecuted(object parameter)
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.Count == 0)
                return;

            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ClothingManager.Instance.ChosenClothesModels;
            tmp.Remove(tmp.Last().Key);
            ClothingManager.Instance.ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
            ClearMenu();
        }
        #endregion
    }
}
