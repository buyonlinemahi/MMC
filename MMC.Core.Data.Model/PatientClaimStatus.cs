using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.dboTables.PatientClaimStatus, Schema = Constant.Constant.Schemas.dbo)]
    public class PatientClaimStatus
    {
        [Key]
        public int PatientClaimStatusID { get; set; }
        public int PatientClaimID { get; set; }
        public int ClaimStatusID { get; set; }
        public string DeniedRationale { get; set; }

        [ForeignKey("PatientClaimID")]
        public PatientClaim patientclaims { get; set; }

        [ForeignKey("ClaimStatusID")]
        public ClaimStatus claimStatuss { get; set; }
    }
}
