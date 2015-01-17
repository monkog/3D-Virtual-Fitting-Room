using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    public class ScreenshotButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public ScreenshotButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Hides all top buttons
        /// </summary>
        public override void ClickExecuted(object parameter)
        {
            PlaySound();
            ClearMenu();
        }
        #endregion
    }
}