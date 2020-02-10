using AutoMapper;
using MMC.Core.BL;
using MMCService.CustomServiceBehaviors;
using System.Collections.Generic;
using System.ServiceModel;
using DLModel = MMC.Core.Data.Model;

namespace MMCService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AutoMapperServiceBehavior]
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _iNotificationRepository;

        public NotificationService(INotificationRepository iNotificationRepository)
        {
            _iNotificationRepository = iNotificationRepository;
        }

        public DTO.Paged.PagedNotification getAllNotifications(int _processlevel, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedNotification>(_iNotificationRepository.getAllNotifications(_processlevel, _skip, _take));
        }

        public IEnumerable<DTO.NotificationEmailSend> getAdjandPhyEmailByReferralID(int _referralID)
        {
            return Mapper.Map <IEnumerable<DTO.NotificationEmailSend>>(_iNotificationRepository.getAdjandPhyEmailByReferralID(_referralID));
        }
    }
}
