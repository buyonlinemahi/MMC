using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class AdjusterByClientID
    {
        [DataMember]
        public int AdjusterID { get; set; }
        [DataMember]
        public string AdjusterName { get; set; }
    }
}
