using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class RequestByReferralID
    {
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int DecisionID { get; set; }
        [DataMember]
        public DateTime RFAHCRGDate { get; set; }
        [DataMember]
        public string RFARequestedTreatment { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public string RFAStatus { get; set; }
        [DataMember]
        public string RFANotes { get; set; }
        [DataMember]
        public string DecisionDesc { get; set; }
        [DataMember]
        public string RFAClinicalReasonforDecision { get; set; }
        [DataMember]
        public string RFAGuidelinesUtilized { get; set; }
        [DataMember]
        public string RFARelevantPortionUtilized { get; set; }
        [DataMember]
        public int? RFAFrequency { get; set; }
        [DataMember]
        public int? RFADuration { get; set; }
        [DataMember]
        public int RFADurationTypeID { get; set; }
        [DataMember]
        public int? RFARequestModifyID { get; set; }
        [DataMember]
        public DateTime? DecisionDate { get; set; }
        [DataMember]
        public DateTime? RFALatestDueDate { get; set; }
        [DataMember]
        public string Decision { get; set; }
    }
}
