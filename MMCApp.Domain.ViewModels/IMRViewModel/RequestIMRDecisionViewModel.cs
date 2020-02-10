using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMCApp.Domain.Models.IMRModel;
using MMCApp.Domain.Models.IntakeModel;

namespace MMCApp.Domain.ViewModels.IMRViewModel
{
    public class RequestIMRDecisionViewModel
    {
        public IMRDecisionReferralDetails IMRReferralDetails { get; set; }
        public IEnumerable<IMRDecisionRequestDetails> IMRRequestsDetails { get; set; }
        public IEnumerable<RFAReferralFile> IMRLetters { get; set; }
        public IEnumerable<IMRDecision> IMRDecision { get; set; }
        public IEnumerable<IMRRFARequest> IMRRFARequest { get; set; }        
    }
}