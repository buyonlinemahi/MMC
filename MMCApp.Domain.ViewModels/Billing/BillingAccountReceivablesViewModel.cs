using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.ViewModels.Billing
{
    public class BillingAccountReceivablesViewModel
    {
        public IEnumerable<MMCApp.Domain.Models.Billing.BillingAccountReceivables> AccountReceivablesDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
