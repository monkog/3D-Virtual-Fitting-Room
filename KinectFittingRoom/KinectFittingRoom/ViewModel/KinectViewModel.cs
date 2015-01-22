using KinectFittingRoom.Model.ClothingItems;
using KinectFittingRoom.ViewModel.ButtonItems;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Media;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel
{
    /// <summary>
    /// View model for MainWindow
    /// </summary>
    public class KinectViewModel : ViewModelBase
    {
        #region Private Fields
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
            ClothingManager.Instance.ClothingCategories = new ObservableCollection<ClothingCategoryButtonViewModel>
            {
                CreateHatsClothingCategoryButton(),
                CreateTiesClothingCategoryButton(),
                CreateSkirtsClothingCategoryButton(),
                CreateDressesClothingCategoryButton(),
                CreateGlassesClothingCategoryButton(),
                CreateBagsClothingCategoryButton()
            };
            ClothingManager.Instance.UpdateActualCategories();
        }
        /// <summary>
        /// Creates the hats clothing category button.
        /// </summary>
        /// <returns>Hats clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateHatsClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Male)
            {
                Image = Properties.Resources.hat_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\cowboy_hat.obj")  
                    { Image = Properties.Resources.cowboy_hat }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\cowboy_hat_straw.obj") 
                    { Image = Properties.Resources.cowboy_straw }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem , @".\Resources\Models\Hats\cowboy_hat_gray.obj")
                    { Image = Properties.Resources.cowboy_dark }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem , @".\Resources\Models\Hats\fedora_brown_hat.obj")
                    { Image = Properties.Resources.fedora_hat_brown }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\fedora_darkgreen_hat.obj")
                    { Image = Properties.Resources.fedora_hat_darkgreen }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\fedora_hat.obj")
                    { Image = Properties.Resources.fedora_hat_black }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\hat_brown.obj")
                    { Image = Properties.Resources.hat_brown }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\hat_black.obj")
                    { Image = Properties.Resources.hat_black }
                    , new HatButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\Hats\hat_white.obj")
                    { Image = Properties.Resources.hat_white }
                }
            };
        }
        /// <summary>
        /// Creates the skirts clothing category button.
        /// </summary>
        /// <returns>Skirts clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateSkirtsClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Female)
            {
                Image = Properties.Resources.skirt_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\short_skirt.obj") 
                    { Image = Properties.Resources.short_skirt }
                    ,new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\short_skirt_checked.obj") 
                    { Image = Properties.Resources.short_skirt_checked }
                    ,new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\short_skirt_denim.obj") 
                    { Image = Properties.Resources.short_skirt_denim }
                    ,new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\short_skirt_green_stripes.obj") 
                    { Image = Properties.Resources.short_skirt_green }
                    ,new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\short_skirt_red.obj") 
                    { Image = Properties.Resources.short_skirt_red }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\long_skirt.obj") 
                    { Image = Properties.Resources.long_skirt, BottomJointToTrackScale = JointType.FootLeft }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\long_skirt_bikes.obj") 
                    { Image = Properties.Resources.long_skirt_bikes, BottomJointToTrackScale = JointType.FootLeft }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\long_skirt_indian.obj") 
                    { Image = Properties.Resources.long_skirt_indian, BottomJointToTrackScale = JointType.FootLeft }
                    , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\long_skirt_violet.obj") 
                    { Image = Properties.Resources.long_skirt_violet2, BottomJointToTrackScale = JointType.FootLeft }
                     , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\big_skirt_green.obj") 
                    { Image = Properties.Resources.breezy_skirt_winter, BottomJointToTrackScale = JointType.FootLeft }
                     , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\big_skirt_waves.obj") 
                    { Image = Properties.Resources.breezy_skirt_waves, BottomJointToTrackScale = JointType.FootLeft }
                     , new SkirtButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\Skirts\big_skirt_red.obj") 
                    { Image = Properties.Resources.breezy_skirt_red, BottomJointToTrackScale = JointType.FootLeft }
                }
            };
        }
        /// <summary>
        /// Creates the dresses clothing category button.
        /// </summary>
        /// <returns>Dresses clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateDressesClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Female)
            {
                Image = Properties.Resources.dress_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_dark.obj") 
                    { Image = Properties.Resources.dress_dark }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_green.obj") 
                    { Image = Properties.Resources.dress_green }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_orange_black.obj") 
                    { Image = Properties.Resources.dress_brownblack }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_red.obj") 
                    { Image = Properties.Resources.dress_red }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_navy.obj") 
                    { Image = Properties.Resources.dress_navy }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_violet.obj") 
                    { Image = Properties.Resources.dress_violet }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_pink.obj") 
                    { Image = Properties.Resources.dress_pink }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_orange.obj") 
                    { Image = Properties.Resources.dress_orange }
                    , new DressButtonViewModel(ClothingItemBase.ClothingType.DressItem, @".\Resources\Models\Dresses\dress_blue.obj") 
                    { Image = Properties.Resources.dress_blue }
                }
            };
        }
        /// <summary>
        /// Creates the glasses clothing category button.
        /// </summary>
        /// <returns>Glasses clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateGlassesClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Both)
            {
                Image = Properties.Resources.glasses_symbol,
                Clothes =
                    new List<ClothingButtonViewModel>
                    {
                        new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_sea.obj")
                        { Image = Properties.Resources.glass_sea }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_maroon.obj")
                        { Image = Properties.Resources.glass_maroon }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_yellow.obj")
                        { Image = Properties.Resources.glass_yellow }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_blue.obj")
                        { Image = Properties.Resources.glass_blue }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\glass_red.obj")
                        { Image = Properties.Resources.glass_red }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\sunglass_green.obj")
                        { Image = Properties.Resources.sunglass_green }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\sunglass_orange.obj")
                        { Image = Properties.Resources.sunglass_orange }
                        , new GlassesButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\Glasses\sunglass_violet.obj")
                        { Image = Properties.Resources.sunglass_violet }
                    }
            };
        }
        /// <summary>
        /// Creates the ties clothing category button.
        /// </summary>
        /// <returns>Ties clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateTiesClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Male)
            {
                Image = Properties.Resources.tie_symbol,
                Clothes =
                    new List<ClothingButtonViewModel>
                    {
                        new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_beige.obj")
                        { Image = Properties.Resources.tie_beige }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_blue.obj")
                        { Image = Properties.Resources.tie_blue }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_colourful.obj")
                        { Image = Properties.Resources.tie_color }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_green.obj")
                        { Image = Properties.Resources.tie_green }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_gray.obj")
                        { Image = Properties.Resources.tie_gray }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_stripes.obj")
                        { Image = Properties.Resources.tie_stripes }
                        , new TieButtonViewModel(ClothingItemBase.ClothingType.TieItem, @".\Resources\Models\Ties\tie_dark.obj")
                        { Image = Properties.Resources.tie_dark }
                    }
            };
        }
        /// <summary>
        /// Creates the bags clothing category button.
        /// </summary>
        /// <returns>Bags clothing category button</returns>
        private ClothingCategoryButtonViewModel CreateBagsClothingCategoryButton()
        {
            return new ClothingCategoryButtonViewModel(ClothingItemBase.MaleFemaleType.Both)
            {
                Image = Properties.Resources.bag_symbol,
                Clothes =
                    new List<ClothingButtonViewModel>
                    {
                        new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\armani_bag_beige.obj")
                        { Image = Properties.Resources.armani_bag_beige }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\armani_bag_brown.obj")
                        { Image = Properties.Resources.armani_bag_brown }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\armani_bag_camel.obj")
                        { Image = Properties.Resources.armani_bag_camel }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\armani_bag_white.obj")
                        { Image = Properties.Resources.armani_bag_white }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\men_bag_brown.obj")
                        { Image = Properties.Resources.men_bag_brown }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\men_bag_brown2.obj")
                        { Image = Properties.Resources.men_bag_brown2 }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\men_bag_gray.obj")
                        { Image = Properties.Resources.men_bag_gray }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\small_bag_beige.obj")
                        { Image = Properties.Resources.small_bag_beige }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\small_bag_brown.obj")
                        { Image = Properties.Resources.small_bag_brown }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Female, @".\Resources\Models\Bags\small_bag_gray.obj")
                        { Image = Properties.Resources.small_bag_gray }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\sport_bag_red.obj")
                        { Image = Properties.Resources.sport_bag_red }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\sport_bag_blue.obj")
                        { Image = Properties.Resources.sport_bag_blue }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\sport_bag_green.obj")
                        { Image = Properties.Resources.sport_bag_green }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\suitcase_brown.obj")
                        { Image = Properties.Resources.suitcase_brown }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\suitcase_brown2.obj")
                        { Image = Properties.Resources.suitcase_brown2 }
                        , new BagButtonViewModel(ClothingItemBase.ClothingType.BagItem, ClothingItemBase.MaleFemaleType.Male, @".\Resources\Models\Bags\suitcase_gray.obj")
                        { Image = Properties.Resources.suitcase_gray }
                    }
            };
        }
        #endregion Private Methods
    }
}
