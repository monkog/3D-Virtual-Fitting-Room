using KinectFittingRoom.ViewModel.ButtonItems;
using KinectFittingRoom.ViewModel.ClothingItems;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static bool SoundsOn { get; set; }
        /// <summary>
        /// Gets the button player.
        /// </summary>
        /// <value>
        /// The button player.
        /// </value>
        public static SoundPlayer ButtonPlayer { get; private set; }
        /// <summary>
        /// Gets the camera player.
        /// </summary>
        /// <value>
        /// The camera player.
        /// </value>
        public static SoundPlayer CameraPlayer { get; private set; }
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
        /// <summary>
        /// Gets a value indicating whether [debug mode on].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug mode on]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugModeOn
        {
            get
            {
#if DEBUG
                return true;
#endif
                return false;
            }
        }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="KinectViewModel"/> class.
        /// </summary>
        /// <param name="kinectService">The kinect service.</param>
        public KinectViewModel(KinectService kinectService)
        {
            SoundsOn = true;
            ButtonPlayer = new SoundPlayer(Properties.Resources.ButtonClick);
            CameraPlayer = new SoundPlayer(Properties.Resources.CameraClick);
            InitializeClothingCategories();
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
            ClothingCategories = new ObservableCollection<ClothingCategoryButtonViewModel>
            {
                CreateHatsClothingCategoryButton(),
                CreateSkirtsClothingCategoryButton(),
                CreateGlassesClothingCategoryButton()
            };
        }
        /// <summary>
        /// Creates the hats clothing category button.
        /// </summary>
        /// <returns>Hats clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateHatsClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.hat_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, ClothingItemBase.MaleFemaleType.Both, @".\Resources\Models\SantaHat.obj", @".\Resources\Models\hat_blue.png")  {
                        Image = Properties.Resources.small_hat_blue, 
                        ImageWidthToItemWidth = 2.07
                    },
                    new SkirtButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\SantaHat.obj", @".\Resources\Models\hat_brown.png") {
                        Image = Properties.Resources.small_hat_brown, 
                        ImageWidthToItemWidth = 1.83
                    },
                    new SkirtButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\SantaHat.obj", @".\Resources\Models\hat_superman.png") {
                        Image = Properties.Resources.small_hat_superman, 
                        ImageWidthToItemWidth = 1.24
                    }
                }
            };
        }
        /// <summary>
        /// Creates the skirts clothing category button.
        /// </summary>
        /// <returns>Skirts clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateSkirtsClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.skirt_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\skirt.obj", @".\Resources\Models\skirt_denim.bmp") {
                        Image = Properties.Resources.small_skirt_jeans, 
                        ImageWidthToItemWidth = 2.21
                    },
                    new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\skirt.obj", @".\Resources\Models\skirt_maroon.png") {
                        Image = Properties.Resources.small_skirt_maroon, 
                        ImageWidthToItemWidth = 2.0
                    },
                    new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\skirt.obj", @".\Resources\Models\skirt_jeans.png") {
                        Image = Properties.Resources.small_skirt_jeans, 
                        ImageWidthToItemWidth = 2.0
                    }
                }
            };
        }
        /// <summary>
        /// Creates the glasses clothing category button.
        /// </summary>
        /// <returns>Glasses clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateGlassesClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.glasses_symbol,
                Clothes =
                    new List<ClothingButtonViewModel>
                    {
                        new SkirtButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\skirt.obj", @".\Resources\Models\glasses_black.png") {
                            Image = Properties.Resources.small_glasses_black, 
                        },
                        new SkirtButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\skirt.obj", @".\Resources\Models\glasses_blue.png") {
                            Image = Properties.Resources.small_glasses_blue, 
                        },
                        new SkirtButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\skirt.obj", @".\Resources\Models\sunglasses_rayban.png") {
                            Image = Properties.Resources.small_sunglasses_rayban, 
                        },
                        new SkirtButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\skirt.obj", @".\Resources\Models\sunglasses_aviator.png") {
                            Image = Properties.Resources.small_sunglasses_aviator, 
                        }
                    }
            };
        }
        #endregion Private Methods
    }
}
