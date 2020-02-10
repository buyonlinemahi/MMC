using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class ThirdPartyAdministratorBranch : BasePaged
    {
         public IEnumerable<BLModel.ThirdPartyAdministratorBranch> ThirdPartyAdministratorBranchDetails { get; set; }
    }
}
