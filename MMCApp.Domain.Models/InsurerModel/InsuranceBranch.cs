using System.ComponentModel.DataAnnotations;

namespace MMCApp.Domain.Models.InsurerModel
{
   public class InsuranceBranch
    {
        public int? InsuranceBranchID { get; set; }
        [Required]
        public int InsurerId { get; set; }
        [Required]
        public string InsBranchName { get; set; }
        [Required]
        public string InsBranchAddress { get; set; }
        [Required]
        public string InsBranchCity { get; set; }
        [Required]
        public int InsBranchStateId { get; set; }
        [Required]
        public string InsBranchZip { get; set; }
        public string InsBranchStateName { get; set; }
    }
}
