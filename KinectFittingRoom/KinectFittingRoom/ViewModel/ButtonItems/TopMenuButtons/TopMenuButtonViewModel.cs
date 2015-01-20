using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    public abstract class TopMenuButtonViewModel : ButtonViewModelBase
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="TopMenuButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public TopMenuButtonViewModel(Bitmap image)
        {
            Image = image;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Clears additional buttons in top menu
        /// </summary>
        public void ClearMenu()
        {
            TopMenuManager.Instance.ActualTopMenuButtons = null;
            TopMenuManager.Instance.CameraButtonVisibility = Visibility.Collapsed;
            TopMenuManager.Instance.SizePositionButtonsVisibility = Visibility.Collapsed;
        }
        #endregion
    }
}
