using System;
using System.Collections.Generic;
using HelixToolkit.Wpf;
using KinectFittingRoom.ViewModel.ClothingItems;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System.IO;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    /// <summary>
    /// View model for the Clothing buttons
    /// </summary>
    public abstract class ClothingButtonViewModel : ButtonViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// Category of item
        /// </summary>
        private ClothingItemBase.ClothingType _category;
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
        public abstract void CategoryExecuted(object parameter);
        #endregion Commands
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClothingButtonViewModel"/> class.
        /// </summary>
        /// <param name="category">Clothing category</param>
        /// <param name="type">Male or female type of clothing</param>
        /// <param name="pathToModel">Path to the model</param>
        protected ClothingButtonViewModel(ClothingItemBase.ClothingType category, ClothingItemBase.MaleFemaleType type, string pathToModel)
        {
            _category = category;
            _type = type;
            ModelPath = pathToModel;
            Importer = new ModelImporter();
        }
        #endregion
        #region Protected Methods
        /// <summary>
        /// Adds the clothing item.
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        protected void AddClothingItem<T>()
        {
            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmpModels = ClothingManager.Instance.ChosenClothesModels;
            GenerateTexturePath(ModelPath);

            tmpModels[Category] = (ClothingItemBase)Activator.CreateInstance(typeof(T), Importer.Load(ModelPath));
            ClothingManager.Instance.ChosenClothesModels = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmpModels);
        }
        /// <summary>
        /// Generates in mtl file actual path to texture
        /// </summary>
        /// <param name="modelPath">Path to model</param>
        private void GenerateTexturePath(string modelPath)
        {
            int i = 0;
            var lines = File.ReadAllLines(Path.GetFullPath(modelPath.Replace("obj", "mtl")));
            foreach (var l in lines)
            {
                if (l.StartsWith("map_Kd"))
                    break;
                i++;
            }
            if (i == lines.Length)
                return;

            lines[i] = "map_Kd " + Path.GetFullPath(@".\Resources\Materials\"+Path.GetFileNameWithoutExtension(modelPath)+".jpg");
            File.WriteAllLines(Path.GetFullPath(modelPath.Replace("obj", "mtl")), lines);
        }
        #endregion Protected Methods
    }
}

