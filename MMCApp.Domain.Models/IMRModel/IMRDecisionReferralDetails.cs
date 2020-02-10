using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.IMRModel
{
    public class IMRDecisionReferralDetails
    {
        public int RFAReferralID { get; set; }
        public int IMRRFAReferralID { get; set; }
        public int PatientClaimID { get; set; }
        public string PatClaimNumber { get; set; }
        public int PatientID { get; set; }
        public string PatFirstName { get; set; }
        public string PatLastName { get; set; }
        public DateTime DueDate { get; set; }
        public string IMRInitialNotification { get; set; }
        public string IMRDecisionProofOfService { get; set; }
        public string IMRDecisionLetter { get; set; }
        public int IMRDecisionID { get; set; }
        public DateTime? IMRDecisionReceivedDate { get; set; }
       
    }
}
