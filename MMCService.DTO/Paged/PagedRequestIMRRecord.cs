using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    public class PagedRequestIMRRecord
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<RequestIMRRecord> RequestIMRRecordDetails { get; set; }
    }
}
