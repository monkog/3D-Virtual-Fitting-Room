using KinectFittingRoom.ViewModel.ClothingItems;
using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class MakeBiggerButton : TopMenuButtonViewModel
    {
        #region Consts
        private const double _plusFactor = 1.05;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="MakeBiggerButton"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public MakeBiggerButton(TopMenuButtonViewModel.Functionality function, Bitmap image)
            : base(function, image)
        { }
        /// <summary>
        /// Makes last added item bigger
        /// </summary>
        public override void DoTheFunctionality()
        {
            if (ClothingManager.Instance.ChosenClothes.Count != 0)
                ClothingManager.Instance.ScaleImage(_plusFactor);
        }
    }
}
