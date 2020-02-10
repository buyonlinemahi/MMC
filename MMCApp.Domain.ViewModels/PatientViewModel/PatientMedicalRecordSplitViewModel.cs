using MMCApp.Domain.Models.DocumentCategoryModel;
using MMCApp.Domain.Models.DocumentTypeModel;
using MMCApp.Domain.Models.PatientModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.PatientViewModel
{
    public class PatientMedicalRecordSplitViewModel
    {
        public IEnumerable<DocumentCategory> documentCategories { get; set; }
        public IEnumerable<DocumentType> documentTypes { get; set; }
        public IEnumerable<PatientMedicalRecord> patientMedicalRecordSplitingDetails { get; set; }
        public PatientMedicalRecord patientMedicalRecord { get; set; }
    }
}
