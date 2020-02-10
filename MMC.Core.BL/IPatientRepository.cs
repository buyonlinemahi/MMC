using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IPatientRepository
    {
        #region Patent Details
        int addPatient(Patient _patient);
        int updatePatient(Patient _patient);
        void deletePatient(int _patientId);
        IEnumerable<Patient> getpatientsAll();
        Patient getPatientByID(int _patientId);
        BLModel.Paged.Patient getPatientsByName(string SearchText, int _skip, int _take);

        #endregion
        #region Patient Current Medical Condition

        int addPatientCurrentMedicalCondition(PatientCurrentMedicalCondition _patientCurrentMedicalCondition);
        int updatePatientCurrentMedicalCondition(PatientCurrentMedicalCondition _patientCurrentMedicalCondition);
        void deletePatientCurrentMedicalCondition(int _patientCurrentMedicalConditionID);
        BLModel.Paged.PatientCurrentMedicalCondition getpatientCurrentMedicalConditionByPatientId(int _patientID, int _skip, int _take);
        void deletePatientCurrentMedicalConditionByPatientID(int _patientID);
        PatientCurrentMedicalCondition getpatientCurrentMedicalConditionByID(int _patientCurrentMedicalConditionID);
        int getPatientCurrentMedicalConditionByPatientAndCMCID(int _patientid, int _currentMedicalConditionId);
        void UpdatePatientMedicareEligibleByID(int PatientID, string mode, int CurrentMedicalConditionId);
        #endregion
        #region Patient(s) Claim
        int addPatientClaim(PatientClaim _patientClaim);
        int updatePatientClaim(PatientClaim _patientClaim);
        void deletePatientClaim(int _patientClaimID);
        BLModel.Paged.PatientClaim getpatientClaimsByPatientID(int _patientID, int _skip, int _take);
        IEnumerable<BLModel.PatientClaim> getAllpatientClaimsByPatientID(int _patientID);
        BLModel.PatientClaim getPatientClaimByID(int _patientClaimID);
        BLModel.Paged.PatientClaim getpatientClaimsByName(string _searchText, int _skip, int _take);

        /// <summary>
        ///  For Intake Claim tab
        /// </summary>
        /// <param name="Patient Claim BodtPartByBPStatusID"></param>
        /// <returns></returns>

        BLModel.Paged.PatientClaimBodyPartByBPStatusID getPatientClaimBodyPartByBPStatusID(int _patientClaimID, int _bodyPartStatusID, int _skip, int _take);
        #endregion
        #region Patient Claim(s) Add On Body Part

        int addPatientClaimAddOnBodyPart(PatientClaimAddOnBodyPart _patientAddOnBodyPart);
        int updatePatientClaimAddOnBodyPart(PatientClaimAddOnBodyPart _patientAddOnBodyPart);
        void deletePatientClaimAddOnBodyPart(int _patientClaimAddOnBodyPartID);
        BLModel.Paged.PatientClaimAddOnBodyPart getPatientClaimAddOnBodyPartByPatientClaimId(int _patientClaimID, int _skip, int _take);
        PatientClaimAddOnBodyPart getPatientClaimAddOnBodyPartByID(int _patientClaimAddOnBodyPartID);
        void deletePatientClaimAddOnBodyPartByPatientClaimID(int _patientClaimID);

        #endregion
        #region Patient(s) Plead Body Part

        int addPatientClaimPleadBodyPart(PatientClaimPleadBodyPart _patientPleadBodyPart);
        int updatePatientClaimPleadBodyPart(PatientClaimPleadBodyPart _patientPleadBodyPart);
        void deletePatientClaimPleadBodyPart(int _patientClaimPleadBodyPartID);
        BLModel.Paged.PatientClaimPleadBodyPart getPatientClaimPleadBodyPartByPatientClaimId(int _patientClaimID, int _skip, int _take);
        PatientClaimPleadBodyPart getPatientClaimPleadBodyPartByID(int _patientClaimPleadBodyPartID);

        void deletePatientClaimPleadBodyPartByPatientClaimID(int _patientClaimID);
        void updatePatientClaimPleadBodyPartByPatientClaimID(int _patientClaimID, int _bodyPartStatusID);
        #endregion
        #region Patient Claim(s) Body Part
        BLModel.Paged.BodyPartDetail getAllBodyPartsByClaimId(int _ClaimID, int _skip, int _take);
        #endregion
        #region Patient(s) Claim Diagnoses

        int addPatientClaimDiagnose(PatientClaimDiagnose _patientClaimDiagnose);
        int updatePatientClaimDiagnose(PatientClaimDiagnose _patientClaimDiagnose);
        void deletePatientClaimDiagnose(int _patientClaimDiagnoseID);
        BLModel.Paged.PatientClaimDiagnose getPatientClaimDiagnoseByPatientClaimId(int _patientClaimID, int _skip, int _take);
        IEnumerable<PatientClaimDiagnose> getPatientClaimDiagnoseByPatientClaimIdAll(int _patientClaimID);
        PatientClaimDiagnose getPatientClaimDiagnoseByID(int _patientClaimDiagnoseID);
        void deletePatientClaimDiagnosePatientClaimID(int _patientClaimID);

        #endregion
        #region Patient(s) Claim Status

        int addPatientClaimStatus(PatientClaimStatus _patientClaimStatus);
        int updatePatientClaimStatus(PatientClaimStatus _patientClaimStatus);
        int updateDeniedRationale(PatientClaimStatus _patientClaimStatus);
        void deletePatientClaimStatus(int _patientClaimStatusID);
        BLModel.Paged.PatientClaimStatus getPatientClaimStatusByPatientClaimId(int _patientClaimID, int _skip, int _take);
        PatientClaimStatus getPatientClaimStatusByID(int _patientClaimStatusID);
        PatientClaimStatus getPatientClaimStatusByRefferalID(int _rfaRefferalID);
        void deletePatientClaimStatusPatientClaimID(int _patientClaimID);

        #endregion
        #region Patient(s) Medical Records

        int addPatientMedicalRecords(PatientMedicalRecord _medicalRecord);
        int updatePatientMedicalRecords(PatientMedicalRecord _medicalRecord);
        int updatePatientMedicalRecordsForMedicalRec(PatientMedicalRecord _medicalRecord);
        IEnumerable<PatientMedicalRecord> getpatientsMedicalRecordAll();
        PatientMedicalRecord getPatientMedicalRecordByID(int _id);
        IEnumerable<PatientMedicalRecord> getMedicalRecordSplittingByPatientID(int _patientID);
        BLModel.Paged.PatientMedicalRecordByPatientID getPatientMedicalRecordByPatientID(int _patientID, int _skip, int _take, string _sortBy, string _order);
        BLModel.PatientMedicalRecordTemplateByPatientID getPatientMedicalRecordTemplateByPatientID(int _patientID);
        IEnumerable<BLModel.PatientMedicalRecordByPatientID> GetPatientMedicalRecordMultipleTemplateByPatientID(int _patientID);
        IEnumerable<RFARequest> getRFARequestRecordsByPatientClaim(string _patientClaim);
        #endregion
        #region Patient(s) Physician
        int updatePatientPhysician(PatientClaim _patientClaim);
        #endregion
        #region Patient(s) Notes
        BLModel.Paged.NotesDetail getNotesByPatientID(int patientID, int skip, int take);
        #endregion
        #region Patient Occupational
        int addPatientOccupational(PatientOccupational _patientOccupational);
        int updatePatientOccupational(PatientOccupational _patientOccupational);
        PatientOccupational getPatientOccupationalByPatientClaimID(int _patientID);
        #endregion
    }
}