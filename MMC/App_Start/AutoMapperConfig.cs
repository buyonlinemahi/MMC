using AutoMapper;

namespace MMC
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            #region AdditionalDocument
            Mapper.CreateMap<MMCService.CommonService.AdditionalDocument, MMCApp.Domain.Models.AdditionalDocumentModel.AdditionalDocument>().ReverseMap();
            #endregion
            #region AttorneyMapping
            Mapper.CreateMap<MMCService.PaticipantService.Attorney, MMCApp.Domain.Models.AttorneyModel.Attorney>().ReverseMap();
            Mapper.CreateMap<MMCService.PaticipantService.AttorneyFirm, MMCApp.Domain.Models.AttorneyModel.AttorneyFirm>().ReverseMap();
            Mapper.CreateMap<MMCService.CommonService.AttorneyFirmType, MMCApp.Domain.Models.AttorneyModel.AttorneyFirmType>().ReverseMap();   
            #endregion

            #region AdjusterMapping
            Mapper.CreateMap<MMCService.PaticipantService.Adjuster, MMCApp.Domain.Models.AdjusterModel.Adjuster>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.AdjusterByClientID, MMCApp.Domain.Models.AdjusterModel.Adjuster>().ReverseMap();
            #endregion AdjusterMapping

            #region VendorMapping
            Mapper.CreateMap<MMCService.PaticipantService.Vendor, MMCApp.Domain.Models.VendorModel.Vendor>().ReverseMap();
            #endregion VendorMapping

            #region PhysicianMapping
            Mapper.CreateMap<MMCService.PaticipantService.Physician, MMCApp.Domain.Models.PhysicianModel.Physician>().ReverseMap();
            #endregion AdjusterMapping

            #region UserMapping
            Mapper.CreateMap<MMCService.UserService.User, MMCApp.Domain.Models.UserModel.User>().ReverseMap();
            #endregion UserMapping

            #region InsurerMapping
            Mapper.CreateMap<MMCService.PaticipantService.Insurer, MMCApp.Domain.Models.InsurerModel.Insurer>().ReverseMap();
            Mapper.CreateMap<MMCService.PaticipantService.InsuranceBranch, MMCApp.Domain.Models.InsurerModel.InsuranceBranch>().ReverseMap();
            #endregion InsurerMapping


            #region EmployerMapping
            Mapper.CreateMap<MMCService.PaticipantService.Employer, MMCApp.Domain.Models.EmployerModel.Employer>().ReverseMap();
            Mapper.CreateMap<MMCService.PaticipantService.EmployerSubsidiary, MMCApp.Domain.Models.EmployerModel.EmployerSubsidiary>().ReverseMap();
            #endregion EmployerMapping

            #region Patient
            Mapper.CreateMap<MMCService.PatientService.Patient, MMCApp.Domain.Models.PatientModel.Patient>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientClaim, MMCApp.Domain.Models.PatientModel.PatientClaim>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientOccupational, MMCApp.Domain.Models.PatientModel.PatientOccupational>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientClaim, MMCApp.Domain.Models.PatientModel.PatientClaimDropDown>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientCurrentMedicalCondition, MMCApp.Domain.Models.PatientModel.PatientCurrentMedicalCondition>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientClaimAddOnBodyPart, MMCApp.Domain.Models.PatientModel.PatientClaimAddOnBodyPart>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientClaimPleadBodyPart, MMCApp.Domain.Models.PatientModel.PatientClaimPleadBodyPart>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientClaimDiagnose, MMCApp.Domain.Models.PatientModel.PatientClaimDiagnose>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientClaimStatus, MMCApp.Domain.Models.PatientModel.PatientClaimStatus>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientClaimBodyPartByBPStatusID, MMCApp.Domain.Models.IntakeModel.PatientClaimBodyPartByBPStatusID>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientMedicalRecordByPatientID, MMCApp.Domain.Models.PatientModel.PatientMedicalRecordByPatientID>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientMedicalRecord, MMCApp.Domain.Models.PatientModel.PatientMedicalRecord>().ReverseMap();
            #endregion Patient

            #region StateMapping
            Mapper.CreateMap<MMCService.CommonService.State, MMCApp.Domain.Models.StateModel.State>().ReverseMap();
            #endregion StateMapping

            #region SpecialtyMapping
            Mapper.CreateMap<MMCService.CommonService.Specialty, MMCApp.Domain.Models.SpecialtyModel.Specialty>().ReverseMap();
            #endregion SpecialtyMapping

            #region LanguageMapping
            Mapper.CreateMap<MMCService.CommonService.Language, MMCApp.Domain.Models.LanguageModel.Language>().ReverseMap();
            #endregion LanguageMapping

            #region CurrentMedicalConditionMapping
            Mapper.CreateMap<MMCService.CommonService.CurrentMedicalCondition, MMCApp.Domain.Models.CurrentMedicalConditionModel.CurrentMedicalCondition>().ReverseMap();
            #endregion CurrentMedicalConditionMapping

            #region CaseManagerMapping
            Mapper.CreateMap<MMCService.PaticipantService.CaseManager, MMCApp.Domain.Models.CaseManagerModel.CaseManager>().ReverseMap();
            #endregion

            #region ThirdPartyAdministratorMapping
            Mapper.CreateMap<MMCService.PaticipantService.ThirdPartyAdministrator, MMCApp.Domain.Models.ThirdPartyAdministratorModel.ThirdPartyAdministrator>().ReverseMap();
            #endregion

            #region ThirdPartyAdministratorBranchMapping
            Mapper.CreateMap<MMCService.PaticipantService.ThirdPartyAdministratorBranch, MMCApp.Domain.Models.ThirdPartyAdministratorBranchModel.ThirdPartyAdministratorBranch>().ReverseMap();
            #endregion

            #region ManagedCareCompanyMapping
            Mapper.CreateMap<MMCService.PaticipantService.ManagedCareCompany, MMCApp.Domain.Models.ManagedCareCompanyModel.ManagedCareCompany>().ReverseMap();
            #endregion ManagedCareCompanyMapping

            #region ClaimStatusMapping
            Mapper.CreateMap<MMCService.CommonService.ClaimStatus, MMCApp.Domain.Models.ClaimStatusModel.ClaimStatus>().ReverseMap();
            #endregion ClaimStatusMapping

            #region BodyPartMapping
            Mapper.CreateMap<MMCService.CommonService.BodyPart, MMCApp.Domain.Models.BodyPartModel.BodyPart>().ReverseMap();
            Mapper.CreateMap<MMCService.CommonService.BodyPartStatus, MMCApp.Domain.Models.BodyPartModel.BodyPartStatus>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.BodyPartDetail, MMCApp.Domain.Models.BodyPartModel.BodyPartDetail>().ReverseMap();
            #endregion BodyPartMapping

            #region ICDCodeMapping
            Mapper.CreateMap<MMCService.CommonService.ICD9Code, MMCApp.Domain.Models.ICD9CodeModel.ICD9Code>().ReverseMap();
            Mapper.CreateMap<MMCService.CommonService.ICDCode, MMCApp.Domain.Models.ICDCodeModel.ICDCode>().ReverseMap();
            Mapper.CreateMap<MMCService.CommonService.ICD10Code, MMCApp.Domain.Models.ICD10CodeModel.ICD10Code>().ReverseMap();
            #endregion ICD9CodeMapping

            #region Intake
            Mapper.CreateMap<MMC.MMCService.IntakeService.RFAReferral, MMCApp.Domain.Models.IntakeModel.RFAReferral>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.IntakeService.RFAReferralFile, MMCApp.Domain.Models.IntakeModel.RFAReferralFile>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.IntakeService.RFARequest, MMCApp.Domain.Models.IntakeModel.RFARequest>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.IntakeService.RFARequest, MMCApp.Domain.Models.IntakeModel.RFARequestsDetails>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.IntakeService.RFARequestCPTCode, MMCApp.Domain.Models.IntakeModel.RFARequestCPTCode>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.IntakeService.RFARecordSplitting, MMCApp.Domain.Models.IntakeModel.RFARecordSpliting>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.IntakeService.IncompleteReferrals, MMCApp.Domain.Models.IntakeModel.IncompleteReferrals>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.PreparationService.RFARequest, MMCApp.Domain.Models.IntakeModel.RFARequest>().ReverseMap();
            //Mapper.CreateMap<MMC.MMCService.PreparationService.RFAReferral, MMCApp.Domain.Models.IntakeModel.RFAReferral>().ReverseMap();


            Mapper.CreateMap<MMC.MMCService.CommonService.FrequencyType, MMCApp.Domain.Models.FrequencyTypeModel.FrequencyType>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.CommonService.RequestType, MMCApp.Domain.Models.RequestTypeModel.RequestType>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.CommonService.TreatmentCategory, MMCApp.Domain.Models.TreatmentCategoryModel.TreatmentCategory>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.CommonService.TreatmentType, MMCApp.Domain.Models.TreatmentTypeModel.TreatmentType>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.CommonService.DocumentCategory, MMCApp.Domain.Models.DocumentCategoryModel.DocumentCategory>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.CommonService.DocumentType, MMCApp.Domain.Models.DocumentTypeModel.DocumentType>().ReverseMap();
            Mapper.CreateMap<MMC.MMCService.CommonService.DurationType, MMCApp.Domain.Models.DurationTypeModel.DurationType>().ReverseMap();
            Mapper.CreateMap<MMCService.PreparationService.PatientMedicalRecordByPatientID, MMCApp.Domain.Models.PatientModel.PatientMedicalRecordByPatientID>().ReverseMap();

            #endregion

            #region MedicalGroupMapping
            Mapper.CreateMap<MMCService.PaticipantService.MedicalGroup, MMCApp.Domain.Models.MedicalGroup.MedicalGroup>().ReverseMap();
            #endregion

            #region ClinicalTriage
            Mapper.CreateMap<MMCService.PreparationService.ClinicalTriage, MMCApp.Domain.Models.ClinicalTriageModel.ClinicalTriage>().ReverseMap();
            Mapper.CreateMap<MMCService.PreparationService.PatientAndRequestModel, MMCApp.Domain.ViewModels.ClinicalTriageViewModel.PatientAndRequestModel>().ReverseMap();
            Mapper.CreateMap<MMCService.PaticipantService.PatientAndRequestModel, MMCApp.Domain.ViewModels.ClinicalTriageViewModel.PatientAndRequestModel>().ReverseMap();
            Mapper.CreateMap<MMCService.PaticipantService.PatientByReferralID, MMCApp.Domain.Models.ClinicalTriage.PatientByReferralID>().ReverseMap();
            Mapper.CreateMap<MMCService.PaticipantService.RequestByReferralID, MMCApp.Domain.Models.ClinicalTriage.RequestByReferralID>().ReverseMap();
            Mapper.CreateMap<MMCService.PreparationService.PatientByReferralID, MMCApp.Domain.Models.ClinicalTriage.PatientByReferralID>().ReverseMap();            
            Mapper.CreateMap<MMCService.PreparationService.RequestByReferralID, MMCApp.Domain.Models.ClinicalTriage.RequestByReferralID>().ReverseMap();
            #endregion

            #region Notification
            Mapper.CreateMap<MMCService.NotificationService.Notification, MMCApp.Domain.Models.Notification.Notification>().ReverseMap();
            Mapper.CreateMap<MMCService.NotificationService.NotificationEmailSend, MMCApp.Domain.Models.Notification.Notification>().ReverseMap();
            #endregion

            #region PatientMedicalRecord
            Mapper.CreateMap<MMCService.PatientService.PatientMedicalRecordTemplateByPatientID, MMCApp.Domain.Models.PatientModel.PatientMedRecordTemplateByPatientID>().ReverseMap();
            Mapper.CreateMap<MMCService.PatientService.PatientMedicalRecordByPatientID, MMCApp.Domain.Models.PatientModel.PatientMedicalRecordByPatientID>().ReverseMap();
            #endregion

            #region RFAAdtionalInfo
            Mapper.CreateMap<MMCService.PreparationService.RFAAdditionalInfo, MMCApp.Domain.Models.ClinicalTriage.RFAAdditionalInfo>().ReverseMap();
            #endregion

            #region RFADiagnosis
            Mapper.CreateMap<MMCService.IntakeService.RFADiagnosis, MMCApp.Domain.Models.RFADiagnosisModel.RFADiagnosis>().ReverseMap();
            #endregion
            #region RFARequest
            Mapper.CreateMap<MMCService.IntakeService.RFARequest, MMCApp.Domain.Models.IntakeModel.RFAReferral>().ReverseMap();
            Mapper.CreateMap<MMCService.PreparationService.RFARequest, MMCApp.Domain.Models.ClinicalTriage.RequestByReferralID>().ReverseMap();
            #endregion

            #region RFAPatMedicalRecordReview
            Mapper.CreateMap<MMCService.IntakeService.RFAPatMedicalRecordReview, MMCApp.Domain.Models.MedicalRecordReview.RFAPatMedicalRecordReview>().ReverseMap();
            Mapper.CreateMap<MMCService.IntakeService.RFAPatMedicalRecordReviewDetail, MMCApp.Domain.Models.MedicalRecordReview.RFAPatMedicalRecordReviewDetail>().ReverseMap();
            #endregion

            #region AttorneyMapping
            Mapper.CreateMap<MMCService.PaticipantService.Attorney, MMCApp.Domain.Models.AttorneyModel.Attorney>().ReverseMap();          
            #endregion AttorneyMapping

            #region Client Mapping
            Mapper.CreateMap<MMCService.ClientService.Client, MMCApp.Domain.Models.ClientModel.Client>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClientEmployer, MMCApp.Domain.Models.ClientModel.ClientEmployer>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClientInsurer, MMCApp.Domain.Models.ClientModel.ClientInsurer>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClientInsuranceBranch, MMCApp.Domain.Models.ClientModel.ClientInsuranceBranch>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClientThirdPartyAdministrator, MMCApp.Domain.Models.ClientModel.ClientThirdPartyAdministrator>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClientThirdPartyAdministratorBranch, MMCApp.Domain.Models.ClientModel.ClientThirdPartyAdministratorBranch>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClientManagedCareCompany, MMCApp.Domain.Models.ClientModel.ClientManagedCareCompany>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClaimAdministratorAllByClientID, MMCApp.Domain.Models.ClientModel.ClaimAdministratorAllByClientID>().ReverseMap();
            #endregion Client

            #region Client Billing Mapping

            Mapper.CreateMap<MMCService.ClientService.ClientBilling, MMCApp.Domain.Models.ClientBillingModel.ClientBilling>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClientBillingRetailRate, MMCApp.Domain.Models.ClientBillingModel.ClientBillingRetailRate>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClientBillingWholesaleRate, MMCApp.Domain.Models.ClientBillingModel.ClientBillingWholesaleRate>().ReverseMap();
            Mapper.CreateMap<MMCService.ClientService.ClientPrivateLabel, MMCApp.Domain.Models.ClientBillingModel.ClientPrivateLabel>().ReverseMap();

            #endregion Client Billing

            
            #region RFASplittedReferralHistories
            Mapper.CreateMap<MMCService.IntakeService.RFASplittedReferralHistory, MMCApp.Domain.Models.RFASplittedReferralHistory.RFASplittedReferralHistory>().ReverseMap();
            #endregion RFASplittedReferralHistories

            #region RFAMergedReferralHistories
            Mapper.CreateMap<MMCService.IntakeService.RFAMergedReferralHistory, MMCApp.Domain.Models.RFAMergedReferralHistory.RFAMergedReferralHistory>().ReverseMap();
            #endregion RFASplittedReferralHistories

            #region CommonMethod
            Mapper.CreateMap<MMCService.CommonService.StorageModel, MMCApp.Domain.Models.StorageModel.StorageModel>().ReverseMap();
            Mapper.CreateMap<MMCService.CommonService.NoOfReferralCount, MMCApp.Domain.Models.CommonModel.NoOfReferralCount>().ReverseMap();
            #endregion
            #region RFA- URHistory
            Mapper.CreateMap<MMCService.IntakeService.PatientHistory, MMCApp.Domain.Models.IntakeModel.PatientHistory>().ReverseMap();
            #endregion
            #region RFA- RequestHistory
            Mapper.CreateMap<MMCService.IntakeService.RequestHistory, MMCApp.Domain.Models.IntakeModel.RequestHistory>().ReverseMap();
            #endregion
            #region RequestModify
            Mapper.CreateMap<MMCService.IntakeService.RFARequestModify, MMCApp.Domain.Models.IntakeModel.RFARequestModify>().ReverseMap();
            #endregion
            #region RFARequestAddionalInfo
            Mapper.CreateMap<MMCService.IntakeService.RFARequestAdditionalInfo, MMCApp.Domain.Models.IntakeModel.RFARequestAdditionalInfo>().ReverseMap();
            Mapper.CreateMap<MMCService.IntakeService.RFARequestAdditionalInfoDetail, MMCApp.Domain.Models.ClinicalTriage.RFARequestAdditionalInfoDetail>().ReverseMap();
            #endregion
            #region Duplicate Case
            Mapper.CreateMap<MMCService.IntakeService.RequestByReferralID, MMCApp.Domain.Models.ClinicalTriage.RequestByReferralID>().ReverseMap();
            #endregion

            #region PeerReviewMapping
            Mapper.CreateMap<MMCService.PaticipantService.PeerReview, MMCApp.Domain.Models.PeerReviewModel.PeerReview>().ReverseMap();          
            #endregion AdjusterMapping

            #region NotesMapping
            Mapper.CreateMap<MMCService.IntakeService.Note, MMCApp.Domain.Models.NoteModel.Note>().ReverseMap();
            #endregion NotesMapping

            #region Patient(s) Notes
            Mapper.CreateMap<MMCService.PatientService.NotesDetail, MMCApp.Domain.Models.NoteModel.NoteDetail>().ReverseMap();
            #endregion
            #region RequestBilling
            Mapper.CreateMap<MMCService.IntakeService.RFARequestBilling, MMCApp.Domain.Models.ClinicalTriage.RFARequestBilling>().ReverseMap();
            #endregion

            #region ADR
            Mapper.CreateMap<MMCService.PaticipantService.ADR, MMCApp.Domain.Models.ADRModel.ADR>().ReverseMap();
            #endregion 

            #region Request IMR Record
            Mapper.CreateMap<MMCService.IMRService.RequestIMRRecord , MMCApp.Domain.Models.IMRModel.RequestIMRRecord>().ReverseMap();
            Mapper.CreateMap<MMCService.IMRService.RFAReferralFile, MMCApp.Domain.Models.IntakeModel.RFAReferralFile>().ReverseMap();
            Mapper.CreateMap<MMCService.IMRService.RFAReferral, MMCApp.Domain.Models.IntakeModel.RFAReferral>().ReverseMap();
            Mapper.CreateMap<MMCService.IMRService.Physician, MMCApp.Domain.Models.PhysicianModel.Physician>().ReverseMap();
            Mapper.CreateMap<MMCService.IMRService.IMRRFAReferral, MMCApp.Domain.Models.IMRModel.IMRRFAReferral>().ReverseMap();
            Mapper.CreateMap<MMCService.PreparationService.PatientAndRequestModel, MMCApp.Domain.ViewModels.IMRViewModel.RequestIMRActionViewModel>().ReverseMap();
            Mapper.CreateMap<MMCService.IMRService.IMRDecisionReferralDetails, MMCApp.Domain.Models.IMRModel.IMRDecisionReferralDetails>().ReverseMap();
            Mapper.CreateMap<MMCService.IMRService.IMRDecisionRequestDetails, MMCApp.Domain.Models.IMRModel.IMRDecisionRequestDetails>().ReverseMap();
            Mapper.CreateMap<MMCService.IMRService.IMRDecision, MMCApp.Domain.Models.IMRModel.IMRDecision>().ReverseMap();
            Mapper.CreateMap<MMCService.IMRService.IMRRFARequest, MMCApp.Domain.Models.IMRModel.IMRDecisionRequestDetails>().ReverseMap();            
            #endregion
            #region RFAReferralAddionalInfo
            Mapper.CreateMap<MMCService.IntakeService.RFAReferralAdditionalInfo, MMCApp.Domain.Models.IntakeModel.RFAReferralAdditionalInfo>().ReverseMap();
        
            #endregion

            #region Billing
            Mapper.CreateMap<MMCService.BillingService.Billing, MMCApp.Domain.Models.Billing.Billing>().ReverseMap();
            Mapper.CreateMap<MMCService.BillingService.RFARequestBilling, MMCApp.Domain.Models.ClinicalTriage.RFARequestBilling>().ReverseMap();
            Mapper.CreateMap<MMCService.BillingService.RFAReferralInvoice, MMCApp.Domain.Models.Billing.RFAReferralInvoice>().ReverseMap();
            Mapper.CreateMap<MMCService.BillingService.BillingAccountReceivables, MMCApp.Domain.Models.Billing.BillingAccountReceivables>().ReverseMap();
            #endregion

            #region RFAReferralAdditionalLink
            Mapper.CreateMap<MMCService.IntakeService.RFAReferralAdditionalLink, MMCApp.Domain.Models.IntakeModel.RFAReferralAdditionalLink>().ReverseMap();
           
            #endregion

            #region RFARequestTimeExtension
            Mapper.CreateMap<MMCService.IntakeService.RFARequestTimeExtension, MMCApp.Domain.Models.MedicalRecordReview.RFARequestTimeExtension>().ReverseMap();
            #endregion

            #region ClientStatement
            Mapper.CreateMap<MMCService.BillingService.ClientStatement, MMCApp.Domain.Models.Billing.ClientStatement>().ReverseMap();
            #endregion

            #region RFARequestBodyPart
            Mapper.CreateMap<MMCService.IntakeService.RFARequestBodyPart, MMCApp.Domain.Models.IntakeModel.RFARequestBodyPart>().ReverseMap();
            #endregion     
       
            #region RFARequestBodyPart
            Mapper.CreateMap<MMCService.IntakeService.PreviousReferralFromCurrentReferral, MMCApp.Domain.ViewModels.ClinicalTriageViewModel.PreviousReferralFromCurrentReferral>().ReverseMap();
            #endregion 

            #region Email Record Attachment
            Mapper.CreateMap<MMCService.EmailRecordAttachmentService.EmailRecord, MMCApp.Domain.Models.EmailRecordModel.EmailRecord>().ReverseMap();
            Mapper.CreateMap<MMCService.EmailRecordAttachmentService.EmailRecordAttachment, MMCApp.Domain.Models.EmailRecordModel.EmailRecordAttachment>().ReverseMap();
            #endregion

            #region StatusMapping
            Mapper.CreateMap<MMCService.CommonService.Status, MMCApp.Domain.Models.StatusModel.Status>().ReverseMap();
            #endregion StatusMapping

            #region CurrentWorkloadMapping
            Mapper.CreateMap<MMCService.CurrentWorkloadService.CurrentWorkloadReport, MMCApp.Domain.Models.CurrentWorkloadModel.CurrentWorkloadReport>().ReverseMap();
            Mapper.CreateMap<MMCService.CurrentWorkloadService.CurrentWorkloadReportParameter, MMCApp.Domain.Models.CurrentWorkloadModel.CurrentWorkloadReportParameter>().ReverseMap();
            #endregion CurrentWorkloadMapping
        }
    }
}