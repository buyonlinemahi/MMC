using System;
namespace MMC.Core.BL.Model
{
    public class RequestByReferralID
    {
        public int RFAReferralID { get; set; }
        public int? DecisionID { get; set; }
        public DateTime RFAHCRGDate { get; set; }
        public string RFARequestedTreatment { get; set; }
        public int RFARequestID { get; set; }
        public string RFAStatus { get; set; }
        public string RFANotes { get; set; }
        public string DecisionDesc { get; set; }
        public string RFAClinicalReasonforDecision { get; set; }
        public string RFAGuidelinesUtilized { get; set; }
        public string RFARelevantPortionUtilized { get; set; }
        public int? RFAFrequency { get; set; }
        public int? RFADuration { get; set; }
        public int RFADurationTypeID { get; set; }
        public int? RFARequestModifyID { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string Decision { get; set; }
        public DateTime? RFALatestDueDate{ get; set; }

    }
}
