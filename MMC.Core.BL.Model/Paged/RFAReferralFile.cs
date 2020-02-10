using System.Collections.Generic;
using MMC.Core.BL.Model.Base;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{   
    public class RFAReferralFile : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.RFAReferralFile> RFAReferralFileDetails { get; set; }
    }
}
