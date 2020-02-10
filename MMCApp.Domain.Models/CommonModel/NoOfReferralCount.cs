using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.CommonModel
{
    public class NoOfReferralCount
    {
        public int IntakeCount { get; set; }
        public int PreparationCount { get; set; }
        public int ClinicalTriageCount { get; set; }
        public int ClinicalAuditCount { get; set; }
        public int NotificationCount { get; set; }
        public int BillingCount { get; set; }
        public int PeerReviewCount { get; set; }
        public int IMRCount { get; set; }
    }
}
