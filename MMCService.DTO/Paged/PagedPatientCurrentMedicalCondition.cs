using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPatientCurrentMedicalCondition
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<PatientCurrentMedicalCondition> PatientCurrentMedicalConditionDetails { get; set; }
    }
}
