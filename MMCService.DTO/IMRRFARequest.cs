using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class IMRRFARequest
    {
        [DataMember]
        public int IMRRFARequestID { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public Boolean? Overturn { get; set; }
        [DataMember]
        public int? IMRApprovedUnits { get; set; }
    }
}
