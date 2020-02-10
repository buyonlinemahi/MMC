
namespace MMCApp.Infrastructure.Global
{
    public class GlobalConst
    {
        public struct SessionKeys
        {
            public const string USER = "User";
            public const string MMCScreenMode = "Normal";          
        }


        

        public struct ContentTypes
        {
            public const string TextHtml = "text/html";
            public const string PDF = "pdf";
        }

        public struct VirtualDirectoryPath
        {
            public const string VirtualPath = "VirtualPath";
        }

        public struct VirtualPathFolders
        {
            public const string Storage = "Storage";
            public const string IntakeUpload = "IntakeUpload";
            public const string IntakeUploadFiles = "IntakeUploadFiles";
            public const string Referrals = "Referrals";
            public const string RecordSplit = "RecordSplit";
            public const string Patients = "Patients";
            public const string PatientMedicalRecords = "PatientMedicalRecords";
            public const string Templates = "Templates";
            public const string MedicalRecordPrintTemplate = "MedicalRecordPrintTemplate";
            public const string RFI = "RFI";
            public const string InitialNotificationLetter = "InitialNotificationLetter";
            public const string NoRFALetter = "NoRFALetter";
            public const string DuplicateLetter = "DuplicateLetter";
            public const string DeferralLetter = "DeferralLetter";
            public const string TimeExtensionPNLetter = "TimeExtensionPNLetter";
            public const string TimeExtensionLetter = "TimeExtensionLetter";
            public const string User = "User";
            public const string UserSignature = "UserSignature";
            public const string StorageUserPath = "/Storage/User/";
            public const string ClientPrivateLableLogo = "ClientPrivateLableLogo";
        }


        public struct Extension
        {
            public const string xml = ".xml";
            public const string pdf = ".pdf";
            public const string PDF = "PDF";
            public const string http = "http://";
            public const string doc = ".doc";
            public const string WORD = "WORD";
            public const string xlsx = ".xls";
            public const string EXCEL = "EXCEL";
        }

        public struct SSRSReportName
        {
            public const string ReportUrl = "ReportUrl";
            public const string RFILetter = "RFILetter";
            public const string RptClientInvoiceStatement = "RptClientInvoiceStatement";
            public const string RptDetermination = "RptDetermination";
            public const string RptIMRForm = "RptIMRForm";
            public const string RptIMRResponse = "RptIMRResponse";
            public const string RptIMRResponseProofOfService = "RptIMRResponseProofOfService";
            public const string RptIMRInitialNotification = "RptIMRInitialNotification";
            public const string RptIMRDecisionLetter = "RptIMRDecisionLetter";
            public const string RptInitialNotification = "RptInitialNotification";
            public const string RptInvoice = "RptInvoice";
            public const string RptNoRFAForm = "RptNoRFAForm";
            public const string RptPatientMediclRecord = "RptPatientMediclRecord";
            public const string RptPatientsNotes = "RptPatientsNotes";
            public const string RptProofOfService = "RptProofOfService";
            public const string RptRFADeferral = "RptRFADeferral";
            public const string RptRFADuplicate = "RptRFADuplicate";
            public const string RptRFATimeExtension = "RptRFATimeExtension";
            public const string RptRFATimeExtensionPN = "RptRFATimeExtensionPN";
            public const string RptIMRDecisionProofOfService = "RptIMRDecisionProofOfService";
            public const string RptCurrentWorkload = "RptCurrentWorkload";


        }

