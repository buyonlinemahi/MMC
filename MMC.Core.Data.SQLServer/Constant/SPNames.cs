using Consts = MMC.Core.Data.Model.Constant.Constant;

namespace MMC.Core.Data.SQLServer.Constant
{
    public class StoredProcedureConst
    {
        private const string SQLExec = "EXEC ";
        
        #region Referral
        public struct ReferralRepositoryProcedure
        {

            public const string GetReferralByProcessLevel = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ReferralByProcessLevel @skip,@take,@processLevel";
            public const string GetReferralCountByProcessLevel = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ReferralCountByProcessLevel @processLevel";
            public const string GetRequestByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestByReferralID @ReferralID";
            public const string GetRequestByReferralIDForPeerReview = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestForPeerReviewByRFAReferralID @referralID";
            public const string GetRequestForNotificationByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestForNotificationByReferralID @ReferralID";
            public const string GetPatientByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientByReferralID @ReferralID";
            public const string GetReferralsInComplete = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ReferralsInComplete @skip,@take,@processLevel";
            public const string GetReferralsTotalCountInComplete = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ReferralsTotalCountInComplete @processLevel";
            public const string GetAcceptedBodyPartByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_AcceptedBodyPartByReferralID @ReferralID";
            public const string GetDeniedBodyPartByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_DeniedBodyPartByReferralID @ReferralID";
            public const string GetDelayedBodyPartByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_DelayedBodyPartByReferralID @ReferralID";
            public const string GetDiagnosisByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_DiagnosisByReferralID @ReferralID";
            public const string GetRFANewReferralIDFromRFAOldReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFANewReferralIDFromRFAOldReferralID @ReferralID,@DecisionID";
            public const string GetRFANewReferralIDFromRFAOldReferralIdPeerReview = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFANewReferralIDFromRFAOldReferralIdPeerReview @ReferralID";
            public const string DeleteRFAReferralIDFromReferralFiles = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Delete_RFAReferralIDFromReferralFiles @ReferralID,@RFAFileTypeID";
            public const string GetRFANewReferralIDFromRFAOldReferralIDForWithdrawn = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFANewReferralIDFromRFAOldReferralIDForWithdrawn @ReferralID,@DecisionID";

            public const string GetReferralMedicalRecordByPatientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ReferralMedicalRecordByPatientID @PatientID,@skip,@take";
            public const string GetReferralMedicalRecordByPatientIDCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ReferralMedicalRecordByPatientIDCount @PatientID";
            public const string AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "AddUpdate_RFAReferralAdditionalLinkStatementIDbyRefID @RFAReferralID,@ClientStatementID";
            public const string AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "AddUpdate_RFAReferralAdditionalLinkInvoiceIDbyRefID @RFAReferralID,@RFAReferralInvoiceID";
            public const string GetRFADiagnosisByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFADiagnosisByReferralID @RFAReferralID,@skip,@take";
            public const string GetRFADiagnosisByReferralIDCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFADiagnosisByReferralIDCount @RFAReferralID";
            public const string GetPreviousReferralIDFromCurrentReferralInDuplicate = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PreviousReferralIDFromCurrentReferralInDuplicate @ReferralID";
            public const string UpdateRFAReferralRequestRFAClinicalReasonforDecisionByID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_RFAReferralRequestRFAClinicalReasonforDecisionByID @RFAReferralID,@RFAClinicalReasonforDecision";
        
        }
        #endregion
        
        #region Patient
        public struct PatientRepositoryProcedure
        {
            public const string GetPatientMedicalRecordByPatientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientMedicalRecordByPatientID @PatientID,@skip,@take,@sortBy,@order";
            public const string GetPatientMedicalRecordByPatientIDCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientMedicalRecordByPatientIDCount @PatientID";



