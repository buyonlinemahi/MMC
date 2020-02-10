using System;
using System.ComponentModel.DataAnnotations;

namespace MMC.Core.Data.Model
{
    public class RFASplittedReferralHistory
    {
        [Key]
        public int RFASplittedReferralID { get; set; }
        public int RFAOldReferralID { get; set; }
        public int RFANewReferralID { get; set; }
        public DateTime RFASplittedReferralDate { get; set; }
    }
}
