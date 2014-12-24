using System.Windows;
using System.Windows.Media;

namespace KinectFittingRoom.View.Buttons.Events
{
    /// <summary>
    /// Manages Hand Cursor
    /// </summary>
    public class ButtonsManager
    {
        #region Variables
        /// <summary>
        /// Instance of the HandCursorManager
        /// </summary>
        private static ButtonsManager _instance;
        /// <summary>
        /// Prevents from reinitializing the singleton
        /// </summary>
        private static bool _isInitialized;
        /// <summary>
        /// Last element hit by cursor
        /// </summary>
        private IInputElement _lastElement;
        #endregion Variables
        #region Properties
        /// <summary>
        /// Hand cursor manager instance
        /// </summary>
        public static ButtonsManager Instance
        {
            get
            {
                if (!_isInitialized)
                    _instance = Initialize();
                return _instance;
            }
        }
        #endregion Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonsManager"/> class.
        /// </summary>
        private static ButtonsManager Initialize()
        {
            _isInitialized = true;
            return new ButtonsManager();
        }
        #endregion .ctor
        #region Methods
        /// <summary>
        /// Raises the cursor events.
        /// </summary>
        /// <param name="element">The ui element under the cursor.</param>
        /// <param name="cursorPosition">Cursor position.</param>
        public void RaiseCursorEvents(IInputElement element, Point cursorPosition)
        {
            element.RaiseEvent(new HandCursorEventArgs(KinectInput.HandCursorMoveEvent, cursorPosition));
            if (element != _lastElement)
            {
                if (_lastElement != null)
                    _lastElement.RaiseEvent(new HandCursorEventArgs(KinectInput.HandCursorLeaveEvent, cursorPosition));
                element.RaiseEvent(new HandCursorEventArgs(KinectInput.HandCursorEnterEvent, cursorPosition));
            }
            _lastElement = element;
        }

        public void RaiseScreenShotEvent(Visual element1, Visual element2, int width, int height)
        {
            if(element1 != null && element2 != null)
                new ScreenShotEvent(element1, element2, width, height);
        }
        #endregion Methods
    }
}
