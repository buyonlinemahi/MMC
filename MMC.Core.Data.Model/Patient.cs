using System;

namespace MMC.Core.Data.Model
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatFirstName { get; set; }
        public string PatLastName { get; set; }
        public string PatSSN { get; set; }
        public DateTime PatDOB { get; set; }
        public string PatGender { get; set; }
        public string PatAddress1 { get; set; }
        public string PatAddress2 { get; set; }
        public string PatCity { get; set; }
        public int PatStateId { get; set; }
        public string PatZip { get; set; }
        public string PatPhone { get; set; }
        public string PatFax { get; set; }
        public string PatEMail { get; set; }
        public string PatEthnicBackground { get; set; }
        public int PatPrimaryLanguageId { get; set; }
        public int PatSecondaryLanguageId { get; set; }
        public string PatMaritalStatus { get; set; }
        public string PatMedicareEligible { get; set; }
    }
}
