using MMCApp.Domain.Models.IntakeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.IntakeViewModel
{
    public class PatientClaimBodyPartByBPStatusIDViewModel
    {
        public IEnumerable<PatientClaimBodyPartByBPStatusID> PatientClaimBodyPartByBPStatusIDDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
