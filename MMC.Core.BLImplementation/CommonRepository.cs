using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
using DLModel = MMC.Core.Data.Model;

namespace MMC.Core.BLImplementation
{
    public class CommonRepository : ICommonRepository
    {
        MMCDbContext _MMCDbContext;
        private readonly BaseRepository<RFAProcessLevels> _rfaProcessLevelsRepository;
        public CommonRepository()
        {
            _MMCDbContext = new MMCDbContext();
            _rfaProcessLevelsRepository = new BaseRepository<RFAProcessLevels>();
        }

        public IEnumerable<State> getStateAll()
        {
            return _MMCDbContext.states.ToList().OrderBy(st => st.StateName);
        }

        public IEnumerable<Language> getLanguageAll()
        {
            return _MMCDbContext.languages.ToList().OrderBy(Lang => Lang.LanguageName);
        }

        public IEnumerable<Specialty> getSpecialtiesAll()
        {
            return _MMCDbContext.specialties.ToList().OrderBy(spty => spty.SpecialtyName);
        }
        public IEnumerable<CurrentMedicalCondition> getCurrentMedicalConditionsAll()
        {
            return _MMCDbContext.currentMedicalConditions.ToList().OrderBy(cmc => cmc.CurrentMedicalConditionName);
        }

        public IEnumerable<ClaimStatus> getClaimStatusesAll()
        {
            return _MMCDbContext.claimStatuses.ToList().OrderBy(clms => clms.ClaimStatusName);
        }

        #region ICDCode
        public IEnumerable<ICD9Code> getICD9CodesAll()
        {
            return _MMCDbContext.iCD9Codes.Where(icd => icd.ICD9Type.Equals(MMC.Core.Data.Model.Constant.Constant.ConstantValue.ICD9Type)).ToList().OrderBy(icd => icd.icdICD9Number);
        }

        public IEnumerable<ICD10Code> getICD10CodesAll()
        {
            //NEW
            return _MMCDbContext.iCD10Codes.ToList().OrderBy(icd => icd.icdICD10NumberID);
        }

        public BLModel.Paged.ICD9Code getICD9CodesByName(string _searchtext, int _skip, int _take)
        {
            var _ICD9CodeByName = _MMCDbContext.iCD9Codes.Where(icd => (icd.icdICD9Number.Contains(_searchtext) || icd.ICD9Description.Contains(_searchtext)) && icd.ICD9Type.Equals(MMC.Core.Data.Model.Constant.Constant.ConstantValue.ICD9Type)).ToList().OrderBy(icd => icd.icdICD9Number).Skip(_skip).Take(_take).ToList();
            return new BLModel.Paged.ICD9Code
            {
                ICD9CodeDetails = _ICD9CodeByName.Select(pcd => new BLModel.ICD9Code().InjectFrom(pcd)).Cast<BLModel.ICD9Code>().ToList(),
                TotalCount = _MMCDbContext.iCD9Codes.Where(icd => (icd.icdICD9Number.Contains(_searchtext) || icd.ICD9Description.Contains(_searchtext)) && icd.ICD9Type.Equals(MMC.Core.Data.Model.Constant.Constant.ConstantValue.ICD9Type)).Count()
            };

        }

        public BLModel.Paged.ICDCode getICDCodesByNumberOrDescription(string _searchtext, string ICDTab, int skip, int take)
        {
            SPImpl _spimpl = new SPImpl();
            string _icdType = "";
            if (ICDTab == DLModel.Constant.Constant.ConstantValue.ICD9)
                _icdType = DLModel.Constant.Constant.ConstantValue.ICD9Type;
            else if (ICDTab == DLModel.Constant.Constant.ConstantValue.ICD10)
                _icdType = DLModel.Constant.Constant.ConstantValue.ICD10Type;

            return new BLModel.Paged.ICDCode
            {
                ICDCodeDetails = _spimpl.getICDCodesByNumberOrDescription(_searchtext, _icdType, ICDTab, skip, take),
                TotalCount = _spimpl.getICDCodesByNumberOrDescriptionCount(_searchtext, DLModel.Constant.Constant.ConstantValue.ICD9Type, ICDTab)              
            };
        }

        public IEnumerable<ICD9Code> getICD9CodesByCode(string _searchtext)
        {
            return _MMCDbContext.iCD9Codes.Where(icd => icd.icdICD9Number.Contains(_searchtext) && icd.ICD9Type.Equals(MMC.Core.Data.Model.Constant.Constant.ConstantValue.ICD9Type)).OrderBy(icd => icd.icdICD9Number).ToList();
        }

        public IEnumerable<ICDCode> getICDCodesByNumber(string _ICDNumber, string _ICDTab)
        {
            SPImpl _spImp = new SPImpl();
            return _spImp.getICDCodesByNumber(_ICDNumber, _ICDTab);
        }
        #endregion

        public IEnumerable<BodyPart> getBodyPartsAll()
        {
            return _MMCDbContext.bodyParts.ToList().OrderBy(bps => bps.BodyPartName);
        }

