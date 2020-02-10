using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class Attorney
    {
        [DataMember]
        public int AttorneyID { get; set; }
        [DataMember]
        public int AttorneyFirmID { get; set; }
        [DataMember]
        public string AttorneyName { get; set; }
        [DataMember]
        public string AttPhone { get; set; }
        [DataMember]
        public string AttFax { get; set; }
        [DataMember]
        public string AttEmail { get; set; }
    }
}
