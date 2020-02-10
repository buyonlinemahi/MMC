using System.Collections.Generic;
using MMC.Core.BL.Model.Base;

namespace MMC.Core.BL.Model.Paged
{
    public class Client : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.Client> ClientDetails { get; set; }
    }
}
