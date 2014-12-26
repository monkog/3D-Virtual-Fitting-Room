﻿using System.Collections.ObjectModel;
using System.Windows;
using KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public sealed class TopMenuManager : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// Only instance of TopMenuManager
        /// </summary>
        private static TopMenuManager _instance;
        /// <summary>
        /// Actually displayed top menu buttons
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _actualTopMenuButtons;
        /// <summary>
        /// All top menu buttons view
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _allButtons;
        /// <summary>
        /// Change size buttons view
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _changeSizeButtons;
        /// <summary>
        /// Visibility of camera button
        /// </summary>
        private Visibility _cameraButtonVisibility;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets actual top menu buttons.
        /// </summary>
        /// <value>
        /// The actual buttons collection.
        /// </value>
        public ObservableCollection<TopMenuButtonViewModel> ActualTopMenuButtons
        {
            get { return _actualTopMenuButtons; }
            set
            {
                if (_actualTopMenuButtons == value)
                    return;
                _actualTopMenuButtons = value;
                OnPropertyChanged("ActualTopMenuButtons");
            }
        }
        /// <summary>
        /// Gets all top menu buttons.
        /// </summary>
        /// <value>
        /// The top menu buttons collection.
        /// </value>
        public ObservableCollection<TopMenuButtonViewModel> AllButtons
        {
            get { return _allButtons; }
        }
        /// <summary>
        /// Gets or sets main menu button
        /// </summary>
        public MenuButtonViewModel MenuButton
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets camera button
        /// </summary>
        public ScreenShotButtonViewModel CameraButton
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the visibility of camera button
        /// </summary>
        public Visibility CameraButtonVisibility
        {
            get { return _cameraButtonVisibility; }
            set
            {
                if (_cameraButtonVisibility == value)
                    return;
                _cameraButtonVisibility = value;
                OnPropertyChanged("CameraButtonVisibility");
            }
        }
        /// <summary>
        /// Gets change size buttons.
        /// </summary>
        /// <value>
        /// The change size buttons collection.
        /// </value>
        public ObservableCollection<TopMenuButtonViewModel> ChangeSizeButtons
        {
            get { return _changeSizeButtons; }
        }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Private constructor of TopMenuManager. 
        /// </summary>
        private TopMenuManager()
        {
            _allButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new ChangeTypeButtonViewModel(TopMenuButtonViewModel.Functionality.MaleFemaleCategory, Properties.Resources.menu_menWomen),
                new ChangeSizeButtonViewModel(TopMenuButtonViewModel.Functionality.ChangeSize, Properties.Resources.menu_arrows),
                new ClearSetButtonViewModel(TopMenuButtonViewModel.Functionality.ClearClothingSet, Properties.Resources.menu_clearSet),
                new SoundsButtonViewModel(TopMenuButtonViewModel.Functionality.TurnOnOffSounds, Properties.Resources.menu_speaker),
                new ExitButtonViewModel(TopMenuButtonViewModel.Functionality.Exit, Properties.Resources.menu_doors)
            };
            _changeSizeButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new MakeBiggerButtonViewModel(TopMenuButtonViewModel.Functionality.MakeBigger, Properties.Resources.vertical_arrows),
                new MakeSmallerButtonViewModel(TopMenuButtonViewModel.Functionality.MakeSmaller, Properties.Resources.vertical_arrows_smaller)
            };
            MenuButton = new MenuButtonViewModel(TopMenuButtonViewModel.Functionality.ShowMenu, Properties.Resources.menu);
            CameraButton = new ScreenShotButtonViewModel(TopMenuButtonViewModel.Functionality.TakePicture, Properties.Resources.menu_camera);
            CameraButtonVisibility = Visibility.Hidden;
        }
        #endregion
        /// <summary>
        /// Method with access to only instance of TopMenuManager
        /// </summary>
        public static TopMenuManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TopMenuManager();
                return _instance;
            }
        }
    }
}
