using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using BLModel= MMC.Core.BL.Model;
namespace MMC.UnitTest
{
    [TestClass]
    public class PatientTest
    {
        IPatientRepository _patientRepository;

        public PatientTest()
        {
            _patientRepository = new PatientRepository();
        }

        #region Patient(s) test case
        [TestMethod]
        public void addPatient()
        {
            Patient _patient = new Patient
            {
                PatAddress1 = "PatAddress1",
                PatAddress2 = "PatAddress2",
                PatCity = "City1",
                PatDOB = DateTime.Parse("10-10-2000"),
                PatEMail = "patient@gmail.com",
                PatEthnicBackground = "PatEthnicBackground1",
                PatFax = "(321)3214-5467",
                PatFirstName = "PatFirstName6",
                PatGender = "Male",
                PatLastName = "PatLastName6",
                PatMaritalStatus = "Unmarried",
                PatMedicareEligible = "PatMedicareEligible",
                PatPhone = "(321)3214-5467",
                PatPrimaryLanguageId = 1,
                PatSecondaryLanguageId = 2,
                PatSSN = "321-21-2134",
                PatStateId = 1,
                PatZip = "90029",
            };
            var _id = _patientRepository.addPatient(_patient);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updatePatient()
        {
            Patient _patient = new Patient
            {
                PatientID = 1,
                PatAddress1 = "PatAddress1",
                PatAddress2 = "PatAddress2",
                PatCity = "City1",
                PatDOB = DateTime.Parse("10-10-2000"),
                PatEMail = "patient@gmail.com",
                PatEthnicBackground = "PatEthnicBackground1",
                PatFax = "(321)3214-5467",
                PatFirstName = "PatFirstName1",
                PatGender = "Female",
                PatLastName = "PatLastName1",
                PatMaritalStatus = "Unmarried",
                PatMedicareEligible = "PatMedicareEligible1",
                PatPhone = "(321)3214-5467",
                PatPrimaryLanguageId = 1,
                PatSecondaryLanguageId = 2,
                PatSSN = "321-21-2134",
                PatStateId = 1,
                PatZip = "90029",
            };
            var _id = _patientRepository.updatePatient(_patient);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deletePatient()
        {
            _patientRepository.deletePatient(47);
        }
        [TestMethod]
        public void getAllPatients()
        {
            var patients = _patientRepository.getpatientsAll();
            Assert.IsTrue(patients != null, "failed");
        }
        [TestMethod]
        public void getPatientByID()
        {
            var patients = _patientRepository.getPatientByID(1);
            Assert.IsTrue(patients != null, "failed");
        }

        [TestMethod]
        public void getAllPatientsName()
        {
            string SearchText = "p";
            int _skip = 0;
            int _take = 20;
            var patients = _patientRepository.getPatientsByName(SearchText, _skip, _take);
            Assert.IsTrue(patients != null, "failed");
        }





        #endregion

        #region Patient(s) Current Medical Condition Test Case


        [TestMethod]
        public void addPatientCurrentMedicalCondition()
        {
            PatientCurrentMedicalCondition _PatientCurrentMedicalCondition = new PatientCurrentMedicalCondition
            {

                CurrentMedicalConditionId = 1,
                PatientID = 1
            };

            var _id = _patientRepository.addPatientCurrentMedicalCondition(_PatientCurrentMedicalCondition);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updatePatientCurrentMedicalCondition()
        {
            PatientCurrentMedicalCondition _PatientCurrentMedicalCondition = new PatientCurrentMedicalCondition
            {
                PatCurrentMedicalConditionId = 2,
                CurrentMedicalConditionId = 2,
                PatientID = 2
            };

            var _id = _patientRepository.updatePatientCurrentMedicalCondition(_PatientCurrentMedicalCondition);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deletePatientCurrentMedicalCondition()
        {
            _patientRepository.deletePatientCurrentMedicalCondition(1);
        }

        [TestMethod]
        public void getpatientCurrentMedicalConditionByPatientId()
        {

            var patientCurrentMedicalCondition = _patientRepository.getpatientCurrentMedicalConditionByPatientId(1, 0, 10);
            Assert.IsTrue(patientCurrentMedicalCondition != null, "failed");
        }

        [TestMethod]
        public void deletePatientCurrentMedicalConditionByPatientID()
        {
            _patientRepository.deletePatientCurrentMedicalConditionByPatientID(47);
        }


        [TestMethod]
        public void getpatientCurrentMedicalConditionByID()
        {

            var patientCurrentMedicalCondition = _patientRepository.getpatientCurrentMedicalConditionByID(654);
            Assert.IsTrue(patientCurrentMedicalCondition != null, "failed");
        }

        [TestMethod]
        public void getPatientCurrentMedicalConditionByPatientAndCMCID()
        {

            var patientCurrentMedicalCondition = _patientRepository.getPatientCurrentMedicalConditionByPatientAndCMCID(131, 1);
            Assert.IsTrue(patientCurrentMedicalCondition != null, "failed");
        }


        #endregion

        #region Patient(s) Claim

        [TestMethod]
        public void addPatientClaim()
        {
            PatientClaim _PatientClaim = new PatientClaim
            {
                PatAdjudicationStateCaseNumber = "testGensadadasd",
                PatClientID = 1,
                PatClaimJurisdictionId = 2,
                PatDOI = DateTime.Parse("10/10/2015"),
                PatEDIExchangeTrackingNumber = "PatEDIExchangeTrackingNumber1",
                PatAdjusterID = 2,
                PatClaimNumber = "PATCxw4Test",
                PatientID = 1,
                //PatInjuryDescription = "PatInjuryDescription2",
                PatInjuryReportedDate = DateTime.Parse("10/10/2015"),
                //PatInjuryType = "PatInjuryType",
                PatApplicantAttorneyID = 1,
                PatDefenseAttorneyID = 1,
                PatPolicyYear = "2015",
                PatInsurerID=1,
                PatInsuranceBranchID=null,
                PatTPAID=2,
                PatTPABranchID=null,
                PatEmployerID=2,
               PatADRID = 1
            };
            var _id = _patientRepository.addPatientClaim(_PatientClaim);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void updatePatientClaim()
        {
            PatientClaim _PatientClaim = new PatientClaim
            {
                PatAdjudicationStateCaseNumber = "PatAdjudicationStateCaseNumber1",
                PatAdjusterID = 2,
                PatClientID = 1,
                PatClaimJurisdictionId = 2,
                PatDOI = DateTime.Parse("10/10/2015"),
                PatEDIExchangeTrackingNumber = "PatEDIExchangeTrackingNumber1",
                PatApplicantAttorneyID = 1,
                PatDefenseAttorneyID = 1,
                PatClaimNumber = "PATCLM123564gen",
                PatientID = 1,
                //PatInjuryDescription = "PatInjuryDescription1",
                PatInjuryReportedDate = DateTime.Parse("10/10/2015"),
               // PatInjuryType = "PatInjuryType",
                PatPolicyYear = "2015",
                PatientClaimID = 3,
                PatInsurerID = 1,
                PatInsuranceBranchID = null,
                PatTPAID = 2,
                PatTPABranchID = null,
                PatEmployerID = 2
            };
            var _id = _patientRepository.updatePatientClaim(_PatientClaim);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void deletePatientClaim()
        {
            _patientRepository.deletePatientClaim(1);
        }

        [TestMethod]
        public void getpatientClaimsByPatientID()
        {

            var _patientClaimsByPatientID = _patientRepository.getpatientClaimsByPatientID(60, 0, 10);
            Assert.IsTrue(_patientClaimsByPatientID != null, "failed");
        }


        [TestMethod]
        public void getPatientClaimByID()
        {
            var _PatientClaim = _patientRepository.getPatientClaimByID(133);
            //   Assert.IsTrue(_PatientClaim != null, "failed");
        }


        [TestMethod]
        public void getpatientClaimsByName()
        {
            var _patientClaimsByName = _patientRepository.getpatientClaimsByName("p", 0, 10);
            Assert.IsTrue(_patientClaimsByName != null, "failed");
        }

        [TestMethod]
        public void getPatientClaimBodyPartByBPStatusID()
        {
            var _patientClaimBodyPartByBPStatusID = _patientRepository.getPatientClaimBodyPartByBPStatusID(3, 2, 0, 20);
            Assert.IsTrue(_patientClaimBodyPartByBPStatusID != null, "failed");
        }

        #endregion

        #region Patient(s) Add On Body Part


        [TestMethod]
        public void addPatientClaimAddOnBodyPart()
        {
            PatientClaimAddOnBodyPart _PatientClaimAddOnBodyPart = new PatientClaimAddOnBodyPart
            {
                AddOnBodyPartID = 6,
                PatientClaimID = 1,
                BodyPartStatusID = 1
            };

            var _id = _patientRepository.addPatientClaimAddOnBodyPart(_PatientClaimAddOnBodyPart);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updatePatientClaimAddOnBodyPart()
        {
            PatientClaimAddOnBodyPart _PatientClaimAddOnBodyPart = new PatientClaimAddOnBodyPart
            {
                AddOnBodyPartID = 5,
                PatientClaimID = 3,
                PatientClaimAddOnBodyPartID = 18,
                BodyPartStatusID = 2
            };

            var _id = _patientRepository.updatePatientClaimAddOnBodyPart(_PatientClaimAddOnBodyPart);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deletePatientClaimAddOnBodyPart()
        {
            _patientRepository.deletePatientClaimAddOnBodyPart(1);
        }

        [TestMethod]
        public void getPatientClaimAddOnBodyPartByPatientClaimId()
        {

            var _patientAddOnBodyPartByPatient = _patientRepository.getPatientClaimAddOnBodyPartByPatientClaimId(163, 0, 10);
            Assert.IsTrue(_patientAddOnBodyPartByPatient != null, "failed");
        }

        [TestMethod]
        public void getPatientClaimAddOnBodyPartByID()
        {

            var _patientAddOnBodyPartByPatient = _patientRepository.getPatientClaimAddOnBodyPartByID(3);
            Assert.IsTrue(_patientAddOnBodyPartByPatient != null, "failed");
        }

        [TestMethod]
        public void deletePatientClaimAddOnBodyPartByPatientClaimID()
        {
            _patientRepository.deletePatientClaimAddOnBodyPartByPatientClaimID(1);
        }

        #endregion

        #region Patient(s) Plead Body Part


        [TestMethod]
        public void addPatientClaimPleadBodyPart()
        {
            PatientClaimPleadBodyPart _PatientClaimPleadBodyPart = new PatientClaimPleadBodyPart
            {
                PleadBodyPartID = 5,
                PatientClaimID = 1,
                BodyPartStatusID = 1,
                AcceptedDate=DateTime.Now
            };

            var _id = _patientRepository.addPatientClaimPleadBodyPart(_PatientClaimPleadBodyPart);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updatePatientClaimPleadBodyPart()
        {
            PatientClaimPleadBodyPart _PatientClaimPleadBodyPart = new PatientClaimPleadBodyPart
            {
                PleadBodyPartID = 4,
                PatientClaimID = 3,
                PatientClaimPleadBodyPartID = 6,
                BodyPartStatusID = 1
            };

            var _id = _patientRepository.updatePatientClaimPleadBodyPart(_PatientClaimPleadBodyPart);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deletePatientCliamPleadBodyPart()
        {
            _patientRepository.deletePatientClaimPleadBodyPart(1);
        }

        [TestMethod]
        public void getPatientClaimPleadBodyPartByPatientClaimId()
        {

            var _patientPleadBodyPartByPatient = _patientRepository.getPatientClaimPleadBodyPartByPatientClaimId(163, 0, 10);
            Assert.IsTrue(_patientPleadBodyPartByPatient != null, "failed");
        }

        [TestMethod]
        public void getPatientPleadBodyPartByID()
        {

            var _patientPleadBodyPart = _patientRepository.getPatientClaimPleadBodyPartByID(1);
            Assert.IsTrue(_patientPleadBodyPart != null, "failed");
        }

        [TestMethod]
        public void deletePatientClaimPleadBodyPartByPatientClaimID()
        {
            _patientRepository.deletePatientClaimPleadBodyPartByPatientClaimID(1);
        }

        [TestMethod]
        public void updatePatientClaimPleadBodyPartByPatientClaimID()
        {
            _patientRepository.updatePatientClaimPleadBodyPartByPatientClaimID(131, 2);
        }


        #endregion

        #region Patient(s) Claim Diagnoses


        [TestMethod]
        public void addPatientClaimDiagnose()
        {
            PatientClaimDiagnose _PatientClaimDiagnose = new PatientClaimDiagnose
            {
                icdICDNumber = "0020",               
                PatientClaimID = 1
            };
            var _id = _patientRepository.addPatientClaimDiagnose(_PatientClaimDiagnose);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updatePatientClaimDiagnose()
        {
            PatientClaimDiagnose _PatientClaimDiagnose = new PatientClaimDiagnose
            {
                icdICDNumber = "0010",
                PatientClaimID = 1,
                PatientClaimDiagnosisID = 1
            };

            var _id = _patientRepository.updatePatientClaimDiagnose(_PatientClaimDiagnose);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deletePatientClaimDiagnose()
        {
            _patientRepository.deletePatientClaimDiagnose(1);
        }

        [TestMethod]
        public void getPatientClaimDiagnoseByPatientClaimNumberId()
        {

            var _patientClaimDiagnoseByPatientClaimNumber = _patientRepository.getPatientClaimDiagnoseByPatientClaimId(35, 0, 10);
            Assert.IsTrue(_patientClaimDiagnoseByPatientClaimNumber != null, "failed");
        }

        [TestMethod]
        public void getPatientClaimDiagnoseByID()
        {

            var _patientClaimDiagnose = _patientRepository.getPatientClaimDiagnoseByID(1);
            Assert.IsTrue(_patientClaimDiagnose != null, "failed");
        }

        [TestMethod]
        public void deletePatientClaimDiagnosePatientClaimID()
        {
            _patientRepository.deletePatientClaimDiagnosePatientClaimID(1);
        }

        #endregion

        #region Patient(s) Claim status


        [TestMethod]
        public void addPatientClaimStatus()
        {
            PatientClaimStatus _PatientClaimStatus = new PatientClaimStatus
            {
                ClaimStatusID = 1,
                PatientClaimID = 5,
                DeniedRationale = "jhgjdfgdjgdjfgdjg"
            };
            var _id = _patientRepository.addPatientClaimStatus(_PatientClaimStatus);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updatePatientClaimStatus()
        {
            PatientClaimStatus _PatientClaimStatus = new PatientClaimStatus
            {
                PatientClaimStatusID = 3,
                ClaimStatusID = 1,
                PatientClaimID = 5,
                DeniedRationale = ""
            };
            var _id = _patientRepository.updatePatientClaimStatus(_PatientClaimStatus);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deletePatientClaimStatus()
        {
            _patientRepository.deletePatientClaimStatus(1);
        }

        [TestMethod]
        public void getPatientClaimStatusByPatientClaimId()
        {

            var _patientClaimStatusByPatientClaimId = _patientRepository.getPatientClaimStatusByPatientClaimId(1, 0, 10);
            Assert.IsTrue(_patientClaimStatusByPatientClaimId != null, "failed");
        }

        [TestMethod]
        public void getPatientClaimStatusByID()
        {

            var _PatientClaimDiagnose = _patientRepository.getPatientClaimDiagnoseByID(1);
            Assert.IsTrue(_PatientClaimDiagnose != null, "failed");
        }
        [TestMethod]
        public void deletePatientClaimStatusPatientClaimID()
        {
            _patientRepository.deletePatientClaimStatusPatientClaimID(1);
        }
        [TestMethod]
        public void getPatientClaimStatusByRefferalID()
        {
            var ss = _patientRepository.getPatientClaimStatusByRefferalID(132);
        }
        #endregion

        #region Patient(s)  Medical Record

        [TestMethod]
        public void addPatientMedicalRecord()
        {
            PatientMedicalRecord _PatientMedicalRecord = new PatientMedicalRecord
            {
                PatientID = 12,
                DocumentCategoryID = 2,
                DocumentTypeID = 40,
                PatMRDocumentDate = System.DateTime.Now,
                PatMRDocumentName = "Testing.pdf",
                PatMRPageStart = 2,
                PatMRPageEnd = 4

            };
            var _id = _patientRepository.addPatientMedicalRecords(_PatientMedicalRecord);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void updatePatientMedicalRecord()
        {

            PatientMedicalRecord _PatientMedicalRecord = new PatientMedicalRecord
            {
                PatientMedicalRecordID = 2,
                PatientID = 12,
                DocumentCategoryID = 2,
                DocumentTypeID = 40,
                PatMRDocumentDate = System.DateTime.Now,
                PatMRDocumentName = "Rajaesh.pdf",
                PatMRPageStart = 3,
                PatMRPageEnd = 4

            };
            var _id = _patientRepository.updatePatientMedicalRecords(_PatientMedicalRecord);
            Assert.IsTrue(_id > 0, "failed");
        }

        [TestMethod]
        public void GetpatientsMedicalRecordAll()
        {

            var _patientALL = _patientRepository.getpatientsMedicalRecordAll();
            Assert.IsTrue(_patientALL != null, "failed");
        }


        [TestMethod]
        public void GetPatientMedicalRecordByID()
        {

            var _PatientMedicalRecord = _patientRepository.getPatientMedicalRecordByID(2);
            Assert.IsTrue(_PatientMedicalRecord != null, "failed");
        }

        [TestMethod]
        public void GetPatientMedicalSpillitingRecordByID()
        {
            var _PatientMedicalRecord = _patientRepository.getMedicalRecordSplittingByPatientID(10);
            Assert.IsTrue(_PatientMedicalRecord != null, "failed");
        }

        [TestMethod]
        public void getPatientMedicalRecordByPatientID()
        {
            var data = _patientRepository.getPatientMedicalRecordByPatientID(130, 0, 20,"Name","ASC");
            Assert.IsTrue(data != null, "failed");
        }

        [TestMethod]
        public void getPatientMedicalRecordTemplateByPatientID()
        {
            var data = _patientRepository.getPatientMedicalRecordTemplateByPatientID(92);
            Assert.IsTrue(data != null, "failed");
        }

        [TestMethod]
        public void GetPatientMedicalRecordMultipleTemplateByPatientID()
        {
            var data = _patientRepository.GetPatientMedicalRecordMultipleTemplateByPatientID(92);
            Assert.IsTrue(data != null, "failed");
        }

        [TestMethod]
        public void GetRFARequestRecordsByPatientClaim()
        {
            var data = _patientRepository.getRFARequestRecordsByPatientClaim("n1234");
            Assert.IsTrue(data != null, "failed");
        }

        #endregion

        [TestMethod]
        public void updatePatPhysician()
        {
            PatientClaim _PatientClaim = new PatientClaim
            {
                PatAdjudicationStateCaseNumber = "PatAdjudicationStateCaseNumber1",
                PatAdjusterID = 2,
                PatClientID = 1,
                PatClaimJurisdictionId = 2,
                PatDOI = DateTime.Parse("10/10/2015"),
                PatEDIExchangeTrackingNumber = "PatEDIExchangeTrackingNumber1",
                PatApplicantAttorneyID = 1,
                PatDefenseAttorneyID = 1,
                PatClaimNumber = "PATCLM123564",
                PatientID = 1,
                //PatInjuryDescription = "PatInjuryDescription1",
                PatInjuryReportedDate = DateTime.Parse("10/10/2015"),
                //PatInjuryType = "PatInjuryType",
                PatPolicyYear = "2015",
                PatientClaimID = 3,
                PatPhysicianID = 17
            };
            var _id = _patientRepository.updatePatientPhysician(_PatientClaim);
            Assert.IsTrue(_id > 0, "failed");
        }

        #region BodyPart
        [TestMethod]
        public void getAllBodyPartsbyClaimID()
        {
            var _result = _patientRepository.getAllBodyPartsByClaimId(152, 0, 5);
            Assert.IsTrue(_result != null, "failed");
        }
        #endregion

        #region Patient(s)  Notes
        [TestMethod]
        public void getNotesByPatientID()
        {
            var _id = _patientRepository.getNotesByPatientID(130, 0, 20);
            Assert.IsTrue(_id != null, "failed");
        }
        #endregion
    }
}
