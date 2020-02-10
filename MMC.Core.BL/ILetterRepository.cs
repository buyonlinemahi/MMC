using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface ILetterRepository
    {
        BLModel.InitialNotificationLetter getInitialNotificationLetterDetails(int referralID);
        IEnumerable<BLModel.RequestInitialNotificationLetter> getRequestDetailsInitialNotificationLetter(int referralID);
        IEnumerable<BLModel.RequestInitialNotificationLetter> getCertifiedRequestDetailsInitialNotificationLetter(int referralID);
        IEnumerable<BLModel.RequestInitialNotificationLetter> getDeniedRequestDetailsInitialNotificationLetter(int referralID);        
    }
}
