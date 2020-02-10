using System.Collections.Generic;
using System.Runtime.Serialization;


namespace MMCService.DTO.Paged
{
  public  class PagedInsurer
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<Insurer> InsurerDetails { get; set; }
    }
}
