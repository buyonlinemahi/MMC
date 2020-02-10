using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class PatientClaimPleadBodyPart
    {
        [Key]
        public int PatientClaimPleadBodyPartID { get; set; }
        public int PatientClaimID { get; set; }
        public int PleadBodyPartID { get; set; }
        public int BodyPartStatusID { get; set; }
        public System.DateTime? AcceptedDate { get; set; }
        public string PleadBodyPartCondition { get; set; }

        [ForeignKey("PatientClaimID")]
        public PatientClaim patientclaims { get; set; }

        [ForeignKey("PleadBodyPartID")]
        public BodyPart bodyparts { get; set; }

        [ForeignKey("BodyPartStatusID")]
        public BodyPartStatus bodyPartStatus { get; set; }
    }
}
