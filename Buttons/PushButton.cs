using System.Windows;
using KinectFittingRoom.Events;

namespace KinectFittingRoom.Buttons
{
    class PushButton : KinectButton
    {
        #region Constants
        /// <summary>
        /// Tolerance of the depth difference
        /// </summary>
        private const double DEPTH_TOLERANCE = 100;
        #endregion Constants

        #region Variables
        /// <summary>
        /// Depth of the hand entering the button
        /// </summary>
        private double m_initialDepth;
        #endregion Variables

        #region Properties
        /// <summary>
        /// Depth of the hand
        /// </summary>
        public double HandDepth
        {
            get { return (double)GetValue(HandDepthDependencyProperty); }
            set { SetValue(HandDepthDependencyProperty, value); }
        }
        #endregion Properties

        #region Dependency Properties
        /// <summary>
        /// HandDepth Dependency Property
        /// </summary>
        public static readonly DependencyProperty HandDepthDependencyProperty = DependencyProperty.Register(
            "HandDepth", typeof(double), typeof(PushButton), new PropertyMetadata(default(double)));
        #endregion Dependency Properties

        #region Methods
        /// <summary>
        /// Sets the initial depth of the cursor
        /// </summary>
        protected override void KinectButton_HandCursorEnter(object sender, HandCursorEventArgs args)
        {
            m_initialDepth = args.Z;
        }

        /// <summary>
        /// Raises HandCursorClickEvent when a push gesture is detected
        /// </summary>
        protected override void KinectButton_HandCursorMove(object sender, HandCursorEventArgs args)
        {
            if (args.Z < m_initialDepth - DEPTH_TOLERANCE)
                RaiseEvent(new HandCursorEventArgs(HandCursorClickEvent, new Point(args.X, args.Y), args.Z));
        }
        #endregion Methods
    }
}
