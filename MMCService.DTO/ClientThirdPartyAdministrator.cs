using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientThirdPartyAdministrator
    {
        [DataMember]
        public int ClientTPAID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int TPAID { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string TPAName { get; set; }
        [DataMember]
        public string TPAAddress { get; set; }
        [DataMember]
        public string TPACity { get; set; }
        [DataMember]
        public string TPAZip { get; set; }
        [DataMember]
        public string TPAState { get; set; }
        [DataMember]
        public string TPAType { get; set; }

    }
}
