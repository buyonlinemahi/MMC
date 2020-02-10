using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.RFAMergedReferralHistory
{
    public class RFAMergedReferralHistory
    {
        public int RFAMergedReferralID { get; set; }
        public int RFAOldReferralID { get; set; }
        public int RFANewReferralID { get; set; }
        public DateTime RFAMergedReferralDate { get; set; }
    }
}
