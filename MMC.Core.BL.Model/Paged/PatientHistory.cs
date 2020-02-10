using MMC.Core.BL.Model.Base;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL.Model.Paged
{
    public class PatientHistory : BasePaged
    {
        public IEnumerable<BLModel.PatientHistory> PatientHistoryDetails { get; set; }
    }
}
