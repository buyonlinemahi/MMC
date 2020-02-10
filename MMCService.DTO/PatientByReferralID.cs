using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientByReferralID
    {
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public string PatFirstName { get; set; }
        [DataMember]
        public string PatLastName { get; set; }
        [DataMember]
        public string PatClaimNumber { get; set; }
        [DataMember]
        public DateTime PatDOI { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int ClientBillingRateTypeID { get; set; }
    }
}
