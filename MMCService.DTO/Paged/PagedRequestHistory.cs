using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
  public  class PagedRequestHistory
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<RequestHistory> RequestHistoryDetails { get; set; }
    }
}
