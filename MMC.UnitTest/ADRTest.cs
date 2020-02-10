using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class ADRTest
    {
        IADRRepository _IADRRepository;
        public ADRTest()
        {
            _IADRRepository = new ADRRepository();
        }
        [TestMethod]
        public void addADR()
        {
            ADR _ADR = new ADR
            {
                ADRName = "Name",
                ADRAddress = "address1",
                ADRAddress2 = "address2",
                ADRCity = "city1",
                ADRStateID = 1,
                ADRZip = "12343",                
            };
            var _id = _IADRRepository.addADR(_ADR);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateADR()
        {
            ADR _ADR = new ADR
            {
                ADRName = "Nameeqwqew",
                ADRAddress = "address1ewe",
                ADRAddress2 = "address2eqwewq",
                ADRCity = "city1wqewq",
                ADRStateID = 2,
                ADRZip = "12133",
                ADRID = 1
            };
            var _id = _IADRRepository.updateADR(_ADR);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteADR()
        {
            _IADRRepository.deleteADR(2);
        }
        [TestMethod]
        public void getAllADRs()
        {
            var adr = _IADRRepository.getADRsAll();
            Assert.IsTrue(adr != null, "failed");
        }
        [TestMethod]
        public void getADRByID()
        {
            var adr = _IADRRepository.getADRByID(1);
            Assert.IsTrue(adr != null, "failed");
        }
        [TestMethod]
        public void getAllADRsName()
        {
            string SearchText = "N";
            int _skip = 0;
            int _take = 10;
            var adrs = _IADRRepository.getADRsByName(SearchText, _skip, _take);
            Assert.IsTrue(adrs != null, "failed");
        }
    }
}
