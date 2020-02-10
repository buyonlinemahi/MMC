using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class MedicalGroup
    {
        [DataMember]
        public int MedicalGroupID { get; set; }
        [DataMember]
        public string MedicalGroupName { get; set; }
        [DataMember]
        public string MGAddress { get; set; }
        [DataMember]
        public string MGAddress2 { get; set; }
        [DataMember]
        public string MGCity { get; set; }
        [DataMember]
        public int MGStateID { get; set; }
        [DataMember]
        public string MGZip { get; set; }
        [DataMember]
        public string MGNote { get; set; }
        [DataMember]
        public string StateName { get; set; }
    }
}
