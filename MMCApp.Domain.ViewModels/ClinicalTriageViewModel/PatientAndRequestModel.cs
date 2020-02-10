using MMCApp.Domain.Models.ClinicalTriage;
using MMCApp.Domain.Models.IntakeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ClinicalTriageViewModel
{
    public class PatientAndRequestModel
    {
        public PatientByReferralID Patients { get; set; }
        public IEnumerable<RequestByReferralID> RequestDetail { get; set; }
        public RFAReferralFile ReferralFile { get; set; }
        public IEnumerable<RFAReferralFile> ReferralFileNotification { get; set; }
        public int RemainingDecision { get; set; }
        public int RequestCount { get; set; }
        public int TimeExtensionPNCount { get; set; }
        public IEnumerable<PreviousReferralFromCurrentReferral> ReferralsForDeterminationLetter { get; set; }
        public IEnumerable<RFAReferralFile> IMRLetters { get; set; }
        public IEnumerable<RFAReferralFile> ReferralFileTimeExtensionPNAndProofOfService { get; set; }
    }
}
