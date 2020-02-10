

namespace MMC.Core.BL.Model
{
    public class PatientClaimBodyPartByBPStatusID
    {
        public int PatientClaimBodyPartID { get; set; }
        public string PatientClaimBodtPartStatus { get; set; }
        public string TableName { get; set; }
        public int BodyPartStatusID { get; set; }
        public int PatientClaimID { get; set; }
    }
}
