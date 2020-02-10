using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MMC.Core.Data.Model
{
    public class ClientManagedCareCompany
    {
          [Key]
        public int ClientCompanyID { get; set; }
        public int ClientID { get; set; }
        public int CompanyID { get; set; }

        [ForeignKey("ClientID")]
        public Client clients { get; set; }

        [ForeignKey("CompanyID")]
        public ManagedCareCompany managedcarecompanys { get; set; }
    }
}
