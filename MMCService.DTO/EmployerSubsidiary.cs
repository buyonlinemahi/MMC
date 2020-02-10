using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class EmployerSubsidiary
    {
        [DataMember]
        public int EMPSubsidiaryID { get; set; }
        [DataMember]
        public int EmployerId { get; set; }
        [DataMember]
        public string EMPSubsidiaryName { get; set; }
        [DataMember]
        public string EMPSubsidiaryAddress { get; set; }
        [DataMember]
        public string EMPSubsidiaryAddress2 { get; set; }
        [DataMember]
        public string EMPSubsidiaryCity { get; set; }
        [DataMember]
        public int EMPSubsidiaryStateId { get; set; }
        [DataMember]
        public string EMPSubsidiaryZip { get; set; }
        [DataMember]
        public string EMPSubsidiaryPhone { get; set; }
        [DataMember]
        public string EMPSubsidiaryFax { get; set; }
        [DataMember]
        public string EMPSubsidiaryEmail { get; set; }
        [DataMember]
        public string EmpSubsidiaryStateName { get; set; }
    }
}
