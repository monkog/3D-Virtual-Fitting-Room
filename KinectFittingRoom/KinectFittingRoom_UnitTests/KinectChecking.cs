using Microsoft.VisualStudio.TestTools.UnitTesting;
using KinectFittingRoom.ViewModel;

namespace KinectFittingRoom_UnitTests
{
    [TestClass]
    public class KinectChecking
    {
        [TestMethod]
        public void CheckNoKinectExceptionHandlingText()
        {
            KinectService s = new KinectService();
            s.Initialize();
            string message = "Proszę podłączyć Kinect";
            Assert.AreEqual(message, s.ErrorGridMessage);
        }
    }
}
