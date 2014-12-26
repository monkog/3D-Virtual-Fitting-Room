using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class SoundsButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundsButtonViewModel"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public SoundsButtonViewModel(Functionality function, Bitmap image)
            : base(function, image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Turns off/on sounds in application
        /// </summary>
        public override void ClickEventExecuted()
        {
            KinectViewModel.SoundsOn = !KinectViewModel.SoundsOn;
            base.ClearMenu();
        }
        #endregion
    }
}
