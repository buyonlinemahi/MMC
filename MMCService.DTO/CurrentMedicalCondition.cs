using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class CurrentMedicalCondition
    {
        [DataMember]
        public int CurrentMedicalConditionId { get; set; }
        [DataMember]
        public string CurrentMedicalConditionName { get; set; }
    }
}
