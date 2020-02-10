using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RFAReferral
    {
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public DateTime RFAReferralCreatedDate { get; set; }
        [DataMember]
        public int? PatientClaimID { get; set; }
        [DataMember]
        public DateTime? RFAReferralDate { get; set; }
        [DataMember]
        public DateTime? RFACEDate { get; set; }
        [DataMember]
        public string RFACETime { get; set; }
        [DataMember]
        public DateTime? RFAHCRGDate { get; set; }
        [DataMember]
        public bool RFASignedByPhysician { get; set; }
        [DataMember]
        public bool RFATreatmentRequestClear { get; set; }
        [DataMember]
        public string RFADiscrepancies { get; set; }
        [DataMember]
        public int? PhysicianID { get; set; }
        [DataMember]
        public DateTime? EvaluationDate { get; set; }
        [DataMember]
        public string EvaluatedBy { get; set; }
        [DataMember]
        public string Credentials { get; set; }
        [DataMember]
        public bool? InternalAppeal { get; set; }
        [DataMember]
        public string RFASignature { get; set; }
        [DataMember]
        public string RFASignatureDescription { get; set; }
        [DataMember]
        public string RFAIMRReferenceNumber { get; set; }
        [DataMember]
        public string RFAIMRHistoryRationale { get; set; }

    }
}