using System.Windows;
using KinectFittingRoom.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KinectFittingRoom_UnitTests
{
    [TestClass]
    public class PointMapping
    {
        [TestMethod]
        public void GetDistanceBetweenJoinPoints()
        {
            var leftJoint = new Point(0, 0);
            var rightJoint = new Point(SystemParameters.PrimaryScreenWidth, 0);

            var distanceFull = KinectService.CalculateDistanceBetweenJoints(rightJoint, leftJoint);
            var distanceFull2 = KinectService.CalculateDistanceBetweenJoints(rightJoint, leftJoint);
            var distanceZero = KinectService.CalculateDistanceBetweenJoints(leftJoint, leftJoint);

            Assert.AreNotEqual(new Point(0, 0), distanceFull);
            Assert.AreNotEqual(new Point(0, 0), distanceFull2);
            Assert.AreEqual(distanceFull2, distanceFull);
            Assert.AreEqual(new Point(0, 0), distanceZero);
        }
    }
}
