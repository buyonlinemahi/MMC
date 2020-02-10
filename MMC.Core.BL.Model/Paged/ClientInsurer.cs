using System.Collections.Generic;
using MMC.Core.BL.Model.Base;

namespace MMC.Core.BL.Model.Paged
{
    public class ClientInsurer : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.ClientInsurer> ClientInsurerDetails { get; set; }
    }
}
