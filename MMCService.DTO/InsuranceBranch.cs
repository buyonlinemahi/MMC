using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class InsuranceBranch
    {
        [DataMember]
        public int InsuranceBranchID { get; set; }
        [DataMember]
        public int InsurerId { get; set; }
        [DataMember]
        public string InsBranchName { get; set; }
        [DataMember]
        public string InsBranchAddress { get; set; }
        [DataMember]
        public string InsBranchCity { get; set; }
        [DataMember]
        public int InsBranchStateId { get; set; }
        [DataMember]
        public string InsBranchZip { get; set; }
        [DataMember]
        public string InsBranchStateName { get; set; }
    }
}
