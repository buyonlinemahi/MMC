using System.Collections.Generic;
using System.Runtime.Serialization;
namespace MMCService.DTO.Paged
{
    public class PagedRFADiagnosis
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<RFADiagnosis> RFADiagnosisDetails { get; set; }
    }
}
