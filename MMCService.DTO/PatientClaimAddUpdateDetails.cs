using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace MMCService.DTO
{
    [DataContract]
    public class PatientClaimAddUpdateDetails
    {
        [DataMember]
        public DTO.PatientClaim PatientClaimDetails { get; set; }
        [DataMember]
        public IEnumerable<DTO.PatientClaimAddOnBodyPart> PatientClaimAddOnBodyPartDetails { get; set; }
        [DataMember]
        public string PatientClaimAddOnBodyPartIds { get; set; }
        [DataMember]
        public IEnumerable<DTO.PatientClaimPleadBodyPart> PatientClaimPleadBodyPartDetails { get; set; }
        [DataMember]
        public string PatientClaimPleadBodyPartIds { get; set; }
        [DataMember]
        public IEnumerable<DTO.PatientClaimDiagnose> PatientClaimDiagnoseDetails { get; set; }
        [DataMember]
        public string PatientClaimDiagnoseIds { get; set; }
        [DataMember]
        public IEnumerable<DTO.PatientClaimStatus> PatientClaimStatusDetails { get; set; }
        [DataMember]
        public string PatientClaimStatusIds { get; set; }
    }
}
