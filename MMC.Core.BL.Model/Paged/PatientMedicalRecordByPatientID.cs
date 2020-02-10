using MMC.Core.BL.Model.Base;
using System.Collections.Generic;

namespace MMC.Core.BL.Model.Paged
{
    public class PatientMedicalRecordByPatientID : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.PatientMedicalRecordByPatientID> PatientMedicalRecordByPatientIDDetails { get; set; }
    }
}
