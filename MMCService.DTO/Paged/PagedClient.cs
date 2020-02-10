using System.Collections.Generic;
using System.Runtime.Serialization;


namespace MMCService.DTO.Paged
{
    public class PagedClient
    {
        [DataMember]
        public int TotalCount { get; set; }    
        [DataMember]
        public IEnumerable<Client> ClientDetails { get; set; }
    }
}
