using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ChangePositionButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePositionButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public ChangePositionButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Shows buttons to change size of item
        /// </summary>
        public override void ClickEventExecuted()
        {
            ClearMenu();
            TopMenuManager.Instance.ChangeSizePositionButtons = TopMenuManager.Instance.ChangePositionButtons;
            TopMenuManager.Instance.SizePositionButtonsVisibility = Visibility.Visible;
        }
        #endregion
    }
}
