using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.Billing
{
    public class RFAReferralInvoice
    {
        public int RFAReferralInvoiceID { get; set; }
        public int PatientClaimID { get; set; }
        public string InvoiceNumber { get; set; }
        public int? ClientID { get; set; }
        public string InvoiceFileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserID { get; set; }
    }
}
