using MMCApp.Domain.Models.PatientModel;
using System.Collections.Generic;


namespace MMCApp.Domain.ViewModels.PatientViewModel
{
    public class PatientClaimAddUpdateDetails
    {
        //Patient Claim Search Grid
        public IEnumerable<PatientClaim> PatientClaimResults { get; set; }
        public int TotalCountClaim { get; set; }

        public PatientClaim PatientClaimDetails { get; set; }
        public IEnumerable<PatientClaimAddOnBodyPart> PatientClaimAddOnBodyPartDetails { get; set; }
        public int TotalCountClaimAddOnBodyPart { get; set; }

        public IEnumerable<PatientClaimPleadBodyPart> PatientClaimPleadBodyPartDetails { get; set; }
        public int TotalCountClaimPleadBodyPart { get; set; }
      
        public IEnumerable<PatientClaimDiagnose> PatientClaimDiagnoseDetails { get; set; }
        public int TotalCountClaimDiagnose { get; set; }
       
        public IEnumerable<PatientClaimStatus> PatientClaimStatusDetails { get; set; }
        public int TotalCountClaimStatus { get; set; }
       
    }
}
