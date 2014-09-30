using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using KinectFittingRoom.Buttons.Events;
using KinectFittingRoom.Events;

namespace KinectFittingRoom.Buttons
{
    public class KinectButton : Button
    {
        #region Constants
        /// <summary>
        /// Number of seconds that need to elapse to invoke Click event
        /// </summary>
        private const int TIMEOUT = 1;
        #endregion Constants

        #region Variables
        /// <summary>
        /// Determines how much time elapsed since HandCursorEvent occured
        /// </summary>
        private DispatcherTimer m_timer;
        /// <summary>
        /// Number of elapsed ticks
        /// </summary>
        private int m_ticks;
        /// <summary>
        /// Was button clicked
        /// </summary>
        private bool m_wasClicked;
        #endregion Variables

        #region Events
        /// <summary>
        /// Hand cursor enter event
        /// </summary>
        public static readonly RoutedEvent HandCursorEnterEvent
            = KinectInput.HandCursorEnterEvent.AddOwner(typeof(KinectButton));
        /// <summary>
        /// Hand cursor move event
        /// </summary>
        public static readonly RoutedEvent HandCursorMoveEvent
            = KinectInput.HandCursorMoveEvent.AddOwner(typeof(KinectButton));
        /// <summary>
        /// Hand cursor leave event
        /// </summary>
        public static readonly RoutedEvent HandCursorLeaveEvent
            = KinectInput.HandCursorLeaveEvent.AddOwner(typeof(KinectButton));
        #endregion Events

        #region Event handlers
        /// <summary>
        /// Hand cursor enter event handler
        /// </summary>
        public event HandCursorEventHandler HandCursorEnter
        {
            add { AddHandler(HandCursorEnterEvent, value); }
            remove { RemoveHandler(HandCursorEnterEvent, value); }
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

        public KinectButton()
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                HandCursorManager.Create(((MainWindow)Application.Current.MainWindow).ParentButtonCanvas);

            m_wasClicked = false;

            m_timer = new DispatcherTimer();
            m_timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            m_ticks = 0;
            m_timer.Tick += m_timer_Tick;

            HandCursorEnter += KinectButton_HandCursorEnter;
            HandCursorMove += KinectButton_HandCursorMove;
            HandCursorLeave += KinectButton_HandCursorLeave;
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
        void KinectButton_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            m_timer.Start();
        }

        /// <summary>
        /// Handles HandCursorMove event
        /// </summary>
        void KinectButton_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            if (m_wasClicked)
                return;

            ((MainWindow)Application.Current.MainWindow).TimerLabel.Content = m_ticks / 60 + ":" + m_ticks % 60;

            if (m_ticks / 60 >= TIMEOUT)
                KinectButtonClick();
        }

        /// <summary>
        /// Handles HandCursorLeave event
        /// </summary>
        void KinectButton_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            ResetTimer();
            m_wasClicked = false;
        }

        /// <summary>
        /// Resets the timer
        /// </summary>
        private void ResetTimer()
        {
            m_timer.Stop();
            m_ticks = 0;
        }

        /// <summary>
        /// Imitates the click event
        /// </summary>
        private void KinectButtonClick()
        {
            m_wasClicked = true;
            ResetTimer();
            ((MainWindow)Application.Current.MainWindow).TimerLabel.Content = "Click";
        }
        #endregion Methods
    }
}
