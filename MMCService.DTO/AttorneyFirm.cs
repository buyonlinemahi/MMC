using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class AttorneyFirm
    {
        [DataMember]
        public int AttorneyFirmID { get; set; }
        [DataMember]
        public string AttorneyFirmName { get; set; }
        [DataMember]
        public int AttorneyFirmTypeID { get; set; }
        [DataMember]
        public string AttAddress1 { get; set; }
        [DataMember]
        public string AttAddress2 { get; set; }
        [DataMember]
        public string AttCity { get; set; }
        [DataMember]
        public int AttStateID { get; set; }
        [DataMember]
        public string AttZip { get; set; }
        [DataMember]
        public string AttPhone { get; set; }
        [DataMember]
        public string AttFax { get; set; }
        [DataMember]
        public string AttStateName { get; set; }
        [DataMember]
        public string AttorneyFirmType { get; set; }
       
    }
}
