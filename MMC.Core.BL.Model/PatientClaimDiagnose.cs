
namespace MMC.Core.BL.Model
{
    public class PatientClaimDiagnose
    {
        public int PatientClaimDiagnosisID { get; set; }
        public int PatientClaimID { get; set; }
        public int icdICDNumberID { get; set; }
        public string icdICDNumber { get; set; }
        public string icdICDTab { get; set; }
        public string ICDDescription { get; set; }

    }
}
