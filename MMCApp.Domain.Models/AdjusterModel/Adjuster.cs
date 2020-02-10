using System.ComponentModel.DataAnnotations;

namespace MMCApp.Domain.Models.AdjusterModel
{
    public class Adjuster
    { 
        
        public int? AdjusterID { get; set; }
        [Required]
        public string AdjFirstName { get; set; }
        [Required]
        public string AdjLastName { get; set; }
        [Required]
        public string AdjAddress1 { get; set; }
        public string AdjAddress2 { get; set; }
        [Required]
        public int AdjStateId { get; set; }
        [Required]
        public string AdjZip { get; set; }
        [Required]
        public string AdjPhone { get; set; }
        [Required]
        public string AdjFax { get; set; }
        [Required]
        public string AdjEMail { get; set; }
        [Required]
        public string AdjCity { get; set; }
        public string AdjStateName { get; set; }
        public int? ClientID { get; set; }
        public string AdjusterName { get; set; }
    }
}
