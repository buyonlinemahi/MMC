using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class AdjusterTest
    {
        IAdjusterRepository _adjusterRepository;

        public AdjusterTest()
        {
            _adjusterRepository = new AdjusterRepository();
        }
        [TestMethod]
        public void addAdjuster()
        {
            Adjuster _adjuster = new Adjuster
            {
                AdjFirstName = "AdjFirstName2",
                AdjLastName = "vLastName2",
                AdjPhone = "2135649872",
                AdjAddress1 = "AdjAddress1",
                AdjAddress2 = "AdjAddress2",
                AdjFax = "2135469872",
                AdjEMail = "a@gmail.com",
                AdjZip = "90024",
                AdjCity = "AdjCity2",
                AdjStateId = 1,
                ClientID = 2
            };
            var _id = _adjusterRepository.addAdjuster(_adjuster);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateAdjuster()
        {
            Adjuster _adjuster = new Adjuster
            {
                AdjusterID = 48,
                AdjFirstName = "ptest",
                AdjLastName = "ptestLa",
                AdjPhone = "2135649872",
                AdjAddress1 = "AdjAddress1",
                AdjAddress2 = "AdjAddress2",
                AdjFax = "2135469872",
                AdjEMail = "a2@gmail.com",
                AdjZip = "90024",
                AdjCity = "AdjCity1",
                AdjStateId = 1,
                ClientID = 2
            };
            var _id = _adjusterRepository.updateAdjuster(_adjuster);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteAdjuster()
        {
            _adjusterRepository.deleteAdjuster(1);
        }
        [TestMethod]
        public void getAllAdjusters()
        {
            var adjusters = _adjusterRepository.getadjustersAll();
            Assert.IsTrue(adjusters != null, "failed");
        }
        [TestMethod]
        public void getAdjusterByID()
        {
            var adjusters = _adjusterRepository.getAdjusterByID(48);
            Assert.IsTrue(adjusters != null, "failed");
        }

        [TestMethod]
        public void getAllAdjustersName()
        {
            string SearchText = "a";
            int _skip = 10;
            int _take = 10;
            var adjusters = _adjusterRepository.getadjustersByName(SearchText, _skip, _take);
            Assert.IsTrue(adjusters != null, "failed");
        }

    }
}
