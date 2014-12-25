using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ChangeSizeButton : TopMenuButtonViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeSizeButton"/> class.
        /// </summary>
        /// <param name="function">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public ChangeSizeButton(TopMenuButtonViewModel.Functionality function, Bitmap image)
            : base(function, image)
        { }
        /// <summary>
        /// Shows buttons to change size of item
        /// </summary>
        public override void DoTheFunctionality()
        {
            TopMenuManager.Instance.ActualTopMenuButtons = new ObservableCollection<TopMenuButtonViewModel>(TopMenuManager.Instance.ChangeSizeButtons);
            TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Collapsed;
        }
    }
}
