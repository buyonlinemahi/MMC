using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedManagedCareCompany
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<ManagedCareCompany> ManagedCareCompanyDetails { get; set; }
    }
}
