using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientAndRequestModel
    {
        [DataMember]
        public PatientByReferralID Patients { get; set; }
        [DataMember]
        public IEnumerable<RequestByReferralID> RequestDetail { get; set; }
        [DataMember]
        public int RemainingDecision { get; set; }
        [DataMember]
        public int RequestCount { get; set; }
        [DataMember]
        public int TimeExtensionPNCount { get; set; }
    }
}
