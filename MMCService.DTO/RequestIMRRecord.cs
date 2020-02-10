using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RequestIMRRecord
    {
        [DataMember]
        public int RequestIMRID { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public int OriginalRFAReferralID { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedOn { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public string PatFirstName { get; set; }
        [DataMember]
        public string PatLastName { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public string RFARequestedTreatment { get; set; }
        [DataMember]
        public string DecisionDesc { get; set; }
        [DataMember]
        public string RFAStatus { get; set; }
        [DataMember]
        public int ProcessLevel { get; set; }
    }
}
