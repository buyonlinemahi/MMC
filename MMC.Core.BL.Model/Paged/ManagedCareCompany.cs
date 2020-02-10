using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;
namespace MMC.Core.BL.Model.Paged
{
    public class ManagedCareCompany : BasePaged
    {
        public IEnumerable<BLModel.ManagedCareCompany> ManagedCareCompanyDetails { get; set; }
    }
}
