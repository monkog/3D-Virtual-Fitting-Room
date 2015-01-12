using KinectFittingRoom.ViewModel.ClothingItems;
using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class MakeBiggerButtonViewModel : TopMenuButtonViewModel
    {
        #region Consts
        private const double PlusFactor = 0.05;
        #endregion
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="MakeBiggerButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public MakeBiggerButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Makes last added item bigger
        /// </summary>
        public override void ClickEventExecuted()
        {
            if (ClothingManager.Instance.ChosenClothesModels.Count != 0)
                ClothingManager.Instance.ScaleImageHeight(PlusFactor);
        }
        #endregion
    }
}
