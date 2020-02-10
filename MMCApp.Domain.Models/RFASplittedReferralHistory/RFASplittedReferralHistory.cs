using System;

namespace MMCApp.Domain.Models.RFASplittedReferralHistory
{
    public class RFASplittedReferralHistory
    {
        public int RFASplittedReferralID { get; set; }
        public int RFAOldReferralID { get; set; }
        public int RFANewReferralID { get; set; }
        public DateTime RFASplittedReferralDate { get; set; }
    }
}
