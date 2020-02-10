using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class PatientClaim
    {
        [Key]
        public int PatientClaimID { get; set; }
        public int PatientID { get; set; }
        public string PatClaimNumber { get; set; }
        public DateTime? PatDOI { get; set; }
        public string PatPolicyYear { get; set; }
        public int PatClaimJurisdictionId { get; set; }
        public string PatAdjudicationStateCaseNumber { get; set; }
        public string PatEDIExchangeTrackingNumber { get; set; }
        /*public string PatInjuryType { get; set; }
        public string PatInjuryDescription { get; set; }*/
        public DateTime? PatInjuryReportedDate { get; set; }
        public int PatAdjusterID { get; set; }
        public int? PatApplicantAttorneyID { get; set; }
        public int? PatDefenseAttorneyID { get; set; }
        public int PatClientID { get; set; }
        public int? PatPhysicianID { get; set; }
        public int? PatCaseManagerID { get; set; }
        public int PatEmployerID { get; set; }
        public int? PatEmpSubsidiaryID { get; set; }
        public int? PatInsurerID { get; set; }
        public int? PatInsuranceBranchID { get; set; }
        public int? PatTPAID { get; set; }
        public int? PatTPABranchID { get; set; }
        public int? PatADRID { get; set; }

        [ForeignKey("PatientID")]
        public Patient patients { get; set; }

        [ForeignKey("PatClaimJurisdictionId")]
        public State states { get; set; }

        [ForeignKey("PatAdjusterID")]
        public Adjuster adjusters { get; set; }

        [ForeignKey("PatApplicantAttorneyID")]
        public Attorney attoneys { get; set; }

        [ForeignKey("PatDefenseAttorneyID")]
        public Attorney defattoneys { get; set; }

        [ForeignKey("PatClientID")]
        public Attorney clients { get; set; }

        [ForeignKey("PatEmployerID")]
        public Employer employers { get; set; }

        [ForeignKey("PatInsurerID")]
        public Insurer insurers { get; set; }

        [ForeignKey("PatInsuranceBranchID")]
        public InsuranceBranch insuranceBranchs { get; set; }

        [ForeignKey("PatTPAID")]
        public ThirdPartyAdministrator thirdPartyAdministrators { get; set; }

        [ForeignKey("PatTPABranchID")]
        public ThirdPartyAdministratorBranch thirdPartyAdministratorBranchs { get; set; }

        [ForeignKey("PatADRID")]
        public ADR ADRs { get; set; }

        [ForeignKey("PatEmpSubsidiaryID")]
        public EmployerSubsidiary employerSubsidiarys { get; set; }
    }
}
