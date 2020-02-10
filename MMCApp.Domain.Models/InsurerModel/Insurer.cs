using System.ComponentModel.DataAnnotations;
namespace MMCApp.Domain.Models.InsurerModel
{
    public class Insurer
    {
        [Required]
        public int InsurerID { get; set; }
        [Required]
        public string InsName { get; set; }
        [Required]
        public string InsAddress1 { get; set; }
        public string InsAddress2 { get; set; }
        [Required]
        public string InsCity { get; set; }
        [Required]
        public int InsStateId { get; set; }
        [Required]
        public string InsZip { get; set; }      
        public string InsEMail { get; set; }
        public string InsPhone { get; set; }
        public string InsFax { get; set; }       
        public string InsContactName { get; set; }
        public string InsPhoneExtension { get; set; }
        public string InsStateName { get; set; }
        public string AlertMessage { get; set; }
    }
}
