using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPatientMedicalRecord
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<PatientMedicalRecordByPatientID> PatientMedicalRecordByPatientIDDetails { get; set; }
    }
}
