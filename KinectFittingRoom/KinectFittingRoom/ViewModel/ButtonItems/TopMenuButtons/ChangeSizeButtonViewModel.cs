using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ChangeSizeButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeSizeButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public ChangeSizeButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Shows buttons to change size of item
        /// </summary>
        public override void ClickEventExecuted()
        {
            base.ClearMenu();
            TopMenuManager.Instance.ActualTopMenuButtons = TopMenuManager.Instance.ChangeSizeButtons;
        }
        #endregion
    }
}
