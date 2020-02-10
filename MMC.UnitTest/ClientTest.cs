using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;
using BLModel = MMC.Core.BL.Model;

namespace MMC.UnitTest
{

    [TestClass]
    public class ClientTest
    {
        IClientRepository _clientRepository;
        public ClientTest()
        {
            _clientRepository = new ClientRepository();
        }
        [TestMethod]
        public void addClient()
        {
            Client _client = new Client
            {

                ClientName = "ClientName",
                ClaimAdministratorID = 1,
                ClaimAdministratorType = "adj",

            };
            var _id = _clientRepository.addClient(_client);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void updateClient()
        {
            Client _Client = new Client
            {
                ClientID = 1,
                ClientName = "ClientName",
                ClaimAdministratorID = 1,
                ClaimAdministratorType = "adj",
            };
            var _id = _clientRepository.updateClient(_Client);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteClient()
        {
            _clientRepository.deleteClient(3);
        }

        [TestMethod]
        public void getClientByID()
        {
            var _Client = _clientRepository.getClientByID(1);
            Assert.IsTrue(_Client != null, "failed");
        }

        [TestMethod]
        public void getClientAll()
        {
            var _Client = _clientRepository.getClientAll();
            Assert.IsTrue(_Client != null, "failed");
        }
        [TestMethod]
        public void getClientByName()
        {
            var _Client = _clientRepository.getClientByName("c", 0, 10);
            Assert.IsTrue(_Client != null, "failed");
        }

        [TestMethod]
        public void getAdjusterByClientID()
        {
            var _Client = _clientRepository.getAdjusterByClientID(1);
            Assert.IsTrue(_Client != null, "failed");
        }

        [TestMethod]
        public void getClientInsurerByClientID()
        {
            var _ClientIns = _clientRepository.getClientInsurerByClientID(5, 0, 10);
            Assert.IsTrue(_ClientIns != null, "failed");
        }

        [TestMethod]
        public void getAllClientInsurerByClientID()
        {
            var _ClientIns = _clientRepository.getAllClientInsurerByClientID(5);
            Assert.IsTrue(_ClientIns != null, "failed");
        }

        [TestMethod]
        public void getClientEmployerByClientID()
        {
            var _ClientEmp = _clientRepository.getClientEmployerByClientID(1, 0, 10);
            Assert.IsTrue(_ClientEmp != null, "failed");
        }

        [TestMethod]
        public void getAllClientEmployerByClientID()
        {
            var _ClientEmp = _clientRepository.getAllClientEmployerByClientID(1);
            Assert.IsTrue(_ClientEmp != null, "failed");
        }

        [TestMethod]
        public void getClientThirdPartyAdministratorByClientID()
        {
            var _ClientTPA = _clientRepository.getClientThirdPartyAdministratorByClientID(5, 0, 10);
            Assert.IsTrue(_ClientTPA != null, "failed");
        }

        [TestMethod]
        public void getAllClientThirdPartyAdministratorByClientID()
        {
            var _ClientTPA = _clientRepository.getAllClientThirdPartyAdministratorByClientID(5);
            Assert.IsTrue(_ClientTPA != null, "failed");
        }
        [TestMethod]
        public void getClientManagedCareCompanyByClientID()
        {
            var _ClientMMC = _clientRepository.getClientManagedCareCompanyByClientID(1, 0, 10);
            Assert.IsTrue(_ClientMMC != null, "failed");
        }

        [TestMethod]
        public void getClaimAdministratorByClientID()
        {
            var _Client = _clientRepository.getClaimAdministratorByClientID(1);
            Assert.IsTrue(_Client != null, "failed");
        }
        [TestMethod]
        public void getAllClaimAdministrator()
        {
            var _Client = _clientRepository.getAllClaimAdministrator();
            Assert.IsTrue(_Client != null, "failed");
        }

        [TestMethod]
        public void getClaimAdministratorAllByClientID()
        {
            var _Client = _clientRepository.getClaimAdministratorAllByClientID(1);
            Assert.IsTrue(_Client != null, "failed");
        }

        [TestMethod]
        public void updateClientManagedCareCompanyByClientID()
        {
            ClientManagedCareCompany _ClientManagedCareCompany = new ClientManagedCareCompany()
            {
                ClientID = 1,
                CompanyID = 5
            };
            var _id = _clientRepository.updateClientManagedCareCompanyByClientID(_ClientManagedCareCompany);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void getClientTPABranchesAllByTPAID()
        {
            var _Client = _clientRepository.getClientTPABranchesAllByTPAID(5, 5);
            Assert.IsTrue(_Client != null, "failed");
        }
        [TestMethod]
        public void getClientInsuranceBranchesAllByInsurerID()
        {
            var _Client = _clientRepository.getClientInsuranceBranchesAllByInsurerID(5, 7);
            Assert.IsTrue(_Client != null, "failed");
        }
        #region Client Billing
        [TestMethod]
        public void addClientBilling()
        {
            ClientBilling _clientBilling = new ClientBilling
            {

                ClientBillingRateTypeID = 1,
                ClientID = 27,
                ClientIsPrivateLabel = false,
                ClientInvoiceNumber="JSYTG643T764",
                ClientAttentionToID =1,
                ClientAttentionToFreeText=null
            };
            var _id = _clientRepository.addClientBilling(_clientBilling);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void updateClientBilling()
        {
            ClientBilling _clientBilling = new ClientBilling
            {

                ClientBillingRateTypeID = 1,
                ClientID = 43,
                ClientIsPrivateLabel = true,
                ClientBillingID = 17,
                ClientInvoiceNumber = "KYU45UN4ER54",
                ClientAttentionToID = 3,
                ClientAttentionToFreeText = "ji jvdjd db dfb jdb jdbjhf jhbj jdbvj dfvjd jd jfhdv jdffjdv fjdvb jdfvbfjdvb jfbhv jhdbv jhfbv jdfbv hbfvd fjbvf vdjfk vbdjfvb dkfjvbjdf vfjdbv jbvjd fvbj dfhb"
            };
            var _id = _clientRepository.updateClientBilling(_clientBilling);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteClientBilling()
        {
            _clientRepository.deleteClientBilling(1);
        }

        [TestMethod]
        public void getClientBillingByID()
        {
            var _clientBilling = _clientRepository.getClientBillingByID(1);
            Assert.IsTrue(_clientBilling != null, "failed");
        }
        
        [TestMethod]
        public void getClientBillingDetailByClientID()
        {
            var _clientBilling = _clientRepository.getClientBillingDetailByClientID(4);
            Assert.IsTrue(_clientBilling != null, "failed");
        }
        

        #endregion

        #region Client Billing Retail Rate

        [TestMethod]
        public void addClientBillingRetailRate()
        {
            ClientBillingRetailRate _clientBillingRetailRate = new ClientBillingRetailRate
            {
                ClientBillingID = 1,
                ClientBillingRetailRateAdminFee = 10,
                ClientBillingRetailRateIMRMD = 20,
                ClientBillingRetailRateIMRPrep = 30,
                ClientBillingRetailRateIMRRush = 40,
                ClientBillingRetailRateMD = 50,
                ClientBillingRetailRateRN = 70,
                ClientBillingRetailRateSpecialityReview = 80,
                CreatedBy = 1,
                CreatedOn = System.DateTime.Now
            };
            var _id = _clientRepository.addClientBillingRetailRate(_clientBillingRetailRate);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateClientBillingRetailRate()
        {
            ClientBillingRetailRate _clientBillingRetailRate = new ClientBillingRetailRate
            {
                ClientBillingRetailRateID = 1,
                ClientBillingID = 1,
                ClientBillingRetailRateAdminFee = 10,
                ClientBillingRetailRateIMRMD = 20,
                ClientBillingRetailRateIMRPrep = 30,
                ClientBillingRetailRateIMRRush = 40,
                ClientBillingRetailRateMD = 50,
                ClientBillingRetailRateRN = 70,
                ClientBillingRetailRateSpecialityReview = 80,
                CreatedBy = 1,
                CreatedOn = System.DateTime.Now
            };
            var _id = _clientRepository.updateClientBillingRetailRate(_clientBillingRetailRate);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteClientBillingRetailRate()
        {
            _clientRepository.deleteClientBillingRetailRate(1);
        }

        [TestMethod]
        public void getClientBillingRetailRateByID()
        {
            var _clientBillingRetailRate = _clientRepository.getClientBillingRetailRateByID(1);
            Assert.IsTrue(_clientBillingRetailRate != null, "failed");
        }

        [TestMethod]
        public void getClientBillingRetailRateByClientBillingID()
        {
            var _clientBillingRetailRate = _clientRepository.getClientBillingRetailRateByClientBillingID(8);
            Assert.IsTrue(_clientBillingRetailRate != null, "failed");
        }

        #endregion

        #region Client Billing Wholesale Rate

        [TestMethod]
        public void addClientBillingWholesaleRate()
        {
            ClientBillingWholesaleRate _clientBillingWholesaleRate = new ClientBillingWholesaleRate
            {
                ClientBillingID = 1,
                ClientBillingWholesaleRateAdminFee = 10,
                ClientBillingWholesaleRateIMRMD = 20,
                ClientBillingWholesaleRateIMRPrep = 30,
                ClientBillingWholesaleRateIMRRush = 40,
                ClientBillingWholesaleRateMD = 50,
                ClientBillingWholesaleRateRN = 70,
                ClientBillingWholesaleRateSpecialityReview = 80,
                CreatedBy = 1,
                CreatedOn = System.DateTime.Now
            };
            var _id = _clientRepository.addClientBillingWholesaleRate(_clientBillingWholesaleRate);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateClientBillingWholesaleRate()
        {
            ClientBillingWholesaleRate _clientBillingWholesaleRate = new ClientBillingWholesaleRate
            {
                ClientBillingWholesaleRateID = 2,
                ClientBillingID = 2,
                ClientBillingWholesaleRateAdminFee = 10,
                ClientBillingWholesaleRateIMRMD = 20,
                ClientBillingWholesaleRateIMRPrep = 30,
                ClientBillingWholesaleRateIMRRush = 40,
                ClientBillingWholesaleRateMD = 50,
                ClientBillingWholesaleRateRN = 70,
                ClientBillingWholesaleRateSpecialityReview = 80,
                CreatedBy = 1,
                CreatedOn = System.DateTime.Now
            };
            var _id = _clientRepository.updateClientBillingWholesaleRate(_clientBillingWholesaleRate);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteClientBillingWholesaleRate()
        {
            _clientRepository.deleteClientBillingWholesaleRate(1);
        }

        [TestMethod]
        public void getClientBillingWholesaleRateByID()
        {
            var _clientBillingWholesaleRate = _clientRepository.getClientBillingWholesaleRateByID(1);
            Assert.IsTrue(_clientBillingWholesaleRate != null, "failed");
        }

        [TestMethod]
        public void getClientBillingWholesaleRateByClientBillingID()
        {
            var _clientBillingWholesaleRate = _clientRepository.getClientBillingWholesaleRateByClientBillingID(9);
            Assert.IsTrue(_clientBillingWholesaleRate != null, "failed");
        }

        #endregion

        #region Client Private Label

        [TestMethod]
        public void addClientPrivateLabel()
        {
            ClientPrivateLabel _clientPrivateLabel = new ClientPrivateLabel
            {
                ClientID = 27,
                ClientPrivateLabelAddress = "ClientPrivateLabelAddress",
                ClientPrivateLabelCity = "ClientPrivateLabelCity",
                ClientPrivateLabelName = "ClientPrivateLabelName",
                ClientPrivateLabelStateID = 1,
                ClientPrivateLabelZip = "12345-6789",
                ClientPrivateLabelLogoName="ClientPrivateLabelLogo.jpg",
                ClientPrivateLabelPhone="(321)654-6541",
                ClientPrivateLabelFax  = "(321)654-9874"
            };
            var _id = _clientRepository.addClientPrivateLabel(_clientPrivateLabel);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void updateClientPrivateLabel()
        {
            ClientPrivateLabel _clientPrivateLabel = new ClientPrivateLabel
            {
                ClientID = 37,
                ClientPrivateLabelAddress = "ClientPrivateLabelAddress1",
                ClientPrivateLabelCity = "ClientPrivateLabelCity1",
                ClientPrivateLabelName = "ClientPrivateLabelName1",
                ClientPrivateLabelStateID = 1,
                ClientPrivateLabelZip = "12345-6789",
                ClientPrivateLabelID = 17,
                ClientPrivateLabelLogoName = "ClientPrivateLabelLogo.jpg",
                ClientPrivateLabelPhone="(321)654-6541",
                ClientPrivateLabelFax  = "(321)654-9874"
            };
            var _id = _clientRepository.updateClientPrivateLabel(_clientPrivateLabel);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteClientPrivateLabel()
        {
            _clientRepository.deleteClientPrivateLabel(1);
        }

        [TestMethod]
        public void getClientPrivateLabelByID()
        {
            var _getClientBillingByID = _clientRepository.getClientPrivateLabelByID(1);
            Assert.IsTrue(_getClientBillingByID != null, "failed");
        }
        [TestMethod]
        public void getClientPrivateLabelDetailByClientID()
        {
            var _getClientPrivateLabelDetailByClientID = _clientRepository.getClientPrivateLabelDetailByClientID(37);
            Assert.IsTrue(_getClientPrivateLabelDetailByClientID != null, "failed");
        }
        #endregion
    }
}
