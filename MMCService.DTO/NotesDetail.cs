using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class NotesDetail
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
        public string NotesDate { get; set; }
        [DataMember]
        public string NotesTime { get; set; }
        [DataMember]
        public string RFARequestedTreatment { get; set; }
        [DataMember]
        public string Users { get; set; }
        [DataMember]
        public string PatClaimNumber { get; set; }
    }
}
