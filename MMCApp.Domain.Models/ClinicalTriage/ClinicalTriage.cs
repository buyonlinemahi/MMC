
namespace MMCApp.Domain.Models.ClinicalTriageModel
{
    public class ClinicalTriage
    {
        public int RFARequestID { get; set; }
        public int RFAReferralID { get; set; }
        public int DecisionID { get; set; }
        public string RFARequestedTreatment { get; set; }
        public string PatFirstName { get; set; }
        public string PatLastName { get; set; }
        public string DecisionDesc { get; set; }
        public string PhysFirstName { get; set; }
        public string PhysLastName { get; set; }
        public string PatClaimNumber { get; set; }
        public string pageURL { get; set; }
        public System.DateTime? RFALatestDueDate { get; set; }
    }
}
