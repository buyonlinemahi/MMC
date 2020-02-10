using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ADR
    {
        [DataMember]
        public int ADRID { get; set; }
        [DataMember]
        public string ADRName { get; set; }
        [DataMember]
        public string ADRAddress { get; set; }
        [DataMember]
        public string ADRAddress2 { get; set; }
        [DataMember]
        public int ADRStateID { get; set; }
        [DataMember]
        public string ADRCity { get; set; }
        [DataMember]
        public string ADRZip { get; set; }
        [DataMember]
        public string StateName { get; set; }
        [DataMember]
        public string ContactName { get; set; }
        [DataMember]
        public string ContactPhone { get; set; }
    }
}
