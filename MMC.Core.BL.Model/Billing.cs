
namespace MMC.Core.BL.Model
{
    public class Billing
    {
        public string ClientName { get; set; }
        public string PatientName { get; set; }
        public int RFAReferralID { get; set; }
        public int PatientClaimID { get; set; }
        public int PatClientID { get; set;}
        public int RFARequestID { get; set; }
        public string RFARequestedTreatment { get; set; }
        public int RFARequestBillingID { get; set; }
        public decimal RFARequestBillingRN { get; set; }
        public decimal RFARequestBillingMD { get; set; }
        public decimal RFARequestBillingSpeciality { get; set; }
        public decimal RFARequestBillingAdmin { get; set; }
        public decimal RFARequestBillingDeferral { get; set; }
        public decimal BillingTotal { get; set; } 
        public int ClientBillingRateTypeID { get; set; }        
        public int DecisionID { get; set; }

    }
}
