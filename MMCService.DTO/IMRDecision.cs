using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class IMRDecision
    {
        [DataMember]
        public int IMRDecisionID { get; set; }
        [DataMember]
        public string IMRDecisionDesc { get; set; }
    }
}
