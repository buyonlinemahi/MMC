using MMCApp.Domain.Models.AdditionalDocumentModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.AdditionalDocumentViewModel
{
    public class AdditionalDocumentViewModel
    {
        public IEnumerable<AdditionalDocument> AdditionalDocumentDetails { get; set; }
        public int TotalCount { get; set; }
        public int PatientID { get; set; }
        public int PatientClaimID { get; set; }
        public int RFAReferralID { get; set; }
        public string PatientFullName { get; set; }
        public string PatClaimNumber { get; set; }
        public string emailPopupName { get; set; }
        public IEnumerable<PreviousLinks> PreviousAttachmentLinks { get; set; }
    }
    public class PreviousLinks
    {
        public string AttachmentLink { get; set; }
    } 
}
