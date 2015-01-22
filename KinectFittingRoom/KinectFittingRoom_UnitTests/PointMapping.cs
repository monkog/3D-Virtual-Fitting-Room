using System.Windows;
using KinectFittingRoom.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KinectFittingRoom_UnitTests
{
    [TestClass]
    public class PointMapping
    {
        /// <summary>
        /// Gets the 3D point from joint point.
        /// </summary>
        /// <summary>
        /// Tests include:
        /// Point [0,0] being mapped to the [-1,1] point
        /// Middle of the screen being mapped to the [0,0] point
        /// Top right corner being mapped to the [1,1] point
        /// </summary>
        [TestMethod]
        public void Get3DPointFromJointPoint()
        {
            var zeroPoint = new Point(0, 0);
            var midScreenPoint = new Point(SystemParameters.PrimaryScreenWidth / 2, SystemParameters.PrimaryScreenHeight / 2);
            var topRightCorner = new Point(SystemParameters.PrimaryScreenWidth, 0);

            Point zeroPoint3D = KinectService.MapJointPointTo3DSpace(zeroPoint, SystemParameters.PrimaryScreenWidth / 2, SystemParameters.PrimaryScreenWidth / 2);
            var midPoint3D = KinectService.MapJointPointTo3DSpace(midScreenPoint, SystemParameters.PrimaryScreenWidth / 2, SystemParameters.PrimaryScreenHeight / 2);
            var topRightCorner3D = KinectService.MapJointPointTo3DSpace(topRightCorner, SystemParameters.PrimaryScreenWidth / 2, SystemParameters.PrimaryScreenWidth / 2);

            Assert.AreEqual(new Point(-1, 1), zeroPoint3D);
            Assert.AreEqual(new Point(0, 0), midPoint3D);
            Assert.AreEqual(new Point(1, 1), topRightCorner3D);
        }

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
