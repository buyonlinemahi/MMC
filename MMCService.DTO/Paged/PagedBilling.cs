using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedBilling
    {
        [DataMember]
        public IEnumerable<Billing> BillingDetails { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
    }
}
