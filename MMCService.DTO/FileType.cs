using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class FileType
    {
        [DataMember]
        public int FileTypeID { get; set; }
        [DataMember]
        public string FileTypeName { get; set; }
    }
}
