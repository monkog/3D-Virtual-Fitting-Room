using System.Collections.ObjectModel;
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
        /// Visibility of all top buttons
        /// </summary>
        private Visibility _allTopButtonsVisibility;
        /// <summary>
        /// Change size buttons view
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _changeSizeButtons;
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
        /// Gets or sets the visibility of all top buttons
        /// </summary>
        /// <value>
        /// The visibility
        /// </value>
        public Visibility AllTopButtonsVisibility
        {
            get { return _allTopButtonsVisibility; }
            set
            {
                if (_allTopButtonsVisibility == value)
                    return;
                _allTopButtonsVisibility = value;
                OnPropertyChanged("AllTopButtonsVisibility");
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
        /// <summary>
        /// Private constructor of TopMenuManager. 
        /// </summary>
        private TopMenuManager()
        {
            _allTopButtonsVisibility = Visibility.Collapsed;
            _actualTopMenuButtons = new ObservableCollection<TopMenuButtonViewModel>();
            _allButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new ChangeTypeButton(TopMenuButtonViewModel.Functionality.maleFemaleCategory,Properties.Resources.menu_menWomen),
                new ChangeSizeButton(TopMenuButtonViewModel.Functionality.changeSize,Properties.Resources.menu_arrows),
                new ClearSetButton(TopMenuButtonViewModel.Functionality.clearClothingSet,Properties.Resources.menu_clearSet),
                new ScreenShotButton(TopMenuButtonViewModel.Functionality.takePicture, Properties.Resources.menu_camera),
                new SoundsButton(TopMenuButtonViewModel.Functionality.turnOnOffSounds, Properties.Resources.menu_speaker),
                new ExitButton(TopMenuButtonViewModel.Functionality.exit, Properties.Resources.menu_doors)
            };
            _changeSizeButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new MenuButton(TopMenuButtonViewModel.Functionality.showMenu, Properties.Resources.menu),
                new MakeBiggerButton(TopMenuButtonViewModel.Functionality.makeBigger, Properties.Resources.vertical_arrows),
                new MakeSmallerButton(TopMenuButtonViewModel.Functionality.makeSmaller,Properties.Resources.vertical_arrows_smaller)
            };
        }
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