        public struct FolderName
        {
            public const string Patients = "Patients";
            public const string MedicalRecords = "MedicalRecords";
            public const string LegalDocs = "LegalDocs";
            public const string Letters = "Letters";
            public const string Invoices = "Invoices";
            public const string Templates = "Templates";
            public const string MergeDOC = "MergeDOC";
            public const string MergePDF = "MergePDF";
            public const string InvoiceMergePDF = "InvoiceMergePDF";
            public const string Signature = "Signature";
            public const string PrintNote = "PrintNote";
            public const string OfficalSignature = "Offical Signature";
            public const string Content = "Content";
            public const string img = "img";
            public const string InvoiceMergeDOC = "InvoiceMergeDOC";
            public const string InvoiceStatement = "InvoiceStatement";
            public const string IntakeUploadFiles = "IntakeUploadFiles";
            public const string IntakeUpload = "_IntakeUpload";
            public const string CurrentWorkloadReport = "CurrentWorkloadReport";

        }
        public struct Richtexteditor
        {
            public const string richtexteditor = "/richtexteditor/";
            public const string EditorNote = "EditorNote";
            public const string Save_help = "save, help";
        }

        public struct ConstantChar
        {
            public const int Zero = 0;
            public const string ScreenPrepration = "P";
            public const string ScreenClinical = "C";
            public const int One = 1;
            public const int Two = 2;
            public const string StringBlank = "";
            public const int MinusOne = -1;
            public const string DoubleBackSlash = "\\";
            public const string ForwardSlash = "/";
            public const string Colon = ":";
            public const char Char_R = 'R';
            public const char Char_C = 'C';
            public const int TwenetyOne = 21;
            public const string Comma = ",";
            public const string Storage = "Storage";
            public const string DoubleForwardSlash = "\\";
            public const string DoubleBackwardSlash = "//";

        }
        public struct ConstantName
        {
            public const string PatientDemographics = "PatientDemographics";
            public const string DeferralInitial = "DeferralInitial";
            public const string DuplicateInitial = "DuplicateInitial";
            public const string UnabletoReviewInitial = "UnabletoReviewInitial";
            public const string ClientAuthorizedInitial = "ClientAuthorizedInitial";
            public const string AuditInitial = "AuditInitial";
            public const string URHistory = "UR History";
            public const string ExceedLimit = "Exceed Limit";
            public const string SessionRFI = "RFI";
            public const string SessionNotification = "Notification";
            public const string SessionIMR = "IMR";
            public const string SessionIMRDecision = "IMRDecision";
            public const string Storage = "/Storage";
            public const string MMCDeferral = "MMCDeferral";
            public const string MMCDuplicate = "MMCDuplicate";
            public const string MMCUnabletoReview = "MMCUnabletoReview";
            public const string MMCClientAuthorized = "MMCClientAuthorized";
            public const string MMCAudit = "MMCAudit";
            public const string MMCRfi = "MMCRfi";
            public const string MMCTimeExtensionPN = "MMCTimeExtensionPN";
            public const string MMCTimeExtension = "MMCTimeExtension";
            public const string MMCNotification = "MMCNotification";
            public const string MMCIMR = "MMCIMR";
            public const string MMCIMRDecision = "MMCIMRDecision";
            public const string MMCProofOfService = "MMCProofOfService";
            public const string PreviousAttachementLinks = "PreviousAttachementLinks";
            public const string IntakeUploadpdf = "IntakeUpload.pdf";
            public const string DeferralRFAReferralID = "DeferralRFAReferralID";
            public const string DuplicateRFAReferralID = "DuplicateRFAReferralID";
            public const string UnableToReviewRFAReferralID = "UnableToReviewRFAReferralID";
            public const string ClientAuthorizedRFAReferralID = "ClientAuthorizedRFAReferralID";
            public const string AuditRFAReferralID = "AuditRFAReferralID";
            public const string RFAReferralFileID = "RFAReferralFileID";
        }

        public struct MMCTemplate
        {
            public const string MedicalRecordPrintTemplate = "/Templates/MedicalRecordPrintTemplate.xml";
            public const string RFITemplate = "/Templates/RFITemplate.xml";
            public const string InitialNotificationLetterTemplate = "/Templates/InitialNotificationLetterTemplate.xml";
        }

        public struct HttpCookieValue
        {
            public const string fileDownloadToken = "fileDownloadToken";
            public const string RFADueDate = "RFADueDate";          
            public const string TimeExtensionFilePath = "TimeExtensionFilePath";
            public const string TimeExtensionFilePathName = "TimeExtensionFilePathName";
            public const string TimeExtensionDownloadToken = "TimeExtensionDownloadToken";
           
        }

