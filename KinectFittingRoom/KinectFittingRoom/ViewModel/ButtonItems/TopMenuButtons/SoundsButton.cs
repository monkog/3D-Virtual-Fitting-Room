using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class SoundsButton : TopMenuButtonViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundsButton"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public SoundsButton(TopMenuButtonViewModel.Functionality function, Bitmap image)
            : base(function, image)
        { }
        /// <summary>
        /// Turns off/on sounds in application
        /// </summary>
        public override void DoTheFunctionality()
        {
            if (KinectViewModel.SoundsOn)
                KinectViewModel.SoundsOn = false;
            else
                KinectViewModel.SoundsOn = true;

            TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Collapsed;
        }

        
    }
}
