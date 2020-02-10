using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    public class PagedMedicalGroup
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<MedicalGroup> MedicalGroupDetails { get; set; }
    }
}
