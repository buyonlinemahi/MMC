using System;
namespace MMCApp.Domain.Models.IntakeModel
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
        public int? DecisionID { get; set; }
        public string RFAStatus { get; set; }
        public string RFANotes { get; set; }
        public string RFAClinicalReasonforDecision { get; set; }
        public string RFAGuidelinesUtilized { get; set; }
        public string RFARelevantPortionUtilized { get; set; }
        public DateTime? RFARequestDate { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? RFALatestDueDate { get; set; }
        public int ProcessLevel { get; set; }
        public int RFARequestModifyID { get; set; }
        public bool IsModify { get; set; }
        public int PatientClaimStatusID { get; set; }
        public string DeniedRationale { get; set; }
        public bool IsDeniedRationale { get; set; }
        public int RFARequestAdditionalInfoID { get; set; }
        public bool? URIncompleteRFAForm { get; set; }
        public bool? URNoRFAForm { get; set; }
        public bool? URDeclineInternalAppeal { get; set; }
        public bool IsUnableToLetter { get; set; }
        public bool IsDuplicate { get; set; }
        public int? OriginalRFARequestID { get; set; }
        public string RFAReqCertificationNumber { get; set; }
    }
}
