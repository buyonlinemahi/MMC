
namespace MMCApp.Domain.Models.ClientModel
{
    public class ClientThirdPartyAdministrator
    {
        public int ClientTPAID { get; set; }
        public int ClientID { get; set; }
        public int TPAID { get; set; }

        public int ClientName { get; set; }
        public string TPAName { get; set; }
        public string TPAAddress { get; set; }
        public string TPACity { get; set; }
        public string TPAZip { get; set; }
        public string TPAState { get; set; }
        public string TPAType { get; set; }
        public string TPAValue { get; set; }
    }
}
