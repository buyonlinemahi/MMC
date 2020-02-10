using System.Collections.Generic;
using MMC.Core.BL.Model.Base;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class PatientClaimStatus : BasePaged
    {
        public IEnumerable<BLModel.PatientClaimStatus> PatientClaimStatustDetails { get; set; }
    }
}
