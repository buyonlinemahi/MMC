using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class RFAReferral
    {
        [Key]
        public int RFAReferralID { get; set; }
        public DateTime RFAReferralCreatedDate { get; set; }
        public int? PatientClaimID { get; set; }
        public DateTime? RFAReferralDate { get; set; }
        public DateTime? RFACEDate { get; set; }
        public string RFACETime { get; set; }
        public DateTime? RFAHCRGDate { get; set; }
        public bool RFASignedByPhysician { get; set; }
        public bool RFATreatmentRequestClear { get; set; }
        public string RFADiscrepancies { get; set; }
        public int? PhysicianID { get; set; }
        public DateTime? EvaluationDate { get; set; }
        public string EvaluatedBy { get; set; }
        public string Credentials { get; set; }
        public bool? InternalAppeal { get; set; }
        public string RFASignature { get; set; }
        public string RFASignatureDescription { get; set; }
        public string RFAIMRReferenceNumber { get; set; }
        public string RFAIMRHistoryRationale { get; set; }

        //public string RFANotes { get; set; }
        [ForeignKey("PatientClaimID")]
        public PatientClaim _patientClaim { get; set; }
        [ForeignKey("PhysicianID")]
        public Physician _physician { get; set; }
    }
}