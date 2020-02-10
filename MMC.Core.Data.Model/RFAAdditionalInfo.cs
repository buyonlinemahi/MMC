using System;

namespace MMC.Core.Data.Model
{
    public class RFAAdditionalInfo
    {
        public int RFAAdditionalInfoID { get; set; }
        public int RFAReferralID { get; set; }
        public string RFAAdditionalInfoRecord { get; set; }
        public DateTime? RFAAdditionalInfoRecordDate { get; set; }
    }
}
