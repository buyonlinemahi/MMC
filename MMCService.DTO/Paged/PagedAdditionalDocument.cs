using System.Collections.Generic;
using System.Runtime.Serialization;


namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedAdditionalDocument
    {

        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<AdditionalDocument> AdditionalDocumentDetails { get; set; }
    }
}
