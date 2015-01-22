using System.Windows;

namespace KinectFittingRoom.View.Helpers
{
    /// <summary>
    /// Enables binding to the readonly properties
    /// </summary>
    public static class SizeObserver
    {
        #region Dependency Properties
        /// <summary>
        /// The observe property
        /// </summary>
        public static readonly DependencyProperty ObserveProperty = DependencyProperty.RegisterAttached(
            "Observe", typeof(bool), typeof(SizeObserver), new FrameworkPropertyMetadata(OnObserveChanged));
        /// <summary>
        /// The observed width property
        /// </summary>
        public static readonly DependencyProperty ObservedWidthProperty = DependencyProperty.RegisterAttached(
            "ObservedWidth", typeof(double), typeof(SizeObserver));
        /// <summary>
        /// The observed height property
        /// </summary>
        public static readonly DependencyProperty ObservedHeightProperty = DependencyProperty.RegisterAttached(
            "ObservedHeight", typeof(double), typeof(SizeObserver));
        #endregion Dependency Properties
        #region Public Static Methods
        /// <summary>
        /// Gets the observe property value.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <returns>Observe property value</returns>
        public static bool GetObserve(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(ObserveProperty);
        }
        /// <summary>
        /// Sets the observe property value.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="observe">if set to <c>true</c> [observe].</param>
        public static void SetObserve(FrameworkElement frameworkElement, bool observe)
        {
            frameworkElement.SetValue(ObserveProperty, observe);
        }
        /// <summary>
        /// Gets the width of the observed framework element.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <returns>Width of the framework element</returns>
        public static double GetObservedWidth(FrameworkElement frameworkElement)
        {
            return (double)frameworkElement.GetValue(ObservedWidthProperty);
        }
        /// <summary>
        /// Sets the width of the observed framework element.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="observedWidth">Width of the observed framework element.</param>
        public static void SetObservedWidth(FrameworkElement frameworkElement, double observedWidth)
        {
            frameworkElement.SetValue(ObservedWidthProperty, observedWidth);
        }
        /// <summary>
        /// Gets the height of the observed framework element.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <returns>Height of the framework element</returns>
        public static double GetObservedHeight(FrameworkElement frameworkElement)
        {
            return (double)frameworkElement.GetValue(ObservedHeightProperty);
        }
        /// <summary>
        /// Sets the height of the observed framework element.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="observedHeight">Height of the observed framework element.</param>
        public static void SetObservedHeight(FrameworkElement frameworkElement, double observedHeight)
        {
            frameworkElement.SetValue(ObservedHeightProperty, observedHeight);
        }
        #endregion Public Static Methods
        #region Private Static Methods
        /// <summary>
        /// Called when [observe changed].
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnObserveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)dependencyObject;

            if ((bool)e.NewValue)
            {
                frameworkElement.SizeChanged += OnFrameworkElementSizeChanged;
                UpdateObservedSizesForFrameworkElement(frameworkElement);
            }
            else
                frameworkElement.SizeChanged -= OnFrameworkElementSizeChanged;
        }
        /// <summary>
        /// Called when [framework element size changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SizeChangedEventArgs"/> instance containing the event data.</param>
        private static void OnFrameworkElementSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateObservedSizesForFrameworkElement((FrameworkElement)sender);
        }
        /// <summary>
        /// Updates the observed sizes for framework element.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        private static void UpdateObservedSizesForFrameworkElement(FrameworkElement frameworkElement)
        {
            frameworkElement.SetCurrentValue(ObservedWidthProperty, frameworkElement.ActualWidth);
            frameworkElement.SetCurrentValue(ObservedHeightProperty, frameworkElement.ActualHeight);
        }
        #endregion Private Static Methods
    }
}
