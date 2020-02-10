
namespace MMCApp.Domain.Models.MedicalRecordReview
{
    public class RFAPatMedicalRecordReview
    {
        public int RFAPatMedicalRecordReviewedID { get; set; }
        public int RFARecSpltID { get; set; }
        public bool IsChecked { get; set; }
        public int RFAReferralID { get; set; }
    }
}
