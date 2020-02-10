using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;
namespace MMC.UnitTest
{
    [TestClass]
    public class VendorTest
    {
        IVendorRepository _vendorRepository;
        public VendorTest()
        {
            _vendorRepository = new VendorRepository();
        }
        [TestMethod]
        public void addVendor()
        {
            Vendor _vendor = new Vendor
            {
                VendorName = "VendorName2",
                VendorTax = "VendTAX",
                VendorAddress1 = "VendorAddress1",
                VendorAddress2 = "VendorAddress2",
                VendorCity = "VendorCity",
                VendorStateId =1,
                VendorZip = "14101",
                VendorPhone = "1234567890",
                VendorFax = "1234567890"
            };
            var _id = _vendorRepository.addVendor(_vendor);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void updateVendor()
        {
            Vendor _vendor = new Vendor
            {
                VendorName = "VendorNameU",
                VendorTax = "VendTAX",
                VendorAddress1 = "VendorAddress1U",
                VendorAddress2 = "VendorAddress2U",
                VendorCity = "VendorCity",
                VendorStateId = 1,
                VendorZip = "14101",
                VendorPhone = "1234567890",
                VendorFax = "1234567890",
                VendorID = 1
            };
            var _id = _vendorRepository.updateVendor(_vendor);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteVendor()
        {
            _vendorRepository.deleteVendor(2);
        }
        [TestMethod]
        public void getAllVendors()
        {
            var vendors = _vendorRepository.getVendorsAll();
            Assert.IsTrue(vendors != null, "failed");
        }
        [TestMethod]
        public void getVendorsByName()
        {
            var vendors = _vendorRepository.getVendorsByName("v", 0, 10);
            Assert.IsTrue(vendors != null, "failed");
        }
        [TestMethod]
        public void getVendorByID()
        {
            var vendors = _vendorRepository.getVendorByID(1);
            Assert.IsTrue(vendors != null, "failed");
        }
    }
}
