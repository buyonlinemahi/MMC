using System.Collections.Generic;
using MMC.Core.BL.Model.Base;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class PatientClaimPleadBodyPart : BasePaged
    {
        public IEnumerable<BLModel.PatientClaimPleadBodyPart> PatientClaimPleadBodyPartDetails { get; set; }
    }
}
