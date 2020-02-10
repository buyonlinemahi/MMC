using MMCApp.Domain.Models.PatientModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.PatientViewModel
{
    public class PatientSearchViewModel
    {
        public IEnumerable<Patient> PatientDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
