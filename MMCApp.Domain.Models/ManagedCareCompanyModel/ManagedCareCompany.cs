using System.ComponentModel.DataAnnotations;

namespace MMCApp.Domain.Models.ManagedCareCompanyModel
{
    public class ManagedCareCompany
    {
     
        public int CompanyId { get; set; }
        [Required]
        public string CompName { get; set; }
        [Required]
        public string CompAddress { get; set; }
        public string CompAddress2 { get; set; }
        [Required]
        public string CompCity { get; set; }
        [Required]
        public int CompStateId { get; set; }
        [Required]
        public string CompZip { get; set; }
        public string CompStateName { get; set; }
    }
}
