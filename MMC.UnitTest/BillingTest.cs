using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class BillingTest
    {
        IBillingRepository _billingRepository;
        public BillingTest()
        {
            _billingRepository = new BillingRepository();
        }

        [TestMethod]
        public void getBillingDetails() 
        {

            var billingDetails = _billingRepository.getBillingDetails(0,10);
            Assert.IsTrue(billingDetails != null, "failed");
        }

        [TestMethod]
        public void getBillingDetailByClientName()
        {

            var billingDetails = _billingRepository.getBillingDetailByClientName("billing gurleen", 0, 10);
            Assert.IsTrue(billingDetails != null, "failed");
        }

        [TestMethod]
        public void updateBillingDetails()
        {
            RFARequestBilling _RFARequestBilling = new RFARequestBilling();
            _RFARequestBilling.RFARequestBillingID = 1;
            _RFARequestBilling.RFARequestBillingRN = 2;
            _RFARequestBilling.RFARequestBillingMD = 3;            
            _RFARequestBilling.RFARequestBillingSpeciality = 8;

            var update = _billingRepository.updateRFARequestBilling(_RFARequestBilling);
            
            Assert.IsTrue(update != null, "failed");
        }

        [TestMethod]
        public void addBillingDetails()
        {
            RFARequestBilling _RFARequestBilling = new RFARequestBilling();
            _RFARequestBilling.RFARequestBillingID = 1;
            _RFARequestBilling.RFARequestBillingRN = 2;
            _RFARequestBilling.RFARequestBillingMD = 3;
            _RFARequestBilling.RFARequestBillingSpeciality = 8;

            var update = _billingRepository.addRFARequestBilling(_RFARequestBilling);

            Assert.IsTrue(update != null, "failed");
        }

        [TestMethod]
        public void getInvoiceByClaimID()
        {
            var billingDetails = _billingRepository.getInvoiceNumberByClientID(163);
            Assert.IsTrue(billingDetails != null, "failed");
        }

        [TestMethod]
        public void addClientStatement()
        {
            ClientStatement _RFARequestBilling = new ClientStatement();
            _RFARequestBilling.ClientID = 40;
            _RFARequestBilling.UserID = 1;
            _RFARequestBilling.ClientStatementFileName = "dfgdf";
            _RFARequestBilling.CreationDate = System.DateTime.Now;

            var update = _billingRepository.addClientStatement(_RFARequestBilling);

            Assert.IsTrue(update != null, "failed");
        }
        [TestMethod]
        public void getAccountReceivablesByClientName()
        {

            var billingDetails = _billingRepository.getAccountReceivablesByClientName("billing gurleen", 0, 10);
            Assert.IsTrue(billingDetails != null, "failed");
        }
    }
}
