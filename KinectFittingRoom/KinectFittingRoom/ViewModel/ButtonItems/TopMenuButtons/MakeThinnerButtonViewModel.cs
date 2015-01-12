using System.Drawing;
using KinectFittingRoom.ViewModel.ClothingItems;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class MakeThinnerButtonViewModel : TopMenuButtonViewModel
    {
        #region Consts
        private const double MinusFactor = -0.05;
        #endregion
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="MakeThinnerButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public MakeThinnerButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Makes last added item thinner
        /// </summary>
        public override void ClickEventExecuted()
        {
            if (ClothingManager.Instance.ChosenClothesModels.Count != 0)
                ClothingManager.Instance.ScaleImageWidth(MinusFactor);
        }
        #endregion
    }
}
