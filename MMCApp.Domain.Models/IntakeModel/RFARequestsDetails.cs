
namespace MMCApp.Domain.Models.IntakeModel
{
    public class RFARequestsDetails
    {
        public int RFARequestID { get; set; }
        public int RFAReferralID { get; set; }
        public bool RFAExpediteReferral { get; set; }
        public string RFARequestedTreatment { get; set; }
        public int FrequencyTypeID { get; set; }
        public string RFAFrequency { get; set; }
        public int RequestTypeID { get; set; }
        public string RFADuration { get; set; }
        public int RFAQuantity { get; set; }
        public int TreatmentCategoryID { get; set; }
        public int TreatmentTypeID { get; set; }
        public string TreatmentCategoryName { get; set; }
        public string TreatmentTypeDesc { get; set; }
        public string RequestTypeDesc { get; set; }
        public string RFAStrenght { get; set; }
        public string RFACPT_NDC { get; set; }
        public int? RFADurationTypeID { get; set; }
        public string RFAReqCertificationNumber { get; set; }
        public string ReqBodyPart { get; set; }
        public int? DecisionID { get; set; }
        public string RFANotes { get; set; }
    }
}