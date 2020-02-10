using AutoMapper;
using MMC.Core.BL;
using MMCService.CustomServiceBehaviors;
using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AutoMapperServiceBehavior]

    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;

        public CommonService(ICommonRepository iCommonRepository)
        {
            _commonRepository = iCommonRepository;
        }

        #region AttorneyFirmType
        public IEnumerable<DTO.AttorneyFirmType> getAttorneyFirmTypeAll()
        {
            return Mapper.Map<IEnumerable<DTO.AttorneyFirmType>>(_commonRepository.getAttorneyFirmTypeAll());
        }
        #endregion
        #region Language

        public IEnumerable<DTO.Language> getLanguageAll()
        {
            return Mapper.Map<IEnumerable<DTO.Language>>(_commonRepository.getLanguageAll());
        }

        #endregion

        #region States

        public IEnumerable<DTO.State> getStateAll()
        {
            return Mapper.Map<IEnumerable<DTO.State>>(_commonRepository.getStateAll());
        }

        #endregion


        #region Status

        public IEnumerable<DTO.Status> getStatusAll()
        {
            return Mapper.Map<IEnumerable<DTO.Status>>(_commonRepository.getStatusAll());
        }

        #endregion

        #region Specialty

        public IEnumerable<DTO.Specialty> getSpecialtiesAll()
        {
            return Mapper.Map<IEnumerable<DTO.Specialty>>(_commonRepository.getSpecialtiesAll());
        }

        #endregion

        #region CurrentMedicalCondition

        public IEnumerable<DTO.CurrentMedicalCondition> getCurrentMedicalConditionsAll()
        {
            return Mapper.Map<IEnumerable<DTO.CurrentMedicalCondition>>(_commonRepository.getCurrentMedicalConditionsAll());
        }

        #endregion

        #region BodyPart

        public IEnumerable<DTO.BodyPart> getBodyPartsAll()
        {
            return Mapper.Map<IEnumerable<DTO.BodyPart>>(_commonRepository.getBodyPartsAll());
        }

        #endregion

        #region BodyPartStatus

        public IEnumerable<DTO.BodyPartStatus> getBodyPartStatusesAll()
        {
            return Mapper.Map<IEnumerable<DTO.BodyPartStatus>>(_commonRepository.getBodyPartStatusesAll());
        }

        #endregion

        #region ICD9Code

        public IEnumerable<DTO.ICD9Code> getICD9CodesAll()
        {
            return Mapper.Map<IEnumerable<DTO.ICD9Code>>(_commonRepository.getICD9CodesAll());
        }

        public IEnumerable<DTO.ICD10Code> getICD10CodesAll()
        {
            return Mapper.Map<IEnumerable<DTO.ICD10Code>>(_commonRepository.getICD10CodesAll());
        }

        public DTO.Paged.PagedICD9Code getICD9CodesByName(string _searchtext, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedICD9Code>(_commonRepository.getICD9CodesByName(_searchtext, _skip, _take));
        }
        public IEnumerable<ICD9Code> getICD9CodesByCode(string _searchtext)
        {
            return Mapper.Map<IEnumerable<DTO.ICD9Code>>(_commonRepository.getICD9CodesByCode(_searchtext));
        }
        public IEnumerable<ICDCode> getICDCodesByNumber(string _ICDNumber, string _ICDTab)
        {
            return Mapper.Map<IEnumerable<DTO.ICDCode>>(_commonRepository.getICDCodesByNumber(_ICDNumber, _ICDTab));
        }
        public DTO.Paged.PagedICDCode getICDCodesByNumberOrDescription(string _searchtext, string ICDTab, int skip, int take)
        {
            return Mapper.Map<DTO.Paged.PagedICDCode>(_commonRepository.getICDCodesByNumberOrDescription(_searchtext, ICDTab, skip, take));
        }

        #endregion

        #region ClaimStatus

        public IEnumerable<DTO.ClaimStatus> getClaimStatusesAll()
        {
            return Mapper.Map<IEnumerable<DTO.ClaimStatus>>(_commonRepository.getClaimStatusesAll());
        }

        #endregion

        public IEnumerable<DTO.FrequencyType> getFrequencyTypeAll()
        {
            return Mapper.Map<IEnumerable<DTO.FrequencyType>>(_commonRepository.getFrequencyTypeAll());
        }

        public IEnumerable<DTO.RequestType> getRequestTypeAll()
        {
            return Mapper.Map<IEnumerable<DTO.RequestType>>(_commonRepository.getRequestTypeAll());
        }

        public IEnumerable<DTO.TreatmentCategory> getTreatmentCategoriesAll()
        {
            return Mapper.Map<IEnumerable<DTO.TreatmentCategory>>(_commonRepository.getTreatmentCategoriesAll());
        }

        public IEnumerable<DTO.TreatmentType> getTreatmentTypesAll()
        {
            return Mapper.Map<IEnumerable<DTO.TreatmentType>>(_commonRepository.getTreatmentTypesAll());
        }

        public IEnumerable<DTO.TreatmentType> getTreatmentTypesByTreatmentCategoryID(int _treatmentCategoryID)
        {
            return Mapper.Map<IEnumerable<DTO.TreatmentType>>(_commonRepository.getTreatmentTypesByTreatmentCategoryID(_treatmentCategoryID));
        }

        public IEnumerable<DTO.DocumentCategory> getDocumentCategoriesAll()
        {
            return Mapper.Map<IEnumerable<DTO.DocumentCategory>>(_commonRepository.getDocumentCategoriesAll());
        }

        public IEnumerable<DTO.DocumentType> getDocumentTypesAll()
        {
            return Mapper.Map<IEnumerable<DTO.DocumentType>>(_commonRepository.getDocumentTypesAll());
        }

        public IEnumerable<DTO.DocumentType> getDocumentTypeByDocumentCategoryID(int _documentCategoryID)
        {
            return Mapper.Map<IEnumerable<DTO.DocumentType>>(_commonRepository.getDocumentTypeByDocumentCategoryID(_documentCategoryID));
        }

        public IEnumerable<DTO.DurationType> getDurationTypesAll()
        {
            return Mapper.Map<IEnumerable<DTO.DurationType>>(_commonRepository.getDurationTypesAll());
        }

        public int AddProcessLevelByReferralID(int _referralID, int _processLevel)
        {
            return _commonRepository.AddProcessLevelByReferralID(_referralID, _processLevel);
        }

        public int getMaxProcessLevelByReferralID(int _referralID) 
        {
            return _commonRepository.getMaxProcessLevelByReferralID(_referralID);
        }

        public IEnumerable<DTO.RFAProcessLevels> getProcessLevelsByReferralID(int _referralID)
        {
            return Mapper.Map<IEnumerable<DTO.RFAProcessLevels>>(_commonRepository.getProcessLevelsByReferralID(_referralID));
        }

        public string getPatientComorbidConditionsByPatientID(int PatientID)
        {
            return _commonRepository.getPatientComorbidConditionsByPatientID(PatientID);
        }

        public  DTO.StorageModel GetStorageStuctureByID(int _id, char _by)
        {
            return Mapper.Map<DTO.StorageModel>(_commonRepository.GetStorageStuctureByID(_id, _by));
        }
        public RFAProcessLevels getPreviousProcessLevelsByReferralID(int _referralID)
        {
            return Mapper.Map<DTO.RFAProcessLevels>(_commonRepository.getPreviousProcessLevelsByReferralID(_referralID));
        }
        public DTO.Paged.PagedAdditionalDocument getAdditionalDocumentsByPatientID(int _patientID,int _patientClaimID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedAdditionalDocument>(_commonRepository.getAdditionalDocumentsByPatientID(_patientID, _patientClaimID, _skip, _take));
        }

        public IEnumerable<DTO.NoOfReferralCount> getNoOfReferralCountAccToProcessLevel()
        {
            return Mapper.Map<IEnumerable<DTO.NoOfReferralCount>>(_commonRepository.getNoOfReferralCountAccToProcessLevel());
        }
    }
}
