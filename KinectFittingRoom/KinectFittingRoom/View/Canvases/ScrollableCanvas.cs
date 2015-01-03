using KinectFittingRoom.View.Buttons.Events;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace KinectFittingRoom.View.Canvases
{
    public class ScrollableCanvas : ItemsControl
    {
        /// <summary>
        /// The last hand position
        /// </summary>
        private Point _lastHandPosition;
        private bool _downMove;
        private bool _isMoved;
        private double _canvasMinHeight;
        private double _canvasMaxHeight;
        #region Events
        /// <summary>
        /// Hand cursor enter event
        /// </summary>
        public static readonly RoutedEvent CanvasInsideMoveEvent
            = KinectInput.HandCursorEnterEvent.AddOwner(typeof(ScrollableCanvas));
        /// <summary>
        /// Hand cursor move event
        /// </summary>
        public static readonly RoutedEvent HandCursorMoveEvent
            = KinectInput.HandCursorMoveEvent.AddOwner(typeof(ScrollableCanvas));
        /// <summary>
        /// Hand cursor leave event
        /// </summary>
        public static readonly RoutedEvent HandCursorLeaveEvent
            = KinectInput.HandCursorLeaveEvent.AddOwner(typeof(ScrollableCanvas));
        #endregion
        #region Event handlers
        /// <summary>
        /// Hand cursor enter event handler
        /// </summary>
        public event HandCursorEventHandler CanvasInsideMove
        {
            add { AddHandler(CanvasInsideMoveEvent, value); }
            remove { RemoveHandler(CanvasInsideMoveEvent, value); }
        }
        /// <summary>
        /// Hand cursor move event handler
        /// </summary>
        public event HandCursorEventHandler HandCursorMove
        {
            add { AddHandler(HandCursorMoveEvent, value); }
            remove { RemoveHandler(HandCursorMoveEvent, value); }
        }
        /// <summary>
        /// Hand cursor leave event handler
        /// </summary>
        public event HandCursorEventHandler HandCursorLeave
        {
            add { AddHandler(HandCursorLeaveEvent, value); }
            remove { RemoveHandler(HandCursorLeaveEvent, value); }
        }
        #endregion Event handlers


        public ScrollableCanvas()
        {
            CanvasInsideMove += ScrollableCanvas_CanvasInsideMove;
            HandCursorMove += ScrollableCanvas_HandCursorMove;
            HandCursorLeave += ScrollableCanvas_HandCursorLeave;
        }

        private void ScrollableCanvas_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            _canvasMinHeight = this.ActualHeight * 0.2;
            _canvasMaxHeight = this.ActualHeight * 0.8;
            _lastHandPosition = new Point(args.X, args.Y);

            if (_lastHandPosition.Y > _canvasMinHeight && _lastHandPosition.Y < _canvasMaxHeight)
                return;

            _isMoved = true;
            _downMove = (_lastHandPosition.Y <= _canvasMinHeight) ? false : true;
            RaiseEvent(new HandCursorEventArgs(CanvasInsideMoveEvent, _lastHandPosition));
        }

        private void ScrollableCanvas_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            _isMoved = false;
        }

        private void ScrollableCanvas_CanvasInsideMove(object sender, HandCursorEventArgs args)
        {
            while (_isMoved)
            {
                var d = this.Margin;
                var c = Items[Items.Count - 1];

                _isMoved = !_isMoved;
            }
        }

        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                var childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null)
                    {
                        break;
                    }
                }
                else if (!String.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }

                    // Need this in case the element we want is nested
                    // in another element of the same type
                    foundChild = FindChild<T>(child, childName);
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }
}
