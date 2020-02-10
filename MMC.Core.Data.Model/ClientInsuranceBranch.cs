using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class ClientInsuranceBranch
    {
        [Key]
        public int ClientInsuranceBranchID { get; set; }
        public int ClientID { get; set; }
        public int InsurerID { get; set; }
        public int InsuranceBranchID { get; set; }

        [ForeignKey("ClientID")]
        public Client clients { get; set; }

        [ForeignKey("InsurerID")]
        public Insurer insurers { get; set; }

        [ForeignKey("InsuranceBranchID")]
        public InsuranceBranch insuranceBranchs { get; set; }
    }
}
