using MMCApp.Domain.Models.Billing;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.BillingViewModel
{
    public class BillingViewModel
    {
        public IEnumerable<MMCApp.Domain.Models.Billing.Billing> BillingDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
