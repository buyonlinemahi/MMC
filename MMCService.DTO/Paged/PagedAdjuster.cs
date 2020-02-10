using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedAdjuster
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<Adjuster> AdjusterDetails { get; set; }
    }
}
