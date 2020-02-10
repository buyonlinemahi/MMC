using MMCApp.Domain.Models.Billing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.ViewModels.Billing
{
    public class RFAReferralInvoiceViewModel
    {
        public IEnumerable<RFAReferralInvoice> RFAReferralInvoiceDetails { get; set; }
    }
}
