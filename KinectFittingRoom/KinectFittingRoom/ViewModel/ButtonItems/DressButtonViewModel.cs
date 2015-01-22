using KinectFittingRoom.Model.ClothingItems;
using Microsoft.Kinect;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class DressButtonViewModel : ClothingButtonViewModel
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the bottom joint to track scale.
        /// </summary>
        /// <value>
        /// The bottom joint to track scale.
        /// </value>
        public JointType? BottomJointToTrackScale { get; set; }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="DressButtonViewModel"/> class.
        /// </summary>
        /// <param name="type">The type of the clothing.</param>
        /// <param name="pathToModel">The path to model.</param>
        public DressButtonViewModel(ClothingItemBase.ClothingType type, string pathToModel)
            : base(type, ClothingItemBase.MaleFemaleType.Female, pathToModel)
        { }
        #endregion .ctor
        #region Commands
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        public override void ClickExecuted()
        {
            PlaySound();
            if (ClothingManager.Instance.ChosenClothesModels.ContainsKey(ClothingItemBase.ClothingType.SkirtItem))
                ClothingManager.Instance.ChosenClothesModels.Remove(ClothingItemBase.ClothingType.SkirtItem);
            ClothingManager.Instance.AddClothingItem<DressItem>(Category, ModelPath, BottomJointToTrackScale);
        }
        #endregion Commands
    }
}
