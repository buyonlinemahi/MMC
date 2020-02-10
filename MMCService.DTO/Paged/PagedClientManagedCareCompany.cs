using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    public class PagedClientManagedCareCompany
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<ClientManagedCareCompany> ClientManagedCareCompanyDetails { get; set; }
    }
}
