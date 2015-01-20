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
        /// <param name="image">Image of button</param>
        public MenuButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Shows or hides all top buttons
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            if (TopMenuManager.Instance.ActualTopMenuButtons == TopMenuManager.Instance.AllButtons)
                ClearMenu();
            else
            {
                TopMenuManager.Instance.ActualTopMenuButtons = TopMenuManager.Instance.AllButtons;
                TopMenuManager.Instance.CameraButtonVisibility = Visibility.Visible;
                TopMenuManager.Instance.SizePositionButtonsVisibility = Visibility.Collapsed;
            }

        }
        #endregion
    }
}
