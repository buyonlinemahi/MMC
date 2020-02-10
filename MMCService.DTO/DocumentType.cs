
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class DocumentType
    {
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public string DocumentTypeDesc { get; set; }
        [DataMember]
        public int DocumentCategoryID { get; set; }
    }
}
