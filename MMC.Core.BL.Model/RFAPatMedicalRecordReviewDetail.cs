
namespace MMC.Core.BL.Model
{
    public class RFAPatMedicalRecordReviewDetail
    {
        public System.DateTime PatMRDocumentDate { get; set; }
        public int? RFAReferralID { get; set; }
        public string PatMRDocumentName { get; set; }
        public int PatientClaimID { get; set; }
        public string PhysicianName { get; set; }
    }
}
