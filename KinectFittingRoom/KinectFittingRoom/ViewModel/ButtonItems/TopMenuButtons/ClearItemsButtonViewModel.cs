using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ClearItemsButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClearItemsButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public ClearItemsButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Shows buttons to clear items
        /// </summary>
        public override void ClickEventExecuted()
        {
            base.ClearMenu();
            TopMenuManager.Instance.ActualTopMenuButtons = TopMenuManager.Instance.ClearButtons;
        }
        #endregion
    }
}
