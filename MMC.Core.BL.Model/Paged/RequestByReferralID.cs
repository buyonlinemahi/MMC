using System.Collections.Generic;
using MMC.Core.BL.Model.Base;
using BLModel = MMC.Core.BL.Model;
namespace MMC.Core.BL.Model.Paged
{
    public class RequestByReferralID : BasePaged
    {
        public IEnumerable<BLModel.RequestByReferralID> RequestDetails { get; set; }
    }
}
