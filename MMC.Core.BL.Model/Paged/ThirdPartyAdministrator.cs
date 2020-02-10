using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class ThirdPartyAdministrator : BasePaged
    {
        public IEnumerable<BLModel.ThirdPartyAdministrator> ThirdPartyAdministratorDetails { get; set; }
    }
}
