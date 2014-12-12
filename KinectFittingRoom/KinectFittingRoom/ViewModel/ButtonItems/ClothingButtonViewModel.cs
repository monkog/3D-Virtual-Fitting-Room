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
        /// Clicked button object
        /// </summary>
        private ClothingButtonViewModel _buttonObject;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets button object
        /// </summary>
        public ClothingButtonViewModel ButtonObject
        {
            get { return _buttonObject; }
            set
            {
                if (_buttonObject == value)
                    return;
                _buttonObject = value;
                OnPropertyChanged("ButtonObject");
            }

        }
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
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ClothingButtonViewModel"/> class.
        /// </summary>
        public ClothingButtonViewModel()
        {
            ButtonObject = this;
        }
        #endregion
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

            foreach (ClothingItemBase item in ClothingManager.Instance.ChosenClothes)
                if (item.Image == clickedButton.Image)
                    return;

            switch (clickedButton.Category)
            {
                case 0:
                    ClothingManager.Instance.ChosenClothes.Add(new Hat(clickedButton.Image, clickedButton.ImageWidthToItemWidth));
                    break;
                case 1:
                    ClothingManager.Instance.ChosenClothes.Add(new Skirt(clickedButton.Image, clickedButton.ImageWidthToItemWidth));
                    break;
                case 2:
                    ClothingManager.Instance.ChosenClothes.Add(new Glasses(clickedButton.Image));
                    break;
            }
        }
        #endregion Commands
    }
}
