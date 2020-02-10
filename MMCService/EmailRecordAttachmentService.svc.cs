using AutoMapper;
using MMC.Core.BL;
using MMCService.CustomServiceBehaviors;
using MMCService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MMCService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AutoMapperServiceBehavior]
    public class EmailRecordAttachmentService : IEmailRecordAttachmentService
    {
        private readonly IEmailRecordRepository _emailRecordRepository;
        public EmailRecordAttachmentService(IEmailRecordRepository emailRecordRepository)
        {
            _emailRecordRepository = emailRecordRepository;
        }

        #region EmailRecord
        public int addEmailRecord(EmailRecord _emailRecord)
        {
            return _emailRecordRepository.addEmailRecord(Mapper.Map<MMC.Core.Data.Model.EmailRecord>(_emailRecord));
        }


        #endregion

        #region EmailRecord Attachement
        public int addEmailRecordAttachment(EmailRecordAttachment _emailRecordAttachment)
        {
            return _emailRecordRepository.addEmailRecordAttachment(Mapper.Map<MMC.Core.Data.Model.EmailRecordAttachment>(_emailRecordAttachment));
        }

        public DTO.Paged.PagedEmailRecordAttachment getEmailRecordAttachmentByEmailRecordId(int emailRecordId)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.EmailRecordAttachment, DTO.Paged.PagedEmailRecordAttachment>(_emailRecordRepository.getEmailRecordAttachmentByEmailRecordId(emailRecordId));
        }
        #endregion

        #region EmailRFARequests

        public void AddEmailRecordAndRFARequestLinkByRFAReferralID(int RFAReferralID, int EmailRecordId)
        {
            _emailRecordRepository.AddEmailRecordAndRFARequestLinkByRFAReferralID(RFAReferralID, EmailRecordId);
        }

        public void AddEmailRecordAndRFARequestLinkByRFITimeExtension(int RFAReferralID,int RFAReferralFileID, int EmailRecordId)
        {
            _emailRecordRepository.AddEmailRecordAndRFARequestLinkByRFITimeExtension(RFAReferralID, RFAReferralFileID, EmailRecordId);
        }
      
        #endregion
    }

}
