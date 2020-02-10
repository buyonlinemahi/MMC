using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using DLModel = MMC.Core.Data.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class PeerReview : BasePaged
    {
        public IEnumerable<DLModel.PeerReview> PeerReviewDetails { get; set; }
    }
}
