using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class Insurer : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.Insurer> InsurerDetails { get; set; }
    }
}
