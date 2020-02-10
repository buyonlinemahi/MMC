
namespace MMCApp.Domain.Models.PatientModel
{
    public class PatientClaimStatus
    {
        public int PatientClaimStatusID { get; set; }
        public int PatientClaimID { get; set; }
        public int ClaimStatusID { get; set; }
        public string DeniedRationale { get; set; }
        public string PatClaimStatusName { get; set; }
    }
}
