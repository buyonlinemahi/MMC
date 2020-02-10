using System;
using System.Web;

namespace MMCApp.Domain.Models.PatientModel
{
    public class PatientMedicalRecord
    {
        public int PatientMedicalRecordID { get; set; }
        public int PatientID { get; set; }
        public int DocumentCategoryID { get; set; }
        public int DocumentTypeID { get; set; }
        public string PatMRDocumentName { get; set; }
        public HttpPostedFileBase PatientMedicalRecordFile { get; set; }
        public DateTime PatMRDocumentDate { get; set; }
        public int PatMRPageStart { get; set; }
        public int PatMRPageEnd { get; set; }
        public string PatFileFullPath { get; set; }
        public string PatFileName { get; set; }
        public int PageTotal { get; set; }
        public int PageRemaining { get; set; }
        public string DocumentCategoryName { get; set; }
        public string DocumentTypeDesc { get; set; }
        public string PatMRSummary { get; set; }
    }
}
