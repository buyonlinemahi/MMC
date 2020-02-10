using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientInsurer
    {
        [DataMember]
        public int ClientInsurerID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int InsurerID { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string InsName { get; set; }
        [DataMember]
        public string InsAddress1 { get; set; }
        [DataMember]
        public string InsCity { get; set; }
        [DataMember]
        public string InsState { get; set; }
        [DataMember]
        public string InsZip { get; set; }
        [DataMember]
        public string InsType { get; set; }
    }
}
