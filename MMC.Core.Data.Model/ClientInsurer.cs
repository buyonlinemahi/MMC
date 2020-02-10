using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MMC.Core.Data.Model
{
    public class ClientInsurer
    {
          [Key]
        public int ClientInsurerID { get; set; }
        public int ClientID { get; set; }
        public int InsurerID { get; set; }

        [ForeignKey("ClientID")]
        public Client clients { get; set; }

        [ForeignKey("InsurerID")]
        public Insurer insurers { get; set; }
    }
}
