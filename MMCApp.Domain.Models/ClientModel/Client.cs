
namespace MMCApp.Domain.Models.ClientModel
{
  public  class Client
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int ClaimAdministratorID { get; set; }
        public string ClaimAdministratorType { get; set; }
        public string ClaimAdministratorName { get; set; }
    }
}
