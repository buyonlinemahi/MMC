using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MMC.Core.Data.Model
{
    public class ClientBillingWholesaleRate
    {
        [Key]
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


        [ForeignKey("ClientBillingID")]
        public ClientBilling clientBillings { get; set; }

        [ForeignKey("CreatedBy")]
        public User users { get; set; }
    }
}
