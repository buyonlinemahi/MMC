using MMCApp.Domain.Models.ClinicalTriage;
using MMCApp.Domain.ViewModels.PatientViewModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ClinicalTriageViewModel
{
    public class RFAAdditionalInfoViewModel
    {
        public IEnumerable<RFAAdditionalInfo> RFAAdditionalInfoDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
