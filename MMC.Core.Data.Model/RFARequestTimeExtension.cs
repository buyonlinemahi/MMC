using System;

namespace MMC.Core.Data.Model
{
    public class RFARequestTimeExtension
    {
        public int RFARequestTimeExtensionID { get; set; }
        public int RFARecSpltID { get; set; }
        public int RFARequestID { get; set; }
        public int RFAReferralID { get; set; }
        public string RFIRecords { get; set; }
        public string AdditionalExamination { get; set; }
        public string SpecialtyConsult { get; set; }
    }
}
