    using MMC.Core.Data.Model;
using System.Data.Entity;

namespace MMC.Core.Data.SQLServer
{

    public class MMCDbContext : DbContext
    {
        public MMCDbContext()
            : base("MMCDbContext")
        {

        }
        #region Paticipant(s)
        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Physician> physicians { get; set; }
        public virtual DbSet<Employer> employers { get; set; }
        public virtual DbSet<Insurer> insurers { get; set; }
        public virtual DbSet<Vendor> vendors { get; set; }
        public virtual DbSet<CaseManager> caseManagers { get; set; }
        public virtual DbSet<Adjuster> adjusters { get; set; }
        public virtual DbSet<InsuranceBranch> insuranceBranches { get; set; }
        public virtual DbSet<ThirdPartyAdministrator> thirdPartyAdministrators { get; set; }
        public virtual DbSet<ThirdPartyAdministratorBranch> thirdPartyAdministratorBranches { get; set; }
        public virtual DbSet<ManagedCareCompany> managedCareCompanies { get; set; }
        public virtual DbSet<MedicalGroup> medicalGroups { get; set; }
        public virtual DbSet<EmployerSubsidiary> employerSubsidiaries { get; set; }
        public virtual DbSet<RFASplittedReferralHistory> rfaSplittedReferralHistories { get; set; }
        public virtual DbSet<RFAMergedReferralHistory> rfaMergedReferralHistories { get; set; }
        public virtual DbSet<Attorney> attorneys { get; set; }
        public virtual DbSet<AttorneyFirm> attorneyFirms { get; set; }
        public virtual DbSet<AttorneyFirmType> attorneyFirmTypes { get; set; }
        public virtual DbSet<PeerReview> peerReviews { get; set; }
        #endregion

        #region Static Table
        public virtual DbSet<State> states { get; set; }
        public virtual DbSet<Specialty> specialties { get; set; }
        public virtual DbSet<Language> languages { get; set; }
        public virtual DbSet<CurrentMedicalCondition> currentMedicalConditions { get; set; }
        public virtual DbSet<ClaimStatus> claimStatuses { get; set; }
        public virtual DbSet<ICD9Code> iCD9Codes { get; set; }
        public virtual DbSet<ICD10Code> iCD10Codes { get; set; }
        public virtual DbSet<BodyPart> bodyParts { get; set; }
        public virtual DbSet<BodyPartStatus> BodyPartStatuses { get; set; }
        public virtual DbSet<FrequencyType> frequencyTypes { get; set; }
        public virtual DbSet<RequestType> requestTypes { get; set; }
        public virtual DbSet<TreatmentCategory> treatmentCategories { get; set; }
        public virtual DbSet<TreatmentType> treatmentTypes { get; set; }
        public virtual DbSet<DocumentCategory> documentCategories { get; set; }
        public virtual DbSet<DocumentType> documentTypes { get; set; }
        public virtual DbSet<DurationType> durationTypes { get; set; }
        public virtual DbSet<FileType> fileTypes { get; set; }
        public virtual DbSet<BillingRateType> billingRateTypes { get; set; }
        public virtual DbSet<Decision> decisions { get; set; }
        public virtual DbSet<IMRDecision> imrDecision { get; set; }
        public virtual DbSet<Status> statuses { get; set; }
        #endregion

        #region Patients

        public virtual DbSet<Patient> patients { get; set; }
        public virtual DbSet<PatientCurrentMedicalCondition> patientCurrentMedicalConditions { get; set; }
        public virtual DbSet<PatientClaim> patientClaims { get; set; }
        public virtual DbSet<PatientClaimAddOnBodyPart> patientClaimAddOnBodyParts { get; set; }
        public virtual DbSet<PatientClaimPleadBodyPart> patientClaimPleadBodyParts { get; set; }
        public virtual DbSet<PatientClaimDiagnose> patientClaimDiagnoses { get; set; }
        public virtual DbSet<PatientClaimStatus> patientClaimStatuses { get; set; }
        public virtual DbSet<PatientMedicalRecord> patientMedicalRecords { get; set; }
        public virtual DbSet<PatientOccupational> patientOccupationalss { get; set; }
        
        #endregion

        #region Intake
        public virtual DbSet<RFAReferral> rfaReferrals { get; set; }
        public virtual DbSet<RFARequestModify> RFARequestModifies { get; set; }
        public virtual DbSet<RFARequestAdditionalInfo> RFARequestAdditionalInfoes { get; set; }
        public virtual DbSet<RFAReferralAdditionalInfo> RFAReferralAdditionalInfoes { get; set; }
        public virtual DbSet<RFAReferralFile> rfaReferralFiles { get; set; }
        public virtual DbSet<RFAReferralInvoice> rFAReferralInvoices { get; set; }
        public virtual DbSet<RFARequest> rfaRequests { get; set; }
        public virtual DbSet<RFARequestCPTCode> rFARequestCPTCodes { get; set; }
        public virtual DbSet<RFAProcessLevels> rfaProcessLevels { get; set; }
        public virtual DbSet<RFARecordSplitting> rfaRecordSplitings { get; set; }
        public virtual DbSet<RFAPatMedicalRecordReview> rfaPatMedicalRecordReviews { get; set; }
        public virtual DbSet<Note> notes { get; set; }
        public virtual DbSet<RFARequestBilling> RFARequestBillings { get; set; }
        public virtual DbSet<RFARequestTimeExtension> RFARequestTimeExtensions { get; set; }
        public virtual DbSet<RFARequestBodyPart> RFARequestBodyParts { get; set; }


        #endregion

        #region RFAReferral
        public virtual DbSet<RFAAdditionalInfo> rfaAdditionalInfo { get; set; }
        #endregion

        #region Client
        public virtual DbSet<Client> clients { get; set; }
        public virtual DbSet<ClientInsurer> clientinsurers { get; set; }
        public virtual DbSet<ClientInsuranceBranch> clientInsurerBranchs { get; set; }
        public virtual DbSet<ClientEmployer> clientemployers { get; set; }
        public virtual DbSet<ClientThirdPartyAdministrator> clientthirdpartyadministrators { get; set; }
        public virtual DbSet<ClientThirdPartyAdministratorBranch> clientThirdPartyAdministratorBranchs { get; set; }
        public virtual DbSet<ClientManagedCareCompany> clientmanagedcarecompanys { get; set; }

        public virtual DbSet<ClientBilling> clientBillings { get; set; }
        public virtual DbSet<ClientBillingRetailRate> clientBillingRetailsRates { get; set; }
        public virtual DbSet<ClientBillingWholesaleRate> clientBillingWholesaleRates { get; set; }
        public virtual DbSet<ClientPrivateLabel> clientPrivateLabelss { get; set; }
        #endregion

        #region ADR
        public virtual DbSet<ADR> ADRs { get; set; }
        #endregion

        #region IMR
        public virtual DbSet<IMRRFAReferral> IMRRFAReferrals { get; set; }
        public virtual DbSet<IMRRFARequest> IMRRFARequests { get; set; }        
        #endregion

        #region ClientStatement
        public virtual DbSet<ClientStatement> ClientStatements { get; set; }
        #endregion

        #region EmailRecord & Attachment
        public virtual DbSet<EmailRecord> emailRecords { get; set; }
        public virtual DbSet<EmailRecordAttachment> emailRecordAttachments { get; set; }
        #endregion 

        #region CurrentWorkload
        public virtual DbSet<CurrentWorkloadReportParameter> CurrentWorkloadReportParameters { get; set; }
        public virtual DbSet<CurrentWorkloadReport> CurrentWorkloadReport { get; set; }
        #endregion
    }
}