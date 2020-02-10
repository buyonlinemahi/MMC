using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClaimStatus
    {
        [DataMember]
        public int ClaimStatusID { get; set; }
        [DataMember]
        public string ClaimStatusName { get; set; }
    }
}
