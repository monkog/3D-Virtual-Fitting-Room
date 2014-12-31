using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using KinectFittingRoom.ViewModel.ButtonItems;
using Microsoft.Kinect;

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
        /// The chosen clothing models collection
        /// </summary>
        private Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> _chosenClothesModels;
        /// <summary>
        /// The clothing collection
        /// </summary>
        private ObservableCollection<ClothingButtonViewModel> _clothing;
        /// <summary>
        /// Position of the spine joint
        /// </summary>
        private Vector3D _spinePosition;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the chosen type of clothes
        /// </summary>
        public ClothingItemBase.MaleFemaleType ChosenType { get; set; }
        /// <summary>
        /// Gets or sets last chosen category
        /// </summary>
        public ClothingCategoryButtonViewModel LastChosenCategory { get; set; }
        /// <summary>
        /// Gets or sets the chosen clothing models collection.
        /// </summary>
        /// <value>
        /// The chosen clothing models collection.
        /// </value>
        public Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> ChosenClothesModels
        {
            get { return _chosenClothesModels; }
            set
            {
                if (_chosenClothesModels == value)
                    return;
                _chosenClothesModels = value;
                OnPropertyChanged("ChosenClothesModels");
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
        /// <summary>
        /// Method with access to only instance of ClothingManager
        /// </summary>
        public static ClothingManager Instance
        {
            get { return _instance ?? (_instance = new ClothingManager()); }
        }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Private constructor of ClothingManager. 
        /// </summary>
        private ClothingManager()
        {
            ChosenType = ClothingItemBase.MaleFemaleType.Female;
            ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
        }
        #endregion .ctor
        #region Protected Methods

        /// <summary>
        /// Scales images of clothes
        /// </summary>
        /// <param name="ratio">The ratio of scaling</param>
        public void ScaleImage(double ratio)
        {
            if (ChosenClothesModels.Count == 0)
                return;

            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ChosenClothesModels;
            tmp.Last().Value.HeightScale += ratio;
            ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }
        #endregion Protected Methods
        #region Public Methods
        /// <summary>
        /// Updates the item position.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void UpdateItemPosition(Skeleton skeleton, KinectSensor sensor, double width, double height)
        {
            foreach (var model in ChosenClothesModels.Values)
                model.UpdateItemPosition(skeleton, sensor, width, height);
        }
        #endregion Public Methods
    }
}
