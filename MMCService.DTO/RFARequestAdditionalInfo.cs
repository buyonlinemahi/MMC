using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class RFARequestAdditionalInfo
    {
        [DataMember]
        public int RFARequestAdditionalInfoID { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public bool? URIncompleteRFAForm { get; set; }
        [DataMember]
        public bool? URNoRFAForm { get; set; }
        [DataMember]
        public bool? URDeclineInternalAppeal { get; set; }
        [DataMember]
        public int? OriginalRFARequestID { get; set; }
    }
}
