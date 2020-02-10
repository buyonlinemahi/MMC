using MMC.Core.Data.Model;
using System;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IEmailRecordRepository
    {
        #region EmailRecord
        int addEmailRecord(EmailRecord _emailRecord);
        #endregion
        #region EmailRecord Attachement
        int addEmailRecordAttachment(EmailRecordAttachment _emailRecordAttachment);
        BLModel.Paged.EmailRecordAttachment getEmailRecordAttachmentByEmailRecordId(int emailRecordId);
        #endregion
        #region EmailRFARequests
        void AddEmailRecordAndRFARequestLinkByRFAReferralID(int RFAReferralID, int EmailRecordId);
        void AddEmailRecordAndRFARequestLinkByRFITimeExtension(int RFAReferralID, int RFAReferralFileID, int EmailRecordId);
        #endregion
    }
}
