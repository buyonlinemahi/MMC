using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientCurrentMedicalCondition
    {
        [DataMember]
        public int PatCurrentMedicalConditionId { get; set; }
        [DataMember]
        public int CurrentMedicalConditionId { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public string PatCurrentMedicalConditionName { get; set; }
    }
}
