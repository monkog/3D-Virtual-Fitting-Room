using KinectFittingRoom.Model.ClothingItems;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class TopButtonViewModel : ClothingButtonViewModel
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the bottom joint to track scale.
        /// </summary>
        /// <value>
        /// The bottom joint to track scale.
        /// </value>
        public JointType BottomJointToTrackScale = JointType.Spine;
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="TopButtonViewModel"/> class.
        /// </summary>
        /// <param name="type">The type of the clothing.</param>
        /// <param name="maleFemaleType">Male/Female type of clothing</param>
        /// <param name="pathToModel">The path to model.</param>
        public TopButtonViewModel(ClothingItemBase.ClothingType type, ClothingItemBase.MaleFemaleType maleFemaleType,
            string pathToModel)
            : base(type, maleFemaleType, pathToModel)
        {
            Ratio = 1.2;
            DeltaY = 0.95;
        }
        #endregion .ctor
        #region Commands
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.ContainsKey(ClothingItemBase.ClothingType.DressItem))
                ClothingManager.Instance.ChosenClothesModels.Remove(ClothingItemBase.ClothingType.DressItem);
            ClothingManager.Instance.AddClothingItem<TopItem>(Category, ModelPath, BottomJointToTrackScale, Ratio, DeltaY);
        }
        #endregion Commands
    }
}

