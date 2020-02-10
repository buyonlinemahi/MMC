using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RFARequestTimeExtension
    {
        [DataMember]
        public int RFARequestTimeExtensionID { get; set; }
        [DataMember]
        public int RFARecSpltID { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public string RFIRecords { get; set; }
        [DataMember]
        public string AdditionalExamination { get; set; }
        [DataMember]
        public string SpecialtyConsult { get; set; }
    }
}
