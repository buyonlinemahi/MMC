using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;
namespace MMCService
{
    [ServiceContract]
    public interface IEmailRecordAttachmentService
    {
        [OperationContract]
        int addEmailRecord(EmailRecord _emailRecord);
        [OperationContract]
        int addEmailRecordAttachment(EmailRecordAttachment _emailRecordAttachment);
        [OperationContract]
        DTO.Paged.PagedEmailRecordAttachment getEmailRecordAttachmentByEmailRecordId(int emailRecordId);
        [OperationContract]
        void AddEmailRecordAndRFARequestLinkByRFAReferralID(int RFAReferralID, int EmailRecordId);
        [OperationContract]
        void AddEmailRecordAndRFARequestLinkByRFITimeExtension(int RFAReferralID, int RFAReferralFileID, int EmailRecordId);
    }
}
