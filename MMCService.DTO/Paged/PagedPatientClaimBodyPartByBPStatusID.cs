using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPatientClaimBodyPartByBPStatusID
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<PatientClaimBodyPartByBPStatusID> PatientClaimBodyPartByBPStatusIDDetails { get; set; }
    }
}
