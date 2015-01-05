using KinectFittingRoom.View.Buttons.Events;
using KinectFittingRoom.ViewModel;
using System.Windows;
using System.Windows.Threading;

namespace KinectFittingRoom.View.Buttons
{
    /// <summary>
    /// Size button class that responds to Kincect events
    /// </summary>
    class KinectSizeButton : KinectButton
    {
        #region Protected Methods
        /// <summary>
        /// Imitates the click event for KinectSizeButton
        /// </summary>
        protected override void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            if ((Application.Current.MainWindow as MainWindow).HandCursor.Visibility == Visibility.Collapsed)
                return;

            SetValue(IsClickedProperty, true);

            if (KinectViewModel.SoundsOn)
                KinectViewModel.ButtonPlayer.Play();

            AfterClickTimer.Start();
        }
        /// <summary>
        /// Resets the timer
        /// </summary>
        protected override void ResetTimer(DispatcherTimer timer)
        {
            timer.Stop();
            if (timer == ClickTimer)
                ClickTicks = 0;
            else
            {
                AfterClickTicks = 0;
                if (HandIsOverButton)
                    ClickTimer.Start();
            }
        }
        #endregion Protected Methods
    }
}
