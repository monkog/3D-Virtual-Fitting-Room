using Microsoft.Practices.Prism.Commands;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    public abstract class TopMenuButtonViewModel : ButtonViewModelBase
    {
        #region Public Properties
        #endregion
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
        #region Commands
        /// <summary>
        /// The top menu command, executed after clicking on button from top menu
        /// </summary>
        private ICommand _topMenuCommand;
        /// <summary>
        /// Gets the top menu command.
        /// </summary>
        /// <value>
        /// The top menu command.
        /// </value>
        public ICommand TopMenuCommand
        {
            get { return _topMenuCommand ?? (_topMenuCommand = new DelegateCommand(ClickEventExecuted)); }
        }
        #endregion Commands
        #region Methods
        /// <summary>
        /// Does functionality of buttons
        /// </summary>
        public abstract void ClickEventExecuted();

        public void ClearMenu()
        {
            TopMenuManager.Instance.ActualTopMenuButtons = null;
            TopMenuManager.Instance.CameraButtonVisibility = Visibility.Collapsed;
        }
        #endregion
    }
}
