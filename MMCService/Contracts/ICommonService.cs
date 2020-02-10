using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICommonService" in both code and config file together.
    [ServiceContract]
    public interface ICommonService
    {
        #region AttorneyFirmType
        [OperationContract]
        IEnumerable<DTO.AttorneyFirmType> getAttorneyFirmTypeAll();
        #endregion

        #region Language

        [OperationContract]
        IEnumerable<DTO.Language> getLanguageAll();
        #endregion

        #region States

        [OperationContract]
        IEnumerable<DTO.State> getStateAll();
        #endregion

        #region Statuses
        [OperationContract]
        IEnumerable<DTO.Status> getStatusAll();
        #endregion

        #region Specialty
        [OperationContract]
        IEnumerable<DTO.Specialty> getSpecialtiesAll();
        #endregion

        #region CurrentMedicalCondition
        [OperationContract]
        IEnumerable<DTO.CurrentMedicalCondition> getCurrentMedicalConditionsAll();
        #endregion

        #region BodyPart
        [OperationContract]
        IEnumerable<DTO.BodyPart> getBodyPartsAll();
        #endregion

        #region ICD9Code
        [OperationContract]
        IEnumerable<DTO.ICD9Code> getICD9CodesAll();
        [OperationContract]
        DTO.Paged.PagedICD9Code getICD9CodesByName(string _searchtext, int _skip, int _take);
        [OperationContract]
        IEnumerable<ICD9Code> getICD9CodesByCode(string _searchtext);
        [OperationContract]
        IEnumerable<DTO.ICD10Code> getICD10CodesAll();
        [OperationContract]
        DTO.Paged.PagedICDCode getICDCodesByNumberOrDescription(string _searchtext, string ICDTab, int skip, int take);
        [OperationContract]
        IEnumerable<ICDCode> getICDCodesByNumber(string _ICDNumber, string _ICDTab);
        #endregion

        #region ClaimStatus
        [OperationContract]
        IEnumerable<DTO.ClaimStatus> getClaimStatusesAll();
        #endregion

        #region BodyPartStatus
        [OperationContract]
        IEnumerable<BodyPartStatus> getBodyPartStatusesAll();
        #endregion

        [OperationContract]
        IEnumerable<DTO.FrequencyType> getFrequencyTypeAll();

        [OperationContract]
        IEnumerable<DTO.DurationType> getDurationTypesAll();

        [OperationContract]
        IEnumerable<DTO.RequestType> getRequestTypeAll();

        [OperationContract]
        IEnumerable<DTO.TreatmentCategory> getTreatmentCategoriesAll();

        [OperationContract]
        IEnumerable<DTO.TreatmentType> getTreatmentTypesAll();

        [OperationContract]
        IEnumerable<DTO.TreatmentType> getTreatmentTypesByTreatmentCategoryID(int _treatmentCategoryID);

        [OperationContract]
        IEnumerable<DocumentCategory> getDocumentCategoriesAll();

        [OperationContract]
        IEnumerable<DocumentType> getDocumentTypesAll();

        [OperationContract]
        IEnumerable<DTO.DocumentType> getDocumentTypeByDocumentCategoryID(int _documentCategoryID);

        [OperationContract]
        int AddProcessLevelByReferralID(int _referralID, int _processLevel);

        [OperationContract]
        DTO.RFAProcessLevels getPreviousProcessLevelsByReferralID(int _referralID);

        [OperationContract]
        int getMaxProcessLevelByReferralID(int _referralID);

        [OperationContract]
        IEnumerable<DTO.RFAProcessLevels> getProcessLevelsByReferralID(int _referralID);

        [OperationContract]
        string getPatientComorbidConditionsByPatientID(int PatientID);

        [OperationContract]
        DTO.StorageModel GetStorageStuctureByID(int _id, char _by);

        [OperationContract]
        DTO.Paged.PagedAdditionalDocument getAdditionalDocumentsByPatientID(int _patientID,int _patientClaimID, int _skip, int _take);

        [OperationContract]
        IEnumerable<DTO.NoOfReferralCount> getNoOfReferralCountAccToProcessLevel();

    }
}
