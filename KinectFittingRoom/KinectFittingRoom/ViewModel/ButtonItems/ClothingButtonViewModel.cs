using System.Collections.Generic;
using System.Windows.Input;
using KinectFittingRoom.ViewModel.ClothingItems;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using KinectFittingRoom.ViewModel.Debug;
using System.Windows;
using System.Drawing;
using System;

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
        /// <para>0 - hat, 1 - skirt, 2 - glasses</para>
        /// </summary>
        private int _category;
        /// <summary>
        /// Proportion image width to significant width of item
        /// </summary>
        private double _imageWidthToItemWidth;
        /// <summary>
        /// List of clothes in current category
        /// </summary>
        private List<ClothingItemBase> _clothes;
        #endregion Private Fields
        /// <summary>
        /// Gets of sets category of item
        /// <para>0 - hat, 1 - skirt, 2 - glasses</para>
        /// </summary>
        public int Category
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
        #region Public Properties
        /// <summary>
        /// Gets or sets the clothes list.
        /// </summary>
        /// <value>
        /// The clothes list.
        /// </value>
        public List<ClothingItemBase> Clothes
        {
            get { return _clothes; }
            set
            {
                if (_clothes == value)
                    return;
                _clothes = value;
                OnPropertyChanged("Clothes");
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

            //MOJE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Bitmap im = (Bitmap)parameter;
            int counter = 0;
            foreach (var i in ClothingManager.Instance.Clothing)
            {
                if (i.Image.Width == im.Width && i.Image.Height == im.Height)
                {
                    foreach (var j in ClothingManager.Instance.ChosenClothes)
                        if (j.PositionInCategoryList == counter)
                            return;
                    switch (i.Category)
                    {
                        case 0:
                            ClothingManager.Instance.ChosenClothes.Add(new Hat(i.Image, i.ImageWidthToItemWidth, counter));
                            break;
                        case 1:
                            ClothingManager.Instance.ChosenClothes.Add(new Skirt(i.Image, i.ImageWidthToItemWidth, counter));
                            break;
                        case 2:
                            ClothingManager.Instance.ChosenClothes.Add(new Glasses(i.Image, counter));
                            break;
                    }
                    break;
                }
                counter++;
            }
        }
        #endregion Commands
    }
}
