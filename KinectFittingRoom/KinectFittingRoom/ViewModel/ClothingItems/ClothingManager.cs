using System.Collections.ObjectModel;
using System.Collections.Generic;
using KinectFittingRoom.ViewModel.ButtonItems;
using System.Drawing;
using System.Linq;

namespace KinectFittingRoom.ViewModel.ClothingItems
{
    public sealed class ClothingManager : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// Only instance of ClothingManager
        /// </summary>
        private static ClothingManager _instance;
        /// <summary>
        /// The chosen clothing collection
        /// </summary>
        private Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> _chosenClothes;
        /// <summary>
        /// The clothing collection
        /// </summary>
        private ObservableCollection<ClothingButtonViewModel> _clothing;
        /// <summary>
        /// The last added item to ChosenClothes collection
        /// </summary>
        private ClothingItemBase _lastAddedItem;
        /// <summary>
        /// The last chosen category
        /// </summary>
        private ClothingCategoryButtonViewModel _lastChosenCategory;
        /// <summary>
        /// Chosen type of clothes
        /// </summary>
        private ClothingItemBase.MaleFemaleType _chosenType;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the chosen type of clothes
        /// </summary>
        public ClothingItemBase.MaleFemaleType ChosenType
        {
            get { return _chosenType; }
            set
            {
                if (_chosenType == value)
                    return;
                _chosenType = value;
            }
        }
        /// <summary>
        /// Gets or sets last chosen category
        /// </summary>
        public ClothingCategoryButtonViewModel LastChosenCategory
        {
            get { return _lastChosenCategory; }
            set
            {
                if (_lastChosenCategory == value)
                    return;
                _lastChosenCategory = value;
            }
        }
        /// <summary>
        /// Gets or sets the last added item to ChosenClothes collection
        /// </summary>
        public ClothingItemBase LastAddedItem
        {
            get { return _lastAddedItem; }
            set
            {
                if (_lastAddedItem == value)
                    return;
                _lastAddedItem = value;
            }
        }
        /// <summary>
        /// Gets or sets the chosen clothing collection.
        /// </summary>
        /// <value>
        /// The chosen clothing collection.
        /// </value>
        public Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> ChosenClothes
        {
            get { return _chosenClothes; }
            set
            {
                if (_chosenClothes == value)
                    return;
                _chosenClothes = value;
                OnPropertyChanged("ChosenClothes");
            }
        }
        /// <summary>
        /// Gets or sets the available clothing collection.
        /// </summary>
        /// <value>
        /// The available clothing collection.
        /// </value>
        public ObservableCollection<ClothingButtonViewModel> Clothing
        {
            get { return _clothing; }
            set
            {
                if (_clothing == value)
                    return;
                _clothing = value;
                OnPropertyChanged("Clothing");
            }
        }

        #endregion Public Properties
        /// <summary>
        /// Private constructor of ClothingManager. 
        /// </summary>
        private ClothingManager() 
        {
            ChosenClothes = new Dictionary<ClothingItemBase.ClothingType,ClothingItemBase>();
            _chosenType = ClothingItemBase.MaleFemaleType.Female;
        }
        /// <summary>
        /// Scales images of clothes
        /// </summary>
        /// <param name="ratio">The ratio of scaling</param>
        public void ScaleImage(double ratio)
        {
            ClothingItemBase lastItem = LastAddedItem;
            lastItem.Image = new Bitmap(lastItem.Image, (int)lastItem.Image.Width, (int)(ratio * lastItem.Image.Height));

            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ChosenClothes;
            tmp[tmp.FirstOrDefault(a => a.Value.PathToImage == lastItem.PathToImage).Key] = lastItem;
            ChosenClothes = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }
        /// <summary>
        /// Method with access to only instance of ClothingManager
        /// </summary>
        public static ClothingManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClothingManager();
                return _instance;
            }
        }
        
    }
}
