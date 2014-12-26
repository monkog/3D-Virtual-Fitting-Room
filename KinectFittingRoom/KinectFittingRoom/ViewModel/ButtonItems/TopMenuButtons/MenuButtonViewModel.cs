using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    public class MenuButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuButtonViewModel"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public MenuButtonViewModel(Functionality function, Bitmap image)
            : base(function, image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Shows or hides all top buttons
        /// </summary>
        public override void ClickEventExecuted()
        {
            if (TopMenuManager.Instance.ActualTopMenuButtons != null)
                base.ClearMenu();
            else
            {
                TopMenuManager.Instance.ActualTopMenuButtons = TopMenuManager.Instance.AllButtons;
                TopMenuManager.Instance.CameraButtonVisibility = Visibility.Visible;
            }

        }
        #endregion
    }
}
