using System;
using System.ComponentModel.DataAnnotations;
namespace MMCApp.Domain.Models.PatientModel
{
    public class Patient
    {
        public int PatientID { get; set; }
        [Required]
        public string PatFirstName { get; set; }
        [Required]
        public string PatLastName { get; set; }
        [Required]
        public string PatSSN { get; set; }
        [Required]
        public DateTime PatDOB { get; set; }
        [Required]
        public string PatGender { get; set; }      

        [Required]
        public string PatAddress1 { get; set; }
        public string PatAddress2 { get; set; }
        [Required]
        public string PatCity { get; set; }
        [Required]
        public int PatStateId { get; set; }
        [Required]
        public string PatZip { get; set; }
        [Required]
        public string PatPhone { get; set; }
        [Required]
        public string PatFax { get; set; }
        [Required]
        public string PatEMail { get; set; }
        [Required]
        public string PatEthnicBackground { get; set; }
        [Required]
        public int PatPrimaryLanguageId { get; set; }
        [Required]
        public int PatSecondaryLanguageId { get; set; }
        [Required]
        public string PatMaritalStatus { get; set; }
        [Required]
        public string PatMedicareEligible { get; set; }
        public string PatClaimNumber { get; set; }
        public DateTime? PatDOI { get; set; }
        public int PatientClaimID { get; set; }
    }
}
