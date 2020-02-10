using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class RequestIMRRecord : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.RequestIMRRecord> RequestIMRRecordDetails { get; set; }
    }
}
