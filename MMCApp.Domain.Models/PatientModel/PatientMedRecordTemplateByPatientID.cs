
namespace MMCApp.Domain.Models.PatientModel
{
    public class PatientMedRecordTemplateByPatientID
    {
        public string PatientName { get; set; }
        public string Diagnosis { get; set; }
        public string Claims { get; set; }
        public string AcceptedBodyParts { get; set; }
        public string DeniedBodyParts { get; set; }
        public string DiscoveryBodyParts { get; set; }
    }
}
