using System.Collections.Generic;

namespace MMC.Core.BL.Model
{
    public class PatientClaimAddUpdateDetails
    {
        public PatientClaim PatientClaimDetails { get; set; }

        public IEnumerable<PatientClaimAddOnBodyPart> PatientClaimAddOnBodyPartDetails { get; set; }
        public string PatientClaimAddOnBodyPartIds { get; set; }

        public IEnumerable<PatientClaimPleadBodyPart> PatientClaimPleadBodyPartDetails { get; set; }
        public string PatientClaimPleadBodyPartIds { get; set; }

        public IEnumerable<PatientClaimDiagnose> PatientClaimDiagnoseDetails { get; set; }
        public string PatientClaimDiagnoseIds { get; set; }

        public IEnumerable<PatientClaimStatus> PatientClaimStatusDetails { get; set; }
        public string PatientClaimStatusIds { get; set; }
    }
}
