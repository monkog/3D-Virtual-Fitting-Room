using System;
using System.Windows;
using System.Windows.Threading;
using KinectFittingRoom.UI.Buttons.Events;

namespace KinectFittingRoom.UI.Buttons
{
    public class TimerButton : KinectButton
    {
        #region Constants
        /// <summary>
        /// Number of seconds that need to elapse to invoke Click event
        /// </summary>
        private const int Timeout = 1;
        #endregion Constants
        #region Variables
        /// <summary>
        /// Determines how much time elapsed since HandCursorEnterEvent occured
        /// </summary>
        private DispatcherTimer _timer;
        /// <summary>
        /// Number of elapsed ticks
        /// </summary>
        private int _ticks;
        #endregion Variables
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerButton"/> class.
        /// </summary>
        public TimerButton()
        {
            _timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            _ticks = 0;
            _timer.Tick += m_timer_Tick;
        }
        #endregion .ctor
        #region Methods
        /// <summary>
        /// Counts the number of timer ticks
        /// </summary>
        void m_timer_Tick(object sender, EventArgs e)
        {
            _ticks++;
        }
        /// <summary>
        /// Handles HandCursorEnter event
        /// </summary>
        protected override void KinectButton_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            _timer.Start();
        }
        /// <summary>
        /// Handles HandCursorMove event
        /// </summary>
        protected override void KinectButton_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            if (_wasClicked)
                return;

            ((MainWindow)Application.Current.MainWindow).TimerLabel.Content = _ticks / 60 + ":" + _ticks % 60;

            if (_ticks / 60 >= Timeout)
                RaiseEvent(new HandCursorEventArgs(HandCursorClickEvent, args.X, args.Y, args.Z));
        }
        /// <summary>
        /// Handles HandCursorLeave event
        /// </summary>
        protected override void KinectButton_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            base.KinectButton_HandCursorLeave(sender, args);
            ResetTimer();
        }
        /// <summary>
        /// Resets the timer
        /// </summary>
        private void ResetTimer()
        {
            _timer.Stop();
            _ticks = 0;
        }
        #endregion Methods
    }
}
