using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class PatientClaimAddOnBodyPart
    {
        [Key]
        public int PatientClaimAddOnBodyPartID { get; set; }
        public int PatientClaimID { get; set; }
        public int AddOnBodyPartID { get; set; }
        public int BodyPartStatusID { get; set; }
        public string AddOnBodyPartCondition { get; set; }
        public DateTime? AcceptedDate { get; set; }

        [ForeignKey("PatientClaimID")]
        public PatientClaim patientclaims { get; set; }

        [ForeignKey("AddOnBodyPartID")]
        public BodyPart bodyparts { get; set; }

        [ForeignKey("BodyPartStatusID")]
        public BodyPartStatus bodyPartStatus { get; set; }
    }
}
 