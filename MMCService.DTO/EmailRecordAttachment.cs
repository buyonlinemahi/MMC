using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class EmailRecordAttachment
    {
        [DataMember]
        public int EmailAttachmentId { get; set; }
        [DataMember]
        public int EmailRecordId { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string DocumentPath { get; set; }
        [DataMember]
        public string EmRecTo { get; set; }
        [DataMember]
        public string EmRecCC { get; set; }
        [DataMember]
        public string EmRecSubject { get; set; }
        [DataMember]
        public string EmRecBody { get; set; }
    }
}
