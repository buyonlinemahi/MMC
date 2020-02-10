using System.Collections.Generic;
using MMC.Core.BL.Model.Base;

namespace MMC.Core.BL.Model.Paged
{
    public class RFADiagnosis : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.RFADiagnosis> RFADiagnosisDetails { get; set; }
    }
}

