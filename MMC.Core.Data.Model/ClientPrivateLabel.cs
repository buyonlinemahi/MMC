using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class ClientPrivateLabel
    {
        [Key]
        public int ClientPrivateLabelID { get; set; }
        public int ClientID { get; set; }
        public string ClientPrivateLabelName { get; set; }
        public string ClientPrivateLabelAddress { get; set; }
        public string ClientPrivateLabelCity { get; set; }
        public int ClientPrivateLabelStateID { get; set; }
        public string ClientPrivateLabelZip { get; set; }
        public string ClientPrivateLabelLogoName { get; set; }
        public string ClientPrivateLabelPhone { get; set; }
        public string ClientPrivateLabelFax { get; set; }
        public string ClientPrivateLabelTax { get; set; }
        public string ClientStatementStart { get; set; }
        public string ClientPrivateEmailID { get; set; }
        [ForeignKey("ClientID")]
        public Client clients { get; set; }

        [ForeignKey("ClientPrivateLabelStateID")]
        public State states { get; set; }
    }
}
