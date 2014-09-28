using System.Dynamic;
using System.Windows;

namespace KinectFittingRoom.Events
{
    public class HandCursorManager
    {
        #region Variables
        /// <summary>
        /// Window
        /// </summary>
        private readonly Window m_window;
        /// <summary>
        /// Last element hit by cursor
        /// </summary>
        private UIElement m_lastElement;
        /// <summary>
        /// Prevents from reinitializing the singleton
        /// </summary>
        private static bool m_isInitialized;
        #endregion Variables

        #region Properties
        /// <summary>
        /// Hand cursor manager instance
        /// </summary>
        public static HandCursorManager Instance { get; private set; }
        #endregion Properties

        private HandCursorManager(Window window)
        {
            m_window = window;
        }

        #region Methods
        /// <summary>
        /// Creates the instance of HandCursorManager
        /// </summary>
        /// <param name="window">Window</param>
        public static void Create(Window window)
        {
            if (!m_isInitialized)
            {
                Instance = new HandCursorManager(window);
                m_isInitialized = true;
            }
        }

        /// <summary>
        /// Handles hand cursor events
        /// </summary>
        /// <param name="point">Cursor position</param>
        /// <param name="z">Depth</param>
        public void HandleHandCursorEvents(Point point, double z)
        {
            UIElement element = GetElementAtPoint(point, m_window);

            if (element != null)
            {
                element.RaiseEvent(new HandCursorEventArgs(KinectInput.HandCursorMoveEvent, point, z));
                if (element != m_lastElement)
                {
                    if (m_lastElement != null)
                        m_lastElement.RaiseEvent(new HandCursorEventArgs(KinectInput.HandCursorLeaveEvent, point, z));
                    element.RaiseEvent(new HandCursorEventArgs(KinectInput.HandCursorEnterEvent, point, z));
                }
            }
            m_lastElement = element;
        }

        /// <summary>
        /// Searches for the element in window at certain point
        /// </summary>
        /// <param name="point">Point</param>
        /// <param name="window">Window</param>
        /// <returns>Element in window at point</returns>
        private static UIElement GetElementAtPoint(Point point, Window window)
        {
            if (!window.IsVisible)
                return null;

            Point windowPoint = window.PointFromScreen(point);
            IInputElement element = window.InputHitTest(windowPoint);

            if (element is UIElement)
                return (UIElement)element;
            return null;
        }
        #endregion Methods
    }
}