            public static string GetPatientMedicalRecordTemplateByPatientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientMedicalRecordTemplateByPatientID @PatientID";
            public static string GetPatientMedicalRecordMultipleTemplateByPatientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientMedicalRecordMultipleTemplateByPatientID @PatientID";
            public static string GetPatientsClaimByName = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientsClaimByName @SearchText,@Skip,@Take";
            public static string GetPatientsClaimByNameCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientsClaimByNameCount @SearchText";
            public static string UpdatePatientMedicareEligibleByID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_PatientMedicareEligibleByID @PatientID,@Mode,@CurrentMedicalConditionId";
            public static string UpdatePatientClaimPleadBodyPartByPatientClaimID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_PatientClaimPleadBodyPartByPatientClaimID @PatientClaimID,@BodyPartStatusID";
            public static string GetAllBodyPartsbyClaimID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_AllBodyPartsbyClaimID @ClaimID,@Skip,@Take";
            public static string GetAllBodyPartsCountbyClaimID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_AllBodyPartsCountbyClaimID @ClaimID";
            public static string GetNotesByPatientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_NotesByPatientID @PatientID,@Skip,@Take";
            public static string GetNotesByPatientIDCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_NotesByPatientIDCount @PatientID";
            public static string GetPatientClaimDiagnoseByPatientClaimId = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientClaimDiagnoseByPatientClaimId @PatientClaimID,@Skip,@Take";
            public static string GetPatientClaimDiagnoseByPatientClaimIdCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientClaimDiagnoseByPatientClaimIdCount @PatientClaimID";
        }
        #endregion
        
        #region Letter
        public struct LetterRepositoryProcedure
        {
            public const string GetInitialNotificationLetterDetails = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_InitialNotificationLetterDetails @referralID";
            public const string GetRequestDetailsInitialNotificationLetter = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestDetailsInitialNotificationLetter @referralID";
            public const string GetCertifiedRequestDetailsInitialNotificationLetter = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_CertifiedRequestDetailsInitialNotificationLetter @referralID";
            public const string GetDeniedRequestDetailsInitialNotificationLetter = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_DeniedRequestDetailsInitialNotificationLetter @referralID";
        }
        #endregion
        
        #region Common
        public struct CommonRepositoryProcedure
        {
            public const string GetPatientComorbidConditionsByPatientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientComorbidConditionsByPatientID @PatientID";
            public const string GetStorageStuctureByID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_StorageStuctureByID @id,@By";
            public const string GetICDCodesByNumber = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ICDCodesByNumber @ICDNumber,@ICDTab";
            public static string GetICDCodesByNumberOrDescription = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ICDCodesByNumberOrDescription @ICDNumberOrDescription,@ICD9Type,@ICDTab,@Skip,@Take";
            public static string GetICDCodesByNumberOrDescriptionCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ICDCodesByNumberOrDescriptionCount @ICDNumberOrDescription,@ICD9Type,@ICDTab";
            public static string GetAdditionalDocumentByPatientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_AdditionalDocumentByPatientID @PatientID,@PatientClaimID,@Skip,@Take";
            public static string GetAdditionalDocumentByPatientIDCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_AdditionalDocumentByPatientIDCount @PatientID,@PatientClaimID";
            public static string GetReferralsForDifferentProcessLevels = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ReferralsForDifferentProcessLevels";
        }
        #endregion
        
