
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedClientBillingRetailRate
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<ClientBillingRetailRate> ClientBillingRetailRateDetails { get; set; }
    }
}
