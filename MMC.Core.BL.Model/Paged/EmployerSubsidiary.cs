using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class EmployerSubsidiary : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.EmployerSubsidiary> EmployerSubsidiaryDetails { get; set; }
    }
}
