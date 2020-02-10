using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class Patient
    {
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public string PatFirstName { get; set; }
        [DataMember]
        public string PatLastName { get; set; }
        [DataMember]
        public string PatSSN { get; set; }
        [DataMember]
        public DateTime PatDOB { get; set; }
        [DataMember]
        public string PatGender { get; set; }
        [DataMember]
        public string PatAddress1 { get; set; }
        [DataMember]
        public string PatAddress2 { get; set; }
        [DataMember]
        public string PatCity { get; set; }
        [DataMember]
        public int PatStateId { get; set; }
        [DataMember]
        public string PatZip { get; set; }
        [DataMember]
        public string PatPhone { get; set; }
        [DataMember]
        public string PatFax { get; set; }
        [DataMember]
        public string PatEMail { get; set; }
        [DataMember]
        public string PatEthnicBackground { get; set; }
        [DataMember]
        public int PatPrimaryLanguageId { get; set; }
        [DataMember]
        public int PatSecondaryLanguageId { get; set; }
        [DataMember]
        public string PatMaritalStatus { get; set; }
        [DataMember]
        public string PatMedicareEligible { get; set; }

        [DataMember]
        public int PatAge { get; set; }
        
        // State Name
        [DataMember]
        public string PatStateName { get; set; }

        // Patient Claim Number
        [DataMember]
        public string PatClaimNumber { get; set; }

        //Patient DOI
        [DataMember]
        public DateTime? PatDOI { get; set; }

        [DataMember]
        public int PatientClaimID { get; set; }
    }
}
