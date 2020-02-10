using MMCApp.Domain.Models.PatientModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.IntakeViewModel
{
    public class IntakePatientClaimDiagnoseViewModel
    {
        public IEnumerable<PatientClaimDiagnose> PatientClaimDiagnoseDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
