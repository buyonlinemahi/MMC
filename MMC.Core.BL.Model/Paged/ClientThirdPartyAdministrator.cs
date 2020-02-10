using System.Collections.Generic;
using MMC.Core.BL.Model.Base;

namespace MMC.Core.BL.Model.Paged
{
    public class ClientThirdPartyAdministrator : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.ClientThirdPartyAdministrator> ClientThirdPartyAdministratorDetails { get; set; }
    }
}
