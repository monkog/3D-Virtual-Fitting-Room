using KinectFittingRoom.ViewModel.ClothingItems;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectFittingRoom.ViewModel.ButtonItems.TopMenuButtons
{
    class ScaleMinusButtonViewModel : TopMenuButtonViewModel
    {
        #region Consts
        private const double MinusFactor = -0.05;
        #endregion
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleMinusButtonViewModel"/> class.
        /// </summary>
        /// <param name="image">Image of button</param>
        public ScaleMinusButtonViewModel(Bitmap image)
            : base(image)
        { }
        #endregion
        #region Methods
        /// <summary>
        /// Makes last added item smaller
        /// </summary>
        public override void ClickEventExecuted()
        {
            if (ClothingManager.Instance.ChosenClothesModels.Count != 0)
            {
                ClothingManager.Instance.ScaleImageHeight(MinusFactor);
                ClothingManager.Instance.ScaleImageWidth(MinusFactor);
            }
                
        }
        #endregion
    }
}
