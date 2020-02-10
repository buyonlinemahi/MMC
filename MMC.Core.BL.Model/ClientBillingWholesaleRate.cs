using System;

namespace MMC.Core.BL.Model
{
    public class ClientBillingWholesaleRate
    {
        public int ClientBillingWholesaleRateID { get; set; }
        public int ClientBillingID { get; set; }

        public decimal? ClientBillingWholesaleRateRN { get; set; }
        public decimal? ClientBillingWholesaleRateMD { get; set; }
        public decimal? ClientBillingWholesaleRateSpecialityReview { get; set; }

        public decimal? ClientBillingWholesaleRateAdminFee { get; set; }
        public decimal? ClientBillingWholesaleRateIMRPrep { get; set; }
        public decimal? ClientBillingWholesaleRateIMRMD { get; set; }
        public decimal? ClientBillingWholesaleRateIMRRush { get; set; }
        public decimal? ClientBillingWholesaleRateDeferral { get; set; }

        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
