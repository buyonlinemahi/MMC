using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class ManagedCareCompanyTest
    {
        IManagedCareCompanyRepository _managedCareCompanyRepository;

         public ManagedCareCompanyTest()
        {
            _managedCareCompanyRepository = new ManagedCareCompanyRepository();
        }

        [TestMethod]
         public void addManagedCareCompanies()
        {
            ManagedCareCompany _company = new ManagedCareCompany
            {
                CompName = "Vinod",
                CompAddress = "Raj international",
                CompAddress2 = "Raj enterprieses",
                CompCity = "Jaaln",
                CompStateId = 17,
                CompZip = "0987654321"
            };
            var _id = _managedCareCompanyRepository.addManagedCareCompany(_company);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateManagedCareCompanies()
        {
            ManagedCareCompany _company = new ManagedCareCompany
            {
                CompanyId = 1,
                CompName = "VAVLL1",
                CompAddress = "Raj international entrerpris",
                CompAddress2 = "Raj enterprieses",
                CompCity = "Jaaln",
                CompStateId = 17,
                CompZip = "0987654321"
            };
            var _id = _managedCareCompanyRepository.updateManagedCareCompany(_company);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteManagedCareCompanies()
        {
            _managedCareCompanyRepository.deleteManagedCareCompany(4);
        }
        [TestMethod]
        public void getAllManagedCareCompanies()
        {
            var _companyAll = _managedCareCompanyRepository.getManagedCareCompanyAll();
            Assert.IsTrue(_companyAll != null, "failed");
        }
        [TestMethod]
        public void getManagedCareCompaniesByID()
        {
            var _companyByID = _managedCareCompanyRepository.getManagedCareCompanyByID(3);
            Assert.IsTrue(_companyByID != null, "failed");
        }

        [TestMethod]
        public void getManagedCareCompaniesByName()
        {
            var employerByName = _managedCareCompanyRepository.getManagedCareCompanyByName("t", 0, 2);
            Assert.IsTrue(employerByName != null, "failed");
        }
    }
}
