using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class MenuButton : TopMenuButtonViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuButton"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public MenuButton(TopMenuButtonViewModel.Functionality function, Bitmap image)
            : base(function, image)
        { }
        /// <summary>
        /// Shows or hides all top buttons
        /// </summary>
        public override void DoTheFunctionality()
        {
            if (TopMenuManager.Instance.AllTopButtonsVisibility == Visibility.Collapsed)
            {
                if (TopMenuManager.Instance.ActualTopMenuButtons.Count > 1)
                    for (int i = TopMenuManager.Instance.ActualTopMenuButtons.Count - 1; i > 0; i--)
                        TopMenuManager.Instance.ActualTopMenuButtons.RemoveAt(i);
                TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Visible;
            }
            else
                TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Collapsed;
        }
    }
}
