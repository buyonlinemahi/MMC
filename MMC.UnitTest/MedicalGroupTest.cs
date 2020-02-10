using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class MedicalGroupTest
    {
         IMedicalGroupRepository _medicalGroupRepository;

        public MedicalGroupTest()
        {
            _medicalGroupRepository = new MedicalGroupRepository();
        }
        [TestMethod]
        public void addMedicalGroup()
        {
            MedicalGroup _MedicalGroup = new MedicalGroup
            {
                MedicalGroupName = "MGNjks",
                MGAddress = "MGAddress",
                MGAddress2 = "MGAdress2",
                MGCity = "MGCity",
                MGStateID = 1,
                MGZip = "21311",
                MGNote = "MGNotesss"
            };
            var _id = _medicalGroupRepository.addMedicalGroup(_MedicalGroup);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateThirdPartyAdministrato()
        {
            MedicalGroup _MedicalGroup = new MedicalGroup
            {
                MedicalGroupID = 1,
                MedicalGroupName = "MGNjksNEw",
                MGAddress = "MGAddressNew",
                MGAddress2 = "MGAdress2New",
                MGCity = "MGCitysd",
                MGStateID = 1,
                MGZip = "21311",
                MGNote = "MGNotesssdddddd"
            };
            var _id = _medicalGroupRepository.updateMedicalGroup(_MedicalGroup);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteMedicalGroup()
        {
            _medicalGroupRepository.deleteMedicalGroup(3);
        }
        [TestMethod]
        public void getMedicalGroupsAll()
        {
            var _MedicalGroup = _medicalGroupRepository.getMedicalGroupsAll();
            Assert.IsTrue(_MedicalGroup != null, "failed");
        }
        [TestMethod]
        public void getMedicalGroupByID()
        {
            var _MedicalGroup = _medicalGroupRepository.getMedicalGroupByID(1);
            Assert.IsTrue(_MedicalGroup != null, "failed");
        }

        [TestMethod]
        public void getMedicalGroupsByName()
        {
            string SearchText = "m";
            int _skip = 0;
            int _take = 2;
            var _MedicalGroup = _medicalGroupRepository.getMedicalGroupsByName(SearchText, _skip, _take);
            Assert.IsTrue(_MedicalGroup != null, "failed");
        }
    }
}
