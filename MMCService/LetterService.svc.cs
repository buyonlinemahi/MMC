using System.Collections.Generic;
using System.Linq;
using MMCService.DTO;
using AutoMapper;
using MMC.Core.BL;
using MMCService.CustomServiceBehaviors;
using System.ServiceModel;

namespace MMCService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AutoMapperServiceBehavior]
    public class LetterService : ILetterService
    {
        private readonly ILetterRepository _iLetterRepository;
        public LetterService(ILetterRepository iLetterRepository)
        {
            _iLetterRepository = iLetterRepository;
        }
        public InitialNotificationLetter getInitialNotificationLetterDetails(int referralID)
        {
            return Mapper.Map<InitialNotificationLetter>(_iLetterRepository.getInitialNotificationLetterDetails(referralID));
        }

        public IEnumerable<RequestInitialNotificationLetter> getRequestDetailsInitialNotificationLetter(int referralID)
        {
            return Mapper.Map<IEnumerable<RequestInitialNotificationLetter>>(_iLetterRepository.getRequestDetailsInitialNotificationLetter(referralID));
        }

        public IEnumerable<RequestInitialNotificationLetter> getCertifiedRequestDetailsInitialNotificationLetter(int referralID)
        {
            return Mapper.Map<IEnumerable<RequestInitialNotificationLetter>>(_iLetterRepository.getCertifiedRequestDetailsInitialNotificationLetter(referralID));
        }

        public IEnumerable<RequestInitialNotificationLetter> getDeniedRequestDetailsInitialNotificationLetter(int referralID)
        {
            return Mapper.Map<IEnumerable<RequestInitialNotificationLetter>>(_iLetterRepository.getDeniedRequestDetailsInitialNotificationLetter(referralID));
        }
    }
}
