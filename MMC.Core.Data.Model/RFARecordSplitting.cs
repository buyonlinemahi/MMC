using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class RFARecordSplitting
    {
        [Key]
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
        public string AuthorName { get; set; }
        public string RFAFileName { get; set; }
        public int? UserID { get; set; }
        public int? RFARequestID { get; set; }

        public DateTime? RFAUploadDate { get; set; }
        [ForeignKey("RFAReferralID")]
        public RFAReferral _rfaReferral { get; set; }
        [ForeignKey("PatientClaimID")]
        public PatientClaim _patientClaim { get; set; }
        [ForeignKey("DocumentCategoryID")]
        public DocumentCategory _documentCategory { get; set; }
        [ForeignKey("DocumentTypeID")]
        public DocumentType _documentType { get; set; }
        [ForeignKey("UserID")]
        public User _user { get; set; }
        [ForeignKey("RFARequestID")]
        public RFARequest _rFARequest { get; set; }
    }
}
 