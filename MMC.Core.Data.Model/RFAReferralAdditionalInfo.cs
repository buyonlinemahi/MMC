using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{  
    public class RFAReferralAdditionalInfo
    {
        public int RFAReferralAdditionalInfoID { get; set; }
        public int RFAReferralID { get; set; }
        public int OriginalRFAReferralID { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreatedDate { get; set; }



        [ForeignKey("RFAReferralID")]
        public RFAReferral _RFAReferral { get; set; }
    }
}
