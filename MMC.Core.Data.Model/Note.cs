using System;

namespace MMC.Core.Data.Model
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
