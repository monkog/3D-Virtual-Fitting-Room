using System.Collections.Generic;
using System.Collections.ObjectModel;
using KinectFittingRoom.ViewModel.ButtonItems;
using KinectFittingRoom.ViewModel.ClothingItems;
using System.Windows;
using System.Drawing;
using System.Media;

namespace KinectFittingRoom.ViewModel
{
    /// <summary>
    /// View model for MainWindow
    /// </summary>
    public class KinectViewModel : ViewModelBase
    {
        #region Private Fields

        /// <summary>
        /// Determines if sounds are played
        /// </summary>
        private static bool _soundsOn;
        /// <summary>
        /// Button sound player
        /// </summary>
        private static SoundPlayer _player;
        /// <summary>
        /// Camera sound player
        /// </summary>
        private static SoundPlayer _player2;
        /// <summary>
        /// The clothing category collection
        /// </summary>
        private ObservableCollection<ClothingCategoryButtonViewModel> _clothingCategory;
        /// <summary>
        /// The clothing manager
        /// </summary>
        private ClothingManager _clothingManager;
        /// <summary>
        /// The Kinect service
        /// </summary>
        private readonly KinectService _kinectService;
        #endregion Private Fields
        #region Public Properties
        public static bool SoundsOn
        {
            get { return _soundsOn; }
            set
            {
                if (_soundsOn == value)
                    return;
                _soundsOn = value;
            }
        }
        public static SoundPlayer Player
        {
            get { return _player; }
        }
        public static SoundPlayer Player2
        {
            get { return _player2; }
        }
        /// <summary>
        /// Gets or sets the clothing categories collection.
        /// </summary>
        /// <value>
        /// The clothing categories collection.
        /// </value>
        public ObservableCollection<ClothingCategoryButtonViewModel> ClothingCategories
        {
            get { return _clothingCategory; }
            set
            {
                if (_clothingCategory == value)
                    return;
                _clothingCategory = value;
                OnPropertyChanged("ClothingCategory");
            }
        }
        /// <summary>
        /// Gets or sets the clothing manager.
        /// </summary>
        /// <value>
        /// The clothing manager.
        /// </value>
        public ClothingManager ClothingManager
        {
            get { return _clothingManager; }
            set
            {
                if (_clothingManager == value)
                    return;
                _clothingManager = value;
                OnPropertyChanged("ClothingManager");
            }
        }
        /// <summary>
        /// Gets the kinect service.
        /// </summary>
        /// <value>
        /// The kinect service.
        /// </value>
        public KinectService KinectService
        {
            get { return _kinectService; }
        }

        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="KinectViewModel"/> class.
        /// </summary>
        /// <param name="kinectService">The kinect service.</param>
        public KinectViewModel(KinectService kinectService)
        {
            _soundsOn = true;
            _player = new SoundPlayer(Properties.Resources.ButtonClick);
            _player2 = new SoundPlayer(Properties.Resources.CameraClick);
            InitializeClothingCategories();
            InitializeTopMenu();
            _kinectService = kinectService;
            _kinectService.Initialize();
        }
        #endregion .ctor
        #region Private Methods
        /// <summary>
        /// Initializes the clothing categories.
        /// </summary>
        private void InitializeClothingCategories()
        {
            ClothingCategories = new ObservableCollection<ClothingCategoryButtonViewModel>();
            ClothingCategoryButtonViewModel clothing = new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.hat_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new ClothingButtonViewModel(ClothingItemBase.ClothingType.HatItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\hat_blue.png")  {
                        Image = Properties.Resources.small_hat_blue, 
                        ImageWidthToItemWidth = 2.07
                    },
                    new ClothingButtonViewModel(ClothingItemBase.ClothingType.HatItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\hat_brown.png") {
                        Image = Properties.Resources.small_hat_brown, 
                        ImageWidthToItemWidth = 1.83
                    },
                    new ClothingButtonViewModel(ClothingItemBase.ClothingType.HatItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\hat_superman.png") {
                        Image = Properties.Resources.small_hat_superman, 
                        ImageWidthToItemWidth = 1.24
                    }
                }
            };
            ClothingCategoryButtonViewModel clothing1 = new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.skirt_symbol,
                Clothes = new List<ClothingButtonViewModel>
                    {
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\skirt_jeans.png") {
                            Image = Properties.Resources.small_skirt_jeans, 
                            ImageWidthToItemWidth = 2.21
                        },
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\skirt_maroon.png") {
                            Image = Properties.Resources.small_skirt_maroon, 
                            ImageWidthToItemWidth = 2.0
                        }
                    }
            };
            ClothingCategoryButtonViewModel clothing2 = new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.glasses_symbol,
                Clothes =
                    new List<ClothingButtonViewModel>
                    {
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, ClothingItemBase.MaleFemaleType.Both, @".\Resources\Models\glasses_black.png") {
                            Image = Properties.Resources.small_glasses_black, 
                        },
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, ClothingItemBase.MaleFemaleType.Both, @".\Resources\Models\glasses_blue.png") {
                            Image = Properties.Resources.small_glasses_blue, 
                        },
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, ClothingItemBase.MaleFemaleType.Both, @".\Resources\Models\sunglasses_rayban.png") {
                            Image = Properties.Resources.small_sunglasses_rayban, 
                        },
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, ClothingItemBase.MaleFemaleType.Both, @".\Resources\Models\sunglasses_aviator.png") {
                            Image = Properties.Resources.small_sunglasses_aviator, 
                        }
                    }
            };

            ClothingCategories.Add(clothing);
            ClothingCategories.Add(clothing1);
            ClothingCategories.Add(clothing2);
        }

        private void InitializeTopMenu()
        {
            TopMenuManager.Instance.TopMenuButtons.Add(new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.showMenu) { Image = Properties.Resources.menu });
        }
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
        }
        #endregion Private Methods
        #region Event Handlers
        #endregion Event Handlers
        #region Protected Methods
        #endregion Protected Methods
    }
}
