using System.Runtime.Serialization;

namespace MMCService.DTO
{
    public class ClinicalTriage
    {
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int DecisionID { get; set; }
        [DataMember]
        public string RFARequestedTreatment { get; set; }
        [DataMember]
        public string PatFirstName { get; set; }
        [DataMember]
        public string PatLastName { get; set; }
        [DataMember]
        public string DecisionDesc { get; set; }
        [DataMember]
        public string PhysFirstName { get; set; }
        [DataMember]
        public string PhysLastName { get; set; }
        [DataMember]
        public string PatClaimNumber { get; set; }
        [DataMember]
        public System.DateTime? RFALatestDueDate { get; set; }
    }
}
