using System;

namespace MMCApp.Domain.Models.PatientModel
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
        public string AddOnBodyPartCondition { get; set; }
        public string PatAddOnBodyPartCondition { get; set; }
    }
}
