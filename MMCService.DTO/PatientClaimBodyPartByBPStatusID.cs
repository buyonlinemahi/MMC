using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientClaimBodyPartByBPStatusID
    {
        [DataMember]
        public int PatientClaimBodyPartID { get; set; }
        [DataMember]
        public string PatientClaimBodtPartStatus { get; set; }
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public int BodyPartStatusID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
    }
}
