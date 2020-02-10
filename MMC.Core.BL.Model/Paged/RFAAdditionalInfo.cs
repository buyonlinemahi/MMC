using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using DLModel = MMC.Core.Data.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class RFAAdditionalInfo : BasePaged
    {
        public IEnumerable<DLModel.RFAAdditionalInfo> RFAAdditionalInfoDetails { get; set; }
    }
}