        public struct BodyPartStatus
        {
            public const int Accepted = 1;
            public const int Denied = 2;
            public const int Unknown = 3;
        }

        public struct Records
        {
            public const int Skip = 0;
            public const int Take = 10;
            public const int LandingTake = 20;
            public const int LandingTakeTest = 4;
            public const int Take5 = 5;
            public const int Take50 = 50;
        }

        public struct FileType
        {
            public const int UploadInitialNotifications = 1;
            public const int InitialNotification = 2;
            public const int ProofofService = 3;
            public const int IMRApplication = 4;
            public const int DeterminationLetter = 5;
            public const int IntakeUpload = 6;
            public const int RFIPreparationLetter = 7;
            public const int NoRFALetter = 8;
            public const int DuplicateLetter = 9;
            public const int DeferralLetter = 10;
            public const int TimeExtensionPNLetter = 11;
            public const int IMRSplitContent = 12;
            public const int IMRSplitBarcode = 13;
            public const int IMRProofofService = 14;
            public const int TimeExtensionLetter = 15;
            public const int IMRResponse = 16;
            public const int IMRDecisionUpload = 20;
            public const int IMRInitialNotification = 17;
            public const int IMRDecisionProofOfService = 19;
            public const int IMRDecisionLetter = 18;
            public const int WithdrawnUpload = 21;
            public const int TimeExtensionProofOfService = 22;
            public const int ClientAuthorizedUpload = 23;
        }
        public struct FileName
        {
            public const string InvoiceStatement = "InvoiceStatement";
        }

        public struct ClaimStatus
        {
            public const int Delayed = 1;
            public const int Denied = 2;
            public const int AcceptedinPart = 3;
            public const int AcceptedinFull = 4;
        }
        public struct Decision
        {
            public const int Certify = 1;
            public const int Modify = 2;
            public const int Deny = 3;
            public const int ConditionalCertify = 4;
            public const int ConditionalModify = 5;
            public const int Delay = 6;
            public const int UnabletoReview = 8;
            public const int Deferral = 9;
            public const int Duplicate = 10;
            public const int InternalAppeal = 11;
            public const int ClientAuthorized = 12;
            public const int Withdrawn = 13;
        }

        public struct OfficalSignatureName
        {
            public const string RickChavez = "Rick Chavez.jpg";
            public const string VickySummogum = "Vicky Summogum.jpg";
        }
        public struct OfficalSignatureDescription
        {
            public const string RickChavezSignatureDescription = "Rick Chavez, M.D., UMC <br/> Medical Director <br/> Board Certified, American Board of Pain Medicine <br/>Board Certified, American Academy of Family Practice<br/>Diplomate, American Academy of Pain Management<br/>Certified, American Society of Addiction Medicine<br/>Assistant Clinical Professor of Family Medicine,<br/>UCLA David Geffen School of Medicine <br/>CA Medical License Number: G43878";
            public const string VickySummogumSignatureDescription = "Vicky Summogum, Ph.D., RN, UMC<br/>Medical Management Director<br/>Utilization Review Specialist";
        }
        public struct Message
        {
            public const string PasswordMessage = "Password Incorrect";
            public const string UserNameMessage = "UserName Does not exist";
            public const string SaveMessage = "Saved Successfully";
            public const string EmailSendMessage = "Email send Successfully";
            public const string UpdateMessage = "Updated Successfully";
            public const string ErrorMessage = "Error";
            public const string DeleteMessage = "Deleted Successfully";
            public const string ModelErrorMessage = "Required fields are not Filled";
            public const string PasswordChanged = "Password Updated Successfully";
            public const string UserExist = "UserName Already exist";
            public const string ClientAlreadyExists = "Client already exists.";
            public const string SendProcessLevelBilling = "Send to process level Billing";
            public const string ErrorSendProcessLevelBilling = "Error in sending  to process level Billing Page";
            public const string RequestProcessSuccessfully = "Request process Successfully";
            public const string TimeExtendSuccessfully = "Time Extend Successfully";
            public const string RFAFileCreationDateSuccessfully = "Date updated Successfully";
            public const string Updated = "Updated";
            public const string InvoiceCreatedSuccessfully = "Invoice Created Successfully";
            public const string  UploadedSucessfully = " Uploaded Sucessfully"; 
            public const string  LetterGeneratedSuccessfully = " Letter Generated Successfully"; 
          
           
            

        }

