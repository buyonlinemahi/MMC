using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class NotificationTest
    {
        INotificationRepository _notificationRepository;
        public NotificationTest()
        {
            _notificationRepository = new NotificationRepository();
        }
        [TestMethod]
        public void getAllNotifications()
        {
            var _notificationsByName = _notificationRepository.getAllNotifications(150, 0, 10);
            Assert.IsTrue(_notificationsByName != null, "failed");
        }

        [TestMethod]
        public void getAdjandPhyEmailByReferralID()
        {
            var _notificationsByName = _notificationRepository.getAdjandPhyEmailByReferralID(33);
            Assert.IsTrue(_notificationsByName != null, "failed");
        }
    }
}
