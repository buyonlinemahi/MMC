using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class BodyPartStatus
    {
        [DataMember]
        public int BodyPartStatusID { get; set; }
        [DataMember]
        public string BodyPartStatusName { get; set; }
    }
}
