using System;

namespace MMCApp.Domain.Models.PatientModel
{
    public class PatientClaim
    {
        public int PatientClaimID { get; set; }
        public int PatientID { get; set; }
        public string PatClaimNumber { get; set; }
        public DateTime? PatDOI { get; set; }
        public string PatPolicyYear { get; set; }
        public int PatClaimJurisdictionId { get; set; }
        public string PatAdjudicationStateCaseNumber { get; set; }
        public string PatEDIExchangeTrackingNumber { get; set; }
        //public string PatInjuryType { get; set; }
        //public string PatInjuryDescription { get; set; }
        public DateTime? PatInjuryReportedDate { get; set; }
        public int PatAdjusterID { get; set; }
        public int? PatApplicantAttorneyID { get; set; }
        public int? PatDefenseAttorneyID { get; set; }
        public int PatClientID { get; set; }
        public int? PatPhysicianID { get; set; }
        public int? PatCaseManagerID { get; set; }
        public string PatientName { get; set; }
        // Patient Client Name and Claim Administrator
        public string ClientName { get; set; }
        public string ClaimAdministratorName { get; set; }
        //claim status detail
        public int ClaimStatusID { get; set; }
        public int PatientClaimStatusID { get; set; }
        public string DeniedRationale { get; set; }
        public int PatEmployerID { get; set; }
        public int? PatEmpSubsidiaryID { get; set; }
        public int? PatInsurerID { get; set; }
        public int? PatInsuranceBranchID { get; set; }
        public int? PatTPAID { get; set; }
        public int? PatTPABranchID { get; set; }
        public string PatInsValue { get; set; }
        public string PatTPAValue { get; set; }
        public int? PatADRID { get; set; }
    }
    public class PatientClaimDropDown
    {
        public int PatientClaimID { get; set; }
        public string PatClaimNumber { get; set; }
    }
    
}
