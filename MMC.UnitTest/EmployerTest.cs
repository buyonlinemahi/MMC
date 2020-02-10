using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class EmployerTest
    {
        IEmployerRepository _employerRepository;

        public EmployerTest()
        {
            _employerRepository = new EmployerRepository();
        }

        #region Employer
        [TestMethod]
        public void addEmployers()
        {
            Employer _employer = new Employer
            {

                EmpName = "Kundra",
                EmpAddress1 = "qwert",
                EmpAddress2 = "this is looking",
                EmpPhoneExtension = "12345",
                EmpCity = "ecom",
                EmpContactName = "Th",
                EmpEMail = "3432",
                EmpFax = "324524",
                EmpPhone ="435345",
                EmpStateId = 2,
                EmpZip="45234"

            };
            var _id = _employerRepository.addEmployer(_employer);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateEmployers()
        {
            Employer _employer = new Employer
            {
                EmployerID = 27,
                EmpName = "Kundra",
                EmpAddress1 = "qwert",
                EmpAddress2 = "this is looking",
                EmpPhoneExtension = "12345",
                EmpCity = "ecom",
                EmpContactName = "Th",
                EmpEMail = "3432",
                EmpFax = "324524",
                EmpPhone = "435345",
                EmpStateId = 24,              
                EmpZip = "45234"

            };
            var _id = _employerRepository.updateEmployer(_employer);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteEmployer()
        {
            _employerRepository.deleteEmployer(27);
        }
        [TestMethod]
        public void getAllEmployers()
        {
            var employerAll = _employerRepository.getEmployerAll();
            Assert.IsTrue(employerAll != null, "failed");
        }
        [TestMethod]
        public void getEmployerByID()
        {
            var employerByID = _employerRepository.getEmployerByID(3);
            Assert.IsTrue(employerByID != null, "failed");
        }

        [TestMethod]
        public void getEmployersByName()
        {
            var employerByName = _employerRepository.getEmployerByName("t", 0, 4);
            Assert.IsTrue(employerByName != null, "failed");
        }
        #endregion

        #region EmployerSubsidiaries
        [TestMethod]
        public void addEmployerSubsidiary()
        {
            EmployerSubsidiary _employerSubsidiaries = new EmployerSubsidiary
            {

                EmployerId = 2,
                EMPSubsidiaryAddress ="Dakhoa",
                EMPSubsidiaryAddress2 = "this is",
                EMPSubsidiaryCity ="Jalandhar",
                EMPSubsidiaryEmail = "tarundakhoa@gmail.com",
                EMPSubsidiaryFax ="245345",
                EMPSubsidiaryID = 0,
                EMPSubsidiaryName = "Rajesh Tikha",
                EMPSubsidiaryPhone = "94383438",
                EMPSubsidiaryStateId = 13,
                EMPSubsidiaryZip = "54363"
            };
            var _id = _employerRepository.addEmployerSubsidiaries(_employerSubsidiaries);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void updateEmployerSubsidiary()
        {
            EmployerSubsidiary _employerSubsidiaries = new EmployerSubsidiary
            {

                EmployerId = 10,
                EMPSubsidiaryAddress = "this is to state that",
                EMPSubsidiaryAddress2 = "this is",
                EMPSubsidiaryCity = "Jalandhar",
                EMPSubsidiaryEmail = "mukul@gmail.com",
                EMPSubsidiaryFax = "2345",
                EMPSubsidiaryID = 1,
                EMPSubsidiaryName = "MahiEmployerName",
                EMPSubsidiaryPhone = "943838",
                EMPSubsidiaryStateId = 13,
                EMPSubsidiaryZip = "54363"
            };
            var _id = _employerRepository.updateEmployerSubsidiaries(_employerSubsidiaries);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void deleteEmployerSubsidiary()
        {
            _employerRepository.deleteEmployerSubsidiaries(11);
        }

        [TestMethod]
        public void getAllEmployerSubsidiary()
        {
            var employerAll = _employerRepository.getEmployerSubsidiariesAll();
            Assert.IsTrue(employerAll != null, "failed");
        }

        [TestMethod]
        public void getEmployerSubsidiaryByID()
        {
            var employerByID = _employerRepository.getEmployerSubsidiariesByID(3);
            Assert.IsTrue(employerByID != null, "failed");
        }

        [TestMethod]
        public void getEmployerSubsidiariesByEmployerID()
        {
            var employerByName = _employerRepository.getEmployerSubsidiariesByEmployerID(2,0,4);
            Assert.IsTrue(employerByName != null, "failed");
        }

        [TestMethod]
        public void getTsdEmployerSubsidiariesByEmployerID()
        {
            var employerByName = _employerRepository.getAllEmployerSubsidiariesByEmployerID(2);
            Assert.IsTrue(employerByName != null, "failed");
        }
        #endregion
    }
}
