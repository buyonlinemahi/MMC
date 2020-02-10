using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class RFAReferralInvoice
    {
        [Key]
        public int RFAReferralInvoiceID { get; set; }
        public int PatientClaimID { get; set; }
        public string InvoiceNumber { get; set; }
        public int? ClientID { get; set; }
        public string InvoiceFileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserID { get; set; }

        [ForeignKey("PatientClaimID")]
        public PatientClaim patientClaim { get; set; }
    }
}
