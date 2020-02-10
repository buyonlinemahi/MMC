using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;

namespace MMC.UnitTest
{
    /// <summary>
    /// Summary description for LetterTest
    /// </summary>
    [TestClass]
    public class LetterTest
    {
        ILetterRepository _ILetterRepository;
        public LetterTest()
        {
            _ILetterRepository = new LetterRepository();
        }

        private TestContext testContextInstance;
                
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void getInitialNotificationLetterDetails()
        {
            var data = _ILetterRepository.getInitialNotificationLetterDetails(32);
            Assert.IsTrue(data != null, "failed");
        }

        [TestMethod]
        public void getRequestDetailsInitialNotificationLetter()
        {
            var data = _ILetterRepository.getRequestDetailsInitialNotificationLetter(32);
            Assert.IsTrue(data != null, "failed");
        }

        [TestMethod]
        public void getCertifiedRequestDetailsInitialNotificationLetter()
        {
            var data = _ILetterRepository.getCertifiedRequestDetailsInitialNotificationLetter(32);
            Assert.IsTrue(data != null, "failed");
        }

        [TestMethod]
        public void getDeniedRequestDetailsInitialNotificationLetter()
        {
            var data = _ILetterRepository.getDeniedRequestDetailsInitialNotificationLetter(138);
            Assert.IsTrue(data != null, "failed");
        }
    }
}
