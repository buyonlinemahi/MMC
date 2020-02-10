using System;
namespace MMCApp.Domain.Models.IntakeModel
{
    public class RFARecordSpliting
    {
        public int RFARecSpltID { get; set; }
        public int? RFAReferralID { get; set; }
        public int DocumentCategoryID { get; set; }
        public int DocumentTypeID { get; set; }
        public string RFARecDocumentName { get; set; }
        public DateTime RFARecDocumentDate { get; set; }
        public int RFARecPageStart { get; set; }
        public int RFARecPageEnd { get; set; }
        public string RFARecSpltSummary { get; set; }
        public int PatientClaimID { get; set; }
        public int PatientID { get; set; }
        public string AuthorName { get; set; }
        public string RFAFileName { get; set; }
        public string DocumentCategoryName { get; set; }
        public string DocumentTypeDesc { get; set; }
        public string RFAReferralFileName { get; set; }
        public string DocumentUrl { get; set; }
        public string oldRFARecDocumentName { get; set; }
        public int PageTotal { get; set; }
        public int PageRemaining { get; set; }
        public int? UserID { get; set; }
        public DateTime? RFAUploadDate { get; set; }
        
        public int? RFARequestID { get; set; }
    }
}