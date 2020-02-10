using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPeerReview
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<PeerReview> PeerReviewDetails { get; set; }
    }

}
