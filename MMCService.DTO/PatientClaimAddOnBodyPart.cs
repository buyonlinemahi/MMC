using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientClaimAddOnBodyPart
    {
        [DataMember]
        public int PatientClaimAddOnBodyPartID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int AddOnBodyPartID { get; set; }
        [DataMember]
        public string PatAddOnBodyPartName { get; set; }
        [DataMember]
        public int BodyPartStatusID { get; set; }
        [DataMember]
        public string PatBodyPartStatusName { get; set; }
        [DataMember]
        public string AddOnBodyPartCondition { get; set; }
        [DataMember]
        public string PatAddOnBodyPartCondition { get; set; }
        [DataMember]
        public DateTime? AcceptedDate { get; set; }
    }
}
