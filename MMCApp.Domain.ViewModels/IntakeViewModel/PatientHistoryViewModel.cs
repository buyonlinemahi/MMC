using MMCApp.Domain.Models.IntakeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.IntakeViewModel
{
    public class PatientHistoryViewModel
    {
        public IEnumerable<PatientHistory> PatientHistoryDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
