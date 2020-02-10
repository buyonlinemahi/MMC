using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientClaim
    {
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public string PatClaimNumber { get; set; }
        [DataMember]
        public DateTime? PatDOI { get; set; }
        [DataMember]
        public string PatPolicyYear { get; set; }
        [DataMember]
        public int PatClaimJurisdictionId { get; set; }
        [DataMember]
        public string PatAdjudicationStateCaseNumber { get; set; }
        [DataMember]
        public string PatEDIExchangeTrackingNumber { get; set; }
        //[DataMember]
        //public string PatInjuryType { get; set; }
        //[DataMember]
        //public string PatInjuryDescription { get; set; }
        [DataMember]
        public DateTime? PatInjuryReportedDate { get; set; }
        [DataMember]
        public int PatAdjusterID { get; set; }
        [DataMember]
        public int? PatApplicantAttorneyID { get; set; }
        [DataMember]
        public int PatClientID { get; set; }
        [DataMember]
        public int? PatDefenseAttorneyID { get; set; }
        [DataMember]
        public int? PatPhysicianID { get; set; }
        [DataMember]
        public int? PatCaseManagerID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        // Patient Client Name and Claim Administrator
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string ClaimAdministratorName { get; set; }

        //claim status detail
        [DataMember]
        public int ClaimStatusID { get; set; }
        [DataMember]
        public int PatientClaimStatusID { get; set; }
        [DataMember]
        public string DeniedRationale { get; set; }

        //Dropdown Feild
        [DataMember]
        public int PatEmployerID { get; set; }
        [DataMember]
        public int PatEmpSubsidiaryID { get; set; }
        [DataMember]
        public int? PatInsurerID { get; set; }
        [DataMember]
        public int? PatInsuranceBranchID { get; set; }
        [DataMember]
        public int? PatTPAID { get; set; }
        [DataMember]
        public int? PatTPABranchID { get; set; }
        [DataMember]
        public string PatInsValue { get; set; }
        [DataMember]
        public string PatTPAValue { get; set; }
        [DataMember]
        public int? PatADRID { get; set; }
    }
}
