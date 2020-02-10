using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class BodyPart
    {
        [DataMember]
        public int BodyPartID { get; set; }
        [DataMember]
        public string BodyPartName { get; set; }
    }
}
