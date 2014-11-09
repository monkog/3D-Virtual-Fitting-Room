using System.Windows;
using System.Windows.Media.Media3D;

namespace KinectFittingRoom.View.Buttons.Events
{
    /// <summary>
    /// Class handling Hand Cursor Events
    /// </summary>
    public class HandCursorEventArgs : RoutedEventArgs
    {
        #region Public Properties
        /// <summary>
        /// X coordinate 
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y coordinate 
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Z coordinate 
        /// </summary>
        public double Z { get; set; }
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="HandCursorEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public HandCursorEventArgs(RoutedEvent routedEvent, double x, double y, double z)
            : base(routedEvent)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HandCursorEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="point">Contains the x and y coordinates.</param>
        /// <param name="z">The z coordinate.</param>
        public HandCursorEventArgs(RoutedEvent routedEvent, Point point, double z)
            : base(routedEvent)
        {
            X = point.X;
            Y = point.Y;
            Z = z;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HandCursorEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="activeHand">The active hand.</param>
        public HandCursorEventArgs(RoutedEvent routedEvent, Vector3D activeHand)
            : base(routedEvent)
        {
            X = activeHand.X;
            Y = activeHand.Y;
            Z = activeHand.Z;
        }
        #endregion .ctor
    }
}
