using System.Windows.Threading;

namespace KinectFittingRoom.View.Buttons
{
    class KinectSizeButton : KinectButton
    {
        #region Protected Methods
        /// <summary>
        /// Resets the timer
        /// </summary>
        protected override void ResetTimer(DispatcherTimer timer)
        {
            timer.Stop();
            if (timer == ClickTimer)
            {
                ClickTicks = 0;
                AfterClickTimer.Start();
            }
            else
            {
                AfterClickTicks = 0;
                ClickTimer.Start();
            }
        }
        #endregion Protected Methods
    }
}
