using System.Collections.Generic;
using System.Runtime.Serialization;
namespace MMCService.DTO.Paged
{
    public class PagedRequestByReferralID
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<RequestByReferralID> RequestDetails { get; set; }
    }
}
