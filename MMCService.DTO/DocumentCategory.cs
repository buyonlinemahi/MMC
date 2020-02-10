
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class DocumentCategory
    {
        [DataMember]
        public int DocumentCategoryID { get; set; }
        [DataMember]
        public string DocumentCategoryName { get; set; }

    }
}
