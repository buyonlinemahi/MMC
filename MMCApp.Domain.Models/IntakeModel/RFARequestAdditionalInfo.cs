
namespace MMCApp.Domain.Models.IntakeModel
{
    public class RFARequestAdditionalInfo
    {
        public int RFARequestAdditionalInfoID { get; set; }
        public int RFARequestID { get; set; }
        public bool? URIncompleteRFAForm { get; set; }
        public bool? URNoRFAForm { get; set; }
        public bool? URDeclineInternalAppeal { get; set; }
        public int? OriginalRFARequestID { get; set; }
    }
}
