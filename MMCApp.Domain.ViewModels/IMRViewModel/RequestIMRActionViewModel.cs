using MMCApp.Domain.Models.ClinicalTriage;
using MMCApp.Domain.Models.IMRModel;
using MMCApp.Domain.Models.IntakeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.IMRViewModel
{
    public class RequestIMRActionViewModel
    {
        public PatientByReferralID Patients { get; set; }
        public IMRRFAReferral IMRRFAReferrals { get; set; }
        public IEnumerable<RequestByReferralID> RequestDetail { get; set; }
        public RFAReferralFile ReferralFile { get; set; }
        public IEnumerable<RFAReferralFile> ReferralFileNotification { get; set; }
        
    }
}
