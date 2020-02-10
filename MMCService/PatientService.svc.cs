using AutoMapper;
using MMC.Core.BL;
using MMCService.CustomServiceBehaviors;
using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AutoMapperServiceBehavior]
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository iPatientRepository)
        {
            _patientRepository = iPatientRepository;
        }

        #region Patient
        public int addPatient(Patient _patient)
        {
            return _patientRepository.addPatient(Mapper.Map<DTO.Patient, MMC.Core.Data.Model.Patient>(_patient));
        }

        public int updatePatient(Patient _patient)
        {
            return _patientRepository.updatePatient(Mapper.Map<DTO.Patient, MMC.Core.Data.Model.Patient>(_patient));
        }

        public void deletePatient(int _patientid)
        {
            _patientRepository.deletePatient(_patientid);
        }

        public IEnumerable<DTO.Patient> getpatientsAll()
        {
            return Mapper.Map<IEnumerable<DTO.Patient>>(_patientRepository.getpatientsAll());
        }

        public DTO.Patient getPatientByID(int _patientid)
        {
            return Mapper.Map<DTO.Patient>(_patientRepository.getPatientByID(_patientid));
        }

        public DTO.Paged.PagedPatient getPatientsByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.Patient, DTO.Paged.PagedPatient>(_patientRepository.getPatientsByName(SearchText, _skip, _take));
        }

        

        #endregion

        #region Patient Current Medical Condition

        public int addPatientCurrentMedicalCondition(PatientCurrentMedicalCondition _patientCurrentMedicalCondition)
        {
            return _patientRepository.addPatientCurrentMedicalCondition(Mapper.Map<DTO.PatientCurrentMedicalCondition, MMC.Core.Data.Model.PatientCurrentMedicalCondition>(_patientCurrentMedicalCondition));
        }

        public int updatePatientCurrentMedicalCondition(PatientCurrentMedicalCondition _patientCurrentMedicalCondition)
        {
            return _patientRepository.updatePatientCurrentMedicalCondition(Mapper.Map<DTO.PatientCurrentMedicalCondition, MMC.Core.Data.Model.PatientCurrentMedicalCondition>(_patientCurrentMedicalCondition));
        }

        public void deletePatientCurrentMedicalCondition(int _patientCurrentMedicalConditionID)
        {
            _patientRepository.deletePatientCurrentMedicalCondition(_patientCurrentMedicalConditionID);
        }

        public DTO.Paged.PagedPatientCurrentMedicalCondition getpatientCurrentMedicalConditionByPatientId(int _patientID, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.PatientCurrentMedicalCondition, DTO.Paged.PagedPatientCurrentMedicalCondition>(_patientRepository.getpatientCurrentMedicalConditionByPatientId(_patientID, _skip, _take));
        }

        public DTO.PatientCurrentMedicalCondition getpatientCurrentMedicalConditionByID(int _patientCurrentMedicalConditionID)
        {
            return Mapper.Map<DTO.PatientCurrentMedicalCondition>(_patientRepository.getpatientCurrentMedicalConditionByID(_patientCurrentMedicalConditionID));
        }

        public void deletePatientCurrentMedicalConditionByPatientID(int _patientID)
        {
            _patientRepository.deletePatientCurrentMedicalConditionByPatientID(_patientID);
        }

        public int getPatientCurrentMedicalConditionByPatientAndCMCID(int _patientid, int _currentMedicalConditionId)
        {
            return _patientRepository.getPatientCurrentMedicalConditionByPatientAndCMCID(_patientid, _currentMedicalConditionId);
        }

        public void UpdatePatientMedicareEligibleByID(int _patientid, string mode, int _currentMedicalConditionId)
        {
            _patientRepository.UpdatePatientMedicareEligibleByID(_patientid, mode, _currentMedicalConditionId);
        }

        #endregion
         
        #region Patient(s) Claim

        public int addPatientClaim(PatientClaim _patientClaim)
        {
            return _patientRepository.addPatientClaim(Mapper.Map<DTO.PatientClaim, MMC.Core.Data.Model.PatientClaim>(_patientClaim));
        }

        public int updatePatientClaim(PatientClaim _patientClaim)
        {
            return _patientRepository.updatePatientClaim(Mapper.Map<DTO.PatientClaim, MMC.Core.Data.Model.PatientClaim>(_patientClaim));
        }

        public void deletePatientClaim(int _patientclaimID)
        {
            _patientRepository.deletePatientClaim(_patientclaimID);
        }

        public DTO.Paged.PagedPatientClaim getpatientClaimsByPatientID(int _patientID, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.PatientClaim, DTO.Paged.PagedPatientClaim>(_patientRepository.getpatientClaimsByPatientID(_patientID, _skip, _take));
        }

        public IEnumerable<DTO.PatientClaim> getAllpatientClaimsByPatientID(int _patientID)
        {
            return Mapper.Map<IEnumerable<DTO.PatientClaim>>(_patientRepository.getAllpatientClaimsByPatientID(_patientID));
        }

        public DTO.PatientClaim getPatientClaimByID(int _patientclaimID)
        {
            return Mapper.Map<DTO.PatientClaim>(_patientRepository.getPatientClaimByID(_patientclaimID));
        }
        public DTO.Paged.PagedPatientClaim getpatientClaimsByName(string _searchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.PatientClaim, DTO.Paged.PagedPatientClaim>(_patientRepository.getpatientClaimsByName(_searchText, _skip, _take));
        }
        /// <summary>
        ///  For Intake Claim tab
        /// </summary>
        /// <param name="Patient Claim BodtPartByBPStatusID"></param>
        /// <returns></returns>
        public DTO.Paged.PagedPatientClaimBodyPartByBPStatusID getPatientClaimBodyPartByBPStatusID(int _patientClaimID, int _bodyPartStatusID, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.PatientClaimBodyPartByBPStatusID, DTO.Paged.PagedPatientClaimBodyPartByBPStatusID>(_patientRepository.getPatientClaimBodyPartByBPStatusID(_patientClaimID,_bodyPartStatusID, _skip, _take));
        }
        #endregion

        #region Patient(s) Add On Body Part

        public int addPatientClaimAddOnBodyPart(PatientClaimAddOnBodyPart _patientAddOnBodyPart)
        {
            return _patientRepository.addPatientClaimAddOnBodyPart(Mapper.Map<DTO.PatientClaimAddOnBodyPart, MMC.Core.Data.Model.PatientClaimAddOnBodyPart>(_patientAddOnBodyPart));
        }

        public int updatePatientClaimAddOnBodyPart(PatientClaimAddOnBodyPart _patientAddOnBodyPart)
        {
            return _patientRepository.updatePatientClaimAddOnBodyPart(Mapper.Map<DTO.PatientClaimAddOnBodyPart, MMC.Core.Data.Model.PatientClaimAddOnBodyPart>(_patientAddOnBodyPart));
        }

        public void deletePatientClaimAddOnBodyPart(int _patientAddOnBodyPartID)
        {
            _patientRepository.deletePatientClaimAddOnBodyPart(_patientAddOnBodyPartID);
        }

        public DTO.Paged.PagedPatientClaimAddOnBodyPart getPatientClaimAddOnBodyPartByPatientClaimId(int _patientclaimID, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.PatientClaimAddOnBodyPart, DTO.Paged.PagedPatientClaimAddOnBodyPart>(_patientRepository.getPatientClaimAddOnBodyPartByPatientClaimId(_patientclaimID, _skip, _take));
        }

        public DTO.PatientClaimAddOnBodyPart getPatientClaimAddOnBodyPartByID(int _patientAddOnBodyPartID)
        {
            return Mapper.Map<DTO.PatientClaimAddOnBodyPart>(_patientRepository.getPatientClaimAddOnBodyPartByID(_patientAddOnBodyPartID));
        }

        public void deletePatientClaimAddOnBodyPartByPatientClaimID(int _patientClaimID)
        {
            _patientRepository.deletePatientClaimAddOnBodyPartByPatientClaimID(_patientClaimID);
        }
        #endregion

        #region Patient(s) Plead Body Part

        public int addPatientClaimPleadBodyPart(PatientClaimPleadBodyPart _patientPleadBodyPart)
        {
            return _patientRepository.addPatientClaimPleadBodyPart(Mapper.Map<DTO.PatientClaimPleadBodyPart, MMC.Core.Data.Model.PatientClaimPleadBodyPart>(_patientPleadBodyPart));
        }

        public int updatePatientClaimPleadBodyPart(PatientClaimPleadBodyPart _patientPleadBodyPart)
        {
            return _patientRepository.updatePatientClaimPleadBodyPart(Mapper.Map<DTO.PatientClaimPleadBodyPart, MMC.Core.Data.Model.PatientClaimPleadBodyPart>(_patientPleadBodyPart));
        }

        public void deletePatientClaimPleadBodyPart(int _patientPleadBodyPartID)
        {
            _patientRepository.deletePatientClaimPleadBodyPart(_patientPleadBodyPartID);
        }

        public DTO.Paged.PagedPatientClaimPleadBodyPart getPatientClaimPleadBodyPartByPatientClaimId(int _patientclaimID, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.PatientClaimPleadBodyPart, DTO.Paged.PagedPatientClaimPleadBodyPart>(_patientRepository.getPatientClaimPleadBodyPartByPatientClaimId(_patientclaimID, _skip, _take));
        }

        public DTO.PatientClaimPleadBodyPart getPatientClaimPleadBodyPartByID(int _patientPleadBodyPartID)
        {
            return Mapper.Map<DTO.PatientClaimPleadBodyPart>(_patientRepository.getPatientClaimPleadBodyPartByID(_patientPleadBodyPartID));
        }
        public void deletePatientClaimPleadBodyPartByPatientClaimID(int _patientClaimID)
        {
            _patientRepository.deletePatientClaimPleadBodyPartByPatientClaimID(_patientClaimID);
        }

        public void updatePatientClaimPleadBodyPartByPatientClaimID(int _patientClaimID, int _bodyPartStatusID)
        {
            _patientRepository.updatePatientClaimPleadBodyPartByPatientClaimID(_patientClaimID, _bodyPartStatusID);
        }
        #endregion

        #region Patient(s) Claim Diagnose

        public int addPatientClaimDiagnose(PatientClaimDiagnose _patientClaimDiagnose)
        {
            return _patientRepository.addPatientClaimDiagnose(Mapper.Map<DTO.PatientClaimDiagnose, MMC.Core.Data.Model.PatientClaimDiagnose>(_patientClaimDiagnose));
        }

        public int updatePatientClaimDiagnose(PatientClaimDiagnose _patientClaimDiagnose)
        {
            return _patientRepository.updatePatientClaimDiagnose(Mapper.Map<DTO.PatientClaimDiagnose, MMC.Core.Data.Model.PatientClaimDiagnose>(_patientClaimDiagnose));
        }

        public void deletePatientClaimDiagnose(int _patientClaimDiagnoseID)
        {
            _patientRepository.deletePatientClaimDiagnose(_patientClaimDiagnoseID);
        }

        public DTO.Paged.PagedPatientClaimDiagnose getPatientClaimDiagnoseByPatientClaimId(int _patientclaimID, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.PatientClaimDiagnose, DTO.Paged.PagedPatientClaimDiagnose>(_patientRepository.getPatientClaimDiagnoseByPatientClaimId(_patientclaimID, _skip, _take));
        }

        public IEnumerable<DTO.PatientClaimDiagnose> getPatientClaimDiagnoseByPatientClaimIdAll(int _patientClaimID)
        {
            return Mapper.Map<IEnumerable<PatientClaimDiagnose>>(_patientRepository.getPatientClaimDiagnoseByPatientClaimIdAll(_patientClaimID));
        }

        public DTO.PatientClaimDiagnose getPatientClaimDiagnoseByID(int _patientClaimDiagnoseID)
        {
            return Mapper.Map<DTO.PatientClaimDiagnose>(_patientRepository.getPatientClaimDiagnoseByID(_patientClaimDiagnoseID));
        }

        public void deletePatientClaimDiagnosePatientClaimID(int _patientClaimID)
        {
            _patientRepository.deletePatientClaimDiagnosePatientClaimID(_patientClaimID);
        }

        #endregion

        #region Patient(s) Claim Status

        public int addPatientClaimStatus(PatientClaimStatus _patientClaimStatus)
        {
            return _patientRepository.addPatientClaimStatus(Mapper.Map<DTO.PatientClaimStatus, MMC.Core.Data.Model.PatientClaimStatus>(_patientClaimStatus));
        }

        public int updatePatientClaimStatus(PatientClaimStatus _patientClaimDiagnose)
        {
            return _patientRepository.updatePatientClaimStatus(Mapper.Map<DTO.PatientClaimStatus, MMC.Core.Data.Model.PatientClaimStatus>(_patientClaimDiagnose));
        }

        public void deletePatientClaimStatus(int _patientClaimStatusID)
        {
            _patientRepository.deletePatientClaimStatus(_patientClaimStatusID);
        }

        public DTO.Paged.PagedPatientClaimStatus getPatientClaimStatusByPatientClaimId(int _patientclaimID, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.PatientClaimStatus, DTO.Paged.PagedPatientClaimStatus>(_patientRepository.getPatientClaimStatusByPatientClaimId(_patientclaimID, _skip, _take));
        }

        public DTO.PatientClaimStatus getPatientClaimStatusByID(int _patientClaimStatusID)
        {
            return Mapper.Map<DTO.PatientClaimStatus>(_patientRepository.getPatientClaimStatusByID(_patientClaimStatusID));
        }

        public DTO.PatientClaimStatus getPatientClaimStatusByRefferalID(int _rfaRefferalID)
        {
            return Mapper.Map<DTO.PatientClaimStatus>(_patientRepository.getPatientClaimStatusByRefferalID(_rfaRefferalID));
        }
        public int updateDeniedRationale(PatientClaimStatus _patientClaimStatus)
        {
            return _patientRepository.updateDeniedRationale(Mapper.Map<DTO.PatientClaimStatus, MMC.Core.Data.Model.PatientClaimStatus>(_patientClaimStatus));
        }

        public void deletePatientClaimStatusPatientClaimID(int _patientClaimID)
        {
            _patientRepository.deletePatientClaimStatusPatientClaimID(_patientClaimID);
        }

        #endregion

        #region Patient(s) Medical Record

        public int addPatientMedicalRecords(DTO.PatientMedicalRecord _medicalRecord)
        {
            return _patientRepository.addPatientMedicalRecords(Mapper.Map<DTO.PatientMedicalRecord, MMC.Core.Data.Model.PatientMedicalRecord>(_medicalRecord));
        }

        public int updatePatientMedicalRecords(DTO.PatientMedicalRecord _medicalRecord)
        {
            return _patientRepository.updatePatientMedicalRecords(Mapper.Map<DTO.PatientMedicalRecord, MMC.Core.Data.Model.PatientMedicalRecord>(_medicalRecord));
        }

        public int updatePatientMedicalRecordsForMedicalRec(PatientMedicalRecord _medicalRecord)
        {
            return _patientRepository.updatePatientMedicalRecordsForMedicalRec(Mapper.Map<DTO.PatientMedicalRecord, MMC.Core.Data.Model.PatientMedicalRecord>(_medicalRecord));
        }



        public IEnumerable<DTO.PatientMedicalRecord> getpatientsMedicalRecordAll()
        {
            return Mapper.Map<IEnumerable<DTO.PatientMedicalRecord>>(_patientRepository.getpatientsMedicalRecordAll());
        }

        public DTO.PatientMedicalRecord getPatientMedicalRecordByID(int _id)
        {
            return Mapper.Map<DTO.PatientMedicalRecord>(_patientRepository.getPatientMedicalRecordByID(_id));
        }


        public IEnumerable<DTO.PatientMedicalRecord> getMedicalRecordSplittingByPatientID(int _patientID)
        {
            return Mapper.Map<IEnumerable<DTO.PatientMedicalRecord>>(_patientRepository.getMedicalRecordSplittingByPatientID(_patientID));
        }

        public DTO.Paged.PagedPatientMedicalRecordByPatientID getPatientMedicalRecordByPatientID(int _patientID, int _skip, int _take, string _sortBy, string _order)
        {
            return Mapper.Map<DTO.Paged.PagedPatientMedicalRecordByPatientID>(_patientRepository.getPatientMedicalRecordByPatientID(_patientID, _skip, _take,_sortBy,_order));
        }

        public DTO.PatientMedicalRecordTemplateByPatientID getPatientMedicalRecordTemplateByPatientID(int _patientID)
        {
            return Mapper.Map<DTO.PatientMedicalRecordTemplateByPatientID>(_patientRepository.getPatientMedicalRecordTemplateByPatientID(_patientID));
        }

        public IEnumerable<DTO.PatientMedicalRecordByPatientID> GetPatientMedicalRecordMultipleTemplateByPatientID(int _patientID)
        {
            return Mapper.Map < IEnumerable<DTO.PatientMedicalRecordByPatientID>>(_patientRepository.GetPatientMedicalRecordMultipleTemplateByPatientID(_patientID));
        }


        public IEnumerable<DTO.RFARequest> getRFARequestRecordsByPatientClaim(string _patientClaim)
        {
            return Mapper.Map<IEnumerable<DTO.RFARequest>>(_patientRepository.getRFARequestRecordsByPatientClaim(_patientClaim));
        }

        #endregion


        #region Patient(s) Physician
        public int updatePatientPhysician(PatientClaim _patientClaim)
        {
            return _patientRepository.updatePatientPhysician(Mapper.Map<DTO.PatientClaim, MMC.Core.Data.Model.PatientClaim>(_patientClaim));
        }
        #endregion

        #region Body Part Detail

        public DTO.Paged.PagedBodyPartDetail getAllBodyPartsByClaimId(int _claimID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedBodyPartDetail>(_patientRepository.getAllBodyPartsByClaimId(_claimID, _skip, _take));
        }

        #endregion

        #region Patient(s) Notes
        public DTO.Paged.PagedNotesDetail getNotesByPatientID(int _patientID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedNotesDetail>(_patientRepository.getNotesByPatientID(_patientID, _skip, _take));
        }
        #endregion

        #region Patient Occupational

        public int addPatientOccupational(PatientOccupational _patientOccupational)
        {
            return _patientRepository.addPatientOccupational(Mapper.Map<DTO.PatientOccupational, MMC.Core.Data.Model.PatientOccupational>(_patientOccupational));
        }

        public int updatePatientOccupational(PatientOccupational _patientOccupational)
        {
            return _patientRepository.updatePatientOccupational(Mapper.Map<DTO.PatientOccupational, MMC.Core.Data.Model.PatientOccupational>(_patientOccupational));
        }

        public PatientOccupational getPatientOccupationalByPatientClaimID(int _patientID)
        {
            return Mapper.Map<DTO.PatientOccupational>(_patientRepository.getPatientOccupationalByPatientClaimID(_patientID));
        }
        #endregion
    }
}
