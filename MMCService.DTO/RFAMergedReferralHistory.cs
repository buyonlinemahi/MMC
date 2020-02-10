using System.Runtime.Serialization;
using System;

namespace MMCService.DTO
{
    [DataContract]
    public class RFAMergedReferralHistory
    {
        [DataMember]
        public int RFAMergedReferralID { get; set; }
        [DataMember]
        public int RFAOldReferralID { get; set; }
        [DataMember]
        public int RFANewReferralID { get; set; }
        [DataMember]
        public DateTime RFAMergedReferralDate { get; set; }
    }
}
