using MMCApp.Domain.Models.PatientModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.PatientViewModel
{
    public class PatientClaimSearchViewModel
    {
        public IEnumerable<PatientClaim> PatientClaimDetails { get; set; }
        public int TotalCount { get; set; }
    }

    public class PatientClaimDropDownList
    {
        public IEnumerable<PatientClaimDropDown> PatientClaimDropLists { get; set; }
    }
}
