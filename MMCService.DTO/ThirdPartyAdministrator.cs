using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ThirdPartyAdministrator
    {
        [DataMember]
        public int TPAID { get; set; }
        [DataMember]
        public string TPAName { get; set; }
        [DataMember]
        public string TPAAddress { get; set; }
        [DataMember]
        public string TPAAddress2 { get; set; }
        [DataMember]
        public string TPACity { get; set; }
        [DataMember]
        public int TPAStateId { get; set; }
        [DataMember]
        public string TPAZip { get; set; }
        [DataMember]
        public string StateName { get; set; }
        [DataMember]
        public string TPAPhone { get; set; }
        [DataMember]
        public string TPAFax { get; set; }
    }
}
