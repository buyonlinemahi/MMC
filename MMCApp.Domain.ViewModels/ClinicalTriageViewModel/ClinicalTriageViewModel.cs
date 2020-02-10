using MMCApp.Domain.Models.ClinicalTriageModel;
using System.Collections.Generic;


namespace MMCApp.Domain.ViewModels.ClinicalTriageViewModel
{
    public class ClinicalTriageViewModel
    {
        public IEnumerable<ClinicalTriage> ClinicalTriages { get; set; }
        public int TotalCount { get; set; }
        public int ProcessLevel { get; set; }
    }
}
