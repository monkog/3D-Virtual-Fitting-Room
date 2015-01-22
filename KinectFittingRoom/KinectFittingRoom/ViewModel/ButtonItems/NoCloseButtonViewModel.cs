using KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class NoCloseButtonViewModel : ButtonViewModelBase
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="NoCloseButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public NoCloseButtonViewModel(Bitmap image)
        {
            Image = image;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Cancel action of closing the application
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            TopMenuManager.Instance.CloseAppGridVisibility = Visibility.Collapsed;
        }
        #endregion
    }
}
