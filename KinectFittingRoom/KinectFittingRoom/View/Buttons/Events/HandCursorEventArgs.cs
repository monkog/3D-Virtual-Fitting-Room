using System.Windows;

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
        #endregion Public Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="HandCursorEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public HandCursorEventArgs(RoutedEvent routedEvent, double x, double y)
            : base(routedEvent)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HandCursorEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="point">Contains the x and y coordinates.</param>
        public HandCursorEventArgs(RoutedEvent routedEvent, Point point)
            : base(routedEvent)
        {
            X = point.X;
            Y = point.Y;
        }
        #endregion .ctor
    }
}
