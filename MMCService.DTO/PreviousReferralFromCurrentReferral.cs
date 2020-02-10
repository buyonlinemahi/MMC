using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class PreviousReferralFromCurrentReferral
    {
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int DecisionID { get; set; }
    }
}
