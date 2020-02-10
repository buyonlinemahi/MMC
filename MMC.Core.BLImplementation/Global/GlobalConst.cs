
namespace MMC.Core.BLImplementation.Global
{
    public class GlobalConst
    {

        public struct FileTypes
        {
            public const int UploadInitialNotifications = 1;
            public const int InitialNotification = 2;
            public const int ProofofService = 3;
            public const int IMRApplication = 4;
            public const int DeterminationLetter = 5;
            public const int IntakeUpload = 6;
            public const int RFIPreparationLetter = 6;
            public const int IMRSplitContent = 12;
            public const int IMRSplitBarcode = 13;
            public const int TimeExtensionPN = 11;
            public const int TimeExtension = 15;
            public const int WithdrawnUpload = 21;
            public const int TimeExtensionProofOfService = 22;
            public const int ClientAuthorizedUpload = 23;
        }
        public struct RequestTypes
        {
            public const int Prospective = 5;
            public const int Retrospective = 30;
            public const int Expedite = 1;
            public const int Concurrent = 5;
            public const int Appeal = 0;
            public const int Urgent = 0;
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
            public const int PeerReview = 140;
            public const int Notifications = 150;
            public const int Billing = 160;
            public const int FileStorage = 180;
            public const int IMRResponse = 170;
            public const int IMRDesicion = 190;
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
            public const string Delete = "Delete";
        }

        public struct FileName
        {
            public const string InvoiceStatement = "InvoiceStatement";
        }

        public struct Extension
        {
            public const string pdf = ".pdf";
            public const string doc = ".doc";
        }
    }
}
