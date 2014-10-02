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
        /// <summary>
        /// Number of seconds that Click event occures
        /// </summary>
        private const int CLICK_TIMEOUT = 20;
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
        /// <summary>
        /// Determines how much time elapsed since Click event occured
        /// </summary>
        private DispatcherTimer m_clickTimer;
        /// <summary>
        /// Number of elapsed ticks for m_clickTimer
        /// </summary>
        private int m_clickTicks;
        /// <summary>
        /// Was button clicked
        /// </summary>
        private bool m_wasClicked;
        #endregion Variables

        #region Properties
        /// <summary>
        /// Has Click event occured
        /// </summary>
        public bool IsClicked
        {
            get { return (bool)GetValue(IsClickedProperty); }
            set { SetValue(IsClickedProperty, value); }
        }
        #endregion Properties

        #region Dependrncy Properties
        /// <summary>
        /// IsClicked dependency property
        /// </summary>
        public static readonly DependencyProperty IsClickedProperty = DependencyProperty.Register(
            "IsClicked", typeof(bool), typeof(KinectButton), new PropertyMetadata(default(bool)));
        #endregion Dependency Properties

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

            SetValue(IsClickedProperty, false);
            m_wasClicked = false;

            m_timer = new DispatcherTimer();
            m_timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            m_ticks = 0;
            m_timer.Tick += m_timer_Tick;

            m_clickTimer = new DispatcherTimer();
            m_clickTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            m_clickTicks = 0;
            m_clickTimer.Tick += m_clickTimer_Tick;

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
        /// Counts the number of timer ticks of m_clickTimer
        /// </summary>
        private void m_clickTimer_Tick(object sender, EventArgs e)
        {
            m_clickTicks++;

            if (m_clickTicks > CLICK_TIMEOUT)
            {
                m_clickTimer.Stop();
                m_clickTicks = 0;
                SetValue(IsClickedProperty, false);
            }
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
            SetValue(IsClickedProperty, true);
            m_wasClicked = true;
            ResetTimer();
            ((MainWindow)Application.Current.MainWindow).TimerLabel.Content = "Click";

            m_clickTimer.Start();
        }
        #endregion Methods
    }
}
