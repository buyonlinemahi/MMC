using System;

namespace MMC.Core.BL.Model
{
    public class PatientClaimPleadBodyPart
    {
        public int PatientClaimPleadBodyPartID { get; set; }
        public int PatientClaimID { get; set; }
        public int PleadBodyPartID { get; set; }
        public int BodyPartStatusID { get; set; }
        public DateTime? AcceptedDate { get; set; }

        public string PatPleadBodyPartName { get; set; }
        public string PatBodyPartStatusName { get; set; }
        public string PatPleadBodyPartCondition { get; set; }
    }
}
