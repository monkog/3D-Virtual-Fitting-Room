using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class MakeSmallerButtonViewModel : TopMenuButtonViewModel
    {
        #region Consts
        private const double MinusFactor = -0.05;
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
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.Count != 0)
                ClothingManager.Instance.ScaleImageHeight(MinusFactor);
        }
        #endregion
    }
}
