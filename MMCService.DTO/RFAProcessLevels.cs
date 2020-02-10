using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RFAProcessLevels
    {
        [DataMember]
        public int RFAProcessLevelID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int ProcessLevel { get; set; }
    }
}
