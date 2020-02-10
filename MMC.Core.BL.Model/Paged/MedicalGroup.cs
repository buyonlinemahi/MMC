using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class MedicalGroup : BasePaged
    {
        public IEnumerable<BLModel.MedicalGroup> MedicalGroupDetails { get; set; }
    }
}
