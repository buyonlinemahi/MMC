using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class InsuranceBranch : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.InsuranceBranch> InsuranceBranchDetails { get; set; }
    }
}
