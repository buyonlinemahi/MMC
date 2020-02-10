using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class RFARequestModify
    {
        [DataMember]
        public int RFARequestModifyID { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public string RFARequestedTreatment { get; set; }
        [DataMember]
        public int? RFAFrequency { get; set; }
        [DataMember]
        public int? RFADuration { get; set; }
        [DataMember]
        public int RFADurationTypeID { get; set; }
    }
}
