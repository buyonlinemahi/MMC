using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;
using BLModel = MMC.Core.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MMC.UnitTest
{
    [TestClass]
    public class IntakeTest
    {
        IntakeRepository _intakeRepository;
        public IntakeTest()
        {
            _intakeRepository = new IntakeRepository();
        }
        [TestMethod]
        public void updateClaimInReferral()
        {
            var data = _intakeRepository.updateClaimInReferral(3, 32);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void updateRFAInReferral()
        {
            RFAReferral obj = new RFAReferral
            {
                RFAReferralID = 32,
                RFACEDate = System.DateTime.Now.Date,
                RFAHCRGDate = System.DateTime.Now.Date,
                RFAReferralDate = System.DateTime.Now.Date,
                RFASignedByPhysician = true,
                RFATreatmentRequestClear = false,
                RFADiscrepancies = "asdfasdfasdfa sfasd fasdf"
            };
            var data = _intakeRepository.updateRFAInReferral(obj);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void updatePhysicianInReferral()
        {
            var data = _intakeRepository.updatePhysicianInReferral(2, 32);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void getReferralByID()
        {
            var data = _intakeRepository.getReferralByID(32);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void getAllIncompleteReferrals()
        {
            var data = _intakeRepository.getAllIncompleteReferrals(0, 20);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void addReferralFileIntake()
        {
            var data = _intakeRepository.addReferralFileIntake("test.pdf", 1);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void updateReferralFile()
        {
            RFAReferralFile obj = new RFAReferralFile
            {
                RFAReferralID = 391,
                RFAReferralFileName = "391_IntakeUpload.pdf",
                RFAReferralFileID = 447,
                RFAFileTypeID = 6,
                RFAFileUserID = 1,
                RFAFileCreationDate = System.DateTime.Now
            };
            var data = _intakeRepository.updateReferralFile(obj);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void ADDReferralFiletEST()
        {
            RFAReferralFile obj = new RFAReferralFile
            {
                RFAReferralID = 1192,
                RFAReferralFileName = "1192_TimeExtensionLetter.pdf",
                RFAFileTypeID = 15,
                RFAFileUserID = 1,
                RFAFileCreationDate = System.DateTime.Now
            };
            var data = _intakeRepository.addReferralFile(obj);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void getReferralFileByID()
        {
            var data = _intakeRepository.getReferralFileByID(1);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void getReferralFileByRFAReferralID()
        {
            var data = _intakeRepository.getReferralFileByRFAReferralID(32);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void getReferralFileByRFAReferralIDandFileType()
        {
            var filetypeid = new int[] { 2, 3, 4, 5 };
            var data = _intakeRepository.getReferralFileByRFAReferralIDandFileType(357);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void addRFARequest()
        {
            BLModel.RFARequest obj = new BLModel.RFARequest()
            {
                RFAReferralID = 396,
                RFARequestedTreatment = "asdfasdfsad",
                RFADurationTypeID = 1,
                RFAFrequency = 2,
                RequestTypeID = 2,
                RFADuration = 2,
                RFAQuantity = 4,
                TreatmentCategoryID = 1,
                TreatmentTypeID = 1,
                RFAStrenght = "test",
                RFACPT_NDC = "123",
            };
            var data = _intakeRepository.addRFARequest(obj);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void AssignNewReferralToRequest()
        {
            RFARequest obj = new RFARequest()
            {
                RFAReferralID = 103,
                RFAStatus = null,
                RFARequestID = 77
            };
            var data = _intakeRepository.AssignNewReferralToRequest(obj);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void updateRFARequest()
        {
            BLModel.RFARequest obj = new BLModel.RFARequest()
            {
                RFAReferralID = 671,
                RFARequestedTreatment = "asdfasdfsad",
                RFADurationTypeID = 1,
                RFAFrequency = 1,
                RequestTypeID = 2,
                RFADuration = 1,
                RFAQuantity = 5,
                TreatmentCategoryID = 1,
                TreatmentTypeID = 1,
                RFAStrenght = "rrrrrr",
                RFACPT_NDC = "",
                RFARequestID = 631
            };
            var data = _intakeRepository.updateRFARequest(obj);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void deleteRFARequest()
        {
            _intakeRepository.deleteRFARequest(2);
        }
        [TestMethod]
        public void getRFARequestByID()
        {
            var data = _intakeRepository.getRFARequestByID(89);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void getRFARequestByReferralID()
        {
            var data = _intakeRepository.getRFARequestByReferralID(117);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void addRFAReferralCPTCodes()
        {
            RFARequestCPTCode obj = new RFARequestCPTCode
            {
                RFARequestID = 32,
                CPT_NDCCode = "00121"
            };
            var data = _intakeRepository.addRFAReferralCPTCodes(obj);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void updateRFAReferralCPTCodes()
        {
            RFARequestCPTCode obj = new RFARequestCPTCode
            {
                RFACPTCodeID = 1,
                RFARequestID = 32,
                CPT_NDCCode = "00154"
            };
            var data = _intakeRepository.updateRFAReferralCPTCodes(obj);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void deleteRFAReferralCPTCodes()
        {
            _intakeRepository.deleteRFAReferralCPTCodes(1);
        }
        [TestMethod]
        public void getRFAReferralCPTCodesByID()
        {
            var data = _intakeRepository.getRFAReferralCPTCodesByID(2);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void udateReferralIntakePatientClaimByID()
        {
            var data = _intakeRepository.udateReferralIntakePatientClaimByID(12, 37);
            Assert.IsTrue(data > 0, "failed");
        }
        [TestMethod]
        public void getPatientDetailsByReferralID()
        {
            var data = _intakeRepository.getPatientDetailsByReferralID(37);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void getRFADiagnosisyBReferralID()
        {
            var data = _intakeRepository.getRFADiagnosisByReferralID(102, 0, 10);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void getSignatureByReferralID()
        {
            var data = _intakeRepository.GetSignaturePathAndDescriptionByReferralID(400);
            Assert.IsTrue(data != null, "failed");
        }
       
        [TestMethod]
        public void udateRFARecordSplittingClaimIDByReferralID()
        {
            var result = _intakeRepository.udateRFARecordSplittingClaimIDByReferralID(97, 69);
            Assert.IsTrue(result != null, "failed");
        }
        [TestMethod]
        public void addRFAPatMedicalRecordReview()
        {
            RFAPatMedicalRecordReview _RFAPatMedicalRecordReview = new Core.Data.Model.RFAPatMedicalRecordReview();
            _RFAPatMedicalRecordReview.RFARecSpltID = 2;
            _RFAPatMedicalRecordReview.RFAReferralID = 93;
            var result = _intakeRepository.addRFAPatMedicalRecordReview(_RFAPatMedicalRecordReview);
            Assert.IsTrue(result != null, "failed");
        }
        [TestMethod]
        public void getRFAPatMedicalRecordReviewByPatientID()
        {
            var data = _intakeRepository.getRFAPatMedicalRecordReviewByPatientID(92, 1, 0, 10);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void GetPreviousReferralIDFromCurrentReferralInDuplicate()
        {
            var data = _intakeRepository.getPreviousReferralIDFromCurrentReferralInDuplicate(740);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void addRFASplittedReferralHistory()
        {
            RFASplittedReferralHistory obj = new RFASplittedReferralHistory
            {
                RFANewReferralID = 150,
                RFAOldReferralID = 105,
                // RFASplittedReferralDate=System.DateTime.Now
                //icdICD9Number = "00154"
            };
            var data = _intakeRepository.addRFASplittedReferralHistory(obj);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void addRFAMergedReferralHistory()
        {
            RFAMergedReferralHistory obj = new RFAMergedReferralHistory
            {
                RFANewReferralID = 150,
                RFAOldReferralID = 105,
                RFAMergedReferralDate  = System.DateTime.Now
                //icdICD9Number = "00154"
            };
            var data = _intakeRepository.addRFAMergedReferralHistory(obj);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void getPatientHistoryByPatientID()
        {
            var data = _intakeRepository.getPatientHistoryByPatientID(121, 0, 10, "Referral","ASC");
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void saveRFARequestModify()
        {
            RFARequestModify obj = new RFARequestModify
            {
                RFADuration = null,
                RFAFrequency = null
                ,
                RFADurationTypeID = 1,
                RFARequestID = 181,
                RFARequestedTreatment = "Test",
                RFARequestModifyID = 1
                // RFASplittedReferralDate=System.DateTime.Now
                //icdICD9Number = "00154"
            };
            var data = _intakeRepository.saveRFARequestModify(obj);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void getRFARequestModify()
        {
            int RFARequestID = 71;
            var data = _intakeRepository.getRFARequestModify(RFARequestID);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void addRFARequestAdditionalInfo()
        {
            RFARequestAdditionalInfo obj = new RFARequestAdditionalInfo
           {
               RFARequestID = 103,
               URIncompleteRFAForm = true
           };
            _intakeRepository.addRFARequestAdditionalInfo(obj);
        }
        [TestMethod]
        public void updateRFARequestAdditionalInfo()
        {
            RFARequestAdditionalInfo obj = new RFARequestAdditionalInfo
            {
                RFARequestID = 102,
                URIncompleteRFAForm = false,
                RFARequestAdditionalInfoID = 1
            };
            _intakeRepository.updateRFARequestAdditionalInfo(obj);
        }
        [TestMethod]
        public void addRFAReferralAdditionalInfo()
        {
            RFAReferralAdditionalInfo obj = new RFAReferralAdditionalInfo
            {
                RFAReferralID = 397,
                OriginalRFAReferralID = 357,
                UserId =1
            };
            _intakeRepository.addRFAReferralAdditionalInfo(obj);
        }
        [TestMethod]
        public void GetRFARequestAdditionalInfo()
        {
            var dd = _intakeRepository.GetRFARequestAdditionalInfo(193);
            Assert.IsTrue(dd != null, "failed");
        }
        [TestMethod]
        public void getRequestForDuplicateDecision()
        {
            var dd = _intakeRepository.GetRequestForDuplicateDecision(126, 0, 10);
            Assert.IsTrue(dd != null, "failed");
        }
        [TestMethod]
        public void SaveRFARequestAdditionalInfo()
        {
            RFARequestAdditionalInfo obj = new RFARequestAdditionalInfo
            {
                RFARequestID = 104,
                OriginalRFARequestID = 10
            };
            var dd = _intakeRepository.SaveRFARequestAdditionalInfo(obj);
            Assert.IsTrue(dd != null, "failed");
        }
        [TestMethod]
        public void addNotes()
        {
            Note obj = new Note
            {
                Notes = "dfgdfgd",
                PatientClaimID = 4,
                RFARequestID = 210,
                Date = DateTime.Now,
                UserID = 1
            };
            var data = _intakeRepository.addNotes(obj);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void updateNotes()
        {
            Note obj = new Note
            {
                Notes = "dfgdfgdok",
                PatientClaimID = 4,
                RFARequestID = 209,
                Date = DateTime.Now,
                UserID = 2,
                NoteID = 55
            };
            var data = _intakeRepository.updateNotes(obj);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void UploadSignature()
        {
            RFAReferral _rfaReferral = new RFAReferral
            {
                RFASignature = "11111",
                RFASignatureDescription = "333333",
                RFAReferralID = 417
            };
            var data = _intakeRepository.UploadSignature(_rfaReferral);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void SaveSignatureDescription()
        {
            RFAReferral _rfaReferral = new RFAReferral
            {
                RFASignature = "wertwert",
                RFASignatureDescription = "wertretttttttttt",
                RFAReferralID = 417
            };
            var data = _intakeRepository.SaveSignatureDescription(_rfaReferral);
            Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
        public void UpdateRFARequestDecisionAndRFAStatusByReferralID()
        {
            _intakeRepository.UpdateRFARequestDecisionAndRFAStatusByReferralID(232, null, null);
        }
        [TestMethod]
        public void getReferralFileByRFAReferralIDAndFileTypeID()
        { 
            var s = _intakeRepository.getReferralFileByRFAReferralIDAndFileTypeID(540, 7);
        }
        [TestMethod]
        public void addRFARequestBilling()
        {
            List<RFARequestBilling> _rfaRequestBilling = new List<RFARequestBilling>();
            RFARequestBilling ff = new RFARequestBilling();
            ff.RFARequestBillingMD = Convert.ToDecimal(1.0);
            ff.RFARequestBillingRN = Convert.ToDecimal(1.088);
            ff.RFARequestBillingSpeciality = Convert.ToDecimal(1.0);
            ff.RFARequestBillingAdmin = Convert.ToDecimal(1.0);
            ff.RFARequestBillingDeferral = Convert.ToDecimal(1.0);
            _rfaRequestBilling.Add(ff);
            _intakeRepository.addRFARequestBilling(_rfaRequestBilling.AsEnumerable());
        }
       [TestMethod]
        public void getDeterminationLetterDecisionDesc()
        {
            var data = _intakeRepository.getDeterminationLetterDecisionDesc(396);
           Assert.IsTrue(data != null, "failed");
        }
        [TestMethod]
       public void UpdateRFARequestLatestDueDateByRefferalID()
       {
           _intakeRepository.UpdateRFARequestLatestDueDateByRefferalID(396, 10, 1);
           Assert.IsTrue(true, "failed");
       }

        [TestMethod]
        public void addRFARecordSplitting()
        {
            RFARecordSplitting _rfaRecordSplitting = new RFARecordSplitting
            {
                AuthorName = "AuthorName",
                DocumentCategoryID = 1,
                DocumentTypeID = 1,
                PatientClaimID = 168,
                RFAFileName = "RFAFileName.pdf",
                RFARecDocumentDate = System.DateTime.Now,
                RFARecDocumentName = "RFARecDocumentName.pdf",
                RFARecPageEnd = 1,
                RFARecPageStart = 1,
                RFARecSpltSummary = "RFARecSpltSummary",
                RFARequestID = null,
                RFAUploadDate = System.DateTime.Now,
                UserID = 1
            };
            var data = _intakeRepository.addRFARecordSplitting(_rfaRecordSplitting);
            Assert.IsTrue(data != null, "failed");

        }
        [TestMethod]
        public void addRFARequestTimeExtensionInfo()
        {
            RFARequestTimeExtension _RFARequestTimeExtension = new RFARequestTimeExtension
            {
                RFARecSpltID = 45,
                RFAReferralID = 35,
                RFIRecords = "RFIRecords",
                RFARequestID=45
            };
            var data = _intakeRepository.addRFARequestTimeExtensionInfo(_RFARequestTimeExtension);
        }

        [TestMethod]
        public void AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID()
        {
            RFAReferralAdditionalLink _RFAReferralAdditionalLink = new RFAReferralAdditionalLink
            {
                RFAReferralID = 542,
              ClientStatementID=2
            };
        _intakeRepository.AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID(_RFAReferralAdditionalLink);
            
        }

        [TestMethod]
        public void AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID()
        {
            RFAReferralAdditionalLink _RFAReferralAdditionalLink = new RFAReferralAdditionalLink
            {
                RFAReferralID = 548,
                RFAReferralInvoiceID = 1
            };
         _intakeRepository.AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID(_RFAReferralAdditionalLink);
          
        }
        [TestMethod]
        public void DeleteRFARequestTimeExtensionInfo()
        {
            _intakeRepository.DeleteRFARequestTimeExtensionInfo(553);
        }

           [TestMethod]
        public void addRFARequestBodyPart()
        {
            RFARequestBodyPart _RFARequestBodyPart = new RFARequestBodyPart
            {
                RFARequestID = 493,
             BodyPartType="sdf",
             ClaimBodyPartID=1
             
            };
            _intakeRepository.addRFARequestBodyPart(_RFARequestBodyPart);
            
        }
          [TestMethod]
           public void getRFARequestBodyPartByRequestID()
        {
            var data = _intakeRepository.getRFARequestBodyPartByRequestID(495);
        }
        
        
 
    }
}
