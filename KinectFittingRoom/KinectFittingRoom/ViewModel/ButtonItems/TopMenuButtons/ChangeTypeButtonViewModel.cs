using System.Linq;
using KinectFittingRoom.ViewModel.ClothingItems;
using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ChangeTypeButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeTypeButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public ChangeTypeButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Changes type of displayed clothes
        /// </summary>
        public override void ClickExecuted(object parameter)
        {
            PlaySound();
            ClothingManager.Instance.ChosenType = ClothingManager.Instance.ChosenType == ClothingItemBase.MaleFemaleType.Female
                ? ClothingItemBase.MaleFemaleType.Male : ClothingItemBase.MaleFemaleType.Female;

            ClothingManager.Instance.UpdateActualCategories();

            if (ClothingManager.Instance.Clothing != null)
            {
                ClothingManager.Instance.Clothing.Clear();
                foreach (var cloth in ClothingManager.Instance.LastChosenCategory.Clothes.Where(
                    cloth => cloth.Type == ClothingManager.Instance.ChosenType || cloth.Type == ClothingItemBase.MaleFemaleType.Both))
                    ClothingManager.Instance.Clothing.Add(cloth);
            }

            ClearMenu();
        }
        #endregion
    }
}
