using System.Windows;
using System.Windows.Controls;
using KinectFittingRoom.Events;

namespace KinectFittingRoom.Buttons
{
    public class KinectButton : Button
    {
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
                HandCursorManager.Create(Application.Current.MainWindow);

            HandCursorEnter += KinectButton_HandCursorEnter;
            HandCursorMove += KinectButton_HandCursorMove;
            HandCursorLeave += KinectButton_HandCursorLeave;
        }

        #region Methods
        /// <summary>
        /// Handles HandCursorEnter event
        /// </summary>
        void KinectButton_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Handles HandCursorMove event
        /// </summary>
        void KinectButton_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Handles HandCursorLeave event
        /// </summary>
        void KinectButton_HandCursorLeave(object sender, HandCursorEventArgs args)
        {
            throw new System.NotImplementedException();
        }
        #endregion Methods
    }
}
