using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class CaseManager
    {
        [DataMember]
        public int CaseManagerID { get; set; }
        [DataMember]
        public string CMFirstName { get; set; }
        [DataMember]
        public string CMLastName { get; set; }
        [DataMember]
        public string CMAddress1 { get; set; }
        [DataMember]
        public string CMAddress2 { get; set; }
        [DataMember]
        public int CMStateId { get; set; }
        [DataMember]
        public string CMZip { get; set; }
        [DataMember]
        public string CMPhone { get; set; }
        [DataMember]
        public string CMFax { get; set; }
        [DataMember]
        public string CMEmail { get; set; }
        [DataMember]
        public string CMCity { get; set; }
        [DataMember]
        public string CMNotes { get; set; }
        [DataMember]
        public string StateName { get; set; }
    }
}
