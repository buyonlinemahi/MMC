using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class InsurerTest
    {
        IInsurerRepository _insurerRepository;

        public InsurerTest()
        {
            _insurerRepository = new InsurerRepository();
        }
        [TestMethod]
        public void addInsurer()
        {
            Insurer _Insurer = new Insurer
            {
                InsName = "vLastName2",
                InsPhone = "2135649872",
                InsAddress1 = "AdjAddress1",
                InsAddress2 = "AdjAddress2",
                InsFax = "2135469872",
                InsEMail = "a@gmail.com",
                InsZip = "90024",
                InsCity = "AdjCity2",
                InsStateId = 1
            };
            var _id = _insurerRepository.addInsurer(_Insurer);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateInsurer()
        {
            Insurer _Insurer = new Insurer
            {
                InsurerID = 1,
                InsName = "vLastName2",
                InsPhone = "2135649872",
                InsAddress1 = "AdjAddress1",
                InsAddress2 = "AdjAddress2",
                InsFax = "2135469872",
                InsEMail = "a@gmail.com",
                InsZip = "90024",
                InsCity = "AdjCity2",
                InsStateId = 1
            };
            var _id = _insurerRepository.updateInsurer(_Insurer);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteInsurer()
        {
            _insurerRepository.deleteInsurer(2);
        }
        [TestMethod]
        public void getAllInsurers()
        {
            var insurers = _insurerRepository.getInsurersAll();
            Assert.IsTrue(insurers != null, "failed");
        }
        [TestMethod]
        public void getInsurerByID()
        {
            var insurers = _insurerRepository.getInsurerByID(1);
            Assert.IsTrue(insurers != null, "failed");
        }

        [TestMethod]
        public void getAllInsurersName()
        {
            string SearchText = "i";
            int _skip = 0;
            int _take = 10;
            var insurers = _insurerRepository.getInsurersByName(SearchText, _skip, _take);
            Assert.IsTrue(insurers != null, "failed");
        }

        [TestMethod]
        public void addInsuranceBranch()
        {
            InsuranceBranch _InsuranceBranch = new InsuranceBranch
            {
                InsBranchAddress = "InsBranchAddress",
                InsBranchCity = "InsBranchCity",
                InsBranchName = "InsBranchName",
                InsBranchStateId = 1,
                InsBranchZip = "90029",
                InsurerId = 1
            };
            var _id = _insurerRepository.addInsuranceBranch(_InsuranceBranch);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateInsuranceBranch()
        {
            InsuranceBranch _InsuranceBranch = new InsuranceBranch
            {
                InsBranchAddress = "InsBranchAddress",
                InsBranchCity = "InsBranchCity",
                InsBranchName = "InsBranchName",
                InsBranchStateId = 4,
                InsBranchZip = "90029-2135",
                InsurerId = 1,
                InsuranceBranchID=1
            };
            var _id = _insurerRepository.updateInsuranceBranch(_InsuranceBranch);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteInsuranceBranch()
        {
            _insurerRepository.deleteInsuranceBranch(2);
        }
        [TestMethod]
        public void getInsuranceBranchesByInsurerID()
        {
            var insuranceBranchlist = _insurerRepository.getInsuranceBranchesByInsurerID(1, 0, 10);
            Assert.IsTrue(insuranceBranchlist != null, "failed");
        }
        [TestMethod]
        public void getInsuranceBranchByID()
        {
            var insuranceBranch = _insurerRepository.getInsuranceBranchByID(1);
            Assert.IsTrue(insuranceBranch != null, "failed");
        }

        [TestMethod]
        public void getInsuranceBranchesAllByInsurerID()
        {
            var insuranceBranch = _insurerRepository.getInsuranceBranchesAllByInsurerID(1);
            Assert.IsTrue(insuranceBranch != null, "failed");
        }

    }
}
