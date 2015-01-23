using Microsoft.VisualStudio.TestTools.UnitTesting;
using KinectFittingRoom.ViewModel;
using Microsoft.Kinect;

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

        [TestMethod]
        public void CheckNoSkeleton()
        {
            Skeleton[] skeletons=new Microsoft.Kinect.Skeleton[2];
            skeletons[0] = new Skeleton();
            skeletons[1] = new Skeleton();
            Skeleton skeleton = KinectService.GetPrimarySkeleton(skeletons);
            Assert.IsNull(skeleton);            
      }

    }
}
