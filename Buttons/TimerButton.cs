using System;
using System.Windows;
using System.Windows.Threading;
using KinectFittingRoom.Events;

namespace KinectFittingRoom.Buttons
{
    public class TimerButton : KinectButton
    {
        #region Constants
        /// <summary>
        /// Number of seconds that need to elapse to invoke Click event
        /// </summary>
        private const int TIMEOUT = 1;
        #endregion Constants

        #region Variables
        /// <summary>
        /// Determines how much time elapsed since HandCursorEnterEvent occured
        /// </summary>
        private DispatcherTimer m_timer;
        /// <summary>
        /// Number of elapsed ticks
        /// </summary>
        private int m_ticks;
        #endregion Variables

        public TimerButton()
        {
            m_timer = new DispatcherTimer();
            m_timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            m_ticks = 0;
            m_timer.Tick += m_timer_Tick;
        }

        #region Methods
        /// <summary>
        /// Counts the number of timer ticks
        /// </summary>
        void m_timer_Tick(object sender, EventArgs e)
        {
            m_ticks++;
        }

        /// <summary>
        /// Handles HandCursorEnter event
        /// </summary>
        protected override void KinectButton_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            m_timer.Start();
        }

        /// <summary>
        /// Handles HandCursorMove event
        /// </summary>
        protected override void KinectButton_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            if (m_wasClicked)
                return;

            ((MainWindow)Application.Current.MainWindow).TimerLabel.Content = m_ticks / 60 + ":" + m_ticks % 60;

            if (m_ticks / 60 >= TIMEOUT)
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
            m_timer.Stop();
            m_ticks = 0;
        }
        #endregion Methods
    }
}
