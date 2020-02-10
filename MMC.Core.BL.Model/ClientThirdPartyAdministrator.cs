
namespace MMC.Core.BL.Model
{
    public class ClientThirdPartyAdministrator
    {
        public int ClientTPAID { get; set; }
        public int ClientID { get; set; }
        public int TPAID { get; set; }

        public string ClientName { get; set; }
        public string TPAName { get; set; }
        public string TPAAddress { get; set; }
        public string TPACity { get; set; }
        public string TPAZip { get; set; }
        public string TPAState { get; set; }
        public string TPAType { get; set; }

    }
}
