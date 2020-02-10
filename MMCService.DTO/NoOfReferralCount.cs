using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class NoOfReferralCount
    {
        [DataMember]
        public int IntakeCount { get; set; }
        [DataMember]
        public int PreparationCount { get; set; }
        [DataMember]
        public int ClinicalTriageCount { get; set; }
        [DataMember]
        public int ClinicalAuditCount { get; set; }
        [DataMember]
        public int NotificationCount { get; set; }
        [DataMember]
        public int BillingCount { get; set; }
        [DataMember]
        public int PeerReviewCount { get; set; }
        [DataMember]
        public int IMRCount { get; set; }
    }
}
