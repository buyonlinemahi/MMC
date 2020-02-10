using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientPrivateLabel
    {
        [DataMember]
        public int ClientPrivateLabelID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public string ClientPrivateLabelName { get; set; }
        [DataMember]
        public string ClientPrivateLabelAddress { get; set; }
        [DataMember]
        public string ClientPrivateLabelCity { get; set; }
        [DataMember]
        public int ClientPrivateLabelStateID { get; set; }
        [DataMember]
        public string ClientPrivateLabelZip { get; set; }
        [DataMember]
        public string ClientPrivateLabelLogoName { get; set; }
        [DataMember]
        public string ClientPrivateLabelPhone { get; set; }
        [DataMember]
        public string ClientPrivateLabelFax { get; set; }
        [DataMember]
        public string ClientPrivateLabelTax { get; set; }
        [DataMember]
        public string ClientStatementStart { get; set; }
        [DataMember]
        public string  ClientPrivateEmailID { get; set; }
    }
}
