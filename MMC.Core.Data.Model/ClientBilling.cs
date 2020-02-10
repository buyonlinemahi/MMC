using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class ClientBilling
    {
        [Key]
        public int ClientBillingID { get; set; }
        public int ClientID { get; set; }
        public int ClientBillingRateTypeID { get; set; }
        public Boolean? ClientIsPrivateLabel { get; set; }
        public string ClientInvoiceNumber { get; set; }
        public int ClientAttentionToID { get; set; }
        public string ClientAttentionToFreeText { get; set; }

        [ForeignKey("ClientID")]
        public Client clients { get; set; }

        [ForeignKey("ClientBillingRateTypeID")]
        public BillingRateType billingRateTypes { get; set; }

    }
}
