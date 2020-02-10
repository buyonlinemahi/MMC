using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedThirdPartyAdministrator
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<ThirdPartyAdministrator> ThirdPartyAdministratorDetails { get; set; }
    }
}
