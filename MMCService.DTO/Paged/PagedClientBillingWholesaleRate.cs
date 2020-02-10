using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedClientBillingWholesaleRate
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<ClientBillingWholesaleRate> ClientBillingWholesaleRateDetails { get; set; }
    }
}