        #region Intake
        public struct IntakeRepositoryProcedure
        {
            public const string GetRFAPatMedicalRecordReviewByPatientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFAPatMedicalRecordReviewByPatientID @PatientID,@ReferralID,@skip,@take";
            public const string GetRFAPatMedicalRecordReviewByPatientIDCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFAPatMedicalRecordReviewByPatientIDCount @PatientID,@ReferralID";
            public static string GetRequestCPTNDCCodesByRFARequestID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestCPTNDCCodesByRFARequestID @RFARequestID";
            public static string GetRequestedTreatmentByRFARequestID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestedTreatmentByRFARequestID @RFARequestID";
            public const string GetPatientHistoryByPatientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientHistoryByPatientID @PatientID,@skip,@take,@sortBy,@order";
            public const string GetPatientHistoryByPatientIDCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_PatientHistoryByPatientIDCount @PatientID";
            public const string GetRequestHistoryByRFARequestID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestHistoryByRFARequestID @RFARequestID,@skip,@take";
            public const string GetRequestHistoryByRFARequestIDCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestHistoryByRFARequestIDCount @RFARequestID";
            public const string GetRequestForDuplicateDecision = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestForDuplicateDecision @patientClaimID,@skip,@take";
            public const string GetRequestForDuplicateDecisionCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RequestForDuplicateDecisionCount @patientClaimID";
            public const string UpdateRFAReqCertificationNumberByID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_RFAReqCertificationNumberByID  @RFARequestID";
            public const string UpdateRFAReferralRequestDecisionByID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_RFAReferralRequestDecisionByID @RFAReferralID";
            public const string getRFARequestByClaimID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFARequestByClaimID @PatientClaimID";
            public const string GetReferralFileByRFAReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ReferralFileByRFAReferralIDandFileType @referralID";
            public const string UpdateRFARequestDecisionAndRFAStatusByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_RFARequestDecisionAndRFAStatusByReferralID @RFAReferralID,@DecisionID,@RFAStatus";
            public const string GetSignaturePathAndDescriptionByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_SignaturePathAndDescription @RFAReferralID";
            public const string GetDeterminationLetterDecisionDesc = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_DeterminationLetterDecisionDesc @referralID";
            public const string SaveUpdateRFAReferralAdditionalInfo = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "SaveUpdate_RFAReferralAdditionalInfo @RFAReferralID,@OriginalRFAReferralID,@UserId";
            public const string GetRFARequestLatestDueDate = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFARequestLatestDueDate @AddDay,@RFARequestDate";
            public const string UpdateRFARequestLatestDueDateByRefferalID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_RFARequestLatestDueDateByRefferalID @AddDay,@rfaRefferalID,@Createdby";
            public const string AddRFIReportAdditionalRecordByRFAReferralFileID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Add_RFIReportAdditionalRecordByRFAReferralFileID  @RFAReferralFileID,@UserID";
            public const string AddRFITemplateRecordByRFARequestID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Add_RFITemplateRecordByRFARequestID  @RFAReferralFileID,@UserID";
            public const string AddRFARequestTimeExtensionRecordByRFARequestID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Add_RFARequestTimeExtensionRecordByRFARequestID  @RFAReferralFileID,@UserID";
            public const string MoveRFARequestRecordToRFADeletedRequest = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Move_RFARequestRecordToRFADeletedRequest  @RFARequestID";
            public const string AddRFIReportAdditionalRecordByRFARequestID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Add_RFIReportAdditionalRecordByRFARequestID @RFAReferralFileID,@RFARequestID,@UserID";
            public const string UpdateRFAAdditionalInfoDetailByRequestID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_RFAAdditionalInfoDetailByRequestID @OldRFAReferralID, @RFARequestID";
            public const string GetRFAReferralFilesAccToReferralIDInPreparationCase = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_RFAReferralFilesAccToReferralIDInPreparationCase @RFAReferralID";

        }
        #endregion

        #region Preparation
       
        #endregion

        #region Billing
        public struct BillingRepositoryProcedure
        {
            public const string GetDetailsForBillingLanding = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_DetailsForBillingLanding @skip,@take";
            public const string GetDetailsForBillingLandingCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_DetailsForBillingLandingCount";
            public const string GetDetailsForBillingLandingByClientName = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_DetailsForBillingLandingByClientName @ClientName,@skip,@take";
            public const string GetDetailsForBillingLandingByClientNameCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_DetailsForBillingLandingByClientNameCount @ClientName";
            public const string GetAccountReceivablesByClientName = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_AccountReceivablesByClientName @ClientName,@Skip,@Take";
            public const string GetAccountReceivablesByClientNameCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_AccountReceivablesByClientNameCount @ClientName";
        }
        #endregion
        
        #region Client

