using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
     [DataContract]
    public class PagedRFAAdditionalInfo
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<RFAAdditionalInfo> RFAAdditionalInfoDetails { get; set; }
    }
}
