using Microsoft.Practices.Prism.Commands;
using System.Drawing;
using System.Windows.Input;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public abstract class TopMenuButtonViewModel : ButtonViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// Type of button
        /// </summary>
        private Functionality _buttonType;
        #endregion
        #region Public Properties
        /// <summary>
        /// Gets or sets button object
        /// </summary>
        public TopMenuButtonViewModel TopButtonObject
        {
            get { return this; }
        }
        /// <summary>
        /// Gets type of button
        /// </summary>
        public Functionality Type
        {
            get { return _buttonType; }
        }
        #endregion
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="TopMenuButtonViewModel"/> class.
        /// </summary>
        /// <param name="buttonType">Functionality of button</param>
        /// <param name="image">Image of button</param>
        public TopMenuButtonViewModel(Functionality buttonType, Bitmap image)
        {
            _buttonType = buttonType;
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
            get { return _topMenuCommand ?? (_topMenuCommand = new DelegateCommand<object>(FunctionalityExecuted)); }
        }
        /// <summary>
        /// Executes when button from top menu was hit.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public void FunctionalityExecuted(object parameter)
        {
            var clickedButton = (TopMenuButtonViewModel)parameter;
            clickedButton.DoTheFunctionality();
        }
        #endregion Commands
        #region Methods
        /// <summary>
        /// Does functionality of buttons
        /// </summary>
        public abstract void DoTheFunctionality();
        #endregion
        /// <summary>
        /// Enumeration of top buttons functionalities
        /// </summary>
        public enum Functionality
        {
            changeSize,
            clearClothingSet,
            makeBigger,
            makeSmaller,
            maleFemaleCategory,
            showMenu,
            takePicture,
            turnOnOffSounds,
            exit
        }
    }
}
