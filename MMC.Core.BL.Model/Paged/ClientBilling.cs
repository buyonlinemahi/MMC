using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class ClientBilling : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.ClientBilling> ClientBillingDetails { get; set; }
    }
}
