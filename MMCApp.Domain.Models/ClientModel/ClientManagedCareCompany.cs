
namespace MMCApp.Domain.Models.ClientModel
{
    public class ClientManagedCareCompany
    {
        public int ClientCompanyID { get; set; }
        public int ClientID { get; set; }
        public int CompanyID { get; set; }
        public string ClientName { get; set; }
        public string CompName { get; set; }
        public string CompAddress { get; set; }
        public string CompCity { get; set; }
        public string CompZip { get; set; }
        public string CompState { get; set; }
    }
}
