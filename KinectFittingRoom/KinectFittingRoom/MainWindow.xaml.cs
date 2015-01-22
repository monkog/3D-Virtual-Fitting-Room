using System.Windows;
using System.Windows.Controls;
using KinectFittingRoom.View.Buttons.Events;

namespace KinectFittingRoom
{
    /// <summary>
    /// Kinect Fitting Room main window
    /// </summary>
    public partial class MainWindow
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the position of the left hand.
        /// </summary>
        /// <value>
        /// The position of the left hand.
        /// </value>
        public Point LeftPosition
        {
            get { return (Point)GetValue(LeftPositionProperty); }
            set { SetValue(LeftPositionProperty, value); }
        }
        /// <summary>
        /// Gets or sets the position of the right hand.
        /// </summary>
        /// <value>
        /// The position of the right hand.
        /// </value>
        public Point RightPosition
        {
            get { return (Point)GetValue(RightPositionProperty); }
            set { SetValue(RightPositionProperty, value); }
        }
        #endregion Public Properties
        #region Dependency Properties
        /// <summary>
        /// The right hand position property
        /// </summary>
        public static readonly DependencyProperty RightPositionProperty =
            DependencyProperty.Register("RightPosition", typeof(Point), typeof(MainWindow)
            , new FrameworkPropertyMetadata(new Point(), Hand_PropertyChanged));
        /// <summary>
        /// The left hand position property
        /// </summary>
        public static readonly DependencyProperty LeftPositionProperty =
            DependencyProperty.RegisterAttached("LeftPosition", typeof(Point), typeof(MainWindow)
            , new FrameworkPropertyMetadata(new Point(), Hand_PropertyChanged));
        #endregion Dependency Properties
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += ((sender, e) => ClothesArea.SetTransformMatrix());
        }
        #endregion .ctor
        #region Private Methods
        /// <summary>
        /// Handles the PropertyChanged event of the Hand.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void Hand_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainWindow window = d as MainWindow;
            if (window != null)
                window.HandleHandMoved(window.LeftPosition, window.RightPosition);
        }
        /// <summary>
        /// Handles the hand moved event.
        /// </summary>
        /// <param name="leftHand">The left hand position.</param>
        /// <param name="rightHand">The right hand position.</param>
        private void HandleHandMoved(Point leftHand, Point rightHand)
        {
            HandCursor.Visibility = Visibility.Collapsed;

            var element = (CloseAppGrid.Visibility == Visibility.Visible) ? CloseAppGrid.InputHitTest(leftHand) : ButtonPanelsCanvas.InputHitTest(leftHand);
            var hand = leftHand;

            if (!(element is UIElement))
            {
                element = (CloseAppGrid.Visibility == Visibility.Visible) ? CloseAppGrid.InputHitTest(rightHand) : ButtonPanelsCanvas.InputHitTest(rightHand);
                hand = rightHand;
                if (!(element is UIElement))
                {
                    ButtonsManager.Instance.RaiseCursorLeaveEvent(leftHand);
                    return;
                }
            }

            HandCursor.Visibility = Visibility.Visible;
            Canvas.SetLeft(HandCursor, hand.X - HandCursor.ActualWidth / 2.0);
            Canvas.SetTop(HandCursor, hand.Y - HandCursor.ActualHeight / 2.0);
            ButtonsManager.Instance.RaiseCursorEvents(element, hand);
        }
        #endregion Private Methods
    }
}
