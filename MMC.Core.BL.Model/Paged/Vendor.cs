using System.Collections.Generic;
using MMC.Core.BL.Model.Base;
using DLModel = MMC.Core.Data.Model;
namespace MMC.Core.BL.Model.Paged
{
    public class Vendor : BasePaged
    {
        public IEnumerable<DLModel.Vendor> VendorDetails { get; set; }
    }
}
