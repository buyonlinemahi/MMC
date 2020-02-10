using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class ClinicalTriage : BasePaged
    {
        public IEnumerable<BLModel.ClinicalTriage> ClinicalTriages { get; set; }
    }
}

