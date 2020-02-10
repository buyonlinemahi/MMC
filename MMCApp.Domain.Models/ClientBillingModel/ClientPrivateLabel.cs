
using System.Web;
namespace MMCApp.Domain.Models.ClientBillingModel
{
    public class ClientPrivateLabel
    {
        public int ClientPrivateLabelID { get; set; }
        public int ClientID { get; set; }
        public string ClientPrivateLabelName { get; set; }
        public string ClientPrivateLabelAddress { get; set; }
        public string ClientPrivateLabelCity { get; set; }
        public int ClientPrivateLabelStateID { get; set; }
        public string ClientPrivateLabelZip { get; set; }
        public HttpPostedFileBase ClientPrivateLabelLogo { get; set; }
        public string ClientPrivateLabelLogoName { get; set; }
        public string ClientPrivateLabelPhone { get; set; }
        public string ClientPrivateLabelFax { get; set; }
        public string ClientPrivateLabelTax { get; set; }
        public string ClientStatementStart { get; set; }
        public string ClientPrivateEmailID { get; set; }
    }
}
