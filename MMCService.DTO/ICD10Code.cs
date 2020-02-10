using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace MMCService.DTO
{
    [DataContract]
    public class ICD10Code
    {
        [DataMember]
        public int icdICD10NumberID { get; set; }
        [DataMember]
        public string icdICD10Number { get; set; }
        [DataMember]
        public string ICD10Description { get; set; }
        [DataMember]
        public string ICD10Type { get; set; }
    }
}
