using System.Windows;
using KinectFittingRoom.ViewModel;

namespace KinectFittingRoom
{
    /// <summary>
    /// Application class
    /// </summary>
    public partial class App
    {
        App()
        {
            //System.Threading.Thread.Sleep(2000);
        }

        #region Protected Methods
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Exit" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            KinectViewModelLoader.Cleanup();
            base.OnExit(e);
        }
        #endregion Protected Methods
    }
}
