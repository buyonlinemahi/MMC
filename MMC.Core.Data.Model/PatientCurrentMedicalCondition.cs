using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{

    public class PatientCurrentMedicalCondition
    {
        [Key]
        public int PatCurrentMedicalConditionId { get; set; }
        public int CurrentMedicalConditionId { get; set; }
        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public Patient patients { get; set; }
        [ForeignKey("CurrentMedicalConditionId")]
        public CurrentMedicalCondition currentMedicalConditions { get; set; }
    }
}