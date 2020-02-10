
namespace MMC.Core.BL.Model
{
    public class ClientInsuranceBranch
    {
        public int ClientInsuranceBranchID { get; set; }
        public int ClientID { get; set; }
        public int InsurerID { get; set; }
        public int InsuranceBranchID { get; set; }
        public string InsBranchName { get; set; }

    }
}
