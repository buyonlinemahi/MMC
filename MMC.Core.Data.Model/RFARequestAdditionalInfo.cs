using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class RFARequestAdditionalInfo
    {
        public int RFARequestAdditionalInfoID { get; set; }
        public int RFARequestID { get; set; }
        public bool? URIncompleteRFAForm { get; set; }
        public bool? URNoRFAForm { get; set; }
        public bool? URDeclineInternalAppeal { get; set; }
        public int? OriginalRFARequestID { get; set; }
        [ForeignKey("RFARequestID")]
        public RFARequest _RFARequest { get; set; }
    }
}
