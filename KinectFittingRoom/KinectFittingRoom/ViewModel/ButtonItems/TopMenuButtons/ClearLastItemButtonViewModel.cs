using KinectFittingRoom.Model.ClothingItems;
using System.Drawing;
using KinectFittingRoom.ViewModel.Helpers;

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
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.Count == 0)
                return;

            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ClothingManager.Instance.ChosenClothesModels;
            tmp.Remove(tmp.LastKey);
            ClothingManager.Instance.ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
            ClearMenu();
        }
        #endregion
    }
}
