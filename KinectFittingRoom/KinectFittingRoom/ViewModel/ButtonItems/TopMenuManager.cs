using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private ObservableCollection<TopMenuButtonViewModel> _topMenuButtons;
        /// <summary>
        /// All top menu buttons view
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _allButtons;
        /// <summary>
        /// Change size buttons view
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _changeSizeButtons;
        /// <summary>
        /// Change category buttons view
        /// </summary>
        private ObservableCollection<TopMenuButtonViewModel> _maleFemaleCategory;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets actual top menu buttons.
        /// </summary>
        /// <value>
        /// The actual buttons collection.
        /// </value>
        public ObservableCollection<TopMenuButtonViewModel> TopMenuButtons
        {
            get { return _topMenuButtons; }
            set
            {
                if (_topMenuButtons == value)
                    return;
                _topMenuButtons = value;
                OnPropertyChanged("TopMenuButtons");
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
        /// Gets change category buttons.
        /// </summary>
        /// <value>
        /// The change category buttons collection.
        /// </value>
        public ObservableCollection<TopMenuButtonViewModel> MaleFemaleCategory
        {
            get { return _maleFemaleCategory; }
        }
        #endregion Public Properties
        /// <summary>
        /// Private constructor of TopMenuManager. 
        /// </summary>
        private TopMenuManager()
        {
            TopMenuButtons = new ObservableCollection<TopMenuButtonViewModel>();
            _allButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.showMenu) { Image = Properties.Resources.menu },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.maleFemaleCategory) { Image = Properties.Resources.menu_menWomen },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.changeSize) { Image = Properties.Resources.menu_arrows },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.clearClothingSet) { Image = Properties.Resources.menu_clearSet },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.takePicture) { Image = Properties.Resources.menu_camera },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.turnOnOffSounds) { Image = Properties.Resources.menu_speaker },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.exit) { Image = Properties.Resources.menu_doors }
            };
            _changeSizeButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.showMenu) { Image = Properties.Resources.menu },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.makeBigger) { Image = Properties.Resources.vertical_arrows },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.makeSmaller) { Image = Properties.Resources.vertical_arrows_smaller }
            };
            _maleFemaleCategory = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.showMenu) { Image = Properties.Resources.menu },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.maleCategory) { Image = Properties.Resources.man },
                new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.femaleCategory) { Image = Properties.Resources.woman }
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
