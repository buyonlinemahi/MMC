
namespace MMCApp.Domain.Models.PatientModel
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