        public struct ClientRepositoryProcedure
        {
            public const string GetClaimAdministratorByClientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ClaimAdministratorByClientID @ClientID";
            public const string GetClientDetailsByName = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ClientDetailsByName  @SearchText, @Skip, @Take";
            public const string GetAllClaimAdministrator = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_AllClaimAdministrator";
            public static string GetClaimAdministratorAllByClientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ClaimAdministratorAllByClientID @ClientID";
            public static string UpdateClientManagedCareCompanyByClientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_ClientManagedCareCompanyByClientID @ClientID,@CompanyID";
            public static string UpdateClaimAdministratorResetDetailsByClientID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Update_ClaimAdministratorResetDetailsByClientID @ClientID";

            public static string GetClientInsuranceBranchesAllByInsurerID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ClientInsuranceBranchesAllByInsurerID @ClientID,@InsurerID";
            public static string GetClientTPABranchesAllByTPAID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_ClientTPABranchesAllByTPAID @ClientID,@TPAID";
        }

        #endregion

        #region Attorney
        public struct AttorneyRepositoryProcedure
        {
            public const string SearchAttorneyFirmByAttorneyORAttorneyFirmName = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Search_AttorneyFirmByAttorneyORAttorneyFirmName @AttorneyFirmSearch,@Skip,@Take";
            public const string SearchAttorneyFirmByAttorneyORAttorneyFirmNameCOUNT = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Search_AttorneyFirmByAttorneyORAttorneyFirmNameCOUNT @AttorneyFirmSearch";
        }
        #endregion

        #region Notification
        public struct NotificationRepositoryProcedure
        {
            public const string GetAdjandPhyEmailByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_AdjandPhyEmailByReferralID @ReferralID";
        }
        #endregion

        #region IMR

        public struct IMRRepositoryProcedure
        {

            public static string CreateRFARefferalByRFARefferalID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Create_RFARefferalByRFARefferalID @RFAReferralID,@RFARequestIDs,@Flag";
            public static string GetMedicalAndLegalDocsByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "get_MedicalAndLegalDocsByReferralID @RFAReferralID";
            public static string GetIMRLandingReferralsByProcessLevelsCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_IMRLandingReferralsByProcessLevelsCount @processLevel";

            public const string GetIMRLandingReferralsByProcessLevels = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_IMRLandingReferralsByProcessLevels @skip,@take,@processLevel";
            public const string GetIMRReferralByProcessLevelnPatientClaim = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_IMRReferralByProcessLevel_PatientClaim @skip,@take,@processLevel,@processLevel2,@patientClaim";
            public const string GetIMRReferralByProcessLevelnPatientClaimCount = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_IMRReferralByProcessLevel_PatientClaimCount @processLevel,@processLevel2,@patientClaim";

            public const string GetIMRLetters = SQLExec + Consts.Schemas.DOT + "get_IMRLetters @ReferralID";
            public const string GetIMRDecisionPageDetailsByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_IMRDecisionPageDetailsByReferralID @RFAReferralID";
            public const string GetIMRDecisionPageRequestDetailsByReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_IMRDecisionPageRequestDetailsByReferralID @RFAReferralID";
            
        }

        #endregion

        #region Email Record Attachment
        public struct EmailRecordAttachmentProcedure
        {
            public const string GetEmailRecordAttachmentByEmailRecordId = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Get_EmailRecordAttachmentByEmailRecordId @EmailRecordId";
            public const string AddEmailRecordAndRFARequestLinkByRFAReferralID = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Add_EmailRecordAndRFARequestLinkByRFAReferralID @RFAReferralID,@EmailRecordId";
            public const string AddEmailRecordAndRFARequestLinkByRFITimeExtension = SQLExec + Consts.Schemas.dbo + Consts.Schemas.DOT + "Add_EmailRecordAndRFARequestLinkByRFITimeExtension @RFAReferralID,@RFAReferralFileID,@EmailRecordId";
        }
        #endregion
    }
}