using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedNotesDetail
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<NotesDetail> NotesDetails { get; set; }
    }
}
