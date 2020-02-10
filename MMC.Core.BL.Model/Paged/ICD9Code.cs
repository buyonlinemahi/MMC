using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class ICD9Code : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.ICD9Code> ICD9CodeDetails { get; set; }
    }
}
