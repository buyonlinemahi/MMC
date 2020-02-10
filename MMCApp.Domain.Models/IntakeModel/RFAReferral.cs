using System;
using System.Web;

namespace MMCApp.Domain.Models.IntakeModel
{
    public class RFAReferral
    {
        public int RFAReferralID { get; set; }
        public DateTime RFAReferralCreatedDate { get; set; }
        public int? PatientClaimID { get; set; }
        public DateTime? RFAReferralDate { get; set; }
        public DateTime? RFACEDate { get; set; }
        public string RFACETime { get; set; }
        public string RFATimeValue { get; set; }
        public DateTime? RFAHCRGDate { get; set; }
        public bool RFASignedByPhysician { get; set; }
        public bool RFATreatmentRequestClear { get; set; }
        public string RFADiscrepancies { get; set; }
        public int? PhysicianID { get; set; }
        public int? PatientID { get; set; }
        public int ProcessLevel { get; set; }
        public DateTime? EvaluationDate { get; set; }
        public string EvaluatedBy { get; set; }
        public string Credentials { get; set; }
        public bool? InternalAppeal { get; set; }
        public string RFASignature { get; set; }
        public string RFASignatureDescription { get; set; }
        public HttpPostedFileBase RFASignatureFile { get; set; }
        public string RFAIMRReferenceNumber { get; set; }
        public string RFAIMRHistoryRationale { get; set; }
    }
}