using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientClaimPleadBodyPart
    {
        [DataMember]
        public int PatientClaimPleadBodyPartID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int PleadBodyPartID { get; set; }
        [DataMember]
        public string PatPleadBodyPartName { get; set; }
        [DataMember]
        public int BodyPartStatusID { get; set; }
        [DataMember]
        public string PatBodyPartStatusName { get; set; }
        [DataMember]
        public DateTime? AcceptedDate { get; set; }
        [DataMember]
        public string PleadBodyPartCondition { get; set; }
        [DataMember]
        public string PatPleadBodyPartCondition { get; set; }
    }
}
