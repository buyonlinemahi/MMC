using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class ICDCode : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.ICDCode> ICDCodeDetails { get; set; }
    }
}
