using System.Drawing;
using System.Windows;
using KinectFittingRoom.View.Buttons;
using System;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    public class ScreenShotButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenShotButtonViewModel"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public ScreenShotButtonViewModel(Functionality function, Bitmap image)
            : base(function, image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Hides all top buttons
        /// </summary>
        public override void ClickEventExecuted()
        {
            base.ClearMenu();
        }
        #endregion
    }
}