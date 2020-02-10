using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class ClientEmployer
    {
          [Key]
        public int ClientEmployerID { get; set; }
        public int ClientID { get; set; }
        public int EmployerID { get; set; }

        [ForeignKey("ClientID")]
        public Client clients { get; set; }

        [ForeignKey("EmployerID")]
        public Employer employers { get; set; }
    }
}
