
namespace MMCApp.Domain.Models.NoteModel
{
    public class NoteDetail
    {
        public int NoteID { get; set; }
        public string Notes { get; set; }
        public string ShortNotes { get; set; }
        public int UserID { get; set; }
        public int PatientClaimID { get; set; }
        public int? RFARequestID { get; set; }
        public string NotesDate { get; set; }
        public string NotesTime { get; set; }
        public string RFARequestedTreatment { get; set; }
        public string Users { get; set; }
        public string PatClaimNumber { get; set; }
    }
}
