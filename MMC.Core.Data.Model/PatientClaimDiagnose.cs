using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class PatientClaimDiagnose
    {
        [Key]
        public int PatientClaimDiagnosisID { get; set; }
        public int PatientClaimID { get; set; }
        public int icdICDNumberID { get; set; }
        public string icdICDNumber { get; set; }
        public string icdICDTab { get; set; }

        [ForeignKey("PatientClaimID")]
        public PatientClaim patientclaims { get; set; }
    }
}
