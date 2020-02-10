using System.ComponentModel.DataAnnotations;

namespace MMC.Core.Data.Model
{
    public class ManagedCareCompany
    {      
        [Key]
        public int CompanyId { get; set; }
        public string CompName  { get; set; }
        public string CompAddress  { get; set; }
        public string CompAddress2  { get; set; }
        public string CompCity  { get; set; }
        public int CompStateId  { get; set; }
        public string CompZip { get; set; }

    }
}
