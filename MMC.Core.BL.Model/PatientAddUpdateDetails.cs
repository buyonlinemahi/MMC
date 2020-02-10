using System.Collections.Generic;

namespace MMC.Core.BL.Model
{
    public class PatientAddUpdateDetails
    {
        public Patient PatientDetails { get; set; }
        public IEnumerable<PatientCurrentMedicalCondition> PatCurrentMedicalConditionsDetails { get; set; }
        public string PatCurrentMedicalConditionIds { get; set; }
    }
}
