using System.Windows;

namespace KinectFittingRoom.View.Helpers
{
    public static class AttachedProperties
    {
        /// <summary>
        /// The left hand property
        /// </summary>
        public static readonly DependencyProperty LeftHandProperty = DependencyProperty.RegisterAttached(
            "LeftHand", typeof(string), typeof(DependencyObject), new PropertyMetadata(null));
        /// <summary>
        /// Gets the left hand.
        /// </summary>
        /// <param name="obj">The LeftHand object.</param>
        /// <returns>Left hand object</returns>
        public static string GetLeftHand(this DependencyObject obj)
        {
            return (string)obj.GetValue(LeftHandProperty);
        }
        /// <summary>
        /// Sets the left hand.
        /// </summary>
        /// <param name="obj">The LeftHand object.</param>
        /// <param name="value">The value to set.</param>
        public static void SetLeftHand(this DependencyObject obj, string value)
        {
            obj.SetValue(LeftHandProperty, value);
        }
    }
}
