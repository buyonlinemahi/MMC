using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
using DLModel = MMC.Core.Data.Model;
namespace MMC.Core.BLImplementation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly BaseRepository<Patient> _patientRepo;
        private readonly BaseRepository<PatientClaimAddOnBodyPart> _patientClaimAddOnBodyPartRepo;
        private readonly BaseRepository<PatientClaim> _patientClaimRepo;
        private readonly BaseRepository<PatientCurrentMedicalCondition> _patientCurrentMedicalConditionRepo;
        private readonly BaseRepository<PatientClaimPleadBodyPart> _patientClaimPleadBodyPartRepo;
        private readonly BaseRepository<PatientClaimDiagnose> _patientClaimDiagnoseRepo;
        private readonly BaseRepository<PatientClaimStatus> _patientClaimStatusRepo;
        private readonly BaseRepository<PatientMedicalRecord> _patientMedicalRecord;
        private readonly BaseRepository<PatientOccupational> _patientOccupationalRepo;
        public PatientRepository()
        {
            _patientRepo = new BaseRepository<Patient>();
            _patientClaimAddOnBodyPartRepo = new BaseRepository<PatientClaimAddOnBodyPart>();
            _patientClaimRepo = new BaseRepository<PatientClaim>();
            _patientCurrentMedicalConditionRepo = new BaseRepository<PatientCurrentMedicalCondition>();
            _patientClaimPleadBodyPartRepo = new BaseRepository<PatientClaimPleadBodyPart>();
            _patientClaimDiagnoseRepo = new BaseRepository<PatientClaimDiagnose>();
            _patientClaimStatusRepo = new BaseRepository<PatientClaimStatus>();
            _patientMedicalRecord = new BaseRepository<PatientMedicalRecord>();
            _patientOccupationalRepo = new BaseRepository<PatientOccupational>();
        }
        #region Patient Details
        public int addPatient(Patient _patient)
        {
            return _patientRepo.Add(_patient).PatientID;
        }
        public int updatePatient(Patient _patient)
        {
            return _patientRepo.Update(_patient);
        }
        public void deletePatient(int _patientid)
        {
            _patientRepo.Delete(_patientid);
        }
        public IEnumerable<Patient> getpatientsAll()
        {
            return _patientRepo.GetAll();
        }
        public Patient getPatientByID(int _id)
        {
            return _patientRepo.GetById(_id);
        }
        public BLModel.Paged.Patient getPatientsByName(string SearchText, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientByName = (from pat in _MMCDbContext.patients
                                  from patclm in _MMCDbContext.patientClaims.Where(patclm => patclm.PatientID == pat.PatientID).DefaultIfEmpty()
                                  where (pat.PatFirstName.StartsWith(SearchText) || pat.PatLastName.StartsWith(SearchText) || patclm.PatClaimNumber.StartsWith(SearchText) || (pat.PatFirstName + " " + pat.PatLastName).StartsWith(SearchText))
                                  select new
                                  {
                                      pat.PatLastName,
                                      pat.PatAddress1,
                                      pat.PatAddress2,
                                      pat.PatCity,
                                      pat.PatDOB,
                                      pat.PatEMail,
                                      pat.PatEthnicBackground,
                                      pat.PatFax,
                                      pat.PatFirstName,
                                      pat.PatGender,
                                      pat.PatientID,
                                      pat.PatMaritalStatus,
                                      pat.PatMedicareEligible,
                                      pat.PatPhone,
                                      pat.PatPrimaryLanguageId,
                                      pat.PatSecondaryLanguageId,
                                      pat.PatSSN,
                                      pat.PatStateId,
                                      pat.PatZip,
                                      PatClaimNumber = patclm.PatClaimNumber,
                                      PatDOI = (DateTime?)patclm.PatDOI,
                                      PatAge = (DateTime.Now.Year - pat.PatDOB.Year),
                                      PatientClaimID = (patclm.PatientClaimID == null ? 0 : patclm.PatientClaimID)
                                  }).OrderByDescending(pat => pat.PatientID).Skip(_skip).Take(_take).ToList();
            return new BLModel.Paged.Patient
            {
                PatientDetails = _patientByName.Select(pat => new BLModel.Patient().InjectFrom(pat)).Cast<BLModel.Patient>().ToList(),
                TotalCount = (from pat in _MMCDbContext.patients
                              from patclm in _MMCDbContext.patientClaims.Where(patclm => patclm.PatientID == pat.PatientID).DefaultIfEmpty()
                              where (pat.PatFirstName.StartsWith(SearchText) || pat.PatLastName.StartsWith(SearchText) || patclm.PatClaimNumber.StartsWith(SearchText) || (pat.PatFirstName + " " + pat.PatLastName).StartsWith(SearchText))
                              select pat).Count()
            };
        }
        #endregion
        #region Patient Current Medical Condition
        public int addPatientCurrentMedicalCondition(PatientCurrentMedicalCondition _patientCurrentMedicalCondition)
        {
            var _PatCurrentMedicalConditionId = _patientCurrentMedicalConditionRepo.Add(_patientCurrentMedicalCondition).PatCurrentMedicalConditionId;
            if (_patientCurrentMedicalCondition.CurrentMedicalConditionId == Global.GlobalConst.CurrentMedicalCondition.EndStageRenalDisease)
                UpdatePatientMedicareEligibleByID(_patientCurrentMedicalCondition.PatientID, Global.GlobalConst.Mode.Add , _patientCurrentMedicalCondition.CurrentMedicalConditionId);
            return _PatCurrentMedicalConditionId;
        }
        public int updatePatientCurrentMedicalCondition(PatientCurrentMedicalCondition _patientCurrentMedicalCondition)
        {
            return _patientCurrentMedicalConditionRepo.Update(_patientCurrentMedicalCondition);
        }
        public void deletePatientCurrentMedicalCondition(int _patientCurrentMedicalConditionID)
        {
            PatientCurrentMedicalCondition _patientCurrentMedicalCondition = _patientCurrentMedicalConditionRepo.GetById(_patientCurrentMedicalConditionID);
            _patientCurrentMedicalConditionRepo.Delete(_patientCurrentMedicalConditionID);
            if (_patientCurrentMedicalCondition.CurrentMedicalConditionId == Global.GlobalConst.CurrentMedicalCondition.EndStageRenalDisease)
                UpdatePatientMedicareEligibleByID(_patientCurrentMedicalCondition.PatientID, Global.GlobalConst.Mode.Delete, _patientCurrentMedicalCondition.CurrentMedicalConditionId);
        }
        
        public BLModel.Paged.PatientCurrentMedicalCondition getpatientCurrentMedicalConditionByPatientId(int _patientID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientCurrentMedicalConditionByPatientId = (from pcmc in _MMCDbContext.patientCurrentMedicalConditions
                                                              join cmc in _MMCDbContext.currentMedicalConditions
                                                              on pcmc.CurrentMedicalConditionId equals cmc.CurrentMedicalConditionId
                                                              where pcmc.PatientID == _patientID
                                                              select new { pcmc.PatCurrentMedicalConditionId, pcmc.PatientID, pcmc.CurrentMedicalConditionId, PatCurrentMedicalConditionName = cmc.CurrentMedicalConditionName }).OrderByDescending(pcmc => pcmc.PatCurrentMedicalConditionId).ToList();
            return new BLModel.Paged.PatientCurrentMedicalCondition
            {
                PatientCurrentMedicalConditionDetails = _patientCurrentMedicalConditionByPatientId.Select(pcmc => new BLModel.PatientCurrentMedicalCondition().InjectFrom(pcmc)).Cast<BLModel.PatientCurrentMedicalCondition>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _patientCurrentMedicalConditionByPatientId.Count()
            };
        }

        public void UpdatePatientMedicareEligibleByID(int PatientID, string mode, int CurrentMedicalConditionId)
        {
            SPImpl _SPImpl = new SPImpl();
            _SPImpl.UpdatePatientMedicareEligibleByID(PatientID, mode, CurrentMedicalConditionId);
        }


        public void deletePatientCurrentMedicalConditionByPatientID(int _patientID)
        {
            _patientCurrentMedicalConditionRepo.Delete(PCMC => PCMC.PatientID == _patientID);
        }
        public PatientCurrentMedicalCondition getpatientCurrentMedicalConditionByID(int _id)
        {
            return _patientCurrentMedicalConditionRepo.GetById(_id);
        }
        public int getPatientCurrentMedicalConditionByPatientAndCMCID(int _patientid, int _currentMedicalConditionId)
        {
            if (_patientCurrentMedicalConditionRepo.GetAll(pat => pat.PatientID == _patientid && pat.CurrentMedicalConditionId == _currentMedicalConditionId).Count() > 0)
                return 1;
            else
            {
                int PatDOBYear = _patientRepo.GetById(_patientid).PatDOB.Year;
                int CurrYear = DateTime.Now.Year;

                if ((CurrYear - PatDOBYear) >= 65)
                {
                    UpdatePatientMedicareEligibleByID(_patientid, Global.GlobalConst.Mode.Add, _currentMedicalConditionId);
                    return 1;
                }
                else
                    return 0;
            }
        }
        #endregion
        #region Patient(s) Claim
        public int addPatientClaim(PatientClaim _patientClaim)
        {
            if (_patientClaimRepo.GetAll(PatClm => PatClm.PatClaimNumber == _patientClaim.PatClaimNumber).Count() > 0)
                return DLModel.Constant.Constant.ConstantValue.AllReadyExists;
            else
                return _patientClaimRepo.Add(_patientClaim).PatientClaimID;
        }
        public int updatePatientClaim(PatientClaim _patientClaim)
        {
            if (_patientClaimRepo.GetAll(PatClm => PatClm.PatientClaimID != _patientClaim.PatientClaimID && PatClm.PatClaimNumber == _patientClaim.PatClaimNumber).Count() > 0)
                return DLModel.Constant.Constant.ConstantValue.AllReadyExists;
            else
                return _patientClaimRepo.Update(_patientClaim);
        }
        public void deletePatientClaim(int _patientClaimID)
        {
            _patientClaimRepo.Delete(_patientClaimID);
        }
        public BLModel.Paged.PatientClaim getpatientClaimsByPatientID(int _patientID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientClaimByPatientId = (from patclm in _MMCDbContext.patientClaims
                                            where patclm.PatientID == _patientID
                                            select new { patclm.PatientClaimID, patclm.PatClaimNumber, patclm.PatDOI, patclm.PatClaimJurisdictionId, patclm.PatientID }).OrderByDescending(patclm => patclm.PatientClaimID).ToList();
            return new BLModel.Paged.PatientClaim
            {
                PatientClaimDetails = _patientClaimByPatientId.Select(pcmc => new BLModel.PatientClaim().InjectFrom(pcmc)).Cast<BLModel.PatientClaim>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _patientClaimByPatientId.Count()
            };
        }

        public IEnumerable<BLModel.PatientClaim> getAllpatientClaimsByPatientID(int _patientID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientClaimByPatientId = (from patclm in _MMCDbContext.patientClaims
                                            where patclm.PatientID == _patientID
                                            select new { patclm.PatientClaimID, patclm.PatClaimNumber, patclm.PatDOI, patclm.PatClaimJurisdictionId, patclm.PatientID }).OrderByDescending(patclm => patclm.PatientClaimID).ToList();
            return _patientClaimByPatientId.Select(pcmc => new BLModel.PatientClaim().InjectFrom(pcmc)).Cast<BLModel.PatientClaim>().ToList();         
        }

        public BLModel.PatientClaim getPatientClaimByID(int _patientClaimID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientClaim = (from patientclaim in _MMCDbContext.patientClaims
                                 join claimstatues in _MMCDbContext.patientClaimStatuses
                                 on patientclaim.PatientClaimID equals claimstatues.PatientClaimID
                                 where patientclaim.PatientClaimID == _patientClaimID
                                 select new
                                 {
                                     patientclaim.PatientClaimID,
                                     patientclaim.PatientID,
                                     patientclaim.PatClaimNumber,
                                     patientclaim.PatDOI,
                                     patientclaim.PatPolicyYear,
                                     patientclaim.PatClaimJurisdictionId,
                                     patientclaim.PatAdjudicationStateCaseNumber,
                                     patientclaim.PatEDIExchangeTrackingNumber,
                                     //patientclaim.PatInjuryType,
                                     //patientclaim.PatInjuryDescription,
                                     patientclaim.PatInjuryReportedDate,
                                     patientclaim.PatAdjusterID,
                                     patientclaim.PatApplicantAttorneyID,
                                     patientclaim.PatDefenseAttorneyID,
                                     patientclaim.PatClientID,
                                     patientclaim.PatCaseManagerID,
                                     claimstatues.PatientClaimStatusID,
                                     claimstatues.DeniedRationale,
                                     claimstatues.ClaimStatusID,
                                     patientclaim.PatPhysicianID,
                                     patientclaim.PatEmployerID,   
                                     patientclaim.PatEmpSubsidiaryID,
                                     patientclaim.PatInsurerID,
                                     patientclaim.PatInsuranceBranchID,
                                     patientclaim.PatTPAID,
                                     patientclaim.PatTPABranchID,
                                     patientclaim.PatADRID,
                                     PatInsValue = patientclaim.PatInsurerID != null ? patientclaim.PatInsurerID + "-Ins" : patientclaim.PatInsuranceBranchID+"-InsB",
                                     PatTPAValue = patientclaim.PatTPAID == null ? patientclaim.PatTPABranchID+"-tpab" : patientclaim.PatTPAID + "-tpa"

                                 }).ToList();
          BLModel.PatientClaim aaa=  _patientClaim.Select(patclm => new BLModel.PatientClaim().InjectFrom(patclm)).Cast<BLModel.PatientClaim>().SingleOrDefault();
          return aaa;
        }
        public BLModel.Paged.PatientClaim getpatientClaimsByName(string _searchText, int _skip, int _take)
        {
            SPImpl _spimpl = new SPImpl();
            return new BLModel.Paged.PatientClaim
            {
                PatientClaimDetails = _spimpl.getPatientsClaimByName(_searchText, _skip, _take),
                TotalCount = _spimpl.getPatientsClaimByNameCount(_searchText)
            };
        }
        public BLModel.Paged.PatientClaimBodyPartByBPStatusID getPatientClaimBodyPartByBPStatusID(int _patientClaimID, int _bodyPartStatusID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientClaimBodyPartByBPStatusID = ((from pcaobp in _MMCDbContext.patientClaimAddOnBodyParts
                                                      join bp in _MMCDbContext.bodyParts
                                                      on pcaobp.AddOnBodyPartID equals bp.BodyPartID
                                                      where pcaobp.PatientClaimID == _patientClaimID && pcaobp.BodyPartStatusID == _bodyPartStatusID
                                                      select new { PatientClaimBodtPartID = pcaobp.PatientClaimAddOnBodyPartID, PatientClaimBodtPartStatus = bp.BodyPartName, TableName = "AddOnBodyPart" })
                                                      .Union(from pcpbp in _MMCDbContext.patientClaimPleadBodyParts
                                                             join bp in _MMCDbContext.bodyParts
                                                             on pcpbp.PleadBodyPartID equals bp.BodyPartID
                                                             where pcpbp.PatientClaimID == _patientClaimID && pcpbp.BodyPartStatusID == _bodyPartStatusID
                                                             select new { PatientClaimBodtPartID = pcpbp.PatientClaimPleadBodyPartID, PatientClaimBodtPartStatus = bp.BodyPartName, TableName = "PleadBodyPart" })).OrderByDescending(pcbp => pcbp.PatientClaimBodtPartID).ToList();
            return new BLModel.Paged.PatientClaimBodyPartByBPStatusID
            {
                PatientClaimBodyPartByBPStatusIDDetails = _patientClaimBodyPartByBPStatusID.Select(pcbp => new BLModel.PatientClaimBodyPartByBPStatusID().InjectFrom(pcbp)).Cast<BLModel.PatientClaimBodyPartByBPStatusID>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _patientClaimBodyPartByBPStatusID.Count()
            };
        }
        #endregion
        #region Patient Claim(s) Body Part
        public BLModel.Paged.BodyPartDetail getAllBodyPartsByClaimId(int _ClaimID, int _skip, int _take)
        {
            SPImpl _spimpl = new SPImpl();
            return new BLModel.Paged.BodyPartDetail
            {
                BodyPartDetails = _spimpl.getBodyPartsByClaimId(_ClaimID,_skip,_take),
                TotalCount = _spimpl.getBodyPartsCountByClaimId(_ClaimID)
            };
        }
        #endregion
        #region Patient(s) Add On Body Part
        public int addPatientClaimAddOnBodyPart(PatientClaimAddOnBodyPart _patientAddOnBodyPart)
        {
            return _patientClaimAddOnBodyPartRepo.Add(_patientAddOnBodyPart).PatientClaimAddOnBodyPartID;
        }
        public int updatePatientClaimAddOnBodyPart(PatientClaimAddOnBodyPart _patientAddOnBodyPart)
        {
            return _patientClaimAddOnBodyPartRepo.Update(_patientAddOnBodyPart);
        }
        public void deletePatientClaimAddOnBodyPart(int _patientAddOnBodyPartID)
        {
            _patientClaimAddOnBodyPartRepo.Delete(_patientAddOnBodyPartID);
        }
        public BLModel.Paged.PatientClaimAddOnBodyPart getPatientClaimAddOnBodyPartByPatientClaimId(int _patientClaimID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientAddOnBodyPart = (from patclm in _MMCDbContext.patientClaims
                                         join paobp in _MMCDbContext.patientClaimAddOnBodyParts
                                         on patclm.PatientClaimID equals paobp.PatientClaimID
                                         join bdp in _MMCDbContext.bodyParts
                                         on paobp.AddOnBodyPartID equals bdp.BodyPartID
                                         join bdps in _MMCDbContext.BodyPartStatuses
                                         on paobp.BodyPartStatusID equals bdps.BodyPartStatusID
                                         where patclm.PatientClaimID == _patientClaimID
                                         select new { paobp.AddOnBodyPartID, paobp.PatientClaimAddOnBodyPartID, paobp.AcceptedDate, paobp.PatientClaimID, paobp.BodyPartStatusID, PatAddOnBodyPartName = bdp.BodyPartName, PatBodyPartStatusName = bdps.BodyPartStatusName, PatAddOnBodyPartCondition = paobp.AddOnBodyPartCondition }).OrderByDescending(bdp => bdp.PatientClaimAddOnBodyPartID).ToList();
            return new BLModel.Paged.PatientClaimAddOnBodyPart
            {
                PatientClaimAddOnBodyPartDetails = _patientAddOnBodyPart.Select(pcmc => new BLModel.PatientClaimAddOnBodyPart().InjectFrom(pcmc)).Cast<BLModel.PatientClaimAddOnBodyPart>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _patientAddOnBodyPart.Count()
            };
        }
        public PatientClaimAddOnBodyPart getPatientClaimAddOnBodyPartByID(int _patientClaimAddOnBodyPartID)
        {
            return _patientClaimAddOnBodyPartRepo.GetById(_patientClaimAddOnBodyPartID);
        }
        public void deletePatientClaimAddOnBodyPartByPatientClaimID(int _patientClaimID)
        {
            _patientClaimAddOnBodyPartRepo.Delete(PCABP => PCABP.PatientClaimID == _patientClaimID);
        }
        #endregion
        #region Patient(s) Plead Body Part
        public int addPatientClaimPleadBodyPart(PatientClaimPleadBodyPart _patientClaimPleadBodyPart)
        {
            return _patientClaimPleadBodyPartRepo.Add(_patientClaimPleadBodyPart).PatientClaimPleadBodyPartID;
        }
        public int updatePatientClaimPleadBodyPart(PatientClaimPleadBodyPart _patientClaimPleadBodyPart)
        {
            return _patientClaimPleadBodyPartRepo.Update(_patientClaimPleadBodyPart);
        }
        public void deletePatientClaimPleadBodyPart(int _patientClaimPleadBodyPartID)
        {
            _patientClaimPleadBodyPartRepo.Delete(_patientClaimPleadBodyPartID);
        }
        public BLModel.Paged.PatientClaimPleadBodyPart getPatientClaimPleadBodyPartByPatientClaimId(int _patientClaimID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientAddOnBodyPart = (from patclm in _MMCDbContext.patientClaims
                                         join pcpbp in _MMCDbContext.patientClaimPleadBodyParts
                                         on patclm.PatientClaimID equals pcpbp.PatientClaimID
                                         join bdp in _MMCDbContext.bodyParts
                                         on pcpbp.PleadBodyPartID equals bdp.BodyPartID
                                         join bdps in _MMCDbContext.BodyPartStatuses
                                         on pcpbp.BodyPartStatusID equals bdps.BodyPartStatusID
                                         where patclm.PatientClaimID == _patientClaimID
                                         select new { pcpbp.PatientClaimID, pcpbp.PatientClaimPleadBodyPartID,
                                             pcpbp.PleadBodyPartID, pcpbp.BodyPartStatusID, pcpbp.AcceptedDate, 
                                             PatPleadBodyPartName = bdp.BodyPartName, PatBodyPartStatusName = bdps.BodyPartStatusName, PatPleadBodyPartCondition = pcpbp.PleadBodyPartCondition }).OrderByDescending(bdp => bdp.PatientClaimPleadBodyPartID).ToList();
            return new BLModel.Paged.PatientClaimPleadBodyPart
            {
                PatientClaimPleadBodyPartDetails = _patientAddOnBodyPart.Select(pcmc => new BLModel.PatientClaimPleadBodyPart().InjectFrom(pcmc)).Cast<BLModel.PatientClaimPleadBodyPart>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _patientAddOnBodyPart.Count()
            };
        }
        public void updatePatientClaimPleadBodyPartByPatientClaimID(int _patientClaimID, int _bodyPartStatusID)
        {
            SPImpl _spImp = new SPImpl();
            _spImp.updatePatientClaimPleadBodyPartByPatientClaimID(_patientClaimID, _bodyPartStatusID);
        }
        public PatientClaimPleadBodyPart getPatientClaimPleadBodyPartByID(int _patientClaimPleadBodyPartID)
        {
            return _patientClaimPleadBodyPartRepo.GetById(_patientClaimPleadBodyPartID);
        }
        public void deletePatientClaimPleadBodyPartByPatientClaimID(int _patientClaimID)
        {
            _patientClaimPleadBodyPartRepo.Delete(PPBP => PPBP.PatientClaimID == _patientClaimID);
        }
        #endregion
        #region Patient(s) Claim Diagnoses
        public int addPatientClaimDiagnose(PatientClaimDiagnose _patientClaimDiagnose)
        {
            return _patientClaimDiagnoseRepo.Add(_patientClaimDiagnose).PatientClaimDiagnosisID;
        }
        public int updatePatientClaimDiagnose(PatientClaimDiagnose _patientClaimDiagnose)
        {
            return _patientClaimDiagnoseRepo.Update(_patientClaimDiagnose);
        }
        public void deletePatientClaimDiagnose(int _patientClaimDiagnoseID)
        {
            _patientClaimDiagnoseRepo.Delete(_patientClaimDiagnoseID);
        }
        public BLModel.Paged.PatientClaimDiagnose getPatientClaimDiagnoseByPatientClaimId(int _patientClaimID, int _skip, int _take)
        {
            //MMCDbContext _MMCDbContext = new MMCDbContext();
            //var _patientClaimDiagnoses = (from patclm in _MMCDbContext.patientClaims
            //                              join pcd in _MMCDbContext.patientClaimDiagnoses
            //                              on patclm.PatientClaimID equals pcd.PatientClaimID
            //                              join icd in _MMCDbContext.iCD9Codes
            //                              on pcd.icdICD9Number.Trim() equals icd.icdICD9Number.Trim()
            //                              where patclm.PatientClaimID == _patientClaimID && icd.ICD9Type.Equals(DLModel.Constant.Constant.ConstantValue.ICD9Type)
            //                              select new { pcd.icdICD9Number, pcd.PatientClaimDiagnosisID, pcd.PatientClaimID, icd.ICD9Description }).OrderByDescending(icd => icd.PatientClaimDiagnosisID).ToList();
            //return new BLModel.Paged.PatientClaimDiagnose
            //{
            //    PatientClaimDiagnoseDetails = _patientClaimDiagnoses.Select(pcd => new BLModel.PatientClaimDiagnose().InjectFrom(pcd)).Cast<BLModel.PatientClaimDiagnose>().Skip(_skip).Take(_take).ToList(),
            //    TotalCount = _patientClaimDiagnoses.Count()
            //};

            SPImpl _spimpl = new SPImpl();
            return new BLModel.Paged.PatientClaimDiagnose
            {
                PatientClaimDiagnoseDetails = _spimpl.getPatientClaimDiagnoseByPatientClaimId(_patientClaimID, _skip,_take),
                TotalCount = _spimpl.getPatientClaimDiagnoseByPatientClaimIdCount(_patientClaimID)
            };
        }
        public IEnumerable<PatientClaimDiagnose> getPatientClaimDiagnoseByPatientClaimIdAll(int _patientClaimID)
        {
            return _patientClaimDiagnoseRepo.GetAll(hp => hp.PatientClaimID == _patientClaimID);
        }
        public PatientClaimDiagnose getPatientClaimDiagnoseByID(int _patientClaimDiagnoseID)
        {
            return _patientClaimDiagnoseRepo.GetById(_patientClaimDiagnoseID);
        }
        public void deletePatientClaimDiagnosePatientClaimID(int _patientClaimID)
        {
            _patientClaimDiagnoseRepo.Delete(PCDP => PCDP.PatientClaimID == _patientClaimID);
        }
        #endregion
        #region Patient(s) Claim Status
        public int addPatientClaimStatus(PatientClaimStatus _patientClaimStatus)
        {
            return _patientClaimStatusRepo.Add(_patientClaimStatus).PatientClaimStatusID;
        }
        public int updatePatientClaimStatus(PatientClaimStatus _patientClaimStatus)
        {
            return _patientClaimStatusRepo.Update(_patientClaimStatus);
        }
        public void deletePatientClaimStatus(int _patientClaimStatusID)
        {
            _patientClaimStatusRepo.Delete(_patientClaimStatusID);
        }
        public BLModel.Paged.PatientClaimStatus getPatientClaimStatusByPatientClaimId(int _patientClaimID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientClaimStatuses = (from patclm in _MMCDbContext.patientClaims
                                         join pcs in _MMCDbContext.patientClaimStatuses
                                         on patclm.PatientClaimID equals pcs.PatientClaimID
                                         join cs in _MMCDbContext.claimStatuses
                                         on pcs.ClaimStatusID equals cs.ClaimStatusID
                                         where patclm.PatientClaimID == _patientClaimID
                                         select new { patclm.PatientClaimID, PatClaimStatusName = cs.ClaimStatusName, pcs.PatientClaimStatusID, pcs.ClaimStatusID }).OrderByDescending(pcs => pcs.PatientClaimStatusID).ToList();
            return new BLModel.Paged.PatientClaimStatus
            {
                PatientClaimStatustDetails = _patientClaimStatuses.Select(pcd => new BLModel.PatientClaimStatus().InjectFrom(pcd)).Cast<BLModel.PatientClaimStatus>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _patientClaimStatuses.Count()
            };
        }
        public PatientClaimStatus getPatientClaimStatusByID(int _patientClaimDiagnoseID)
        {
            return _patientClaimStatusRepo.GetById(_patientClaimDiagnoseID);
        }
        public int updateDeniedRationale(PatientClaimStatus _patientClaimStatus)
        {
            return _patientClaimStatusRepo.Update(_patientClaimStatus, rs =>rs.PatientClaimStatusID,rs=>rs.DeniedRationale);
        }
        public PatientClaimStatus getPatientClaimStatusByRefferalID(int _rfaRefferalID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _patientClaimStatus = (from pcs in _MMCDbContext.patientClaimStatuses
                                                        join rfarefferals in _MMCDbContext.rfaReferrals
                                                        on pcs.PatientClaimID equals rfarefferals.PatientClaimID
                                                        where rfarefferals.RFAReferralID == _rfaRefferalID
                                                        select new 
                                                        {
                                                            PatientClaimID = pcs.PatientClaimID,
                                                            ClaimStatusID = pcs.ClaimStatusID,
                                                            PatientClaimStatusID = pcs.PatientClaimStatusID,
                                                            DeniedRationale = pcs.DeniedRationale
                                                        }).ToList();
            return _patientClaimStatus.Select(pcd => new DLModel.PatientClaimStatus().InjectFrom(pcd)).Cast<DLModel.PatientClaimStatus>().SingleOrDefault();
        }
        public void deletePatientClaimStatusPatientClaimID(int _patientClaimID)
        {
            _patientClaimStatusRepo.Delete(PCS => PCS.PatientClaimID == _patientClaimID);
        }
        #endregion
        #region Patient(s) Medical Records
        public int addPatientMedicalRecords(PatientMedicalRecord _medicalRecord)
        {
            return _patientMedicalRecord.Add(_medicalRecord).PatientMedicalRecordID;
        }
        public int updatePatientMedicalRecords(PatientMedicalRecord _medicalRecord)
        {
            return _patientMedicalRecord.Update(_medicalRecord);
        }
        public int updatePatientMedicalRecordsForMedicalRec(PatientMedicalRecord _medicalRecord)
        {
            return _patientMedicalRecord.Update(_medicalRecord, hp => hp.DocumentTypeID, hp => hp.PatMRDocumentName, hp => hp.PatMRDocumentDate, hp => hp.PatMRSummary);
        }
        public IEnumerable<PatientMedicalRecord> getpatientsMedicalRecordAll()
        {
            return _patientMedicalRecord.GetAll();
        }
        public PatientMedicalRecord getPatientMedicalRecordByID(int _id)
        {
            return _patientMedicalRecord.GetById(_id);
        }
        public IEnumerable<PatientMedicalRecord> getMedicalRecordSplittingByPatientID(int _patientID)
        {
            return _patientMedicalRecord.GetAll(MR => MR.PatientID == _patientID);
        }
        public BLModel.Paged.PatientMedicalRecordByPatientID getPatientMedicalRecordByPatientID(int _patientID, int _skip, int _take, string _sortBy, string _order)
        {
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.PatientMedicalRecordByPatientID
            {
                PatientMedicalRecordByPatientIDDetails = _spImpl.getPatientMedicalRecordByPatientID(_patientID, _skip, _take, _sortBy, _order),
                TotalCount = _spImpl.GetPatientMedicalRecordByPatientIDCount(_patientID)
            };
        }

      

        public BLModel.PatientMedicalRecordTemplateByPatientID getPatientMedicalRecordTemplateByPatientID(int _patientID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.GetPatientMedicalRecordTemplateByPatientID(_patientID);
        }
        public IEnumerable<BLModel.PatientMedicalRecordByPatientID> GetPatientMedicalRecordMultipleTemplateByPatientID(int _patientID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.GetPatientMedicalRecordMultipleTemplateByPatientID(_patientID).ToList();
        }
        public IEnumerable<BLModel.RequestByReferralID> getAllRequestByReferralID(int ReferralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getRequestByReferralId(ReferralID).ToList();
        }

        public IEnumerable<RFARequest> getRFARequestRecordsByPatientClaim(string _patientClaim)
        {

            MMCDbContext _mMCDbContext = new MMCDbContext();

            var results = (from rfa in _mMCDbContext.rfaReferrals
                           join rfareq in _mMCDbContext.rfaRequests
                           on rfa.RFAReferralID equals rfareq.RFAReferralID
                           join patclm in _mMCDbContext.patientClaims
                           on rfa.PatientClaimID equals patclm.PatientClaimID
                           where patclm.PatClaimNumber == _patientClaim.Trim()
                           select new { rfareq.RFARequestedTreatment, rfareq.RFARequestDate, rfareq.RFARequestID }
                           ).OrderBy(rfareq => rfareq.RFARequestedTreatment).ToList().Select(rk => new DLModel.RFARequest()
                           {
                               RFARequestedTreatment = rk.RFARequestedTreatment + " - " + rk.RFARequestDate.Value.ToString("MM/dd/yyyy"),
                               RFARequestID = rk.RFARequestID
                           });

            return results.Select(pat => new DLModel.RFARequest().InjectFrom(pat)).Cast<DLModel.RFARequest>().ToList();
        }
        #endregion
        #region Patient(s) Physician
        public int updatePatientPhysician(PatientClaim _patientClaim)
        {
           return _patientClaimRepo.Update(_patientClaim,ms => ms.PatPhysicianID);
        }
        #endregion
        #region Pateint(s) Notes
        public BLModel.Paged.NotesDetail getNotesByPatientID(int patientID, int skip, int take)
        {
            SPImpl _spimpl = new SPImpl();
            return new BLModel.Paged.NotesDetail
            {
                NotesDetails = _spimpl.getNotesByPatientID(patientID,skip,take),
                TotalCount = _spimpl.getNotesByPatientIDCount(patientID)
            };
        }
        #endregion
        #region Patient Occupational

        public int addPatientOccupational(PatientOccupational _patientOccupational)
        {
            return _patientOccupationalRepo.Add(_patientOccupational).PatientOccupationalID;
        }
        public int updatePatientOccupational(PatientOccupational _patientOccupational)
        {
            return _patientOccupationalRepo.Update(_patientOccupational);
        }
        public PatientOccupational getPatientOccupationalByPatientClaimID(int _patientID)
        {
            return _patientOccupationalRepo.GetAll(po => po.PatientID == _patientID).SingleOrDefault();
        }
        #endregion
    }
}
