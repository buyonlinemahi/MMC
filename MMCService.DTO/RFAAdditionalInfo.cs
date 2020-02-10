using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RFAAdditionalInfo
    {
        [DataMember]
        public int RFAAdditionalInfoID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public string RFAAdditionalInfoRecord { get; set; }     
        [DataMember]
        public DateTime? RFAAdditionalInfoRecordDate { get; set; }     
    }
}
