using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class ClientThirdPartyAdministratorBranch
    {
        [Key]
        public int ClientTPABranchID { get; set; }
        public int ClientID { get; set; }
        public int TPAID { get; set; }
        public int TPABranchID { get; set; }

        [ForeignKey("ClientID")]
        public Client clients { get; set; }

        [ForeignKey("TPAID")]
        public ThirdPartyAdministrator thirdpartyadministrators { get; set; }

        [ForeignKey("TPABranchID")]
        public ThirdPartyAdministratorBranch thirdPartyAdministratorBranchs { get; set; }
    }
}
