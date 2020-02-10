using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPatientClaimAddOnBodyPart
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<PatientClaimAddOnBodyPart> PatientClaimAddOnBodyPartDetails { get; set; }
    }
}
