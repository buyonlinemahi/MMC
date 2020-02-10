using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    public class PagedClientThirdPartyAdministrator
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<ClientThirdPartyAdministrator> ClientThirdPartyAdministratorDetails { get; set; }
    }
}
