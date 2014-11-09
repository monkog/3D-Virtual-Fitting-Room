using System.Drawing;
using Microsoft.Kinect;

namespace KinectFittingRoom.Items
{
    public class HandItem : ClothingItem
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the right or left hand joint.
        /// </summary>
        public Joint Hand
        {
            get { return m_hand; }
            set { m_hand = value; }
        }
        #endregion Public Properties
        #region Private Fields
        /// <summary>
        /// The hand joint that the item will follow
        /// </summary>
        private Joint m_hand;
        #endregion Private Fields

        public HandItem(Bitmap image, int x, int y)
            : base(image, x, y)
        {
        }
    }
}
