using HelixToolkit.Wpf;
#if DEBUG
using KinectFittingRoom.ViewModel.Debug;
#endif
using KinectFittingRoom.ViewModel.ClothingItems;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    /// <summary>
    /// View model for the Clothing buttons
    /// </summary>
    public class ClothingButtonViewModel : ButtonViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// Category of item
        /// </summary>
        private ClothingItemBase.ClothingType _category;
        /// <summary>
        /// Proportion image width to significant width of item
        /// </summary>
        private double _imageWidthToItemWidth;
        /// <summary>
        /// Type of clothing item
        /// </summary>
        private ClothingItemBase.MaleFemaleType _type;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets category of item
        /// </summary>
        public ClothingItemBase.ClothingType Category
        {
            get { return _category; }
        }
        /// <summary>
        /// Gets type of item
        /// </summary>
        public ClothingItemBase.MaleFemaleType Type
        {
            get { return _type; }
        }
        /// <summary>
        /// Gets or sets proportion image width to significant width of item
        /// </summary>
        public double ImageWidthToItemWidth
        {
            get { return _imageWidthToItemWidth; }
            set
            {
                if (_imageWidthToItemWidth == value)
                    return;
                _imageWidthToItemWidth = value;
            }
        }
        /// <summary>
        /// Gets or sets the model importer.
        /// </summary>
        /// <value>
        /// The model importer.
        /// </value>
        public ModelImporter Importer { get; set; }
        /// <summary>
        /// Gets or sets the model path.
        /// </summary>
        /// <value>
        /// The model path.
        /// </value>
        public string ModelPath { get; set; }
        /// <summary>
        /// Gets or sets the texture path.
        /// </summary>
        /// <value>
        /// The texture path.
        /// </value>
        public string TexturePath { get; set; }
        #endregion Public Properties
        #region Commands
        /// <summary>
        /// The category command, executed after clicking on Category button
        /// </summary>
        private ICommand _clothCommand;
        /// <summary>
        /// Gets the category command.
        /// </summary>
        /// <value>
        /// The category command.
        /// </value>
        public ICommand ClothCommand
        {
            get { return _clothCommand ?? (_clothCommand = new DelegateCommand<object>(CategoryExecuted)); }
        }
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public virtual void CategoryExecuted(object parameter)
        { }
        #endregion Commands
        #region .ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClothingButtonViewModel"/> class.
        /// </summary>
        /// <param name="category">Clothing category</param>
        /// <param name="type">Male or female type of clothing</param>
        /// <param name="pathToModel">Path to the model</param>
        /// <param name="pathToTexture">Path to the texture of the item</param>
        public ClothingButtonViewModel(ClothingItemBase.ClothingType category, ClothingItemBase.MaleFemaleType type, string pathToModel, string pathToTexture)
        {
            _category = category;
            _type = type;
            TexturePath = pathToTexture;
            ModelPath = pathToModel;
            Importer = new ModelImporter();
        }
        #endregion
    }
}

