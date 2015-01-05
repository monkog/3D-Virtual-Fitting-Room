using KinectFittingRoom.View.Buttons.Events;
using KinectFittingRoom.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace KinectFittingRoom.View.Buttons
{
    /// <summary>
    /// Button class that responds to Kincect events
    /// </summary>
    public class KinectButton : Button
    {
        #region Constants
        /// <summary>
        /// Number of seconds that Click event occures
        /// </summary>
        private const int ClickTimeout = 40;
        /// <summary>
        /// Number of seconds after Click event
        /// </summary>
        private const int AfterClickTimeout = 10;
        #endregion Constants
        #region Private Fields
        /// <summary>
        /// Determines if hand is over button
        /// </summary>
        private bool _handIsOverButton;
        /// <summary>
        /// The last hand position
        /// </summary>
        private Point _lastHandPosition;
        #endregion Private Fields
        #region Events
        /// <summary>
        /// Hand cursor enter event
        /// </summary>
        public static readonly RoutedEvent HandCursorEnterEvent
            = KinectEvents.HandCursorEnterEvent.AddOwner(typeof(KinectButton));
        /// <summary>
        /// Hand cursor move event
        /// </summary>
        public static readonly RoutedEvent HandCursorMoveEvent
            = KinectEvents.HandCursorMoveEvent.AddOwner(typeof(KinectButton));
        /// <summary>
        /// Hand cursor leave event
        /// </summary>
        public static readonly RoutedEvent HandCursorLeaveEvent
            = KinectEvents.HandCursorLeaveEvent.AddOwner(typeof(KinectButton));
        /// <summary>
        /// Hand cursor click event
        /// </summary>
        public static readonly RoutedEvent HandCursorClickEvent
            = KinectEvents.HandCursorClickEvent.AddOwner(typeof(KinectButton));
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
        /// Get information if hand is over button
        /// </summary>
        public bool HandIsOverButton
        {
            get { return _handIsOverButton; }
        }
        /// <summary>
        /// Has Click event occured
        /// </summary>
        public bool IsClicked
        {
            get { return (bool)GetValue(IsClickedProperty); }
            set { SetValue(IsClickedProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command to invoke when this button is pressed.
        /// </summary>
        public new ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        /// <summary>
        /// Number of elapsed ticks for _clickTimer
        /// </summary>
        protected int ClickTicks { get; set; }
        /// <summary>
        /// Number of elapsed ticks for _afterClickTimer
        /// </summary>
        protected int AfterClickTicks { get; set; }
        /// <summary>
        /// Determines how much time elapsed since HandCursorEnterEvent occured
        /// </summary>
        protected DispatcherTimer ClickTimer { get; private set; }
        /// <summary>
        /// Determines how much time elapsed since HandCursorClickEvent occured
        /// </summary>
        protected DispatcherTimer AfterClickTimer { get; private set; }
        #endregion Properties
        #region Dependency Properties
        /// <summary>
        /// IsClicked dependency property
        /// </summary>
        public static readonly DependencyProperty IsClickedProperty = DependencyProperty.Register(
            "IsClicked", typeof(bool), typeof(KinectButton), new PropertyMetadata(default(bool)));
        #endregion Dependency Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="KinectButton"/> class.
        /// </summary>
        public KinectButton()
        {
            SetValue(IsClickedProperty, false);
            _handIsOverButton = false;
            ClickTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            ClickTicks = 0;
            ClickTimer.Tick += ClickTimer_Tick;
            AfterClickTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            AfterClickTicks = 0;
            AfterClickTimer.Tick += AfterClickTimer_Tick;

            HandCursorEnter += KinectButton_HandCursorEnter;
            HandCursorMove += KinectButton_HandCursorMove;
            HandCursorLeave += KinectButton_HandCursorLeave;
            HandCursorClick += KinectButton_HandCursorClick;
        }
        #endregion .ctor
        #region Methods
        /// <summary>
        /// Handles HandCursorEnter event
        /// </summary>
        protected void KinectButton_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            _handIsOverButton = true;
            ClickTimer.Start();
        }
        /// <summary>
        /// Handles HandCursorMove event
        /// </summary>
        protected void KinectButton_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            _lastHandPosition = new Point(args.X, args.Y);
        }
        /// <summary>
        /// Handles HandCursorLeave event
        /// </summary>
        protected void KinectButton_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            _handIsOverButton = false;
            if (IsClicked)
                SetValue(IsClickedProperty, false);
            ResetTimer(ClickTimer);
        }
        /// <summary>
        /// Counts the number of timer ticks of_clickTimer
        /// </summary>
        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            ClickTicks++;

            if (ClickTicks <= ClickTimeout)
                return;

            ResetTimer(ClickTimer);
            RaiseEvent(new HandCursorEventArgs(HandCursorClickEvent, _lastHandPosition));
        }
        /// <summary>
        /// Counts the number of timer ticks of m_afterClickTimer
        /// </summary>
        private void AfterClickTimer_Tick(object sender, EventArgs e)
        {
            AfterClickTicks++;

            if (AfterClickTicks <= AfterClickTimeout)
                return;

            ResetTimer(AfterClickTimer);
            SetValue(IsClickedProperty, false);
        }
        /// <summary>
        /// Imitates the click event
        /// </summary>
        protected virtual void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            SetValue(IsClickedProperty, true);

            if (KinectViewModel.SoundsOn)
                KinectViewModel.ButtonPlayer.Play();

            AfterClickTimer.Start();
        }
        /// <summary>
        /// Resets the timer
        /// </summary>
        protected virtual void ResetTimer(DispatcherTimer timer)
        {
            timer.Stop();
            if (timer == ClickTimer)
                ClickTicks = 0;
            else
                AfterClickTicks = 0;
        }
        #endregion Methods
    }
}
