using System.Collections.ObjectModel;
using System.Collections.Generic;
using KinectFittingRoom.ViewModel.ButtonItems;

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
        #endregion Private Fields
        #region Public Properties
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
