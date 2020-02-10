using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface INotificationRepository
    {
        BLModel.Paged.Notification getAllNotifications(int _processlevel, int _skip, int _take);
        IEnumerable<BLModel.NotificationEmailSend> getAdjandPhyEmailByReferralID(int ReferralID);
    }
}
