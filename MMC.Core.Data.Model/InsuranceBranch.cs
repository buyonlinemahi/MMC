
namespace MMC.Core.Data.Model
{
    public class InsuranceBranch
    {
        public int InsuranceBranchID { get; set; }
        public int InsurerId { get; set; }
        public string InsBranchName { get; set; }
        public string InsBranchAddress { get; set; }
        public string InsBranchCity { get; set; }
        public int InsBranchStateId { get; set; }
        public string InsBranchZip { get; set; }
    }
}
