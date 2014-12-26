using KinectFittingRoom.ViewModel.ClothingItems;
using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ChangeTypeButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeTypeButtonViewModel"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public ChangeTypeButtonViewModel(Functionality function, Bitmap image)
            : base(function, image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Changes type of displayed clothes
        /// </summary>
        public override void ClickEventExecuted()
        {
            if (ClothingManager.Instance.ChosenType == ClothingItemBase.MaleFemaleType.Female)
                ClothingManager.Instance.ChosenType = ClothingItemBase.MaleFemaleType.Male;
            else
                ClothingManager.Instance.ChosenType = ClothingItemBase.MaleFemaleType.Female;

            if (ClothingManager.Instance.Clothing != null)
            {
                ClothingManager.Instance.Clothing.Clear();
                foreach (var c in ClothingManager.Instance.LastChosenCategory.Clothes)
                    if (c.Type == ClothingManager.Instance.ChosenType || c.Type == ClothingItemBase.MaleFemaleType.Both)
                        ClothingManager.Instance.Clothing.Add(c);
            }

            base.ClearMenu();
        }
        #endregion
    }
}
