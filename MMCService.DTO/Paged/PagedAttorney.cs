using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedAttorney
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<Attorney> AttorneyDetails { get; set; }
    }
}
