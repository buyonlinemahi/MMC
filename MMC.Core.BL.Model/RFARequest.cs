using System;

namespace MMC.Core.BL.Model
{
    public class RFARequest
    {
        public int RFARequestID { get; set; }
        public int? RFAReferralID { get; set; }
        public bool? RFAExpediteReferral { get; set; }
        public string RFARequestedTreatment { get; set; }
        public int? RFADurationTypeID { get; set; }
        public int? RFAFrequency { get; set; }
        public int? RequestTypeID { get; set; }
        public int? RFADuration { get; set; }
        public int? RFAQuantity { get; set; }
        public int? TreatmentCategoryID { get; set; }
        public int? TreatmentTypeID { get; set; }
        public string RFAStrenght { get; set; }
        public string RFACPT_NDC { get; set; }
        public string RFAStatus { get; set; }
        public int? DecisionID { get; set; }
        public string RFANotes { get; set; }
        public string RFAClinicalReasonforDecision { get; set; }
        public string RFAGuidelinesUtilized { get; set; }
        public string RFARelevantPortionUtilized { get; set; }
        public string RFAReqCertificationNumber { get; set; }
        public DateTime? RFARequestIMRCreatedDate { get; set; }
    }
}
