using System.Collections.Generic;
using MMC.Core.BL.Model.Base;

namespace MMC.Core.BL.Model.Paged
{
    public class ClientManagedCareCompany : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.ClientManagedCareCompany> ClientManagedCareCompanyDetails { get; set; }
    }
}
