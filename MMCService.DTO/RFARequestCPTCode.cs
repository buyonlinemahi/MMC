using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class RFARequestCPTCode
    {
        [DataMember]
        public int RFACPTCodeID { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public string CPT_NDCCode { get; set; }
    }
}