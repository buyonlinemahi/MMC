using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class EmailRecord
    {
        [DataMember]
        public int EmailRecordId { get; set; }
        [DataMember]
        public string EmRecTo { get; set; }
        [DataMember]
        public string EmRecCC { get; set; }
        [DataMember]
        public string EmRecSubject { get; set; }
        [DataMember]
        public string EmRecBody { get; set; }
        [DataMember]
        public DateTime EmailRecDate { get; set; }
        [DataMember]
        public int UserID { get; set; }
    }
}
