using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.Billing
{
    public class BillingAccountReceivables
    {
        public int ClientID { get; set; }
        public int PatientID { get; set; }
        public int PatientClaimID { get; set; }
        public int RFAReferralInvoiceID { get; set; }
        public int? ClientStatementID { get; set; }
        public string ClientName { get; set; }
        public string PatientName { get; set; }
        public string PatClaimNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceFileName { get; set; }
        public string ClientStatementNumber { get; set; }
        public string ClientStatementFileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FullPathInvoiceNumber { get; set; }
        public string FullPathClientStatementNumber { get; set; }
        
    }
}
