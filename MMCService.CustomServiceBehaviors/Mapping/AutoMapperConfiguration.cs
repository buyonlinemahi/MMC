using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace MMCService.CustomServiceBehaviors.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static void Init()
        {
            #region AdditionalDocument
            Mapper.CreateMap<MMC.Core.BL.Model.AdditionalDocument, DTO.AdditionalDocument>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.AdditionalDocument, DTO.Paged.PagedAdditionalDocument>().ReverseMap();
            #endregion

            #region Attorney
            Mapper.CreateMap<MMC.Core.Data.Model.AttorneyFirmType, DTO.AttorneyFirmType>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.Attorney, DTO.Attorney>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Attorney, DTO.Attorney>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Attorney, DTO.Paged.PagedAttorney>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.AttorneyFirm, DTO.AttorneyFirm>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.AttorneyFirm, DTO.AttorneyFirm>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.AttorneyFirm, DTO.Paged.PagedAttorneyFirm>().ReverseMap();
            #endregion
            #region UserMap
            Mapper.CreateMap<MMC.Core.Data.Model.User, DTO.User>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.User, DTO.Paged.PagedUser>().ReverseMap();
            #endregion
            #region PhysicianMap
            Mapper.CreateMap<MMC.Core.Data.Model.Physician, DTO.Physician>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Physician, DTO.Physician>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Physician, DTO.Paged.PagedPhysician>().ReverseMap();
            #endregion
            #region StateMap
            Mapper.CreateMap<MMC.Core.Data.Model.State, DTO.State>().ReverseMap();
            #endregion
            #region AdjusterMap
            Mapper.CreateMap<MMC.Core.Data.Model.Adjuster, DTO.Adjuster>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Adjuster, DTO.Adjuster>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Adjuster, DTO.Paged.PagedAdjuster>().ReverseMap();
            #endregion
            #region SpecialtyMap
            Mapper.CreateMap<MMC.Core.Data.Model.Specialty, DTO.Specialty>().ReverseMap();
            #endregion
            #region StateMap
            Mapper.CreateMap<MMC.Core.Data.Model.State, DTO.State>().ReverseMap();
            #endregion
            #region VendorMap
            Mapper.CreateMap<MMC.Core.Data.Model.Vendor, DTO.Vendor>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Vendor, DTO.Paged.PagedVendor>().ReverseMap();
            #endregion
            #region Insurer Map
            Mapper.CreateMap<MMC.Core.Data.Model.Insurer, DTO.Insurer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Insurer, DTO.Paged.PagedInsurer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Insurer, DTO.Insurer>().ReverseMap();
            #endregion
            #region InsuranceBranch Map
            Mapper.CreateMap<MMC.Core.Data.Model.InsuranceBranch, DTO.InsuranceBranch>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.InsuranceBranch, DTO.Paged.PagedInsuranceBranch>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.InsuranceBranch, DTO.InsuranceBranch>().ReverseMap();
            #endregion
            #region EmployerMap
            Mapper.CreateMap<MMC.Core.Data.Model.Employer, DTO.Employer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Employer, DTO.Employer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Employer, DTO.Paged.PagedEmployer>().ReverseMap();
            #endregion
            #region EmployerSubsidiaries
            Mapper.CreateMap<MMC.Core.Data.Model.EmployerSubsidiary, DTO.EmployerSubsidiary>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.EmployerSubsidiary, DTO.Paged.PagedEmployerSubsidiary>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.EmployerSubsidiary, DTO.EmployerSubsidiary>().ReverseMap();
            #endregion
            #region PatientMap
            Mapper.CreateMap<MMC.Core.Data.Model.Patient, DTO.Patient>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Patient, DTO.Patient>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Patient, DTO.Paged.PagedPatient>().ReverseMap();
            #endregion
            #region PatientClaimMap
            Mapper.CreateMap<MMC.Core.Data.Model.PatientClaim, DTO.PatientClaim>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientClaim, DTO.PatientClaim>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PatientClaim, DTO.Paged.PagedPatientClaim>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientClaimBodyPartByBPStatusID, DTO.PatientClaimBodyPartByBPStatusID>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PatientClaimBodyPartByBPStatusID, DTO.Paged.PagedPatientClaimBodyPartByBPStatusID>().ReverseMap();
            #endregion
            #region PatientClaimAddOnBodyPart Map
            Mapper.CreateMap<MMC.Core.Data.Model.PatientClaimAddOnBodyPart, DTO.PatientClaimAddOnBodyPart>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientClaimAddOnBodyPart, DTO.PatientClaimAddOnBodyPart>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PatientClaimAddOnBodyPart, DTO.Paged.PagedPatientClaimAddOnBodyPart>().ReverseMap();
            #endregion
            #region PatientCurrentMedicalCondition Map
            Mapper.CreateMap<MMC.Core.Data.Model.PatientCurrentMedicalCondition, DTO.PatientCurrentMedicalCondition>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientCurrentMedicalCondition, DTO.PatientCurrentMedicalCondition>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PatientCurrentMedicalCondition, DTO.Paged.PagedPatientCurrentMedicalCondition>().ReverseMap();
            #endregion
            #region PatientClaimPleadBodyPart Map
            Mapper.CreateMap<MMC.Core.Data.Model.PatientClaimPleadBodyPart, DTO.PatientClaimPleadBodyPart>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientClaimPleadBodyPart, DTO.PatientClaimPleadBodyPart>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PatientClaimPleadBodyPart, DTO.Paged.PagedPatientClaimPleadBodyPart>().ReverseMap();
            #endregion
            #region PatientClaimDiagnose Map
            Mapper.CreateMap<MMC.Core.Data.Model.PatientClaimDiagnose, DTO.PatientClaimDiagnose>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientClaimDiagnose, DTO.PatientClaimDiagnose>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PatientClaimDiagnose, DTO.Paged.PagedPatientClaimDiagnose>().ReverseMap();
            #endregion
            #region PatientClaimStatus Map
            Mapper.CreateMap<MMC.Core.Data.Model.PatientClaimStatus, DTO.PatientClaimStatus>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientClaimStatus, DTO.PatientClaimStatus>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PatientClaimStatus, DTO.Paged.PagedPatientClaimStatus>().ReverseMap();
            #endregion
            #region PatientMedicalRecord
            Mapper.CreateMap<MMC.Core.BL.Model.PatientMedicalRecordByPatientID, DTO.PatientMedicalRecordByPatientID>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PatientMedicalRecordByPatientID, DTO.Paged.PagedPatientMedicalRecordByPatientID>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.PatientMedicalRecord, DTO.PatientMedicalRecord>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientMedicalRecordTemplateByPatientID, DTO.PatientMedicalRecordTemplateByPatientID>().ReverseMap();
            #endregion
            #region LanguageMap
            Mapper.CreateMap<MMC.Core.Data.Model.Language, DTO.Language>().ReverseMap();
            #endregion
            #region FileTypeMap
            Mapper.CreateMap<MMC.Core.Data.Model.FileType, DTO.FileType>().ReverseMap();
            #endregion
            #region CurrentMedicalConditionMap
            Mapper.CreateMap<MMC.Core.Data.Model.CurrentMedicalCondition, DTO.CurrentMedicalCondition>().ReverseMap();
            #endregion
            #region ICDCodeMap
            Mapper.CreateMap<MMC.Core.Data.Model.ICD9Code, DTO.ICD9Code>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ICD9Code, DTO.ICD9Code>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ICD9Code, DTO.Paged.PagedICD9Code>().ReverseMap();

            Mapper.CreateMap<MMC.Core.Data.Model.ICDCode, DTO.ICDCode>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ICDCode, DTO.ICDCode>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ICDCode, DTO.Paged.PagedICDCode>().ReverseMap();

            Mapper.CreateMap<MMC.Core.Data.Model.ICD10Code, DTO.ICD10Code>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ICD10Code, DTO.ICD10Code>().ReverseMap();
            #endregion
            #region ClaimStatusMap
            Mapper.CreateMap<MMC.Core.Data.Model.ClaimStatus, DTO.ClaimStatus>().ReverseMap();
            #endregion
            #region BodyPartMap
            Mapper.CreateMap<MMC.Core.Data.Model.BodyPart, DTO.BodyPart>().ReverseMap();
            #endregion
            #region BodyPart
            Mapper.CreateMap<MMC.Core.Data.Model.BodyPartStatus, DTO.BodyPartStatus>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.BodyPartDetail, DTO.BodyPartDetail>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.BodyPartDetail, DTO.Paged.PagedBodyPartDetail>().ReverseMap();
            #endregion
            #region CaseManager
            Mapper.CreateMap<MMC.Core.Data.Model.CaseManager, DTO.CaseManager>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.CaseManager, DTO.CaseManager>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.CaseManager, DTO.Paged.PagedCaseManager>().ReverseMap();
            #endregion
            #region ThirdPartyAdministrator
            Mapper.CreateMap<MMC.Core.Data.Model.ThirdPartyAdministrator, DTO.ThirdPartyAdministrator>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ThirdPartyAdministrator, DTO.ThirdPartyAdministrator>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ThirdPartyAdministrator, DTO.Paged.PagedThirdPartyAdministrator>().ReverseMap();
            #endregion
            #region ThirdPartyAdministratorBranch
            Mapper.CreateMap<MMC.Core.Data.Model.ThirdPartyAdministratorBranch, DTO.ThirdPartyAdministratorBranch>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ThirdPartyAdministratorBranch, DTO.ThirdPartyAdministratorBranch>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ThirdPartyAdministratorBranch, DTO.Paged.PagedThirdPartyAdministratorBranch>().ReverseMap();
            #endregion
            #region ManagedCareCompanyMap
            Mapper.CreateMap<MMC.Core.Data.Model.ManagedCareCompany, DTO.ManagedCareCompany>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ManagedCareCompany, DTO.ManagedCareCompany>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ManagedCareCompany, DTO.Paged.PagedManagedCareCompany>().ReverseMap();
            #endregion
            #region Intake
            Mapper.CreateMap<MMC.Core.Data.Model.RFAReferral, DTO.RFAReferral>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.RFAReferralFile, DTO.RFAReferralFile>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.RFAReferralFile, DTO.RFAReferralFile>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.RFARequest, DTO.RFARequest>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.RFARequestAdditionalInfo, DTO.RFARequestAdditionalInfo>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.RFAReferralAdditionalInfo, DTO.RFAReferralAdditionalInfo>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.RFARequestAdditionalInfoDetail, DTO.RFARequestAdditionalInfoDetail>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.RFARequest, DTO.RFARequest>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.RFARequestCPTCode, DTO.RFARequestCPTCode>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.RFARecordSplitting, DTO.RFARecordSplitting>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientDetailsByReferralID, DTO.PatientDetailsByReferralID>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.IncompleteReferrals, DTO.IncompleteReferrals>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.IncompleteReferrals, DTO.Paged.PagedIncompleteReferrals>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.FrequencyType, DTO.FrequencyType>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.DurationType, DTO.DurationType>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.RequestType, DTO.RequestType>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.TreatmentCategory, DTO.TreatmentCategory>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.TreatmentType, DTO.TreatmentType>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.DocumentCategory, DTO.DocumentCategory>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.DocumentType, DTO.DocumentType>().ReverseMap();
            #endregion
            #region MedicalGroup
            Mapper.CreateMap<MMC.Core.Data.Model.MedicalGroup, DTO.MedicalGroup>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.MedicalGroup, DTO.MedicalGroup>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.MedicalGroup, DTO.Paged.PagedMedicalGroup>().ReverseMap();
            #endregion
            #region ClinicalTriage
            Mapper.CreateMap<MMC.Core.BL.Model.ClinicalTriage, DTO.ClinicalTriage>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ClinicalTriage, DTO.Paged.PagedClinicalTriage>().ReverseMap();
            #endregion
            #region ReferralProcess
            Mapper.CreateMap<MMC.Core.BL.Model.PatientAndRequestModel, DTO.PatientAndRequestModel>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.PatientByReferralID, DTO.PatientByReferralID>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.RequestByReferralID, DTO.RequestByReferralID>().ReverseMap();
            #endregion
            #region Notification
            Mapper.CreateMap<MMC.Core.BL.Model.Notification, DTO.Notification>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.NotificationEmailSend, DTO.NotificationEmailSend>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Notification, DTO.Paged.PagedNotification>().ReverseMap();
            #endregion
            #region AddtionalInfo
            Mapper.CreateMap<MMC.Core.Data.Model.RFAAdditionalInfo, DTO.RFAAdditionalInfo>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.RFAAdditionalInfo, DTO.Paged.PagedRFAAdditionalInfo>().ReverseMap();
            #endregion
            #region  RFADiagnosis
            Mapper.CreateMap<MMC.Core.BL.Model.RFADiagnosis, DTO.RFADiagnosis>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.RFADiagnosis, DTO.Paged.PagedRFADiagnosis>().ReverseMap();
            #endregion
            #region  MedicalRecordReview
            Mapper.CreateMap<MMC.Core.Data.Model.RFAPatMedicalRecordReview, DTO.RFAPatMedicalRecordReview>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.RFAPatMedicalRecordReviewDetail, DTO.RFAPatMedicalRecordReviewDetail>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.RFAPatMedicalRecordReview, DTO.Paged.PagedRFAPatMedicalRecordReview>().ReverseMap();
            #endregion
            #region  Letters
            Mapper.CreateMap<MMC.Core.BL.Model.InitialNotificationLetter, DTO.InitialNotificationLetter>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.RequestInitialNotificationLetter, DTO.RequestInitialNotificationLetter>().ReverseMap();
            #endregion
            #region Client(s)
            Mapper.CreateMap<MMC.Core.Data.Model.Client, DTO.Client>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Client, DTO.Client>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Client, DTO.Paged.PagedClient>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.AdjusterByClientID, DTO.AdjusterByClientID>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClaimAdministratorAllByClientID, DTO.ClaimAdministratorAllByClientID>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.ClientInsurer, DTO.ClientInsurer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientInsurer, DTO.ClientInsurer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ClientInsurer, DTO.Paged.PagedClientInsurer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.ClientInsuranceBranch, DTO.ClientInsuranceBranch>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientInsuranceBranch, DTO.ClientInsuranceBranch>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.ClientEmployer, DTO.ClientEmployer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientEmployer, DTO.ClientEmployer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ClientEmployer, DTO.Paged.PagedClientEmployer>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.ClientThirdPartyAdministrator, DTO.ClientThirdPartyAdministrator>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientThirdPartyAdministrator, DTO.ClientThirdPartyAdministrator>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ClientThirdPartyAdministrator, DTO.Paged.PagedClientThirdPartyAdministrator>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.ClientThirdPartyAdministratorBranch, DTO.ClientThirdPartyAdministratorBranch>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientThirdPartyAdministratorBranch, DTO.ClientThirdPartyAdministratorBranch>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.ClientManagedCareCompany, DTO.ClientManagedCareCompany>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientManagedCareCompany, DTO.ClientManagedCareCompany>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ClientManagedCareCompany, DTO.Paged.PagedClientManagedCareCompany>().ReverseMap();
            #endregion

            #region Client Billing (s)

            Mapper.CreateMap<MMC.Core.Data.Model.ClientBilling, DTO.ClientBilling>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientBilling, DTO.ClientBilling>().ReverseMap();

            Mapper.CreateMap<MMC.Core.Data.Model.ClientBillingRetailRate, DTO.ClientBillingRetailRate>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientBillingRetailRate, DTO.ClientBillingRetailRate>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ClientBillingRetailRate, DTO.Paged.PagedClientBillingRetailRate>().ReverseMap();

            Mapper.CreateMap<MMC.Core.Data.Model.ClientBillingWholesaleRate, DTO.ClientBillingWholesaleRate>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientBillingWholesaleRate, DTO.ClientBillingWholesaleRate>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ClientBillingWholesaleRate, DTO.Paged.PagedClientBillingWholesaleRate>().ReverseMap();

            Mapper.CreateMap<MMC.Core.Data.Model.ClientPrivateLabel, DTO.ClientPrivateLabel>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ClientPrivateLabel, DTO.ClientPrivateLabel>().ReverseMap();

            #endregion

            #region RFASplittedReferralHistories
            Mapper.CreateMap<MMC.Core.Data.Model.RFASplittedReferralHistory, DTO.RFASplittedReferralHistory>().ReverseMap();
            #endregion
            #region RFAMergedReferralHistories
            Mapper.CreateMap<MMC.Core.Data.Model.RFAMergedReferralHistory, DTO.RFAMergedReferralHistory>().ReverseMap();
            #endregion
            #region CommonMethod
            Mapper.CreateMap<MMC.Core.BL.Model.StorageModel, DTO.StorageModel>().ReverseMap();
            #endregion
            #region RFA- URHistory
            Mapper.CreateMap<MMC.Core.BL.Model.PatientHistory, DTO.PatientHistory>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PatientHistory, DTO.Paged.PagedPatientHistory>().ReverseMap();
            #endregion
            #region RequestHistory
            Mapper.CreateMap<MMC.Core.BL.Model.RequestHistory, DTO.RequestHistory>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.RequestHistory, DTO.Paged.PagedRequestHistory>().ReverseMap();
            #endregion
            #region RequestModify
            Mapper.CreateMap<MMC.Core.Data.Model.RFARequestModify, DTO.RFARequestModify>().ReverseMap();
            #endregion
            #region Duplicate Case
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.RequestByReferralID, DTO.Paged.PagedRequestByReferralID>().ReverseMap();
            #endregion
            #region PeerReviewMap
            Mapper.CreateMap<MMC.Core.Data.Model.PeerReview, DTO.PeerReview>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.PeerReview, DTO.Paged.PagedPeerReview>().ReverseMap();
            #endregion
            #region NoteswMap
            Mapper.CreateMap<MMC.Core.Data.Model.Note, DTO.Note>().ReverseMap();

            #endregion

            #region Patient(s) Notes
            Mapper.CreateMap<MMC.Core.BL.Model.NotesDetail, DTO.NotesDetail>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.NotesDetail, DTO.Paged.PagedNotesDetail>().ReverseMap();
            #endregion
            #region RequestBilling
            Mapper.CreateMap<MMC.Core.Data.Model.RFARequestBilling, DTO.RFARequestBilling>().ReverseMap();
            #endregion

            #region ADR
            Mapper.CreateMap<MMC.Core.Data.Model.ADR, DTO.ADR>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.ADR, DTO.Paged.PagedADR>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.ADR, DTO.ADR>().ReverseMap();
            #endregion

            #region IMR
            Mapper.CreateMap<MMC.Core.BL.Model.RequestIMRRecord, DTO.RequestIMRRecord>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.RequestIMRRecord, DTO.Paged.PagedRequestIMRRecord>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.IMRDecisionReferralDetails, DTO.IMRDecisionReferralDetails>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.IMRDecisionRequestDetails, DTO.IMRDecisionRequestDetails>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.IMRDecision, DTO.IMRDecision>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.IMRRFARequest, DTO.IMRRFARequest>().ReverseMap();
            #endregion
            #region RFAProcessLevels
            Mapper.CreateMap<MMC.Core.Data.Model.RFAProcessLevels, DTO.RFAProcessLevels>().ReverseMap();
            #endregion

            #region Billing
            Mapper.CreateMap<MMC.Core.BL.Model.Billing, DTO.Billing>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.Billing, DTO.Paged.PagedBilling>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.RFAReferralInvoice, DTO.RFAReferralInvoice>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.BillingAccountReceivables, DTO.BillingAccountReceivables>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.BillingAccountReceivables, DTO.Paged.PagedBillingAccountReceivables>().ReverseMap();

            #endregion

            #region IMRRFAReferral
            Mapper.CreateMap<MMC.Core.Data.Model.IMRRFAReferral, DTO.IMRRFAReferral>().ReverseMap();
            #endregion
            #region RFARequestTimeExtension
            Mapper.CreateMap<MMC.Core.Data.Model.RFARequestTimeExtension, DTO.RFARequestTimeExtension>().ReverseMap();
            #endregion
            #region RFAReferralAdditionalLink
            Mapper.CreateMap<MMC.Core.Data.Model.RFAReferralAdditionalLink, DTO.RFAReferralAdditionalLink>().ReverseMap();
            #endregion
            #region ClientStatement
            Mapper.CreateMap<MMC.Core.Data.Model.ClientStatement, DTO.ClientStatement>().ReverseMap();
            #endregion
            #region RFARequestBodyPart
            Mapper.CreateMap<MMC.Core.Data.Model.RFARequestBodyPart, DTO.RFARequestBodyPart>().ReverseMap();
            #endregion
            #region PreviousReferralFromCurrentReferral
            Mapper.CreateMap<MMC.Core.Data.Model.PreviousReferralFromCurrentReferral, DTO.PreviousReferralFromCurrentReferral>().ReverseMap();
            #endregion
            #region Email Record Attachment
            Mapper.CreateMap<MMC.Core.Data.Model.EmailRecord, DTO.EmailRecord>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.EmailRecordAttachment, DTO.EmailRecordAttachment>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.EmailRecordAttachment, DTO.EmailRecordAttachment>().ReverseMap();
            Mapper.CreateMap<MMC.Core.BL.Model.Paged.EmailRecordAttachment, DTO.Paged.PagedEmailRecordAttachment>().ReverseMap();

            #endregion

            #region PatientOccupational
            Mapper.CreateMap<MMC.Core.Data.Model.PatientOccupational, DTO.PatientOccupational>().ReverseMap();
            #endregion

            #region NoReferralCount
            Mapper.CreateMap<MMC.Core.Data.Model.NoOfReferralCount, DTO.NoOfReferralCount>().ReverseMap();
            #endregion

            #region StatusMap
            Mapper.CreateMap<MMC.Core.Data.Model.Status, DTO.Status>().ReverseMap();
            #endregion

            #region CurrentWorkload
            Mapper.CreateMap<MMC.Core.Data.Model.CurrentWorkloadReportParameter, DTO.CurrentWorkloadReportParameter>().ReverseMap();
            Mapper.CreateMap<MMC.Core.Data.Model.CurrentWorkloadReport, DTO.CurrentWorkloadReport>().ReverseMap();
            #endregion

           

        }
    }
}
