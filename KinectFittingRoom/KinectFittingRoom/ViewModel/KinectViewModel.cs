using System.Collections.Generic;
using System.Collections.ObjectModel;
using KinectFittingRoom.ViewModel.ButtonItems;
using KinectFittingRoom.ViewModel.ClothingItems;

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
            ClothingCategories = new ObservableCollection<ClothingCategoryButtonViewModel>();

            ClothingCategories.Add(CreateHatsClothingCategoryButton());
            ClothingCategories.Add(CreateSkirtsClothingCategoryButton());
            ClothingCategories.Add(CreateGlassesClothingCategoryButton());
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
                    new ClothingButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\hat_blue.png")  {
                        Image = Properties.Resources.small_hat_blue, 
                        ImageWidthToItemWidth = 2.07
                    },
                    new ClothingButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\hat_brown.png") {
                        Image = Properties.Resources.small_hat_brown, 
                        ImageWidthToItemWidth = 1.83
                    },
                    new ClothingButtonViewModel(ClothingItemBase.ClothingType.HatItem, @".\Resources\Models\hat_superman.png") {
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
                    new ClothingButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\skirt_jeans.png") {
                        Image = Properties.Resources.small_skirt_jeans, 
                        ImageWidthToItemWidth = 2.21
                    },
                    new ClothingButtonViewModel(ClothingItemBase.ClothingType.SkirtItem, @".\Resources\Models\skirt_maroon.png") {
                        Image = Properties.Resources.small_skirt_maroon, 
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
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\glasses_black.png") {
                            Image = Properties.Resources.small_glasses_black, 
                        },
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\glasses_blue.png") {
                            Image = Properties.Resources.small_glasses_blue, 
                        },
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\sunglasses_rayban.png") {
                            Image = Properties.Resources.small_sunglasses_rayban, 
                        },
                        new ClothingButtonViewModel(ClothingItemBase.ClothingType.GlassesItem, @".\Resources\Models\sunglasses_aviator.png") {
                            Image = Properties.Resources.small_sunglasses_aviator, 
                        }
                    }
            };
        }
        #endregion Private Methods
    }
}
