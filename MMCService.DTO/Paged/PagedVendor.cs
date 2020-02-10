using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    public class PagedVendor
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<Vendor> VendorDetails { get; set; }
    }
}
