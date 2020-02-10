using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace MMCService.DTO
{
    [DataContract]
    public class Status
    {
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string StatusName { get; set; }
    }
}
