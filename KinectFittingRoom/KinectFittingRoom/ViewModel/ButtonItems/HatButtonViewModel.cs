﻿using KinectFittingRoom.ViewModel.ClothingItems;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class HatButtonViewModel : ClothingButtonViewModel
    {
        #region .ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="HatButtonViewModel"/> class.
        /// </summary>
        /// <param name="type">The type of the clothing.</param>
        /// <param name="maleFemaleType">Male or female type.</param>
        /// <param name="pathToModel">The path to model.</param>
        public HatButtonViewModel(ClothingItemBase.ClothingType type, ClothingItemBase.MaleFemaleType maleFemaleType, string pathToModel)
            : base(type, maleFemaleType, pathToModel)
        { }
        #endregion .ctor
        #region Commands
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            ClothingManager.Instance.AddClothingItem<HatItem>(Category, ModelPath);
        }
        #endregion Commands
    }
}
