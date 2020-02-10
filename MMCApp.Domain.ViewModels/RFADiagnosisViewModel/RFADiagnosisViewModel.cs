using MMCApp.Domain.Models.RFADiagnosisModel;
using System.Collections.Generic;
namespace MMCApp.Domain.ViewModels.RFADiagnosisViewModel
{
    public class RFADiagnosisViewModel
    {
        public int TotalCount { get; set; }
        public IEnumerable<RFADiagnosis> RFADiagnosisDetails { get; set; }
    }
}
