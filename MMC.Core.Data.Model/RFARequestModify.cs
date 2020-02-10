
namespace MMC.Core.Data.Model
{
    public class RFARequestModify
    {
        public int RFARequestModifyID { get; set; }
        public int RFARequestID { get; set; }
        public string RFARequestedTreatment { get; set; }
        public int? RFAFrequency { get; set; }
        public int? RFADuration { get; set; }
        public int RFADurationTypeID { get; set; }
    }
}
