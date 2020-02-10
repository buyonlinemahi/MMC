using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class ClientThirdPartyAdministrator
    {
          [Key]
        public int ClientTPAID { get; set; }
        public int ClientID { get; set; }
        public int TPAID { get; set; }

        [ForeignKey("ClientID")]
        public Client clients { get; set; }

        [ForeignKey("TPAID")]
        public ThirdPartyAdministrator thirdpartyadministrators { get; set; }
    }
}
