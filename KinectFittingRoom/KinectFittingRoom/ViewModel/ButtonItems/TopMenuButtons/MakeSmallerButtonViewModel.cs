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
        /// <param name="image">Image of button</param>
        public MakeSmallerButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Makes last added item smaller
        /// </summary>
        public override void ClickEventExecuted()
        {
            if (ClothingManager.Instance.ChosenClothesModels.Count != 0)
                ClothingManager.Instance.ScaleImage(_minusFactor);
        }
        #endregion
    }
}
