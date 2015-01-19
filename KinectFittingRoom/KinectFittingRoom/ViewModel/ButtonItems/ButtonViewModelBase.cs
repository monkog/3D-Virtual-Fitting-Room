using Microsoft.Practices.Prism.Commands;
using System.Drawing;
using System.Windows.Input;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    /// <summary>
    /// Base class for button view models
    /// </summary>
    public abstract class ButtonViewModelBase : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// The button's image
        /// </summary>
        private Bitmap _image;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the button's image.
        /// </summary>
        /// <value>
        /// The button's image.
        /// </value>
        public Bitmap Image
        {
            get { return _image; }
            set
            {
                if (_image == value)
                    return;
                _image = value;
                OnPropertyChanged("Image");
            }
        }
        #endregion Public Properties
        #region Commands
        /// <summary>
        /// The command, executed after clicking on button
        /// </summary>
        private ICommand _clickCommand;
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand ClickCommand
        {
            get { return _clickCommand ?? (_clickCommand = new DelegateCommand(ClickExecuted)); }
        }
        /// <summary>
        /// Executes when button was hit.
        /// </summary>
        public abstract void ClickExecuted();
        #endregion Commands

        public void PlaySound()
        {
            if (KinectViewModel.SoundsOn)
                KinectViewModel.ButtonPlayer.Play();
        }

    }
}
