using System;
using System.ComponentModel.DataAnnotations;

namespace MMC.Core.Data.Model
{
    public class RFAMergedReferralHistory
    {
        [Key]
        public int RFAMergedReferralID { get; set; }
        public int RFAOldReferralID { get; set; }
        public int RFANewReferralID { get; set; }
        public DateTime RFAMergedReferralDate { get; set; }
    }
}
