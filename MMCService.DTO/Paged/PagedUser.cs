using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
   public class PagedUser
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<User> UserDetails { get; set; }
    }
}