        public struct ReportName
        {
            public const string Determination = "_Determination";
            public const string ProofOfService = "_ProofOfService";
            public const string IMRForm = "_IMRForm";
            public const string IMRProofOfService = "_IMRProofOfService";
            public const string Mergepdf = "_MergeDocument.pdf";
            public const string Mergedoc = "_MergeDocument.doc";
            public const string IMRDecisionMergeDoc = "_IMRDecisionMergeDocument.doc";
            public const string MergePatientURHistoryDocumnentdoc = "_MergePatientURHistoryDocumnent.doc";
            public const string MergePatientURHistoryDocumnentpdf = "_MergePatientURHistoryDocumnent.pdf";
            public const string InvoiceMergePDF = "InvoiceMerge.pdf";
            public const string PrintNote = "_PrintNote.pdf";
            public const string Invoice = "_Invoice";
            public const string InvoiceMergeDOC = "InvoiceMerge.doc";
            public const string IMRInitialNotification = "IMR_IN";
            public const string IMRCertify = "IMR_Certify";
            public const string IMRModify = "IMR_Modify";
            public const string IMRDecisionMergePdf = "_IMRDecisionMergeDocument.pdf";
            public const string IMRDecisionProofOfService = "IMRDecision_POS";
            public const string TimeExtensionProofOfService = "_TimeExtensionProofOfService";
        }

        public struct Controllers
        {
            public const string Vendor = "Vendor";
            public const string Physician = "Physician";
            public const string User = "User";
            public const string Adjuster = "Adjuster";
            public const string Insurer = "Insurer";
            public const string Employer = "Employer";
            public const string Patient = "Patient";
            public const string CaseManager = "CaseManager";
            public const string Intake = "Intake";
            public const string ThirdPartyAdministrator = "ThirdPartyAdministrator";
            public const string ManagedCareCompany = "ManagedCareCompany";
            public const string MedicalGroup = "MedicalGroup";
            public const string Preparation = "Preparation";
            public const string PatientMedicalRecord = "PatientMedicalRecord";
            public const string Attorney = "Attorney";
            public const string Client = "Client";
            public const string Notification = "Notification";
            public const string PeerReview = "PeerReview";
            public const string ClientBilling = "ClientBilling";
            public const string ADR = "ADR";
            public const string IMR = "IMR";
            public const string Billing = "Billing";
            public const string Common = "Common";
            
        }

        public struct FileDownloadExtension
        {
            public const string Application_Pdf = "application/pdf";
            public const string Application_doc = "application/doc";
        }

        public struct Views
        {
            public struct IntakeController
            {
                public const string Intake = "Intake";
            }
            public struct CommonController
            {
                public const string AdditionalDocument = "AdditionalDocument";
               
            }
        }
        public struct RFAStatus
        {
            public const string SendToPreparation = "SendToPreparation";
            public const string PeerReview = "Peer Review";
            public const string SendToClinical = "SendToClinical";
            public const string SendToNotification = "SendToNotification";
            public const string Withdrawn = "Withdrawn";

          }

