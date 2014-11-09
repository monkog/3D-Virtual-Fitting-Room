using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace KinectFittingRoom.Items
{
    public class ClothingItem : UIElement
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public double X
        {
            get { return m_x; }
            set
            {
                m_x = value;
                Canvas.SetLeft(this, m_x);
            }
        }
        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public double Y
        {
            get { return m_y; }
            set
            {
                m_y = value;
                Canvas.SetTop(this, m_y);
            }
        }
        #endregion Public Properties
        #region Private Fields
        /// <summary>
        /// Item image
        /// </summary>
        private Bitmap m_image;
        /// <summary>
        /// The X coordinate
        /// </summary>
        private double m_x;
        /// <summary>
        /// The Y coordinate
        /// </summary>
        private double m_y;
        #endregion Private Fields

        public ClothingItem(Bitmap image, int x, int y)
        {
            m_image = image;
            X = x;
            Y = y;
        }
    }
}
