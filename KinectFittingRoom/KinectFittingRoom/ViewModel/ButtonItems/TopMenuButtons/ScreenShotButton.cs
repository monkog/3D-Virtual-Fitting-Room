using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ScreenShotButton : TopMenuButtonViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenShotButton"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public ScreenShotButton(TopMenuButtonViewModel.Functionality function, Bitmap image)
            : base(function, image)
        { }
        /// <summary>
        /// Hides all top buttons
        /// </summary>
        public override void DoTheFunctionality()
        {
            TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Collapsed;
        }
    }
}
