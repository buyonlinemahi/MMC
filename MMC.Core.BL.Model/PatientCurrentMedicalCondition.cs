

namespace MMC.Core.BL.Model
{
    public class PatientCurrentMedicalCondition
    {
        public int PatCurrentMedicalConditionId { get; set; }
        public int CurrentMedicalConditionId { get; set; }
        public int PatientID { get; set; }

        /// 
        public string PatCurrentMedicalConditionName { get; set; }
    }
}
