using System.Collections.Generic;
using System.Runtime.Serialization;


namespace MMCService.DTO.Paged
{
    public class PagedClinicalTriage
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<ClinicalTriage> ClinicalTriages { get; set; }
    }
}

