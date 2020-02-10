using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPatientClaimDiagnose
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<PatientClaimDiagnose> PatientClaimDiagnoseDetails { get; set; }
    }
}
