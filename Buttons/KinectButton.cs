using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using KinectFittingRoom.Buttons.Events;
using KinectFittingRoom.Events;

namespace KinectFittingRoom.Buttons
{
    public abstract class KinectButton : Button
    {
        #region Constants
        /// <summary>
        /// Number of seconds that Click event occures
        /// </summary>
        private const int CLICK_TIMEOUT = 20;
        #endregion Constants

        #region Variables
        /// <summary>
        /// Was button clicked
        /// </summary>
        protected bool m_wasClicked;
        /// <summary>
        /// Determines how much time elapsed since Click event occured
        /// </summary>
        private DispatcherTimer m_clickTimer;
        /// <summary>
        /// Number of elapsed ticks for m_clickTimer
        /// </summary>
        private int m_clickTicks;
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
        /// <summary>
        /// Hand cursor click event
        /// </summary>
        public static readonly RoutedEvent HandCursorClickEvent
            = KinectInput.HandCursorClickEvent.AddOwner(typeof(KinectButton));
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

        /// <summary>
        /// Hand cursor click event handler
        /// </summary>
        public event HandCursorEventHandler HandCursorClick
        {
            add { AddHandler(HandCursorClickEvent, value); }
            remove { RemoveHandler(HandCursorClickEvent, value); }
        }
        #endregion Event handlers

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

        public KinectButton()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
                HandCursorManager.Create(((MainWindow)Application.Current.MainWindow).ParentButtonCanvas);

            m_wasClicked = false;
            SetValue(IsClickedProperty, false);

            m_clickTimer = new DispatcherTimer();
            m_clickTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            m_clickTicks = 0;
            m_clickTimer.Tick += m_clickTimer_Tick;

            HandCursorEnter += KinectButton_HandCursorEnter;
            HandCursorMove += KinectButton_HandCursorMove;
            HandCursorLeave += KinectButton_HandCursorLeave;
            HandCursorClick += KinectButton_HandCursorClick;
        }
        
        #region Methods
        /// <summary>
        /// Handles HandCursorEnter event
        /// </summary>
        protected abstract void KinectButton_HandCursorEnter(object sender, HandCursorEventArgs args);

        /// <summary>
        /// Handles HandCursorMove event
        /// </summary>
        protected abstract void KinectButton_HandCursorMove(object sender, HandCursorEventArgs args);

        /// <summary>
        /// Handles HandCursorLeave event
        /// </summary>
        protected virtual void KinectButton_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            m_wasClicked = false;
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
        /// Imitates the click event
        /// </summary>
        protected virtual void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            SetValue(IsClickedProperty, true);
            m_wasClicked = true;
            ((MainWindow)Application.Current.MainWindow).TimerLabel.Content = "Click";
            m_clickTimer.Start();
        }
        #endregion Methods
    }
}
