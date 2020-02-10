using System.Collections.Generic;
using System.Runtime.Serialization;
namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedICDCode
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<ICDCode> ICDCodeDetails { get; set; }
    }
}
