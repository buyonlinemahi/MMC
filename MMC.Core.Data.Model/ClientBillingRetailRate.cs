using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class ClientBillingRetailRate
    {
        [Key]
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


        [ForeignKey("ClientBillingID")]
        public ClientBilling clientBillings { get; set; }

        [ForeignKey("CreatedBy")]
        public User users { get; set; }
    }
}
