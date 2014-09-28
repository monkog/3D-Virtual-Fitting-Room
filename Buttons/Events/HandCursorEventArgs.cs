using System.Windows;

namespace KinectFittingRoom.Events
{
    public class HandCursorEventArgs : RoutedEventArgs
    {
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

        public HandCursorEventArgs(RoutedEvent routedEvent, Point point, double z)
            : base(routedEvent)
        {
            X = point.X;
            Y = point.Y;
            Z = z;
        }
    }
}
