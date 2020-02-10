using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class CommonTest
    {
        ICommonRepository _commonRepository;
        public CommonTest()
        {
            _commonRepository = new CommonRepository();
        }

        [TestMethod]
        public void getStateAll()
        {
            var _state = _commonRepository.getStateAll();
            Assert.IsTrue(_state != null, "failed");
        }

        [TestMethod]
        public void getLanguageAll()
        {
            var Lang = _commonRepository.getLanguageAll();
            Assert.IsTrue(Lang != null, "failed");
        }
        [TestMethod]
        public void getCurrentMedicalConditionsAll()
        {
            var _cmc = _commonRepository.getCurrentMedicalConditionsAll();
            Assert.IsTrue(_cmc != null, "failed");
        }
        [TestMethod]
        public void getSpecialtiesAll()
        {
            var _specialties = _commonRepository.getSpecialtiesAll();
            Assert.IsTrue(_specialties != null, "failed");
        }

        [TestMethod]
        public void getClaimStatusesAll()
        {
            var _claimStatus = _commonRepository.getClaimStatusesAll();
            Assert.IsTrue(_claimStatus != null, "failed");
        }

        [TestMethod]
        public void getICD9CodesAll()
        {
            var _icd = _commonRepository.getICD9CodesAll();
            Assert.IsTrue(_icd != null, "failed");
        }

        [TestMethod]
        public void getICD9CodesByName()
        {
            var _icd = _commonRepository.getICD9CodesByName("011", 0, 10);
            Assert.IsTrue(_icd != null, "failed");
        }
        [TestMethod]
        public void getICD9CodesByCode()
        {
            var _icd = _commonRepository.getICD9CodesByCode("011");
            Assert.IsTrue(_icd != null, "failed");
        }

        [TestMethod]
        public void getBodyPartsAll()
        {
            var _bps = _commonRepository.getBodyPartsAll();
            Assert.IsTrue(_bps != null, "failed");
        }

        [TestMethod]
        public void getAttorneyFirmsTypesAll()
        {
            var _attfirmType = _commonRepository.getAttorneyFirmTypeAll();
            Assert.IsTrue(_attfirmType != null, "failed");
        }

        [TestMethod]
        public void getBodyPartStatusesAll()
        {
            var _bps = _commonRepository.getBodyPartStatusesAll();
            Assert.IsTrue(_bps != null, "failed");
        }
        [TestMethod]
        public void AddProcessLevelByReferralID()
        {
            var _bps = _commonRepository.AddProcessLevelByReferralID(61,10);
            Assert.IsTrue(_bps != null, "failed");
        }

        [TestMethod]
        public void getMaxProcessLevelByReferralID()
        {
            var _bps = _commonRepository.getMaxProcessLevelByReferralID(32);
            Assert.IsTrue(_bps != null, "failed");
        }

        [TestMethod]
        public void getProcessLevelsByReferralID()
        {
            var _bps = _commonRepository.getProcessLevelsByReferralID(32);
            Assert.IsTrue(_bps != null, "failed");
        }
        [TestMethod]
        public void getDurationTypesAll()
        {
            var _bps = _commonRepository.getDurationTypesAll();
            Assert.IsTrue(_bps != null, "failed");
        }
        [TestMethod]
        public void getPatientComorbidConditionsByPatientID()
        {
            var _bps = _commonRepository.getPatientComorbidConditionsByPatientID(92);
            Assert.IsTrue(_bps != null, "failed");
        }
        [TestMethod]
        public void GetStorageStuctureByID()
        {
            var _bps = _commonRepository.GetStorageStuctureByID(32, 'R');
            Assert.IsTrue(_bps != null, "failed");
        }
        [TestMethod]
        public void getPreviousProcessLevelsByReferralID()
        {
            var _bps = _commonRepository.getPreviousProcessLevelsByReferralID(396);
            Assert.IsTrue(_bps != null, "failed");
        }

        [TestMethod]
        public void GetICDCodesByNumberOrDescription()
        {
            var _icdCode = _commonRepository.getICDCodesByNumberOrDescription("002", "ICD9", 0, 100);
            Assert.IsTrue(_icdCode != null, "failed");
        }

        [TestMethod]
        public void GETICDCodesByNumber()
        {
            var _icdCode = _commonRepository.getICDCodesByNumber("0010", "ICD9");
            Assert.IsTrue(_icdCode != null, "failed");
        }

        [TestMethod]
        public void GETAdditionalDocumentsByPatientID()
        {
            var _GetAdditional = _commonRepository.getAdditionalDocumentsByPatientID(130,152,20,20);
            Assert.IsTrue(_GetAdditional != null, "failed");
        }

        [TestMethod]
        public void GETNoOfReferralCountAccToProcessLevel()
        {
            var _getProcessLevel = _commonRepository.getNoOfReferralCountAccToProcessLevel();
            Assert.IsTrue(_getProcessLevel != null, "failed");
        }
    }
}
