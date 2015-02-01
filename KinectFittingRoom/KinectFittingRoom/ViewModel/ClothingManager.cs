using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using KinectFittingRoom.Model.ClothingItems;
using KinectFittingRoom.ViewModel.ButtonItems;
using KinectFittingRoom.ViewModel.Helpers;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel
{
    public sealed class ClothingManager : ViewModelBase
    {
        #region Private Fields
        private ObservableCollection<ClothingCategoryButtonViewModel> _actualClothingCategories;
        /// <summary>
        /// Only instance of ClothingManager
        /// </summary>
        private static ClothingManager _instance;
        /// <summary>
        /// The chosen clothing models collection
        /// </summary>
        private OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> _chosenClothesModels;
        /// <summary>
        /// The clothing collection
        /// </summary>
        private ObservableCollection<ClothingButtonViewModel> _clothing;
        /// <summary>
        /// The viewport transform
        /// </summary>
        private Matrix3D _viewportTransform;
        /// <summary>
        /// The camera transform
        /// </summary>
        private Matrix3D _cameraTransform;
        /// <summary>
        /// Gets or sets the model importer.
        /// </summary>
        /// <value>
        /// The model importer.
        /// </value>
        private ModelImporter _importer;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the clothing categories collection.
        /// </summary>
        /// <value>
        /// The clothing categories collection.
        /// </value>
        public ObservableCollection<ClothingCategoryButtonViewModel> ClothingCategories { get; set; }
        /// <summary>
        /// Gets or sets the actually displayed collection of categories
        /// </summary>
        public ObservableCollection<ClothingCategoryButtonViewModel> ActualClothingCategories
        {
            get { return _actualClothingCategories; }
            set
            {
                if (_actualClothingCategories == value)
                    return;
                _actualClothingCategories = value;
                OnPropertyChanged("ActualClothingCategories");
            }
        }
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
        public OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> ChosenClothesModels
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
        /// <summary>
        /// Gets or sets the viewport transform.
        /// </summary>
        /// <value>
        /// The viewport transform.
        /// </value>
        public Matrix3D ViewportTransform
        {
            get { return _viewportTransform; }
            set
            {
                if (_viewportTransform == value)
                    return;
                _viewportTransform = value;
                OnPropertyChanged("ViewportTransform");
            }
        }
        /// <summary>
        /// Gets or sets the camera transform.
        /// </summary>
        /// <value>
        /// The camera transform.
        /// </value>
        public Matrix3D CameraTransform
        {
            get { return _cameraTransform; }
            set
            {
                if (_cameraTransform == value)
                    return;
                _cameraTransform = value;
                OnPropertyChanged("CameraTransform");
            }
        }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Private constructor of ClothingManager. 
        /// </summary>
        private ClothingManager()
        {
            ChosenType = ClothingItemBase.MaleFemaleType.Female;
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
            _importer = new ModelImporter();
        }
        #endregion .ctor
        #region Protected Methods
        /// <summary>
        /// Scales height of clothes
        /// </summary>
        /// <param name="ratio">The ratio of scaling</param>
        public void ScaleImageHeight(double ratio)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ChosenClothesModels;
            tmp.Last.HeightScale += ratio;
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }
        /// <summary>
        /// Scales width of clothes
        /// </summary>
        /// <param name="ratio">The ratio of scaling</param>
        public void ScaleImageWidth(double ratio)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ChosenClothesModels;
            tmp.Last.WidthScale += ratio;
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }
        /// <summary>
        /// Changes position of clothes
        /// </summary>
        /// <param name="delta">Position delta</param>
        public void ChangeImagePosition(double delta)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ChosenClothesModels;
            tmp.Last.DeltaPosition += delta;
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
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
        /// <summary>
        /// Updates actually displayed clothing categories
        /// </summary>
        public void UpdateActualCategories()
        {
            if (ActualClothingCategories == null)
                ActualClothingCategories = new ObservableCollection<ClothingCategoryButtonViewModel>();

            ActualClothingCategories.Clear();
            foreach (var category in ClothingCategories)
                if (category.Type == ClothingItemBase.MaleFemaleType.Both || category.Type == ChosenType)
                    ActualClothingCategories.Add(category);
        }
        /// <summary>
        /// Adds the clothing item.
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="category">The category of the item.</param>
        /// <param name="modelPath">The model path.</param>
        /// <param name="bottomJoint">Bottom joint to track size</param>
        /// <param name="ratio">Scale ratio</param>
        /// <param name="deltaY">Default y position</param>
        public void AddClothingItem<T>(ClothingItemBase.ClothingType category, string modelPath, JointType bottomJoint, double ratio, double deltaY)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmpModels = ChosenClothesModels;
            tmpModels[category] = (ClothingItemBase)Activator.CreateInstance(typeof(T), _importer.Load(modelPath), bottomJoint, ratio, deltaY);
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmpModels);
        }
        /// <summary>
        /// Adds the clothing item.
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="category">The category of the item.</param>
        /// <param name="modelPath">The model path.</param>
        /// <param name="ratio">Scale ratio</param>
        /// <param name="deltaY">Default y position</param>
        public void AddClothingItem<T>(ClothingItemBase.ClothingType category, string modelPath, double ratio, double deltaY)
        {
            OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmpModels = ChosenClothesModels;
            tmpModels[category] = (ClothingItemBase)Activator.CreateInstance(typeof(T), _importer.Load(modelPath), ratio, deltaY);
            ChosenClothesModels = new OrderedDictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmpModels);
        }
        #endregion Public Methods
    }
}
