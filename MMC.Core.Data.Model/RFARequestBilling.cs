
namespace MMC.Core.Data.Model
{
    public class RFARequestBilling
    {
        public int RFARequestBillingID { get; set; }
        public int RFARequestID { get; set; }
        public decimal RFARequestBillingRN { get; set; }
        public decimal RFARequestBillingMD { get; set; }
        public decimal RFARequestBillingSpeciality { get; set; }
        public decimal RFARequestBillingAdmin { get; set; }
        public decimal RFARequestBillingDeferral { get; set; }
    }
}
