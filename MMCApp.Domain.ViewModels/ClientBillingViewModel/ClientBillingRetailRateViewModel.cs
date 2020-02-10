using MMCApp.Domain.Models.ClientBillingModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ClientBillingViewModel
{
    public class ClientBillingRetailRateViewModel
    {
        public IEnumerable<ClientBillingRetailRate> ClientBillingRetailRateDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
