using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INotificationService" in both code and config file together.
    [ServiceContract]
    public interface INotificationService
    {
        [OperationContract]
        DTO.Paged.PagedNotification getAllNotifications(int _processlevel, int _skip, int _take);
          [OperationContract]
        IEnumerable<NotificationEmailSend> getAdjandPhyEmailByReferralID(int _referralID);
    }
}
