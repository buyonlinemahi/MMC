using System;

namespace MMCApp.Domain.Models.IMRModel
{
    public class RequestIMRRecord
    {
        public int RequestIMRID { get; set; }
        public int RFARequestID { get; set; }
        public int OriginalRFAReferralID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int PatientID { get; set; }
        public string PatFirstName { get; set; }
        public string PatLastName { get; set; }
        public int RFAReferralID { get; set; }
        public string RFARequestedTreatment { get; set; }
        public string DecisionDesc { get; set; }
        public string RFAStatus { get; set; }
        public int ProcessLevel { get; set; }
    }
}
