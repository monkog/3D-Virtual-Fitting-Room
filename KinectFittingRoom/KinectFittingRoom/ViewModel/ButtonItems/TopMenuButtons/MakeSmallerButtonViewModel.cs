using KinectFittingRoom.ViewModel.ClothingItems;
using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class MakeSmallerButtonViewModel : TopMenuButtonViewModel
    {
        #region Consts
        private const double _minusFactor = 0.95;
        #endregion
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="MakeSmallerButtonViewModel"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public MakeSmallerButtonViewModel(Functionality function, Bitmap image)
            : base(function, image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Makes last added item smaller
        /// </summary>
        public override void ClickEventExecuted()
        {
            if (ClothingManager.Instance.ChosenClothes.Count != 0)
                ClothingManager.Instance.ScaleImage(_minusFactor);
        }
        #endregion
    }
}
