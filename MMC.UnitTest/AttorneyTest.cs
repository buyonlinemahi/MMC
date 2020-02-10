using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class AttorneyTest
    {
        IAttorneyRepository _attorneyRepository;

        public AttorneyTest()
        {
            _attorneyRepository = new AttorneyRepository();
        }

        #region Attorney
        [TestMethod]
        public void addAttorneys()
        {
            Attorney _attorney = new Attorney
            {
                AttorneyName = "kAjaa",
                AttPhone = "9875656345",
                AttorneyFirmID = 2,
                AttorneyID = 0,
                AttFax = "6524998786",
                AttEmail = "rajTara@gmail.com"
            };
            var _id = _attorneyRepository.addAttorney(_attorney);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateAttorneys()
        {
            Attorney _attorney = new Attorney
            {
                AttorneyName = "RajaTara",
                AttPhone = "9875656345",
                AttorneyFirmID = 2,
                AttorneyID = 2,
                AttFax = "6524998786",
                AttEmail = "rajTara@gmail.com"
            };
            var _id = _attorneyRepository.addAttorney(_attorney);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteAttorneys()
        {
            _attorneyRepository.deleteAttorney(3);
        }
        [TestMethod]
        public void getAllAttorneys()
        {
            var attorneyAll = _attorneyRepository.getAttorneyAll();
            Assert.IsTrue(attorneyAll != null, "failed");
        }
        [TestMethod]
        public void getAttorneysByID()
        {
            var attorneyByid = _attorneyRepository.getAttorneyByID(2);
            Assert.IsTrue(attorneyByid != null, "failed");
        }
        [TestMethod]
        public void getAttorneysByAttorenyFirmByID()
        {
            var attorneyByName = _attorneyRepository.getAttorneyByAttorneyFirmID(2, 0, 4);
            Assert.IsTrue(attorneyByName != null, "failed");
        }

        [TestMethod]
        public void getAttorneysByAttorenyFirmTypesID()
        {
            var attorneyByName = _attorneyRepository.getAttorneyRecordsByFirmTypeID(1);
            Assert.IsTrue(attorneyByName != null, "failed");
        }
        #endregion

        #region AttorneyFirm
        [TestMethod]
        public void addAttorneyFirms()
        {
            AttorneyFirm _attorneyFirm = new AttorneyFirm
            {
                AttorneyFirmName = "Gurleen",
                AttAddress1 = "Jal Baba buda ji enclave",
                AttAddress2 = "fdg ",
                AttCity = "Nokadar",
                AttorneyFirmTypeID = 2,
                AttorneyFirmID = 0,
                AttPhone = "111111111",
                AttFax = "998559999",
                AttStateID = 7,
                AttZip = "9000777"
            };
            var _id = _attorneyRepository.addAttorneyFirm(_attorneyFirm);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateAttorneyFirms()
        {
            AttorneyFirm _attorneyFirm = new AttorneyFirm
            {
                AttorneyFirmName = "RohitTara",
                AttAddress1 = "9875656345",
                AttAddress2 = "9875656345",
                AttCity = "Jalandhar",
                AttorneyFirmTypeID = 2,
                AttorneyFirmID = 2,
                AttPhone = "9888564646",
                AttFax = "6524998786",
                AttStateID = 4,
                AttZip = "12255522"
            };
            var _id = _attorneyRepository.updateAttorneyFirm(_attorneyFirm);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteAttorneyFirms()
        {
            _attorneyRepository.deleteAttorneyFirm(11);
        }
        [TestMethod]
        public void getAllAttorneyFirms()
        {
            var attorneyAll = _attorneyRepository.getAttorneyFirmAll();
            Assert.IsTrue(attorneyAll != null, "failed");
        }
        [TestMethod]
        public void getAttorneyFirmsByID()
        {
            var attorneyByALL = _attorneyRepository.getAttorneyFirmByID(3);
            Assert.IsTrue(attorneyByALL != null, "failed");
        }

        [TestMethod]
        public void getAttorneyAndAttorneyFirm()
        {
            var attorneyFirmByAll = _attorneyRepository.getAttorneyAndAttorneyFirmByName("k", 0, 5);
            Assert.IsTrue(attorneyFirmByAll != null, "failed");
        }
        #endregion
    }
}
