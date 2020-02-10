
namespace MMCApp.Domain.Models.ClientModel
{
 public  class ClientInsurer
    {
        public int ClientInsurerID { get; set; }
        public int ClientID { get; set; }
        public int InsurerID { get; set; }

        public int ClientName { get; set; }
        public string InsName { get; set; }
        public string InsAddress1 { get; set; }
        public string InsCity { get; set; }
        public string InsState { get; set; }
        public string InsZip { get; set; }
        public string InsType { get; set; }
        public string InsValue { get; set; }


    }
}
