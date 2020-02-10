using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class ThirdPartyAdministratorBranchTest
    {
        IThirdPartyAdministratorBranchRepository _thirdPartyAdministratorBranchRepository;

        public ThirdPartyAdministratorBranchTest()
        {
            _thirdPartyAdministratorBranchRepository = new ThirdPartyAdministratorBranchRepository();
        }
        [TestMethod]
        public void addThirdPartyAdministratorBranch()
        {
            ThirdPartyAdministratorBranch _ThirdPartyAdministratorBranch = new ThirdPartyAdministratorBranch
            {
                TPABranchName = "TPswq",
                TPABranchAddress = "TPwewq",
                TPABranchAddress2 = "TPw3ewq",
                TPABranchCity = "dwwewsas",
                TPABranchStateId = 1,
                TPABranchZip = "21221",
                TPABranchPhone = "(904) 108-0635",
                TPABranchFax = "(904) 108-0635",
                TPAID = 1
            };
            var _id = _thirdPartyAdministratorBranchRepository.addThirdPartyAdministratorBranch(_ThirdPartyAdministratorBranch);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateThirdPartyAdministratorBranch()
        {
            ThirdPartyAdministratorBranch _ThirdPartyAdministratorBranch = new ThirdPartyAdministratorBranch
            {
                TPABranchName = "T43",
                TPABranchAddress = "4twt",
                TPABranchAddress2 = "Ttwe",
                TPABranchCity = "TPAwet4ty",
                TPABranchStateId = 1,
                TPABranchZip = "21311",
                TPAID = 2,
                TPABranchID = 3
            };
            var _id = _thirdPartyAdministratorBranchRepository.updateThirdPartyAdministratorBranch(_ThirdPartyAdministratorBranch);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteThirdPartyAdministratorBranch()
        {
            _thirdPartyAdministratorBranchRepository.deleteThirdPartyAdministratorBranch(3);
        }
        [TestMethod]
        public void getThirdPartyAdministratorsAll()
        {
            var _ThirdPartyAdministratorBranch = _thirdPartyAdministratorBranchRepository.getThirdPartyAdministratorBranchesAll();
            Assert.IsTrue(_ThirdPartyAdministratorBranch != null, "failed");
        }
        [TestMethod]
        public void getThirdPartyAdministratorBranchByID()
        {
            var _ThirdPartyAdministratorBranch = _thirdPartyAdministratorBranchRepository.getThirdPartyAdministratorBranchByID(1);
            Assert.IsTrue(_ThirdPartyAdministratorBranch != null, "failed");
        }

        [TestMethod]
        public void getThirdPartyAdministratorBranchesByName()
        {
            string SearchText = "t";
            int _skip = 0;
            int _take = 10;
            var _ThirdPartyAdministrator = _thirdPartyAdministratorBranchRepository.getThirdPartyAdministratorBranchesByName(SearchText, _skip, _take);
            Assert.IsTrue(_ThirdPartyAdministrator != null, "failed");
        }
        [TestMethod]
        public void getThirdPartyAdministratorBranchesByTPAID()
        {
            int _thirdPartyAdministratorId = 1;
            int _skip = 0;
            int _take = 10;
            var _ThirdPartyAdministrator = _thirdPartyAdministratorBranchRepository.getThirdPartyAdministratorBranchesByTPAID(_thirdPartyAdministratorId, _skip, _take);
            Assert.IsTrue(_ThirdPartyAdministrator != null, "failed");
        }
        [TestMethod]
        public void getThirdPartyAdministratorBranchesAllByTPAID()
        {
            var _ThirdPartyAdministratorBranch = _thirdPartyAdministratorBranchRepository.getThirdPartyAdministratorBranchesAllByTPAID(11);
            Assert.IsTrue(_ThirdPartyAdministratorBranch != null, "failed");
        }


        
    }
}
