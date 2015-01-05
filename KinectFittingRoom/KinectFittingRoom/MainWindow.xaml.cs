using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using KinectFittingRoom.View.Buttons.Events;
using KinectFittingRoom.ViewModel;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace KinectFittingRoom
{
    /// <summary>
    /// Kinect Fitting Room main window
    /// </summary>
    public partial class MainWindow
    {
        #region .ctor
        public MainWindow()
        {
            InitializeComponent();
            SubscribeForHandPositionChanges();
        }
        #endregion .ctor
        #region Private Methods
        /// <summary>
        /// Subscribes for hand position changes.
        /// </summary>
        private void SubscribeForHandPositionChanges()
        {
            var dataContext = DataContext as KinectViewModel;
            Debug.Assert(dataContext != null, "DataContext != null");
            dataContext.KinectService.Hand.PropertyChanged += KinectService_PropertyChanged;
        }
        /// <summary>
        /// Handles the PropertyChanged event of the Hand.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void KinectService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "RightPosition":
                case "LeftPosition":
                    var dataContext = DataContext as KinectViewModel;
                    Debug.Assert(dataContext != null, "DataContext != null");
                    HandleHandMoved(dataContext.KinectService.Hand.LeftPosition, dataContext.KinectService.Hand.RightPosition);
                    break;
            }
        }
        /// <summary>
        /// Handles the hand moved event.
        /// </summary>
        /// <param name="leftHand">The left hand position.</param>
        /// <param name="rightHand">The right hand position.</param>
        private void HandleHandMoved(Point leftHand, Point rightHand)
        {
            HandCursor.Visibility = Visibility.Collapsed;

            var element = ButtonPanelsCanvas.InputHitTest(leftHand);
            var hand = leftHand;
            if (!(element is UIElement))
            {
                element = ButtonPanelsCanvas.InputHitTest(rightHand);
                hand = rightHand;
                if (!(element is UIElement))
                    return;
            }

#warning TODO
            // TODO: Change that to MVVM
            HandCursor.Visibility = Visibility.Visible;
            Canvas.SetLeft(HandCursor, hand.X - HandCursor.ActualWidth / 2.0);
            Canvas.SetTop(HandCursor, hand.Y - HandCursor.ActualHeight / 2.0);
            ButtonsManager.Instance.RaiseCursorEvents(element, hand);
        }
        #endregion Private Methods
    }
}
