
namespace MMCApp.Domain.Models.ClientModel
{
    public class ClientInsuranceBranch
    {
        public int ClientInsuranceBranchID { get; set; }
        public int ClientID { get; set; }
        public int InsurerID { get; set; }
        public int InsuranceBranchID { get; set; }
    }
}
