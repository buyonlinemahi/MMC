using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedThirdPartyAdministratorBranch
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<ThirdPartyAdministratorBranch> ThirdPartyAdministratorBranchDetails { get; set; }
    }
}
