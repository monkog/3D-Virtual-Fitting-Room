using System.Windows;
using System.Windows.Controls;
using KinectFittingRoom.Events;

namespace KinectFittingRoom.Buttons.Events
{
    public class HandCursorManager
    {
        #region Variables
        /// <summary>
        /// Parent canvas for all buttons
        /// </summary>
        private readonly Canvas m_canvas;
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

        private HandCursorManager(Canvas canvas)
        {
            m_canvas = canvas;
        }

        #region Methods
        /// <summary>
        /// Creates the instance of HandCursorManager
        /// </summary>
        /// <param name="canvas">Parent canvas</param>
        public static void Create(Canvas canvas)
        {
            if (!m_isInitialized)
            {
                Instance = new HandCursorManager(canvas);
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
            UIElement element = GetElementAtPoint(point, m_canvas);

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
        /// Searches for the element in canvas at certain point
        /// </summary>
        /// <param name="point">Point</param>
        /// <param name="canvas">Canvas</param>
        /// <returns>Element in canvas at point</returns>
        private static UIElement GetElementAtPoint(Point point, Canvas canvas)
        {
            if (!canvas.IsVisible)
                return null;

            Point canvasPoint = canvas.PointFromScreen(point);
            IInputElement element = canvas.InputHitTest(canvasPoint);

            if (element is UIElement)
                return (UIElement)element;
            return null;
        }
        #endregion Methods
    }
}
