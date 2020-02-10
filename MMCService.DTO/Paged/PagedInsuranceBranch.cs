using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    public class PagedInsuranceBranch
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<InsuranceBranch> InsuranceBranchDetails { get; set; }
    }
}
