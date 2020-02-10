using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;
using DLModel =MMC.Core.Data.Model;


namespace MMC.UnitTest
{
    [TestClass]
    public class IMRTest
    {
        IIMRRepository _iMRRepository;
        public IMRTest()
        {
            _iMRRepository = new IMRRepository();
        }
        [TestMethod]
        public void getIMRRecordAll()
        {
            var _getIMRRecordAll = _iMRRepository.getRequestIMRRecordAll(1, 10);
            //int IMRCount = _getIMRRecordAll.TotalCount;
            Assert.IsTrue(_getIMRRecordAll != null, "failed");
        }

        [TestMethod]
        public void getIMRRequestDetailByPatientClaim()
        {
            var _getIMRRecordAll = _iMRRepository.getRequestIMRRecordByPatientClaim("Latest1234", 1, 20);
            Assert.IsTrue(_getIMRRecordAll != null, "failed");
        }
        [TestMethod]
        public void SaveRequestIMRRecord()
        {
            string[] arr = { "379", "381" };
            _iMRRepository.SaveRequestIMRRecord(arr, 1);
        }
        [TestMethod]
        public void UpdateRFAIMRReferenceNumberByReferralID()
        {
            DLModel.RFAReferral _rFAReferral = new DLModel.RFAReferral
            {
                RFAIMRReferenceNumber = "IMR123GHK",
                RFAReferralID = 236
            };
            var _id = _iMRRepository.UpdateRFAIMRReferenceNumberByReferralID(_rFAReferral);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void addIMRRFAReferral()
        {
            IMRRFAReferral _IMRRFAReferral = new IMRRFAReferral
            {
                IMRApplicationReceivedDate = System.DateTime.Now,
                IMRNoticeInformationDate = System.DateTime.Now,
                IMRRFAClaimPhysicianID = 111,
                RFAReferralID = 520,
                 
                IMRCEReceivedDate = System.DateTime.Now,
                IMRInternalReceivedDate = System.DateTime.Now,
                IMRPriority = 1,
                IMRReceivedVia=2,
                IMRResponseDueDate =  System.DateTime.Now,
                IMRResponseType = 1,
                



            };
            var _id = _iMRRepository.addIMRRFAReferral(_IMRRFAReferral);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateIMRRFAReferral()
        {
            IMRRFAReferral _IMRRFAReferral = new IMRRFAReferral
            {
                IMRApplicationReceivedDate = System.DateTime.Now,
                IMRNoticeInformationDate = System.DateTime.Now,
                IMRRFAClaimPhysicianID = 19,

                IMRCEReceivedDate = System.DateTime.Now,
                IMRInternalReceivedDate = System.DateTime.Now,
                IMRPriority = 1,
                IMRReceivedVia = 2,
                IMRResponseDueDate = System.DateTime.Now,
                IMRResponseType = 1,
                IMRDecisionID = 1,
                IMRResponseDate = System.DateTime.Now,                
                IMRRFAReferralID = 13,
                RFAReferralID = 715
            };
            var _id = _iMRRepository.updateIMRRFAReferral(_IMRRFAReferral);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updateIMRRFAReferralByValue()
        {
            IMRRFAReferral _IMRRFAReferral = new IMRRFAReferral
            {   
                IMRDecisionID = 1,
                IMRResponseDate = System.DateTime.Now,
                IMRDecisionReceivedDate=System.DateTime.Now,
                IMRRFAReferralID = 13,
                RFAReferralID = 715
            };
            var _id = _iMRRepository.updateIMRRFAReferralByValues(_IMRRFAReferral);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deleteIMRRFAReferral()
        {
            _iMRRepository.deleteIMRRFAReferral(1);
        }

        [TestMethod]
        public void getIMRRFAReferralByRefID()
        {
            var IMRRFAReferrals = _iMRRepository.getIMRRFAReferralByRefID(715);
            Assert.IsTrue(IMRRFAReferrals != null, "failed");
        }

        [TestMethod]
        public void getPhysicianByReferralID()
        {
            var IMRRFAReferrals = _iMRRepository.getPhysicianByReferralID(520);
            Assert.IsTrue(IMRRFAReferrals != null, "failed");
        }

        [TestMethod]
        public void getMedicalAndLegalDocsByReferralID()
        {
            var IMRRFAReferrals = _iMRRepository.getMedicalAndLegalDocsByReferralID(549);
            Assert.IsTrue(IMRRFAReferrals != null, "failed");
        }

        [TestMethod]
        public void getIMRLetters()
        {
            var IMRLetters= _iMRRepository.getIMRLetters(745);
            Assert.IsTrue(IMRLetters != null, "failed");
        }

        [TestMethod]
        public void getIMRDecisionPageDetailsByReferralID()
        {
            var IMRDetails = _iMRRepository.getIMRDecisionPageDetailsByReferralID(1049);
            Assert.IsTrue(IMRDetails != null, "failed");
        }

        [TestMethod]
        public void getIMRDecisionPageRequestDetailsByReferralID()
        {
            var IMRDetails = _iMRRepository.getIMRDecisionPageRequestDetailsByReferralID(1049);
            Assert.IsTrue(IMRDetails != null, "failed");
        }

        [TestMethod]
        public void getIMRDecisionList()
        {
            var IMRDecisionList = _iMRRepository.getIMRDecisionList();
            Assert.IsTrue(IMRDecisionList != null, "failed");
        }
    }
}
