using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILetterService" in both code and config file together.
    [ServiceContract]
    public interface ILetterService
    {
        [OperationContract]
        InitialNotificationLetter getInitialNotificationLetterDetails(int referralID);

        [OperationContract]
        IEnumerable<RequestInitialNotificationLetter> getRequestDetailsInitialNotificationLetter(int referralID);

        [OperationContract]
        IEnumerable<RequestInitialNotificationLetter> getCertifiedRequestDetailsInitialNotificationLetter(int referralID);

        [OperationContract]
        IEnumerable<RequestInitialNotificationLetter> getDeniedRequestDetailsInitialNotificationLetter(int referralID);
    }
}
