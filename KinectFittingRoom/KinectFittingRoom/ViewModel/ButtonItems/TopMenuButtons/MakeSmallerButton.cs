using KinectFittingRoom.ViewModel.ClothingItems;
using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class MakeSmallerButton : TopMenuButtonViewModel
    {
        #region Consts
        private const double _minusFactor = 0.95;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="MakeSmallerButton"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public MakeSmallerButton(TopMenuButtonViewModel.Functionality function, Bitmap image)
            : base(function, image)
        { }
        /// <summary>
        /// Makes last added item smaller
        /// </summary>
        public override void DoTheFunctionality()
        {
            if (ClothingManager.Instance.ChosenClothes.Count != 0)
                ClothingManager.Instance.ScaleImage(_minusFactor);
        }
    }
}
