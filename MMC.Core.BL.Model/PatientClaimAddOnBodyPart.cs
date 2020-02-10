
using System;
namespace MMC.Core.BL.Model
{
    public class PatientClaimAddOnBodyPart
    {
        public int PatientClaimAddOnBodyPartID { get; set; }
        public int PatientClaimID { get; set; }
        public int AddOnBodyPartID { get; set; }
        public int BodyPartStatusID { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public string PatAddOnBodyPartName { get; set; }
        public string PatBodyPartStatusName { get; set; }
        public string PatAddOnBodyPartCondition { get; set; }
    }
}
