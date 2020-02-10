using System.Collections.Generic;
using MMC.Core.BL.Model.Base;

namespace MMC.Core.BL.Model.Paged
{
    public class User : BasePaged
    {
        public IEnumerable<MMC.Core.Data.Model.User> UserDetails { get; set; }
    }
}
