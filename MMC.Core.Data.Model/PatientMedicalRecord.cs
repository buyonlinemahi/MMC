using System;

namespace MMC.Core.Data.Model
{
    public class PatientMedicalRecord
    {
        public int PatientMedicalRecordID { get;set; }
        public int PatientID { get;set; }
        public int DocumentCategoryID { get;set; }
        public int DocumentTypeID { get;set; }
        public string PatMRDocumentName { get;set; }
        public DateTime PatMRDocumentDate { get;set; }
        public int PatMRPageStart { get;set; }
        public int PatMRPageEnd { get; set; }
        public string PatMRSummary { get; set; }
    }
}
