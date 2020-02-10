using MMCApp.Domain.Models.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.ViewModels.PatientViewModel
{
    public class PatientOccupationalAddUpdateDetails
    {
        public IEnumerable<PatientOccupational> PatientOccupationalDetails { get; set; }
        public string TotalPatientOccupational { get; set; }
    }
}
