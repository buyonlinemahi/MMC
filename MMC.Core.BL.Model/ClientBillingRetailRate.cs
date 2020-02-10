using System;

namespace MMC.Core.BL.Model
{
    public class ClientBillingRetailRate
    {
        public int ClientBillingRetailRateID { get; set; }
        public int ClientBillingID { get; set; }

        public decimal? ClientBillingRetailRateRN { get; set; }
        public decimal? ClientBillingRetailRateMD { get; set; }
        public decimal? ClientBillingRetailRateSpecialityReview { get; set; }

        public decimal? ClientBillingRetailRateAdminFee { get; set; }
        public decimal? ClientBillingRetailRateIMRPrep { get; set; }
        public decimal? ClientBillingRetailRateIMRMD { get; set; }
        public decimal? ClientBillingRetailRateIMRRush { get; set; }
        public decimal? ClientBillingRetailRateDeferral { get; set; }    

        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
