using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;


namespace MMC.Core.BLImplementation
{
    public class LetterRepository : ILetterRepository
    {
        public BLModel.InitialNotificationLetter getInitialNotificationLetterDetails(int referralID)
        {
            SPImpl _spImpl = new SPImpl();
            return  _spImpl.getInitialNotificationLetterDetails(referralID);
        }

        public IEnumerable<BLModel.RequestInitialNotificationLetter> getRequestDetailsInitialNotificationLetter(int referralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getRequestDetailsInitialNotificationLetter(referralID);            
        }

        public IEnumerable<BLModel.RequestInitialNotificationLetter> getCertifiedRequestDetailsInitialNotificationLetter(int referralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getCertifiedRequestDetailsInitialNotificationLetter(referralID);
        }

        public IEnumerable<BLModel.RequestInitialNotificationLetter> getDeniedRequestDetailsInitialNotificationLetter(int referralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getDeniedRequestDetailsInitialNotificationLetter(referralID);
        }
    }
}
