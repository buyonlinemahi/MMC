using MMCApp.Domain.Models.IntakeModel;
using MMCApp.Domain.Models.PatientModel;
using MMCApp.Domain.ViewModels.PatientViewModel;

namespace MMCApp.Domain.ViewModels.ClinicalTriageViewModel
{
    public class ClinicalTriageActionViewModel
    {
        public PatientAndRequestModel patientAndRequestModel { get; set; }
        public PatientMedRecordTemplateByPatientID patientMedRecordTemplateByPatientID { get; set; }
        public PatientMedicalRecordViewModel PatientMedicalRecordViewModels { get; set; }
        public RFADiagnosisViewModel.RFADiagnosisViewModel rfaDiagnosisViewModel { get; set; }
        public MedicalRecordReviewViewModel.RFAPatMedicalRecordReviewViewModel rfaPatMedicalRecordReviewViewModels { get; set; }
        public int ProcessLevel { get; set; }
        public int PreviousProcessLevel { get; set; }
        public string AcceptedBodyParts { get; set; }
        public string DeniedBodyParts { get; set; }
        public string DelayedBodyParts { get; set; }
        public string Diagnosis { get; set; }
        public string CombroidCondition { get; set; }
        public RFAReferral RFAReferral { get; set; }
    }
}
