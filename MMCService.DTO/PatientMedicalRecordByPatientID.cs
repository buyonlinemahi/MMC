using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientMedicalRecordByPatientID
    {
        [DataMember]
        public int PatientMedicalRecordID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public string PatMRDocumentName { get; set; }
        [DataMember]
        public DateTime PatMRDocumentDate { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public string PhysicianName { get; set; }
        [DataMember]
        public string Diagnosis { get; set; }
        [DataMember]
        public string Summary { get; set; }
        [DataMember]
        public string PatClaimNumber { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public string DocumentCategoryName { get; set; }
    }
}
