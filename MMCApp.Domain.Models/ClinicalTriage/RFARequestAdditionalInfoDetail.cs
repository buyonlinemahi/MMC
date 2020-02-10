
namespace MMCApp.Domain.Models.ClinicalTriage
{
    public class RFARequestAdditionalInfoDetail
    {
        public int RFARequestAdditionalInfoID { get; set; }
        public int RFARequestID { get; set; }
        public bool? URIncompleteRFAForm { get; set; }
        public bool? URNoRFAForm { get; set; }
        public bool? URDeclineInternalAppeal { get; set; }
        public int? OriginalRFARequestID { get; set; }
        public int? DecisionID { get; set; }
    }
}
