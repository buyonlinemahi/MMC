using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPhysician
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<Physician> PhysicianDetails { get; set; }
    }
}
