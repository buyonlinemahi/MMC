namespace MMC.Core.BL.Model
{
    public class RFADiagnosis
    {
        public int RFAReferralID { get; set; }
        public int PatientClaimID { get; set; }
        public string icdICDNumber { get; set; }
        public string ICDDescription { get; set; }
        public int PatientClaimDiagnosisID { get; set; }
    }
}
