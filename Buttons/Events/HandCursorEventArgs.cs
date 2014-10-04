using System.Windows;
using System.Windows.Media.Media3D;

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

        public HandCursorEventArgs(RoutedEvent routedEvent, double x, double y, double z)
            : base(routedEvent)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public HandCursorEventArgs(RoutedEvent routedEvent, Point point, double z)
            : base(routedEvent)
        {
            X = point.X;
            Y = point.Y;
            Z = z;
        }

        public HandCursorEventArgs(RoutedEvent routedEvent, Vector3D activeHand)
            : base(routedEvent)
        {
            X = activeHand.X;
            Y = activeHand.Y;
            Z = activeHand.Z;
        }
    }
}
