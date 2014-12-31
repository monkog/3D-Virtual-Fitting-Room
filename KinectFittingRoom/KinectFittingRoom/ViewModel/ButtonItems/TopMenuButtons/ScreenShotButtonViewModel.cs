using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    public class ScreenShotButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenShotButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public ScreenShotButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Hides all top buttons
        /// </summary>
        public override void ClickEventExecuted()
        {
            ClearMenu();
        }
        #endregion
    }
}