using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ICD9Code
    {
        [DataMember]
        public int icdICD9NumberID { get; set; }
        [DataMember]
        public string icdICD9Number { get; set; }
        [DataMember]
        public string ICD9Description { get; set; }
        [DataMember]
        public string ICD9Type { get; set; }
    }
}
