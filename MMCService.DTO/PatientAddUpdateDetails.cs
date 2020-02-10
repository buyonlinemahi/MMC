using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientAddUpdateDetails
    {
        [DataMember]
        public DTO.Patient PatientDetails { get; set; }
        [DataMember]
        public IEnumerable<DTO.PatientCurrentMedicalCondition> PatCurrentMedicalConditionsDetails { get; set; }
        [DataMember]
        public string PatCurrentMedicalConditionIds { get; set; }
    }
}
