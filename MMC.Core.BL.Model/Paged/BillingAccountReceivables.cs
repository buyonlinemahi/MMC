using MMC.Core.BL.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class BillingAccountReceivables : BasePaged
    {
        public IEnumerable<BLModel.BillingAccountReceivables> AccountReceivableDetails { get; set; }
    }
}