        public IEnumerable<BodyPartStatus> getBodyPartStatusesAll()
        {
            return _MMCDbContext.BodyPartStatuses.ToList().OrderBy(bps => bps.BodyPartStatusName);
        }

        public IEnumerable<FrequencyType> getFrequencyTypeAll()
        {
            return _MMCDbContext.frequencyTypes.ToList().OrderBy(FT => FT.FrequencyTypeName);
        }

        public IEnumerable<RequestType> getRequestTypeAll()
        {
            return _MMCDbContext.requestTypes.ToList().OrderBy(RT => RT.RequestTypeName);
        }

        public IEnumerable<TreatmentCategory> getTreatmentCategoriesAll()
        {
            return _MMCDbContext.treatmentCategories.ToList();
        }

        public IEnumerable<TreatmentType> getTreatmentTypesAll()
        {
            return _MMCDbContext.treatmentTypes.ToList();
        }

        public IEnumerable<TreatmentType> getTreatmentTypesByTreatmentCategoryID(int _treatmentCategoryID)
        {
            return _MMCDbContext.treatmentTypes.Where(hp => hp.TreatmentCategoryID == _treatmentCategoryID).ToList();
        }

        public IEnumerable<DocumentCategory> getDocumentCategoriesAll()
        {
            return _MMCDbContext.documentCategories.ToList();
        }

        public IEnumerable<DocumentType> getDocumentTypeByDocumentCategoryID(int _documentCategoryID)
        {
            return _MMCDbContext.documentTypes.Where(rk => rk.DocumentCategoryID == _documentCategoryID).ToList();
        }

        public IEnumerable<DocumentType> getDocumentTypesAll()
        {
            return _MMCDbContext.documentTypes.ToList();
        }

        public IEnumerable<DurationType> getDurationTypesAll()
        {
            return _MMCDbContext.durationTypes.ToList();
        }

        public IEnumerable<IMRDecision> getIMRDecisionAll()
        {
            return _MMCDbContext.imrDecision.ToList();
        }

        public IEnumerable<AttorneyFirmType> getAttorneyFirmTypeAll()
        {
            return _MMCDbContext.attorneyFirmTypes.ToList();
        }


        public int AddProcessLevelByReferralID(int _referralID, int _processLevel)
        {
            var _prcLvl = _rfaProcessLevelsRepository.GetAll(hp => hp.ProcessLevel == _processLevel && hp.RFAReferralID == _referralID).ToList();
            if (_prcLvl.Count() == 0)
            {
                return _rfaProcessLevelsRepository.Add(new RFAProcessLevels { RFAReferralID = _referralID, ProcessLevel = _processLevel }).RFAProcessLevelID;
            }
            else
                return _prcLvl.FirstOrDefault().RFAProcessLevelID;
        }

        public RFAProcessLevels getPreviousProcessLevelsByReferralID(int _referralID)
        {
            return _rfaProcessLevelsRepository.GetAll(hp => hp.RFAReferralID == _referralID).Reverse().Skip(1).Take(1).SingleOrDefault();
        }

        public int getMaxProcessLevelByReferralID(int _referralID)
        {
            return _rfaProcessLevelsRepository.GetAll(hp => hp.RFAReferralID == _referralID).Max(hp => hp.ProcessLevel);
        }

        public IEnumerable<RFAProcessLevels> getProcessLevelsByReferralID(int _referralID)
        {
            return _rfaProcessLevelsRepository.GetAll(hp => hp.RFAReferralID == _referralID);
        }

        public string getPatientComorbidConditionsByPatientID(int PatientID)
        {
            SPImpl _spImp = new SPImpl();
            return _spImp.getPatientComorbidConditionsByPatientID(PatientID);
        }
        public BLModel.StorageModel GetStorageStuctureByID(int _id, char _by)
        {
            SPImpl _spImp = new SPImpl();
            return _spImp.GetStorageStuctureByID(_id, _by);
        }
        public BLModel.Paged.AdditionalDocument getAdditionalDocumentsByPatientID(int _patientID, int _patientClaimID, int _skip, int _take)
        {
            SPImpl _SPImpl = new SPImpl();
            return new BLModel.Paged.AdditionalDocument
            {
                AdditionalDocumentDetails = _SPImpl.getAdditionalDocumentByPatientID(_patientID, _patientClaimID, _skip, _take),
                TotalCount = _SPImpl.getAdditionalDocumentByPatientIDCount(_patientID, _patientClaimID)
            };

        }
        public IEnumerable<NoOfReferralCount> getNoOfReferralCountAccToProcessLevel()
        {
            SPImpl _SPImpl = new SPImpl();

            return _SPImpl.getNoOfReferralCountForDifferentProcessLevel();
        }
        public IEnumerable<Status> getStatusAll()
        {
            return _MMCDbContext.statuses.ToList().OrderBy(st => st.StatusName);
        }
    }
}