using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class RFARequest
    {
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public int? RFAReferralID { get; set; }
        [DataMember]
        public bool? RFAExpediteReferral { get; set; }
        [DataMember]
        public string RFARequestedTreatment { get; set; }
        [DataMember]
        public int? RFADurationTypeID { get; set; }
        [DataMember]
        public int? RFAFrequency { get; set; }
        [DataMember]
        public int? RequestTypeID { get; set; }
        [DataMember]
        public int? RFADuration { get; set; }
        [DataMember]
        public int? RFAQuantity { get; set; }
        [DataMember]
        public int? TreatmentCategoryID { get; set; }
        [DataMember]
        public int? TreatmentTypeID { get; set; }
        [DataMember]
        public string RFAStrenght { get; set; }
        [DataMember]
        public string RFACPT_NDC { get; set; }
        [DataMember]
        public string RFAStatus { get; set; }
        [DataMember]
        public int? DecisionID { get; set; }
        [DataMember]
        public string RFANotes { get; set; }
        [DataMember]
        public string RFAClinicalReasonforDecision { get; set; }
        [DataMember]
        public string RFAGuidelinesUtilized { get; set; }
        [DataMember]
        public string RFARelevantPortionUtilized { get; set; }
        [DataMember]
        public DateTime? RFARequestDate { get; set; }
        [DataMember]
        public DateTime? DecisionDate { get; set; }
        [DataMember]
        public string RFAReqCertificationNumber { get; set; }
        [DataMember]
        public DateTime? RFALatestDueDate { get; set; }
    }
}