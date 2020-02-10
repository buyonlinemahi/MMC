namespace MMC.Core.Data.Model.Constant
{
    public class Constant
    {
        public struct lookupTables
        {
            public const string State = "States";
            public const string Specialties = "Specialties";
            public const string Language = "Languages";
            public const string CurrentMedicalCondition = "CurrentMedicalConditions";
            public const string ClaimStatus = "ClaimStatuses";
            public const string ICD9Code = "ICD9Codes";
            public const string ICD10Code = "ICD10Codes";
            public const string BodyPart = "BodyParts";
            public const string FrequencyType = "FrequencyTypes";
            public const string RequestType = "RequestTypes";
            public const string TreatmentCategory = "TreatmentCategories";
            public const string TreatmentType = "TreatmentTypes";
            public const string BodyPartStatus = "BodyPartStatuses";
            public const string DocumentCategory = "DocumentCategories";
            public const string DocumentType = "DocumentTypes";
            public const string DurationType = "DurationTypes";
            public const string FileType = "FileTypes";
            public const string AttorneyFirmType = "AttorneyFirmTypes";
            public const string RateType = "RateTypes";
            public const string BillingRateType = "BillingRateTypes";
            public const string Decision = "Decisions";
            public const string IMRDecision = "IMRDecision";
            public const string Status = "Statuses";
            
        }
        public struct linkTables
        {
            public const string RFAReferralAdditionalLink = "RFAReferralAdditionalLinks";

        }
        public struct dboTables
        {
            public const string PatientClaimStatus = "PatientClaimStatuses";
            public const string CurrentWorkloadReportParameter = "CurrentWorkloadReportParameters";
            public const string CurrentWorkloadReport = "CurrentWorkloadReport";

        }
        public struct Schemas
        {
            public const string lookup = "lookup";
            public const string link = "link";
            public const string dbo = "dbo";
            public const string DOT = ".";
        }

        public struct ConstantValue
        {
            public const int AllReadyExists = -1;
            public const string ICD9Type = "1";
            public const string ICD10Type = "1";
            public const string ICD10 = "ICD10";
            public const string ICD9 = "ICD9";

        }
    }
}
