using System.Collections.Generic;
using System.Collections.ObjectModel;
using KinectFittingRoom.ViewModel.ButtonItems;
using KinectFittingRoom.ViewModel.ClothingItems;
using System.Windows;

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
                    new ClothingButtonViewModel {Image = Properties.Resources.hat_blue},
                    new ClothingButtonViewModel {Image = Properties.Resources.hat_brown},
                    new ClothingButtonViewModel {Image = Properties.Resources.hat_superman}
                }
            };
            clothing.Clothes[0].Image.Tag = new Point(2.07, 0.0);
            clothing.Clothes[1].Image.Tag = new Point(1.83, 0.0);
            clothing.Clothes[2].Image.Tag = new Point(1.24, 0.0);
            ClothingCategoryButtonViewModel clothing1 = new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.skirt_symbol,
                Clothes = new List<ClothingButtonViewModel>
                    {
                        new ClothingButtonViewModel {Image = Properties.Resources.skirt_jeans},
                        new ClothingButtonViewModel {Image = Properties.Resources.skirt_maroon}
                    }
            };
            clothing1.Clothes[0].Image.Tag = new Point(2.21, 1.0);
            clothing1.Clothes[1].Image.Tag = new Point(2.0, 1.0);
            ClothingCategoryButtonViewModel clothing2 = new ClothingCategoryButtonViewModel
            {
                Image = Properties.Resources.glasses_symbol,
                Clothes =
                    new List<ClothingButtonViewModel>
                    {
                        new ClothingButtonViewModel {Image = Properties.Resources.glasses_black},
                        new ClothingButtonViewModel {Image = Properties.Resources.glasses_blue},
                        new ClothingButtonViewModel {Image = Properties.Resources.sunglasses_rayban},
                        new ClothingButtonViewModel {Image = Properties.Resources.sunglasses_aviator}
                    }
            };
            clothing2.Clothes[0].Image.Tag = new Point(1.0, 2.0);
            clothing2.Clothes[1].Image.Tag = new Point(1.0, 2.0);
            clothing2.Clothes[2].Image.Tag = new Point(1.0, 2.0);
            clothing2.Clothes[3].Image.Tag = new Point(1.0, 2.0);

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
