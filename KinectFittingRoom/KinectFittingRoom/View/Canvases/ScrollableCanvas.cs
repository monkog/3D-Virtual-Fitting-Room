using KinectFittingRoom.View.Buttons.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace KinectFittingRoom.View.Canvases
{
    /// <summary>
    /// ItemsControl class that responds to Kincect events
    /// </summary>
    public class ScrollableCanvas : ItemsControl
    {
        #region Constants
        /// <summary>
        /// Translation of controls in panels
        /// </summary>
        private const int _distance = 20;
        /// <summary>
        /// Number of seconds of animation
        /// </summary>
        private const int _timeOfAnimation = 3;
        #endregion
        #region Fields
        /// <summary>
        /// The last hand position
        /// </summary>
        private Point _lastHandPosition;
        /// <summary>
        /// Position of last button in panel
        /// </summary>
        double _lastButtonPositionY;
        /// <summary>
        /// Position of first button in panel
        /// </summary>
        double _firstButtonPositionY;
        /// <summary>
        /// Start point of animation
        /// </summary>
        double _startAnimationPoint;
        /// <summary>
        /// Defines if buttons are moving
        /// </summary>
        private bool _isMoved;
        /// <summary>
        /// Top boundary to start scroll up
        /// </summary>
        private double _canvasMinHeight;
        /// <summary>
        /// Bottom boundary to start scroll down
        /// </summary>
        private double _canvasMaxHeight;
        #endregion
        #region Events
        /// <summary>
        /// Hand cursor enter event
        /// </summary>
        public static readonly RoutedEvent CanvasInsideMoveEvent
            = KinectEvents.HandCursorEnterEvent.AddOwner(typeof(ScrollableCanvas));
        /// <summary>
        /// Hand cursor move event
        /// </summary>
        public static readonly RoutedEvent HandCursorMoveEvent
            = KinectEvents.HandCursorMoveEvent.AddOwner(typeof(ScrollableCanvas));
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
        #endregion Event handlers
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollableCanvas"/> class.
        /// </summary>
        public ScrollableCanvas()
        {
            _lastButtonPositionY = 0;
            _firstButtonPositionY = 0;
            _isMoved = false;
            CanvasInsideMove += ScrollableCanvas_CanvasInsideMove;
            HandCursorMove += ScrollableCanvas_HandCursorMove;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Handles HandCursorMove event
        /// </summary>
        private void ScrollableCanvas_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            Point canvas = TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
            _canvasMinHeight = ActualHeight * 0.3 + canvas.Y;
            _canvasMaxHeight = ActualHeight * 0.5;
            if (args.Y > _canvasMinHeight && args.Y < _canvasMaxHeight)
                return;

            RaiseEvent(new HandCursorEventArgs(CanvasInsideMoveEvent, _lastHandPosition));
        }
        /// <summary>
        /// Scroll all buttons in ItemsControl
        /// </summary>
        private void ScrollableCanvas_CanvasInsideMove(object sender, HandCursorEventArgs args)
        {
            StackPanel stackPanel = (Name == "LeftScrollableCanvas") ? FindChild<StackPanel>(Application.Current.MainWindow, "LeftStackPanel") : FindChild<StackPanel>(Application.Current.MainWindow, "RightStackPanel");

            if (_firstButtonPositionY == 0)
                _firstButtonPositionY = stackPanel.Children[0].TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0)).Y;
            if (_lastButtonPositionY == 0)
                _lastButtonPositionY = stackPanel.Children[stackPanel.Children.Count - 1].TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0)).Y;

            if (args.Y != 0)
            {
                if (args.Y > _canvasMaxHeight)
                {
                    _isMoved = !_isMoved;
                    while (_isMoved && _lastButtonPositionY + _startAnimationPoint > _canvasMinHeight)
                    {
                        _startAnimationPoint -= _distance;
                        MoveButtons(stackPanel, _startAnimationPoint, true);
                        _isMoved = !_isMoved;
                    }
                }
                else if (args.Y < _canvasMinHeight)
                {
                    _isMoved = !_isMoved;
                    while (_isMoved && _firstButtonPositionY + _startAnimationPoint < _canvasMaxHeight)
                    {
                        _startAnimationPoint += _distance;
                        MoveButtons(stackPanel, _startAnimationPoint, false);
                        _isMoved = !_isMoved;
                    }
                }
            }
        }
        /// <summary>
        /// Moves buttons in panels
        /// </summary>
        /// <param name="stackpanel"></param>
        /// <param name="startPoint"></param>
        /// <param name="moveUp"></param>
        public void MoveButtons(StackPanel stackpanel, double startPoint, bool moveUp)
        {
            Button button;
            TranslateTransform translation = new TranslateTransform();
            DoubleAnimation animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(_timeOfAnimation),
                From = moveUp ? startPoint + _distance : startPoint,
                To = moveUp ? startPoint : startPoint + _distance
            };
            foreach (var control in stackpanel.Children)
            {
                button = FindChild<Button>(control as ContentPresenter, "");
                if (button != null)
                    button.RenderTransform = translation;
            }
            translation.BeginAnimation(TranslateTransform.YProperty, animation);
        }
        /// <summary>
        /// Find child control in Visual Tree Helper of parent control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">Parent control</param>
        /// <param name="childName">Name of child control</param>
        /// <returns>Found child control</returns>
        public T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
                return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType == null)
                {
                    T foundChild = FindChild<T>(child, childName);
                    if (foundChild != null)
                        return foundChild;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                        return (T)child;
                }
                else
                    return (T)child;
            }
            return null;
        }
        #endregion
    }
}