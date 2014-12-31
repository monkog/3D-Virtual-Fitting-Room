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
        public override void ClickEventExecuted()
        {
            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ClothingManager.Instance.ChosenClothes;
            tmp.Remove(tmp.FirstOrDefault(a => a.Value.PathToImage == ClothingManager.Instance.LastAddedItem.PathToImage).Key);
            ClothingManager.Instance.ChosenClothes = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
            base.ClearMenu();
        }
        #endregion
    }
}
