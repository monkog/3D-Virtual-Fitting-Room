using System.Drawing;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    public class YesCloseButtonViewModel : TopMenuButtonViewModel
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="YesCloseButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public YesCloseButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Closes the application
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            Application.Current.MainWindow.Close();
        }
        #endregion
    }
}
