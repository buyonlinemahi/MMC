﻿using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class ClientBillingWholesaleRate : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.ClientBillingWholesaleRate> ClientBillingWholesaleRateDetails { get; set; }
    }
}
