using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class ClientBillingRetailRate : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.ClientBillingRetailRate> ClientBillingRetailRateDetails { get; set; }
    }
}
