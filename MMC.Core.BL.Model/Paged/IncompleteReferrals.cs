using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class IncompleteReferrals:BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.IncompleteReferrals> IncompleteReferralsDetails { get; set; }
    }
}
