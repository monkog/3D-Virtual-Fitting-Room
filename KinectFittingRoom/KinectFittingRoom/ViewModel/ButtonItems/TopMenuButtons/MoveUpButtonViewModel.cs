using KinectFittingRoom.Model.ClothingItems;
using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class MoveUpButtonViewModel : TopMenuButtonViewModel
    {
        #region Consts
        private const double MoveFactor = -5;
        #endregion
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveUpButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public MoveUpButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Moves last added item up
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.Count != 0)
                ClothingManager.Instance.ChangeImagePosition(MoveFactor);
        }
        #endregion
    }
}
