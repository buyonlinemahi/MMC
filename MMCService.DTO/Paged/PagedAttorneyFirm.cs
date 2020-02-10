using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedAttorneyFirm
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<AttorneyFirm> AttorneyFirmDetails { get; set; }
    }
}