        public struct Actions
        {
            public struct IntakeController
            {
                public const string uploadIntakeFile = "uploadIntakeFile";
                public const string SaveRFAIntake = "SaveRFAIntake";
                public const string SaveRFARequest = "SaveRFARequest";
                public const string saveRFAReferralCPTCode = "saveRFAReferralCPTCode";
                public const string saveRFARecordSplitting = "saveRFARecordSplitting";
                public const string saveIntakePhysician = "saveIntakePhysician";
                public const string updateProcessLevelRecSplt = "updateProcessLevelRecSplt";
                public const string viewRecSpltFile = "viewRecSpltFile";
            }
            public struct CommonController
            {
                public const string AddLinkForSendingEmail = "AddLinkForSendingEmail";
                public const string EmailSendMultipleDoc = "EmailSendMultipleDoc";
                public const string EmailSendMultipleAttachment = "EmailSendMultipleAttachment";
            }

            public struct VendorController
            {
                public const string SaveVendorDetail = "SaveVendorDetail";
                public const string Index = "Index";
            }
            public struct ThirdPartyAdministratorController
            {
                public const string SaveTPADetail = "SaveTPADetail";
                public const string Index = "Index";
                public const string SaveTPABranchDetail = "SaveTPABranchDetail";
            }

            public struct PhysicianController
            {
                public const string SavePhysicianDetail = "SavePhysicianDetail";
            }

            public struct UserController
            {
                public const string LogOff = "LogOff";
                public const string Home = "Home";
                public const string Login = "Login";
                public const string Index = "Index";
                public const string SaveUserDetail = "SaveUserDetail";
                public const string ChangePassword = "ChangePassword";
            }

            public struct AdjusterController
            {
                public const string SaveAdjusterDetail = "SaveAdjusterDetail";
            }

            public struct PeerReviewController
            {
                public const string SavePeerReviewDetail = "SavePeerReviewDetail";
            }

            public struct BillingController
            {
                public const string CreateInvoice = "CreateInvoice";
                public const string UpdateRequestBillings = "UpdateRequestBillings";
            }

            public struct AttorneyController
            {
                public const string SaveAttorneyDetail = "SaveAttorneyDetail";
                public const string SaveAttorneyFirmDetail = "SaveAttorneyFirmDetail";
            }

            public struct InsurerController
            {
                public const string SaveInsurerDetail = "SaveInsurerDetail";
                public const string SaveInsuranceBranchDetail = "SaveInsuranceBranchDetail";
            }

            public struct CaseManagerController
            {
                public const string SaveCaseManagerDetail = "SaveCaseManagerDetail";
            }

            public struct EmployerController
            {
                public const string SaveEmployerDetail = "SaveEmployerDetail";
                public const string SaveEmployerSubsidiaryDetail = "SaveEmployerSubsidiaryDetail";
            }
            public struct PatientController
            {
                public const string SavePatientDetail = "SavePatientDetail";
                public const string SavePatientClaimDetail = "SavePatientClaimDetail";
                public const string SavePatientPhysician = "SavePatientPhysician";
                public const string PrintNotes = "PrintNotes";
                public const string SavePatientOccupational = "SavePatientOccupational";
            }

            public struct ManagedCareCompanyController
            {
                public const string SaveCareCompanyDetail = "SaveCareCompanyDetail";
            }


            public struct Client
            {
                public const string AddClient = "AddClient";
                public const string AddClientBilling = "AddClientBilling";
                public const string AddClientInsurer = "AddClientInsurer";
                public const string AddClientInsuranceBranch = "AddClientInsuranceBranch";
                public const string AddClientEmployer = "AddClientEmployer";
                public const string AddClientThirdPartyAdministrator = "AddClientThirdPartyAdministrator";
                public const string AddClientThirdPartyAdministratorBranch = "AddClientThirdPartyAdministratorBranch";
                public const string AddClientManagedCareCompany = "AddClientManagedCareCompany";
                public const string AddClientPrivateLabel = "AddClientPrivateLabel";

            }

