using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class AdditionalDocument : BasePaged
    {
        public IEnumerable<BLModel.AdditionalDocument> AdditionalDocumentDetails { get; set; }
    }
}
