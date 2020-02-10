using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientClaimStatus
    {
        [DataMember]
        public int PatientClaimStatusID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int ClaimStatusID { get; set; }
        [DataMember]
        public string PatClaimStatusName { get; set; }
        [DataMember]
        public string DeniedRationale { get; set; }
    }
}
