using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
namespace MMC.Core.BLImplementation
{
    public class EmailRecordRepository : IEmailRecordRepository
    {
        private readonly BaseRepository<EmailRecord> _emailRecordRepo;
        private readonly BaseRepository<EmailRecordAttachment> _emailRecordAttachmentRepo;

        public EmailRecordRepository()
        {
            _emailRecordRepo = new BaseRepository<EmailRecord>();
            _emailRecordAttachmentRepo = new BaseRepository<EmailRecordAttachment>();
        }
        #region EmailRecord
        public int addEmailRecord(EmailRecord _emailRecord)
        {
            return _emailRecordRepo.Add(_emailRecord).EmailRecordId;
        }        
        #endregion

        #region EmailRecord Attachement
        public int addEmailRecordAttachment(EmailRecordAttachment _emailRecordAttachment)
        {
            return _emailRecordAttachmentRepo.Add(_emailRecordAttachment).EmailAttachmentId;
        }

        public BLModel.Paged.EmailRecordAttachment getEmailRecordAttachmentByEmailRecordId(int emailRecordId)
        {
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.EmailRecordAttachment
            {
                EmailRecordAttachmentDetails = _spImpl.GetEmailRecordAttachmentByEmailRecordId(emailRecordId) 
            };
        }
        #endregion

        #region EmailRFARequests
        public void AddEmailRecordAndRFARequestLinkByRFAReferralID(int RFAReferralID, int EmailRecordId)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.AddEmailRecordAndRFARequestLinkByRFAReferralID(RFAReferralID, EmailRecordId);
        }
        public void AddEmailRecordAndRFARequestLinkByRFITimeExtension(int RFAReferralID, int RFAReferralFileID, int EmailRecordId)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.AddEmailRecordAndRFARequestLinkByRFITimeExtension(RFAReferralID, RFAReferralFileID, EmailRecordId);
        } 
        #endregion
    }
}
