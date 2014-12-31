using System.Windows;

namespace KinectFittingRoom.View.Buttons.Events
{
    /// <summary>
    /// HandCursor event handler type declaration
    /// </summary>
    public delegate void HandCursorEventHandler(object sender, HandCursorEventArgs args);
    /// <summary>
    /// Handles the Kinect input
    /// </summary>
    public static class KinectEvents
    {
        #region  Events
        /// <summary>
        /// Hand cursor enter event
        /// </summary>
        public static readonly RoutedEvent HandCursorEnterEvent
            = EventManager.RegisterRoutedEvent("HandCursorEnter", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));
        /// <summary>
        /// Hand cursor move event
        /// </summary>
        public static readonly RoutedEvent HandCursorMoveEvent
            = EventManager.RegisterRoutedEvent("HandCursorMove", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));
        /// <summary>
        /// Hand cursor leave event
        /// </summary>
        public static readonly RoutedEvent HandCursorLeaveEvent
            = EventManager.RegisterRoutedEvent("HandCursorLeave", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));
        /// <summary>
        /// Hand cursor click event
        /// </summary>
        public static readonly RoutedEvent HandCursorClickEvent
            = EventManager.RegisterRoutedEvent("HandCursorClick", RoutingStrategy.Bubble
            , typeof(HandCursorEventHandler), typeof(KinectEvents));
        /// <summary>
        /// The clear3D items event
        /// </summary>
        public static readonly RoutedEvent Clear3DItemsEvent
            = EventManager.RegisterRoutedEvent("Clear3DItems", RoutingStrategy.Direct
            , typeof(RoutedEventHandler), typeof(KinectEvents));
        #endregion Events
        #region Methods
        /// <summary>
        /// Adds hand cursor enter event handler to the dependency object
        /// </summary>
        /// <param name="dependencyObject">Dependency object</param>
        /// <param name="handler">Event handler</param>
        public static void AddHandCursorEnterHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorEnterEvent, handler);
        }
        /// <summary>
        /// Removes hand cursor enter event handler from the dependency object
        /// </summary>
        /// <param name="dependencyObject">Dependency object</param>
        /// <param name="handler">Event handler</param>
        public static void RemoveHandCursorEnterHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorEnterEvent, handler);
        }
        /// <summary>
        /// Adds hand cursor move event handler to the dependency object
        /// </summary>
        /// <param name="dependencyObject">Dependency object</param>
        /// <param name="handler">Event handler</param>
        public static void AddHandCursorMoveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorMoveEvent, handler);
        }
        /// <summary>
        /// Removes hand cursor move event handler from the dependency object
        /// </summary>
        /// <param name="dependencyObject">Dependency object</param>
        /// <param name="handler">Event handler</param>
        public static void RemoveHandCursorMoveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorMoveEvent, handler);
        }
        /// <summary>
        /// Adds hand cursor leave event handler to the dependency object
        /// </summary>
        /// <param name="dependencyObject">Dependency object</param>
        /// <param name="handler">Event handler</param>
        public static void AddHandCursorLeaveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorLeaveEvent, handler);
        }
        /// <summary>
        /// Removes hand cursor leave event handler from the dependency object
        /// </summary>
        /// <param name="dependencyObject">Dependency object</param>
        /// <param name="handler">Event handler</param>
        public static void RemoveHandCursorLeaveHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorLeaveEvent, handler);
        }
        /// <summary>
        /// Adds hand cursor click event handler to the dependency object
        /// </summary>
        /// <param name="dependencyObject">Dependency object</param>
        /// <param name="handler">Event handler</param>
        public static void AddHandCursorClickHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(HandCursorClickEvent, handler);
        }
        /// <summary>
        /// Removes hand cursor click event handler from the dependency object
        /// </summary>
        /// <param name="dependencyObject">Dependency object</param>
        /// <param name="handler">Event handler</param>
        public static void RemoveHandCursorClickHandler(DependencyObject dependencyObject, HandCursorEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(HandCursorClickEvent, handler);
        }
        /// <summary>
        /// Adds the clear3D items handler.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="handler">The handler.</param>
        public static void AddClear3DItemsHandler(DependencyObject dependencyObject, RoutedEventHandler handler)
        {
            ((UIElement)dependencyObject).AddHandler(Clear3DItemsEvent, handler);
        }
        /// <summary>
        /// Removes the clear3D items handler.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="handler">The handler.</param>
        public static void RemoveClear3DItemsHandler(DependencyObject dependencyObject, RoutedEventHandler handler)
        {
            ((UIElement)dependencyObject).RemoveHandler(Clear3DItemsEvent, handler);
        }
        #endregion Methods
    }
}
