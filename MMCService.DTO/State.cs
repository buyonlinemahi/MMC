using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class State
    {
        [DataMember]
        public int StateId { get; set; }
        [DataMember]
        public string StateName { get; set; }
    }
}
