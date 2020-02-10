using System;

namespace MMCApp.Domain.Models.PatientModel
{
    public class PatientMedicalRecordByPatientID
    {
        public int PatientMedicalRecordID { get; set; }
        public int RFAReferralID { get; set; }
        public string PatMRDocumentName { get; set; }
        public DateTime PatMRDocumentDate { get; set; }
        public int PatientID { get; set; }
        public string PhysicianName { get; set; }
        public string Diagnosis { get; set; }
        public string Summary { get; set; }
        public string PatClaimNumber { get; set; }
        public bool IsChecked { get; set; }
        public string DocumentURL { get; set; }
        public string Author { get; set; }
        public int PatientClaimID { get; set; }
        public string DocumentCategoryName { get; set; }
    }
}
