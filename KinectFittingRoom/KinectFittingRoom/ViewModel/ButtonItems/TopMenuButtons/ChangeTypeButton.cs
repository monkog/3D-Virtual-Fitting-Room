using KinectFittingRoom.ViewModel.ClothingItems;
using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ChangeTypeButton : TopMenuButtonViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeTypeButton"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public ChangeTypeButton(TopMenuButtonViewModel.Functionality function, Bitmap image)
            : base(function, image)
        { }
        /// <summary>
        /// Changes type of displayed clothes
        /// </summary>
        public override void DoTheFunctionality()
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

            TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Collapsed;
        }
    }
}
