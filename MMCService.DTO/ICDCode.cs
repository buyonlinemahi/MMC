using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ICDCode
    {
        [DataMember]
        public int icdICDNumberID { get; set; }
        [DataMember]
        public string icdICDNumber { get; set; }
        [DataMember]
        public string icdICDTab { get; set; }
        [DataMember]
        public string ICDDescription { get; set; }
        [DataMember]
        public string ICDType { get; set; }
    }
}
