using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPatientService" in both code and config file together.
    [ServiceContract]
    public interface IPatientService
    {
        #region Patent Details

        [OperationContract]
        int addPatient(Patient _patient);
        [OperationContract]
        int updatePatient(Patient _patient);
        [OperationContract]
        void deletePatient(int _patientId);
        [OperationContract]
        IEnumerable<Patient> getpatientsAll();
        [OperationContract]
        Patient getPatientByID(int _patientId);
        [OperationContract]
        DTO.Paged.PagedPatient getPatientsByName(string SearchText, int _skip, int _take);

        #endregion

        #region Patient Current Medical Condition

        [OperationContract]
        int addPatientCurrentMedicalCondition(PatientCurrentMedicalCondition _patientCurrentMedicalCondition);
        [OperationContract]
        int updatePatientCurrentMedicalCondition(PatientCurrentMedicalCondition _patientCurrentMedicalCondition);
        [OperationContract]
        void deletePatientCurrentMedicalCondition(int _patientCurrentMedicalConditionID);
        [OperationContract]
        DTO.Paged.PagedPatientCurrentMedicalCondition getpatientCurrentMedicalConditionByPatientId(int _patientID, int _skip, int _take);
        [OperationContract]
        PatientCurrentMedicalCondition getpatientCurrentMedicalConditionByID(int _patientCurrentMedicalConditionID);
        [OperationContract]
        void deletePatientCurrentMedicalConditionByPatientID(int _patientID);
        [OperationContract]
        int getPatientCurrentMedicalConditionByPatientAndCMCID(int _patientid, int _currentMedicalConditionId);
        [OperationContract]
        void UpdatePatientMedicareEligibleByID(int _patientid, string mode, int _currentMedicalConditionId);

        #endregion

        #region Patient(s) Claim

        [OperationContract]
        int addPatientClaim(PatientClaim _patientClaim);
        [OperationContract]
        int updatePatientClaim(PatientClaim _patientClaim);
        [OperationContract]
        void deletePatientClaim(int _patientClaimID);
        [OperationContract]
        DTO.Paged.PagedPatientClaim getpatientClaimsByPatientID(int _patientID, int _skip, int _take);
        [OperationContract]
        IEnumerable<DTO.PatientClaim> getAllpatientClaimsByPatientID(int _patientID);
        [OperationContract]
        PatientClaim getPatientClaimByID(int _patientClaimID);
        [OperationContract]
        DTO.Paged.PagedPatientClaim getpatientClaimsByName(string _searchText, int _skip, int _take);
        /// <summary>
        ///  For Intake Claim tab
        /// </summary>
        /// <param name="Patient Claim BodtPartByBPStatusID"></param>
        /// <returns></returns>

        [OperationContract]
        DTO.Paged.PagedPatientClaimBodyPartByBPStatusID getPatientClaimBodyPartByBPStatusID(int _patientClaimID, int _bodyPartStatusID, int _skip, int _take);
        #endregion

        #region Patient(s) Add On Body Part

        [OperationContract]
        int addPatientClaimAddOnBodyPart(PatientClaimAddOnBodyPart _patientClaimAddOnBodyPart);
        [OperationContract]
        int updatePatientClaimAddOnBodyPart(PatientClaimAddOnBodyPart _patientClaimAddOnBodyPart);
        [OperationContract]
        void deletePatientClaimAddOnBodyPart(int _patientAddOnBodyPartID);
        [OperationContract]
        DTO.Paged.PagedPatientClaimAddOnBodyPart getPatientClaimAddOnBodyPartByPatientClaimId(int _patientclaimID, int _skip, int _take);
        [OperationContract]
        PatientClaimAddOnBodyPart getPatientClaimAddOnBodyPartByID(int _patientAddOnBodyPartID);
        [OperationContract]
        void deletePatientClaimAddOnBodyPartByPatientClaimID(int _patientClaimID);
        #endregion

        #region Patient(s) Plead Body Part

        [OperationContract]
        int addPatientClaimPleadBodyPart(PatientClaimPleadBodyPart _patientClaimPleadBodyPart);
        [OperationContract]
        int updatePatientClaimPleadBodyPart(PatientClaimPleadBodyPart _patientClaimPleadBodyPart);
        [OperationContract]
        void deletePatientClaimPleadBodyPart(int _patientClaimPleadBodyPartID);
        [OperationContract]
        DTO.Paged.PagedPatientClaimPleadBodyPart getPatientClaimPleadBodyPartByPatientClaimId(int _patientclaimID, int _skip, int _take);
        [OperationContract]
        PatientClaimPleadBodyPart getPatientClaimPleadBodyPartByID(int _patientClaimPleadBodyPartID);
        [OperationContract]
        void deletePatientClaimPleadBodyPartByPatientClaimID(int _patientClaimID);
        [OperationContract]
        void updatePatientClaimPleadBodyPartByPatientClaimID(int _patientClaimID, int _bodyPartStatusID);
        #endregion

        #region Patient Claim Diagnose

        [OperationContract]
        int addPatientClaimDiagnose(PatientClaimDiagnose _patientClaimDiagnose);
        [OperationContract]
        int updatePatientClaimDiagnose(PatientClaimDiagnose _patientClaimDiagnose);
        [OperationContract]
        void deletePatientClaimDiagnose(int _patientClaimDiagnoseID);
        [OperationContract]
        DTO.Paged.PagedPatientClaimDiagnose getPatientClaimDiagnoseByPatientClaimId(int _patientclaimID, int _skip, int _take);

        [OperationContract]
        IEnumerable<PatientClaimDiagnose> getPatientClaimDiagnoseByPatientClaimIdAll(int _patientClaimID);

        [OperationContract]
        PatientClaimDiagnose getPatientClaimDiagnoseByID(int _patientClaimDiagnoseID);

        [OperationContract]
        void deletePatientClaimDiagnosePatientClaimID(int _patientClaimID);
        #endregion

        #region Patient(s) Claim Status

        [OperationContract]
        int addPatientClaimStatus(PatientClaimStatus _patientClaimStatus);
        [OperationContract]
        int updatePatientClaimStatus(PatientClaimStatus _patientClaimStatus);
        [OperationContract]
        int updateDeniedRationale(PatientClaimStatus _patientClaimStatus);
        [OperationContract]
        void deletePatientClaimStatus(int _patientClaimStatusID);
        [OperationContract]
        DTO.Paged.PagedPatientClaimStatus getPatientClaimStatusByPatientClaimId(int _patientClaimID, int _skip, int _take);
        [OperationContract]
        PatientClaimStatus getPatientClaimStatusByID(int _patientClaimStatusID);
        [OperationContract]
        PatientClaimStatus getPatientClaimStatusByRefferalID(int _rfaRefferalID);
        [OperationContract]
        void deletePatientClaimStatusPatientClaimID(int _patientClaimID);
        #endregion

        #region Patient(s) Medical Records
        [OperationContract]
        int addPatientMedicalRecords(DTO.PatientMedicalRecord PatientMedicalRecord);

        [OperationContract]
        int updatePatientMedicalRecords(DTO.PatientMedicalRecord _medicalRecord);

        [OperationContract]
        int updatePatientMedicalRecordsForMedicalRec(PatientMedicalRecord _medicalRecord);

        [OperationContract]
        IEnumerable<DTO.PatientMedicalRecord> getpatientsMedicalRecordAll();

        [OperationContract]
        DTO.PatientMedicalRecord getPatientMedicalRecordByID(int _id);

        [OperationContract]
        IEnumerable<DTO.PatientMedicalRecord> getMedicalRecordSplittingByPatientID(int _patientID);

        [OperationContract]
        DTO.Paged.PagedPatientMedicalRecordByPatientID getPatientMedicalRecordByPatientID(int _patientID, int _skip, int _take,string _sortBy, string _order);

        [OperationContract]
        DTO.PatientMedicalRecordTemplateByPatientID getPatientMedicalRecordTemplateByPatientID(int _patientID);

        [OperationContract]
        IEnumerable<DTO.PatientMedicalRecordByPatientID> GetPatientMedicalRecordMultipleTemplateByPatientID(int _patientID);

        [OperationContract]
        IEnumerable<DTO.RFARequest> getRFARequestRecordsByPatientClaim(string _patientClaim);
        #endregion

        #region Patient(s) Physician
        [OperationContract]
        int updatePatientPhysician(PatientClaim _patientClaim);
        #endregion

        #region Body Part Detail

        [OperationContract]
        DTO.Paged.PagedBodyPartDetail getAllBodyPartsByClaimId(int _claimID, int _skip, int _take);

        #endregion

        #region Patient(s) Notes
        [OperationContract]
        DTO.Paged.PagedNotesDetail getNotesByPatientID(int _patientID, int _skip, int _take);
        #endregion

        #region Patient Occupational
        [OperationContract]
        int addPatientOccupational(PatientOccupational _patientOccupational);
        [OperationContract]
        int updatePatientOccupational(PatientOccupational _patientOccupational);
        [OperationContract]
        PatientOccupational getPatientOccupationalByPatientClaimID(int _patientClaimID);
        #endregion

    }
}
