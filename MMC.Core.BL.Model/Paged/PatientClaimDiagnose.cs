using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class PatientClaimDiagnose : BasePaged
    {
        public IEnumerable<BLModel.PatientClaimDiagnose> PatientClaimDiagnoseDetails { get; set; }
    }
}
