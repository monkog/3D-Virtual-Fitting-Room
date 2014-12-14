using KinectFittingRoom.ViewModel.ClothingItems;
using KinectFittingRoom.ViewModel.Debug;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
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
        /// Path to original size of item's image
        /// </summary>
        private string _pathToImage;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets button object
        /// </summary>
        public ClothingButtonViewModel ButtonObject
        {
            get { return this; }
        }
        /// <summary>
        /// Gets or sets path to original size of item's image
        /// </summary>
        public string PathToImage
        {
            get { return _pathToImage; }
            set
            {
                if (_pathToImage == value)
                    return;
                _pathToImage = value;
            }
        }
        /// <summary>
        /// Gets or sets category of item
        /// </summary>
        public ClothingItemBase.ClothingType Category
        {
            get { return _category; }
            set
            {
                if (_category == value)
                    return;
                _category = value;
            }
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
        public void CategoryExecuted(object parameter)
        {
            // TODO: Resolve the Clothing Manager via the IOC container?
            // TODO: Change the clothing collection in Clothing Manager to the one corresponding to the chosen button
            // TODO: Preload the collection at startup or load dynamically in another thread?

            ClothingButtonViewModel clickedButton = (ClothingButtonViewModel)parameter;
            ClothingItemBase clickedClothingItem;

            if (ClothingManager.Instance.ChosenClothes.TryGetValue(clickedButton.Category, out clickedClothingItem))
                if (clickedButton.ImageWidthToItemWidth == clickedClothingItem.ImageWidthToItemWidth)
                    return;

            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(ClothingManager.Instance.ChosenClothes);

            switch (clickedButton.Category)
            {
                case ClothingItemBase.ClothingType.GlassesItem:
                    clickedClothingItem = new GlassesItem(new Bitmap(Bitmap.FromFile(clickedButton.PathToImage)));
                    break;
                case ClothingItemBase.ClothingType.HatItem:
                    clickedClothingItem = new HatItem(new Bitmap(Bitmap.FromFile(clickedButton.PathToImage)), clickedButton.ImageWidthToItemWidth);
                    break;
                case ClothingItemBase.ClothingType.SkirtItem:
                    clickedClothingItem = new SkirtItem(new Bitmap(Bitmap.FromFile(clickedButton.PathToImage)), clickedButton.ImageWidthToItemWidth);
                    break;
            }

            tmp[clickedButton.Category] = clickedClothingItem;
            ClothingManager.Instance.ChosenClothes = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }
        #endregion Commands
    }
}

