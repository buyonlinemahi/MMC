using MMCApp.Domain.Models.PatientModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.PatientViewModel
{
    public class PatientMedicalRecordViewModel
    {
        public IEnumerable<PatientMedicalRecordByPatientID> PatientMedicalRecordByPatientIDDetails { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
