using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
namespace MMCService.DTO
{
    [DataContract]
    public class PatientMedicalRecord
    {
        [DataMember]
        public int PatientMedicalRecordID { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public int DocumentCategoryID { get; set; }
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public string PatMRDocumentName { get; set; }
        [DataMember]
        public DateTime PatMRDocumentDate { get; set; }
        [DataMember]
        public int PatMRPageStart { get; set; }
        [DataMember]
        public int PatMRPageEnd { get; set; }
        [DataMember]
        public string PatMRSummary { get; set; }
    }
}
