using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class BodyPartDetail
    {
        [DataMember]
        public string BodyPartName { get; set; }
        [DataMember]
        public string BodyPartStatusName { get; set; }
        [DataMember]
        public string TableName { get; set; }
         [DataMember]
        public int BodyPartID { get; set; }
    }
}
