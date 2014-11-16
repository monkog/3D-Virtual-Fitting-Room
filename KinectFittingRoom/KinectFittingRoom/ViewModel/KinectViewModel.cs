using System.Collections.Generic;
using System.Collections.ObjectModel;
using KinectFittingRoom.ViewModel.ButtonItems;

namespace KinectFittingRoom.ViewModel
{
    /// <summary>
    /// View model for MainWindow
    /// </summary>
    public class KinectViewModel : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// The Kinect service
        /// </summary>
        private readonly KinectService _kinectService;
        /// <summary>
        /// The clothing category collection
        /// </summary>
        private ObservableCollection<ClothingCategory> _clothingCategory;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the clothing categories collection.
        /// </summary>
        /// <value>
        /// The clothing categories collection.
        /// </value>
        public ObservableCollection<ClothingCategory> ClothingCategories
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
            ClothingCategories = new ObservableCollection<ClothingCategory>();
            ClothingCategory clothing = new ClothingCategory();
            clothing.Clothes = new List<ClothingCategory>();
            clothing.Clothes.Add(new ClothingCategory { Image = Properties.Resources.Hat });
            clothing.Clothes.Add(new ClothingCategory { Image = Properties.Resources.Hand });
            clothing.Clothes.Add(new ClothingCategory { Image = Properties.Resources.Hand });
            clothing.Clothes.Add(new ClothingCategory { Image = Properties.Resources.Hat });

            ClothingCategory clothing1 = new ClothingCategory();
            clothing1.Clothes = new List<ClothingCategory>();
            clothing1.Clothes.Add(new ClothingCategory { Image = Properties.Resources.Hat });
            clothing1.Clothes.Add(new ClothingCategory { Image = Properties.Resources.Hand });
            clothing1.Clothes.Add(new ClothingCategory { Image = Properties.Resources.Hand });

            ClothingCategory clothing2 = new ClothingCategory();
            clothing2.Clothes = new List<ClothingCategory>();
            clothing2.Clothes.Add(new ClothingCategory { Image = Properties.Resources.Hat });
            clothing2.Clothes.Add(new ClothingCategory { Image = Properties.Resources.Hand });

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
