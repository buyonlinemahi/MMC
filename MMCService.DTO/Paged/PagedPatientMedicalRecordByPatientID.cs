using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPatientMedicalRecordByPatientID
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<PatientMedicalRecordByPatientID> PatientMedicalRecordByPatientIDDetails { get; set; }
    }
}
