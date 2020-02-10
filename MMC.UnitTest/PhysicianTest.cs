using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class PhysicianTest
    {
        IPhysicianRepository _physicianRepository;

        public PhysicianTest()
        {
            _physicianRepository = new PhysicianRepository();
        }

        [TestMethod]
        public void addPhysician()
        {
             Physician _physician = new Physician
            {

                PhysFirstName = "PhysUpdate1",
                PhysLastName = "PhysLast1",
                PhysQualification = "qwert",
                PhysSpecialtyId = 1,
                PhysNPI = "12345",
                PhysEMail = "email@gmail.com",
                PhysNotes = "This is to states that",
                PhysAddress1 = "this",
                PhysAddress2 = "this",
                PhysCity = "lt",
                PhysFax = "3245345",
                PhysPhone = "3",
                PhysStateId = 4,
                PhysZip = "234134"
            };
            var _id = _physicianRepository.addPhysician(_physician);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updatePhysician()
        {
             Physician _physician = new Physician
            {
                PhysicianId  = 3,
                PhysFirstName = "PhysUpdate1",
                PhysLastName = "PhysLast1",
                PhysQualification = "qwert",
                PhysSpecialtyId = 1,
                PhysNPI = "12345",
                PhysEMail = "email@gmail.com",
                PhysNotes = "This is to states that",
                PhysAddress1 = "this",
                PhysAddress2 = "this",
                PhysCity ="lt",
                PhysFax ="3245345",
                PhysPhone= "3",
                PhysStateId = 4,
                PhysZip  = "234134"
               };
            var _id = _physicianRepository.updatePhysican(_physician);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deletePhysician()
        {
            _physicianRepository.deletePhysican(1);
        }
        [TestMethod]
        public void getAllPhysicians()
        {
            var physicanAll = _physicianRepository.getPhysicianAll();
            Assert.IsTrue(physicanAll != null, "failed");
        }
        [TestMethod]
        public void getPhysiciansByID()
        {
            var physicanByID = _physicianRepository.getPhysicianByID(52);
            Assert.IsTrue(physicanByID != null, "failed");
        }

        [TestMethod]
        public void getPhysiciansByName()
        {
            var physicanByName = _physicianRepository.getPhysicianByName("Gurleen Singh", 0, 4);
            Assert.IsTrue(physicanByName != null, "failed");
        }
    }
}