            public struct IMRController
            {
                public const string SearchRequesrIMRDetail = "SearchRequesrIMRDetail";
                public const string uploadIMRDoc = "uploadIMRDoc";
                public const string UploadIMRDecisionDoc = "UploadIMRDecisionDoc";
                public const string SendEmail = "SendEmail";
                public const string SendEmailFromIMRDecision = "SendEmailFromIMRDecision";
            }
            public struct MedicalGroup
            {
                public const string SaveMedicalGroupDetail = "SaveMedicalGroupDetail";

            }
            public struct PreparationController
            {
                public const string PreparationActionProcess = "PreparationActionProcess";
                public const string ClinicalTriageActionProcess = "ClinicalTriageActionProcess";
                public const string Index = "Index";
                public const string SaveAddtionalInfo = "SaveAddtionalInfo";
                public const string SavePatMedicalRecordReview = "SavePatMedicalRecordReview";
                public const string GenerateLetter = "GenerateLetter";
                public const string ClinicalAuditAction = "ClinicalAuditAction";
                public const string ClinicalTriageAction = "ClinicalTriageAction";
                public const string SavePatientClaimDiagnose = "SavePatientClaimDiagnose";
                public const string UploadSignature = "UploadSignature";
                public const string SaveSignatureDescription = "SaveSignatureDescription";
                public const string SaveRFARequestBilling = "SaveRFARequestBilling";
                public const string UpdateRFARequestLatestDueDate = "UpdateRFARequestLatestDueDate";
                public const string SendRFIReportEmailWithAttachment = "SendRFIReportEmailWithAttachment";
                public const string GenerateTimeExtensionLetter = "GenerateTimeExtensionLetter";
                public const string EmailSendMultipleDoc = "EmailSendMultipleDoc";
                public const string UploadFileOnWithdrawnORClientAuthorizedDecision = "UploadFileOnWithdrawnORClientAuthorizedDecision";
            }
            public struct MedicalRecordController
            {
                public const string Index = "Index";
                public const string UploadPatientMedicalRecord = "UploadPatientMedicalRecord";
                public const string SavePatientMedicalRecordSplitting = "SavePatientMedicalRecordSplitting";
            }

            public struct PatientMedicalRecordController
            {
                public const string updatePatientMedicalRecord = "updatePatientMedicalRecord";
            }

            public struct NotificationController
            {
                public const string EmailNotificationDoc = "EmailNotificationDoc";
                public const string uploadNotificationDoc = "uploadNotificationDoc";
                public const string PrintDocumnent = "PrintDocumnent";
            }

            public struct ADRController
            {
                public const string SaveADRDetail = "SaveADRDetail";
            }
        }
        public struct FileTypes
        {
            public const int UploadInitialNotifications = 1;
            public const int InitialNotification = 2;
            public const int ProofofService = 3;
            public const int IMRApplication = 4;
            public const int DeterminationLetter = 5;
            public const int IntakeUpload = 6;
        }
        public struct ProcessLevel
        {
            public const int Intake = 10;
            public const int ClaimVerification = 20;
            public const int RFAVerification = 30;
            public const int Physician = 40;
            public const int Requests = 50;
            public const int RecordSplit = 60;
            public const int Preparation = 70;
            public const int Deferral = 80;
            public const int Duplicate = 90;
            public const int UnabletoReview = 100;
            public const int InternalAppeals = 110;
            public const int Clinical = 120;
            public const int ClinicalAudit = 130;
            public const int PeerReview = 125;
            public const int Notifications = 150;
            public const int Billing = 160;
            public const int FileStorage = 180;
            public const int IMR = 170;
            public const int IMRDecision = 190;
        }

        public struct TableAbbreviation
        {
            public const string Ins = "ins";
            public const string Insb = "insb";
            public const string Emp = "emp";
            public const string Mcc = "mcc";
            public const string Tpa = "tpa";
            public const string Tpab = "tpab";
        }
        public struct CurrentMedicalCondition
        {
            public const int EndStageRenalDisease = 1;
        }

        public struct Mode
        {
            public const string Add = "Add";
            public const string Update = "Update";
            
        }

        public struct BillingRateType
        {
            public const int BillingRateTypeHourly = 1;
            public const int BillingRateTypeFlat = 2;
        }
        public struct TimeExtension
        {
            public const int TimeExtensionDays = 14;
        }
    }
}