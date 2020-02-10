using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class ThirdPartyAdministratorTest
    {
        IThirdPartyAdministratorRepository _thirdPartyAdministratorRepository;

        public ThirdPartyAdministratorTest()
        {
            _thirdPartyAdministratorRepository = new ThirdPartyAdministratorRepository();
        }
        [TestMethod]
        public void addThirdPartyAdministrator()
        {
            ThirdPartyAdministrator _ThirdPartyAdministrator = new ThirdPartyAdministrator
            {
                TPAName = "TPA2sa3",
                TPAAddress = "TPAs34Addsss1",
                TPAAddress2 = "TP3ss2",
                TPACity = "TPACi4ty",
                TPAStateId = 1,
                TPAZip = "21311",
                TPAPhone = "(904) 108-0635",
                TPAFax = "(904) 108-0635"
            };
            var _id = _thirdPartyAdministratorRepository.addThirdPartyAdministrator(_ThirdPartyAdministrator);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateThirdPartyAdministrato()
        {
            ThirdPartyAdministrator _ThirdPartyAdministrator = new ThirdPartyAdministrator
            {
                TPAName = "TPANameu",
                TPAAddress = "TPAAddress1weu",
                TPAAddress2 = "TPAAddresseew2",
                TPACity = "TPACitwerewy",
                TPAStateId = 1,
                TPAZip = "213121",
                TPAID = 1
            };
            var _id = _thirdPartyAdministratorRepository.updateThirdPartyAdministrator(_ThirdPartyAdministrator);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteThirdPartyAdministrator()
        {
            _thirdPartyAdministratorRepository.deleteThirdPartyAdministrator(3);
        }
        [TestMethod]
        public void getThirdPartyAdministratorsAll()
        {
            var _ThirdPartyAdministrator = _thirdPartyAdministratorRepository.getThirdPartyAdministratorsAll();
            Assert.IsTrue(_ThirdPartyAdministrator != null, "failed");
        }
        [TestMethod]
        public void getThirdPartyAdministratorByID()
        {
            var _ThirdPartyAdministrator = _thirdPartyAdministratorRepository.getThirdPartyAdministratorByID(1);
            Assert.IsTrue(_ThirdPartyAdministrator != null, "failed");
        }

        [TestMethod]
        public void getThirdPartyAdministratorsByName()
        {
            string SearchText = "t";
            int _skip = 0;
            int _take = 10;
            var _ThirdPartyAdministrator = _thirdPartyAdministratorRepository.getThirdPartyAdministratorsByName(SearchText, _skip, _take);
            Assert.IsTrue(_ThirdPartyAdministrator != null, "failed");
        }
    }
}
