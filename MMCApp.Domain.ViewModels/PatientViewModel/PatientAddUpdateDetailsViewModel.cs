using MMCApp.Domain.Models.PatientModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.PatientViewModel
{
    public class PatientAddUpdateDetailsViewModel
    {
        public Patient PatientDetails { get; set; }
        public IEnumerable<PatientCurrentMedicalCondition> PatCurrentMedicalConditionsDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
