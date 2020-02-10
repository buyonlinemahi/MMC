using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
   public class CaseManagerTest
    {
         ICaseManagerRepository _caseManagerRepository;
         public CaseManagerTest()
        {
            _caseManagerRepository = new CaseManagerRepository();
        }
        [TestMethod]
        public void addCaseManager()
        {
            CaseManager _caseManager = new CaseManager
            {
                CMFirstName = "hMFirst12",
                CMLastName = "dMLast12",
                CMAddress1 = "CMAddress1",
                CMAddress2 = "CMAddress2",
                CMCity = "CMCity",
                CMStateId =1,
                CMZip = "14101",
                CMPhone = "1234567890",
                CMFax = "1234567890",
                CMEmail = "s@s.com",
                CMNotes = "skafla fdsaflsaf fasfsafa fasfs"
            };
            var _id = _caseManagerRepository.addCaseManager(_caseManager);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void updateCaseManager()
        {
            CaseManager _caseManager = new CaseManager
            {
                CaseManagerID = 2,
                CMFirstName = "CMFirstdfs",
                CMLastName = "CMLastfsdf",
                CMAddress1 = "CMAddrefsfss1",
                CMAddress2 = "CMAddrefsdfss2",
                CMCity = "CMCity",
                CMStateId =1,
                CMZip = "14122",
                CMPhone = "1234567890",
                CMFax = "1234567890",
                CMEmail = "s@s.com",
                CMNotes = "skafla fdsaflsaf fasfsafa fasfs"
            };
            var _id = _caseManagerRepository.updateCaseManager(_caseManager);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteCaseManager()
        {
            _caseManagerRepository.deleteCaseManager(3);
        }
        [TestMethod]
        public void getCaseManagerAll()
        {
            var _caseManager = _caseManagerRepository.getCaseManagerAll();
            Assert.IsTrue(_caseManager != null, "failed");
        }
        [TestMethod]
        public void getCaseManagerByName()
        {
            var _caseManager = _caseManagerRepository.getCaseManagerByName("h", 0, 2);
            Assert.IsTrue(_caseManager != null, "failed");
        }
        [TestMethod]
        public void getCaseManagerByID()
        {
            var _caseManager = _caseManagerRepository.getCaseManagerByID(1);
            Assert.IsTrue(_caseManager != null, "failed");
        }
    }
}
