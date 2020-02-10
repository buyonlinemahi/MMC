using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
    public class PreparationTest
    {
        IPreparationRepository _IPreparationRepository;

        public PreparationTest()
        {
            _IPreparationRepository = new PreparationRepository();
        }
        [TestMethod]
        public void getReferralByProcessLevel()
        {
            var _id = _IPreparationRepository.getReferralByProcessLevel(0, 10,70);
            Assert.IsTrue(_id != null, "failed");
        }
        [TestMethod]
        public void getPatientAndRequestByReferralId()
        {
            var PatientAndRequest = _IPreparationRepository.getPatientAndRequestByReferralId(383);
            Assert.IsTrue(PatientAndRequest != null, "failed");
        }
           [TestMethod]
        public void getPatientAndRequestForNotificationByReferralId()
        {
            var PatientAndRequest = _IPreparationRepository.getPatientAndRequestForNotificationByReferralId(360,0,10);
            Assert.IsTrue(PatientAndRequest != null, "failed");
        }
           [TestMethod]
           public void getRemainingRequest()
           {
               var PatientAndRequest = _IPreparationRepository.getRemainingRequest(383);
               Assert.IsTrue(PatientAndRequest != null, "failed");
           }

        [TestMethod]
        public void updateProcessLevel()
        {
            var _id = _IPreparationRepository.updateProcessLevel(32, 70);
            Assert.IsTrue(_id != null, "failed");
        }
        [TestMethod]
        public void addRFAAdditionalInfo()
        {
            RFAAdditionalInfo rfaAdditionalInfo = new RFAAdditionalInfo();
            rfaAdditionalInfo.RFAReferralID = 32;
            rfaAdditionalInfo.RFAAdditionalInfoRecord = "test";   
            var _id = _IPreparationRepository.addRFAAdditionalInfo(rfaAdditionalInfo);
            Assert.IsTrue(_id != null, "failed");
        }
        [TestMethod]
        public void updateRFAAdditionalInfo()
        {
            RFAAdditionalInfo rfaAdditionalInfo = new RFAAdditionalInfo();
            rfaAdditionalInfo.RFAReferralID = 32;
            rfaAdditionalInfo.RFAAdditionalInfoRecord = "testing";
            rfaAdditionalInfo.RFAAdditionalInfoID = 33;
            var _id = _IPreparationRepository.updateRFAAdditionalInfo(rfaAdditionalInfo);
            Assert.IsTrue(_id != null, "failed");
        }
        [TestMethod]
        public void getAllRFAAdditionalInfo()
        {
            var _id = _IPreparationRepository.getAllRFAAdditionalInfo(32,0,10);
            Assert.IsTrue(_id != null, "failed");
        }
        [TestMethod]
        public void getRFAAdditionalInfoByID()
        {
            var _id = _IPreparationRepository.getRFAAdditionalInfoById(1);
            Assert.IsTrue(_id != null, "failed");
        }
        [TestMethod]
        public void updateDecisionByRequestID()
        {
            RFARequest rfaRequest = new RFARequest();
            rfaRequest.RFARequestID = 1168;
            rfaRequest.DecisionID = 9;
            rfaRequest.DecisionDate = System.DateTime.Now;
            //rfaRequest.RFAStatus = "SendT0Clinical";
            rfaRequest.RFAFrequency = 2;
            rfaRequest.RFARequestedTreatment = "5435";
            var _id = _IPreparationRepository.updateDecisionByRequestID(rfaRequest);
            Assert.IsTrue(_id != null, "failed");
        }
        [TestMethod]
        public void AcceptedBodyPartsByReferralID()
        {
            var _id = _IPreparationRepository.getAcceptedBodyPartsByReferralID(32);
            Assert.IsTrue(_id != null, "failed");
        }
        
        [TestMethod]
        public void DeniedBodyPartsByReferralID()
        {
            var _id = _IPreparationRepository.getDeniedBodyPartsByReferralID(32);
            Assert.IsTrue(_id != null, "failed");
        }
        [TestMethod]
        public void DignosisByReferralID()
        {
            var _id = _IPreparationRepository.getDignosisByReferralID(32);
            Assert.IsTrue(_id != null, "failed");
        }
         [TestMethod]
        public void getAllRequestByReferralID()
        {
            var _id = _IPreparationRepository.getAllRequestByReferralID(97);
            Assert.IsTrue(_id != null, "failed");
        }

         [TestMethod]
         public void getReferralMedicalRecordByPatientID()
         {
             var _data = _IPreparationRepository.getReferralMedicalRecordByPatientID(130, 0, 10);
             Assert.IsTrue(_data != null, "failed");
         }
       
    }
}