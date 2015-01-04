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
                CreateDressesClothingCategoryButton(),
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
                    new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, ClothingItemBase.MaleFemaleType.Both
                        , @".\Resources\Models\Hats\cowboy_hat.obj")  { Image = Properties.Resources.small_hat_blue }
                        , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, ClothingItemBase.MaleFemaleType.Both
                        , @".\Resources\Models\Hats\fedora_hat.obj") { Image = Properties.Resources.small_hat_brown }
                        , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, ClothingItemBase.MaleFemaleType.Both
                        , @".\Resources\Models\Hats\fedora_hat.obj") { Image = Properties.Resources.small_hat_superman }
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
                    new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\long_skirt.obj") 
                    { Image = Properties.Resources.small_skirt_jeans }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\simple_skirt.obj") 
                    { Image = Properties.Resources.small_skirt_maroon }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\short_skirt.obj") 
                    { Image = Properties.Resources.small_skirt_jeans }
                }
            };
        }
        /// <summary>
        /// Creates the dresses clothing category button.
        /// </summary>
        /// <returns>Dresses clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateDressesClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.skirt_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\white_dress.obj") 
                    { Image = Properties.Resources.white_dress }
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
                        new SkirtButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\skirt.obj")
                        { Image = Properties.Resources.small_glasses_black }
                        , new SkirtButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\skirt.obj")
                        { Image = Properties.Resources.small_glasses_blue }
                        , new SkirtButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\skirt.obj")
                        { Image = Properties.Resources.small_sunglasses_rayban }
                        , new SkirtButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\skirt.obj")
                        { Image = Properties.Resources.small_sunglasses_aviator }
                    }
            };
        }
        #endregion Private Methods
    }
}
