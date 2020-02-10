using MMCApp.Domain.Models.ClientBillingModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ClientBillingViewModel
{
    public class ClientBillingWholesaleRateViewModel
    {
        public IEnumerable<ClientBillingWholesaleRate> ClientBillingWholesaleRateDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
