using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.ViewModels.ClinicalTriageViewModel
{
    public class PreviousReferralFromCurrentReferral
    {
        public int RFAReferralID { get; set; }
        public int DecisionID { get; set; }
    }
}
