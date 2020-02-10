using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;


namespace MMC.Core.BL
{
    public interface ICommonRepository
    {
        #region State
        IEnumerable<State> getStateAll();
        #endregion

        #region Status
        IEnumerable<Status> getStatusAll();
        #endregion

        #region Language
        IEnumerable<Language> getLanguageAll();
        #endregion

        #region Specialty
        IEnumerable<Specialty> getSpecialtiesAll();
        #endregion

        #region CurrentMedicalCondition
        IEnumerable<CurrentMedicalCondition> getCurrentMedicalConditionsAll();
        #endregion

        #region ClaimStatus
        IEnumerable<ClaimStatus> getClaimStatusesAll();
        #endregion

        #region ICDCode
        IEnumerable<ICD9Code> getICD9CodesAll();
        IEnumerable<ICD10Code> getICD10CodesAll();
        BLModel.Paged.ICD9Code getICD9CodesByName(string _searchtext, int _skip, int _take);
        IEnumerable<ICD9Code> getICD9CodesByCode(string _searchtext);
        IEnumerable<ICDCode> getICDCodesByNumber(string _ICDNumber, string _ICDTab);
        BLModel.Paged.ICDCode getICDCodesByNumberOrDescription(string _searchtext, string ICDTab, int skip, int take);
        #endregion

        #region BodyPart
        IEnumerable<BodyPart> getBodyPartsAll();
        #endregion

        #region BodyPartStatus
        IEnumerable<BodyPartStatus> getBodyPartStatusesAll();
        #endregion

        #region Attorney
        IEnumerable<AttorneyFirmType> getAttorneyFirmTypeAll();
        #endregion
        IEnumerable<FrequencyType> getFrequencyTypeAll();

        IEnumerable<RequestType> getRequestTypeAll();

        IEnumerable<TreatmentCategory> getTreatmentCategoriesAll();

        IEnumerable<TreatmentType> getTreatmentTypesAll();

        IEnumerable<TreatmentType> getTreatmentTypesByTreatmentCategoryID(int _treatmentCategoryID);

        IEnumerable<DocumentCategory> getDocumentCategoriesAll();

        IEnumerable<DocumentType> getDocumentTypesAll();

        IEnumerable<DocumentType> getDocumentTypeByDocumentCategoryID(int _documentCategoryID);

        IEnumerable<DurationType> getDurationTypesAll();

        IEnumerable<IMRDecision> getIMRDecisionAll();

        int AddProcessLevelByReferralID(int _referralID, int _processLevel);

        int getMaxProcessLevelByReferralID(int _referralID);

        IEnumerable<RFAProcessLevels> getProcessLevelsByReferralID(int _referralID);

        RFAProcessLevels getPreviousProcessLevelsByReferralID(int _referralID);
   

        string getPatientComorbidConditionsByPatientID(int PatientID);

        BLModel.StorageModel GetStorageStuctureByID(int _id, char _by);

        BLModel.Paged.AdditionalDocument getAdditionalDocumentsByPatientID(int _patientID,int _patientClaimID, int _skip, int _take);
        IEnumerable<NoOfReferralCount> getNoOfReferralCountAccToProcessLevel();
    }
}