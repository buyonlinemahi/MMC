using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class Note
    {
        [DataMember]
        public int NoteID { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int? RFARequestID { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
    }
}
