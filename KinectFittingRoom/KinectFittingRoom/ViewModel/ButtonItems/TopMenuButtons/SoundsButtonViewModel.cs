using System.Drawing;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class SoundsButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundsButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public SoundsButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Turns off/on sounds in application
        /// </summary>
        public override void ClickExecuted(object parameter)
        {
            PlaySound();
            KinectViewModel.SoundsOn = !KinectViewModel.SoundsOn;
            ClearMenu();
        }
        #endregion
    }
}
