using System;

namespace MMCApp.Domain.Models.NoteModel
{
   public class Note
    {
        public int NoteID { get; set; }
        public string Notes { get; set; }
        public int UserID { get; set; }
        public int PatientClaimID { get; set; }
        public int? RFARequestID { get; set; }
        public DateTime Date { get; set; }
    }
}
