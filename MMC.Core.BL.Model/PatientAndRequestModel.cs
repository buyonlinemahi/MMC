using System.Collections.Generic;

namespace MMC.Core.BL.Model
{
    public class PatientAndRequestModel
    {
        public PatientByReferralID Patients { get; set; }
        public IEnumerable<RequestByReferralID> RequestDetail { get; set; }
        public int RemainingDecision { get; set; }
        public int RequestCount { get; set; }
        public int TimeExtensionPNCount { get; set; }       
    }
}
