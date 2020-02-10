using MMCApp.Domain.Models.ClientBillingModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ClientBillingViewModel
{
    public class ClientBillingDetailViewModel
    {
        public ClientBilling ClientBillingDetail { get; set; }
        public ClientBillingRetailRate ClientBillingRetailRateDetail { get; set; }
        public ClientBillingWholesaleRate ClientBillingWholesaleRateDetail { get; set; }
        public ClientPrivateLabel ClientPrivateLabelDetail { get; set; }
    }
}
