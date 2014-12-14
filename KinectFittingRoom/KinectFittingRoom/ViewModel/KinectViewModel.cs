using System.Collections.Generic;
using System.Collections.ObjectModel;
using KinectFittingRoom.ViewModel.ButtonItems;
using KinectFittingRoom.ViewModel.ClothingItems;
using System.Windows;
using System.Drawing;

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
            ClothingCategoryButtonViewModel clothing = new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.hat_symbol,
                Clothes = new List<ClothingButtonViewModel>
                {
                    new ClothingButtonViewModel {
                        Image = Properties.Resources.small_hat_blue, 
                        Category = ClothingItemBase.ClothingType.HatItem, 
                        PathToImage=@".\Resources\Models\hat_blue.png", 
                        ImageWidthToItemWidth = 2.07
                    },
                    new ClothingButtonViewModel {
                        Image = Properties.Resources.small_hat_brown, 
                        Category = ClothingItemBase.ClothingType.HatItem, 
                        PathToImage=@".\Resources\Models\hat_brown.png", 
                        ImageWidthToItemWidth = 1.83
                    },
                    new ClothingButtonViewModel {
                        Image = Properties.Resources.small_hat_superman, 
                        Category = ClothingItemBase.ClothingType.HatItem, 
                        PathToImage=@".\Resources\Models\hat_superman.png", 
                        ImageWidthToItemWidth = 1.24
                    }
                }
            };
            ClothingCategoryButtonViewModel clothing1 = new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.skirt_symbol,
                Clothes = new List<ClothingButtonViewModel>
                    {
                        new ClothingButtonViewModel {
                            Image = Properties.Resources.small_skirt_jeans, 
                            Category = ClothingItemBase.ClothingType.SkirtItem, 
                            PathToImage=@".\Resources\Models\skirt_jeans.png", 
                            ImageWidthToItemWidth = 2.21
                        },
                        new ClothingButtonViewModel {
                            Image = Properties.Resources.small_skirt_maroon, 
                            Category = ClothingItemBase.ClothingType.SkirtItem, 
                            PathToImage=@".\Resources\Models\skirt_maroon.png", 
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
                        new ClothingButtonViewModel {
                            Image = Properties.Resources.small_glasses_black, 
                            Category = ClothingItemBase.ClothingType.GlassesItem, 
                            PathToImage=@".\Resources\Models\glasses_black.png"
                        },
                        new ClothingButtonViewModel {
                            Image = Properties.Resources.small_glasses_blue, 
                            Category = ClothingItemBase.ClothingType.GlassesItem, 
                            PathToImage=@".\Resources\Models\glasses_blue.png"
                        },
                        new ClothingButtonViewModel {
                            Image = Properties.Resources.small_sunglasses_rayban, 
                            Category = ClothingItemBase.ClothingType.GlassesItem, 
                            PathToImage=@".\Resources\Models\sunglasses_rayban.png"
                        },
                        new ClothingButtonViewModel {
                            Image = Properties.Resources.small_sunglasses_aviator, 
                            Category = ClothingItemBase.ClothingType.GlassesItem, 
                            PathToImage=@".\Resources\Models\sunglasses_aviator.png"
                        }
                    }
            };

            ClothingCategories.Add(clothing);
            ClothingCategories.Add(clothing1);
            ClothingCategories.Add(clothing2);
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
