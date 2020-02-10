
using System.ComponentModel.DataAnnotations;
namespace MMC.Core.Data.Model
{
    public class RFAPatMedicalRecordReview
    {
        [Key]
        public int RFAPatMedicalRecordReviewedID { get; set; }
        public int RFARecSpltID { get; set; }
        public int RFAReferralID { get; set; }
    }
}
