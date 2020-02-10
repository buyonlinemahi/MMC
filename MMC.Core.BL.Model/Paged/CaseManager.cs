using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
namespace MMC.Core.BL.Model.Paged
{
    public class CaseManager : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.CaseManager> CaseManagerDetails { get; set; }
    }
}
