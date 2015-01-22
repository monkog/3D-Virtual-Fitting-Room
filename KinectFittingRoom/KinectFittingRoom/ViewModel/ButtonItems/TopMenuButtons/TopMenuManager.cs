using System.Collections.ObjectModel;
using System.Windows;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
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
        /// Change position buttons view
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _changePositionButtons;
        /// <summary>
        /// Change position buttons view
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _changeSizePositionButtons;
        /// <summary>
        /// Clear buttons view
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _clearButtons;
        /// <summary>
        /// Visibility of camera button
        /// </summary>
        private Visibility _cameraButtonVisibility;
        /// <summary>
        /// Visibility of size and position buttons
        /// </summary>
        private Visibility _sizePositionButtonsVisibility;
        /// <summary>
        /// Visibility of CloseAppGrid
        /// </summary>
        private Visibility _closeAppGridVisibility;
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
        /// Gets change size buttons.
        /// </summary>
        /// <value>
        /// The change size buttons collection.
        /// </value>
        public ObservableCollection<TopMenuButtonViewModel> ChangeSizeButtons
        {
            get { return _changeSizeButtons; }
        }
        /// <summary>
        /// Gets change position buttons.
        /// </summary>
        /// <value>
        /// The change position buttons collection.
        /// </value>
        public ObservableCollection<TopMenuButtonViewModel> ChangePositionButtons
        {
            get { return _changePositionButtons; }
        }
        /// <summary>
        /// Gets change position buttons.
        /// </summary>
        /// <value>
        /// The change position buttons collection.
        /// </value>
        public ObservableCollection<TopMenuButtonViewModel> ChangeSizePositionButtons
        {
            get { return _changeSizePositionButtons; }
            set
            {
                if (_changeSizePositionButtons == value)
                    return;
                _changeSizePositionButtons = value;
                OnPropertyChanged("ChangeSizePositionButtons");
            }
        }
        /// <summary>
        /// Gets clear buttons.
        /// </summary>
        /// <value>
        /// The clear buttons collection.
        /// </value>
        public ObservableCollection<TopMenuButtonViewModel> ClearButtons
        {
            get { return _clearButtons; }
        }
        /// <summary>
        /// Gets or sets camera button
        /// </summary>
        public ScreenshotButtonViewModel CameraButton
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
        /// Gets or sets the visibility of size and position button
        /// </summary>
        public Visibility SizePositionButtonsVisibility
        {
            get { return _sizePositionButtonsVisibility; }
            set
            {
                if (_sizePositionButtonsVisibility == value)
                    return;
                _sizePositionButtonsVisibility = value;
                OnPropertyChanged("SizePositionButtonsVisibility");
            }
        }
        /// <summary>
        /// Gets or sets cancel closing button
        /// </summary>
        public NoCloseButtonViewModel NoCloseButton
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets confirm closing button
        /// </summary>
        public YesCloseButtonViewModel YesCloseButton
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the visibility of CloseAppGrid
        /// </summary>
        public Visibility CloseAppGridVisibility
        {
            get { return _closeAppGridVisibility; }
            set
            {
                if (_closeAppGridVisibility == value)
                    return;
                _closeAppGridVisibility = value;
                OnPropertyChanged("CloseAppGridVisibility");
            }
        }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Private constructor of TopMenuManager. 
        /// </summary>
        private TopMenuManager()
        {
            InitializeTopMenuButtons();
            
            CreateCloseButtons();
            CloseAppGridVisibility = Visibility.Collapsed;
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Initializes buttons in top menu
        /// </summary>
        private void InitializeTopMenuButtons()
        {
            CreateAllTopMenuButtons();
            CreateChangePositionButtons();
            CreateChangeSizeButtons();
            CreateClearButtons();
            CameraButtonVisibility = Visibility.Collapsed;
            SizePositionButtonsVisibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Creates basic top menu buttons
        /// </summary>
        private void CreateAllTopMenuButtons()
        {
            MenuButton = new MenuButtonViewModel(Properties.Resources.menu);
            CameraButton = new ScreenshotButtonViewModel(Properties.Resources.menu_camera);
            _allButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new ChangeTypeButtonViewModel(Properties.Resources.menu_menWomen),
                new ChangeSizeButtonViewModel(Properties.Resources.menu_arrows),
                new ChangePositionButtonViewModel(Properties.Resources.arrows_up_down),
                new ClearItemsButtonViewModel(Properties.Resources.menu_clear),
                new SoundsButtonViewModel(Properties.Resources.menu_speaker),
                new ExitButtonViewModel(Properties.Resources.menu_doors)
            };
        }
        /// <summary>
        /// Creates change position buttons
        /// </summary>
        private void CreateChangePositionButtons()
        {
            _changePositionButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new MoveUpButtonViewModel(Properties.Resources.move_up),
                new MoveDownButtonViewModel(Properties.Resources.move_down)
            };
        }
        /// <summary>
        /// Creates change size positions
        /// </summary>
        private void CreateChangeSizeButtons()
        {
            _changeSizeButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new MakeBiggerButtonViewModel(Properties.Resources.vertical_arrows),
                new MakeSmallerButtonViewModel(Properties.Resources.vertical_arrows_smaller),
                new MakeThinnerButtonViewModel(Properties.Resources.horizontal_arrows_thinner),
                new MakeWiderButtonViewModel(Properties.Resources.horizontal_arrows),
                new ScalePlusButtonViewModel(Properties.Resources.arrows_bigger),
                new ScaleMinusButtonViewModel(Properties.Resources.arrows_smaller)
            };
        }
        /// <summary>
        /// Creates clear buttons
        /// </summary>
        private void CreateClearButtons()
        {
            _clearButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new ClearLastItemButtonViewModel(Properties.Resources.menu_clearOne),
                new ClearSetButtonViewModel(Properties.Resources.menu_clearSet)
            };
        }
        /// <summary>
        /// Creates cancel and confirm closing buttons
        /// </summary>
        private void CreateCloseButtons()
        {
            NoCloseButton = new NoCloseButtonViewModel(null);
            YesCloseButton = new YesCloseButtonViewModel(null);
        }
        #endregion Private Methods
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
