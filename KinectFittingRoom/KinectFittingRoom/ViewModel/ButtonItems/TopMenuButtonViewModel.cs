using KinectFittingRoom.ViewModel.ClothingItems;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class TopMenuButtonViewModel : ButtonViewModelBase
    {
        #region Consts
        private const double _plusFactor = 1.05;
        private const double _minusFactor = 0.95;
        #endregion
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
        #endregion
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="TopMenuButtonViewModel"/> class.
        /// </summary>
        /// <param name="buttonType">Type of button</param>
        public TopMenuButtonViewModel(Functionality buttonType)
        {
            _buttonType = buttonType;
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


            switch (clickedButton._buttonType)
            {
                case Functionality.showMenu:
                    if (TopMenuManager.Instance.TopMenuButtons.Count > 1)
                        TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Collapsed;
                    else
                        TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Visible;
                        break;
                case Functionality.maleFemaleCategory:
                    ChangeMaleFemaleCategory();
                    ReturnToMainMenu();
                    break;
                case Functionality.changeSize:
                    TopMenuManager.Instance.TopMenuButtons = new ObservableCollection<TopMenuButtonViewModel>(TopMenuManager.Instance.ChangeSizeButtons);
                    break;
                case Functionality.makeBigger:
                    if (ClothingManager.Instance.ChosenClothes.Count != 0)
                        ClothingManager.Instance.ScaleImage(_plusFactor);
                    break;
                case Functionality.makeSmaller:
                    if (ClothingManager.Instance.ChosenClothes.Count != 0)
                        ClothingManager.Instance.ScaleImage(_minusFactor);
                    break;
                case Functionality.clearClothingSet:
                    ClothingManager.Instance.ChosenClothes = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
                    ReturnToMainMenu();
                    break;
                case Functionality.takePicture:
                    MakeScreenShot();
                    ReturnToMainMenu();
                    break;
                case Functionality.turnOnOffSounds:
                    if (KinectViewModel.SoundsOn)
                        KinectViewModel.SoundsOn = false;
                    else
                        KinectViewModel.SoundsOn = true;
                    ReturnToMainMenu();
                    break;
                case Functionality.exit:
                    Application.Current.MainWindow.Close();
                    break;

            }
        }
        #endregion Commands
        #region Methods
        /// <summary>
        /// Changes type of displayed clothes
        /// </summary>
        private void ChangeMaleFemaleCategory()
        {
            if (ClothingManager.Instance.ChosenType == ClothingItemBase.MaleFemaleType.Female)
                ClothingManager.Instance.ChosenType = ClothingItemBase.MaleFemaleType.Male;
            else
                ClothingManager.Instance.ChosenType = ClothingItemBase.MaleFemaleType.Female;

            if (ClothingManager.Instance.Clothing != null)
                foreach (var c in ClothingManager.Instance.LastChosenCategory.Clothes)
                    if (c.Type == ClothingManager.Instance.ChosenType || c.Type == ClothingItemBase.MaleFemaleType.Both)
                        ClothingManager.Instance.Clothing.Add(c);
        }

        /// <summary>
        /// Raise screen shot event
        /// </summary>
        /// <param name="visual">ImageArea control from UI</param>
        /// <param name="visual2">ClothesArea control from UI</param>
        /// <param name="actualWidth">Actual screen width</param>
        /// <param name="actualHeight">Actual screen height</param>
        private void MakeScreenShot()
        {
            //WYWOLAC EVENT
        }
        /// <summary>
        /// Clean all top butons and displays main button
        /// </summary>
        private void ReturnToMainMenu()
        {
            TopMenuManager.Instance.TopMenuButtons.Clear();
            TopMenuManager.Instance.AllTopButtonsVisibility = Visibility.Collapsed;
            TopMenuManager.Instance.TopMenuButtons.Add(new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.showMenu) { Image = Properties.Resources.menu });
        }
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
