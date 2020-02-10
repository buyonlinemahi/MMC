using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPatientClaimPleadBodyPart
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<PatientClaimPleadBodyPart> PatientClaimPleadBodyPartDetails { get; set; }
    }
}
