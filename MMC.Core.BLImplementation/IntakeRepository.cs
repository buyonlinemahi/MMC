using MMC.Core.BL;
using MMC.Core.BL.Model;
using MMC.Core.BLImplementation.Global;
using MMC.Core.BLImplementation.SPImplementation;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
using DLModel = MMC.Core.Data.Model;
namespace MMC.Core.BLImplementation
{
    public class IntakeRepository : IIntakeRepository
    {
        private readonly BaseRepository<RFAReferral> _rfaReferralRepo;
        private readonly BaseRepository<RFARequestModify> _rfaRequestModifyRepo;
        private readonly BaseRepository<RFASplittedReferralHistory> _rfaSplittedReferralHistoryRepo;
        private readonly BaseRepository<DLModel.RFAReferralFile> _rfaReferralFileRepo;
        private readonly BaseRepository<DLModel.RFARequest> _rfaRequestRepo;
        private readonly BaseRepository<DLModel.RFARequestBilling> _rfaRequestBillingRepo;
        private readonly BaseRepository<DLModel.RFARequestAdditionalInfo> _rfaRequestAdditionalInfoRepo;
        private readonly BaseRepository<DLModel.RFAReferralAdditionalInfo> _rfaReferralAdditionalInfoRepo;
        private readonly BaseRepository<RFARequestCPTCode> _rfaReferralCPTCodeRepo;
        private readonly BaseRepository<RFARecordSplitting> _rfaRecordSplittingRepo;
        private readonly BaseRepository<DLModel.RFAPatMedicalRecordReview> _rfaPatMedicalRecordReviewRepo;
        private readonly BaseRepository<DLModel.RFARequestTimeExtension> _rfaRequestTimeExtensionRepo;
        private readonly BaseRepository<Note> _noteRepo;
        private readonly BaseRepository<DLModel.RFAReferralAdditionalLink> _rfaReferralAdditionalLinkRepo;
        private readonly BaseRepository<DLModel.RFARequestBodyPart> _rfaRequestBodyPartRepo;
        private readonly BaseRepository<RFAMergedReferralHistory> _rfaMergedHistoryRepo;

        public IntakeRepository()
        {
            _rfaReferralRepo = new BaseRepository<RFAReferral>();
            _rfaRequestModifyRepo = new BaseRepository<RFARequestModify>();
            _rfaSplittedReferralHistoryRepo = new BaseRepository<RFASplittedReferralHistory>();
            _rfaMergedHistoryRepo = new BaseRepository<RFAMergedReferralHistory>();
            _rfaReferralFileRepo = new BaseRepository<DLModel.RFAReferralFile>();
            _rfaRequestRepo = new BaseRepository<DLModel.RFARequest>();
            _rfaRequestBillingRepo = new BaseRepository<RFARequestBilling>();
            _rfaReferralCPTCodeRepo = new BaseRepository<RFARequestCPTCode>();
            _rfaRecordSplittingRepo = new BaseRepository<RFARecordSplitting>();
            _rfaPatMedicalRecordReviewRepo = new BaseRepository<DLModel.RFAPatMedicalRecordReview>();
            _rfaRequestAdditionalInfoRepo = new BaseRepository<RFARequestAdditionalInfo>();
            _rfaReferralAdditionalInfoRepo = new BaseRepository<RFAReferralAdditionalInfo>();
            _rfaRequestTimeExtensionRepo = new BaseRepository<RFARequestTimeExtension>();
            _noteRepo = new BaseRepository<Note>();
            _rfaReferralAdditionalLinkRepo = new BaseRepository<RFAReferralAdditionalLink>();
            _rfaRequestBodyPartRepo = new BaseRepository<RFARequestBodyPart>();
        }

        #region RFAReferral
        private int AddReferral()
        {
            return _rfaReferralRepo.Add(new RFAReferral { RFAReferralCreatedDate = System.DateTime.Now.Date }).RFAReferralID;
        }
        public int addRFAReferral(RFAReferral _rfaReferral)
        {
            return _rfaReferralRepo.Add(_rfaReferral).RFAReferralID;
        }
        public int addRFASplittedReferralHistory(RFASplittedReferralHistory _rfaSplittedReferralHistory)
        {
            if (_rfaSplittedReferralHistoryRepo.GetAll(rs => rs.RFAOldReferralID == _rfaSplittedReferralHistory.RFAOldReferralID && rs.RFANewReferralID == _rfaSplittedReferralHistory.RFANewReferralID).Count() > 0)
                return 0;
            else
                return _rfaSplittedReferralHistoryRepo.Add(_rfaSplittedReferralHistory).RFASplittedReferralID;
        }

        public int addRFAMergedReferralHistory(RFAMergedReferralHistory _rfaMergedReferralHistory)
        {
            if (_rfaMergedHistoryRepo.GetAll(rs => rs.RFAOldReferralID == _rfaMergedReferralHistory.RFAOldReferralID && rs.RFANewReferralID == _rfaMergedReferralHistory.RFANewReferralID).Count() > 0)
                return 0;
            else
                return _rfaMergedHistoryRepo.Add(_rfaMergedReferralHistory).RFAMergedReferralID;
        }
        public int updateClaimInReferral(int _claimID, int _rfaReferralID)
        {
            return _rfaReferralRepo.Update(new RFAReferral { RFAReferralID = _rfaReferralID, PatientClaimID = _claimID }, hp => hp.PatientClaimID);
        }
        public int UploadSignature(RFAReferral _rfaReferral)
        {
            return _rfaReferralRepo.Update(_rfaReferral, rs => rs.RFASignature, rs => rs.RFAReferralID);

        }
        public int SaveSignatureDescription(RFAReferral _rfaReferral)
        {
            return _rfaReferralRepo.Update(_rfaReferral, rs => rs.RFASignatureDescription,rs=>rs.RFAReferralID);
        }
       
        public int updateRFAInReferral(RFAReferral _rfaReferral)
        {
            return _rfaReferralRepo.Update(_rfaReferral, hp => hp.RFAReferralDate, hp => hp.RFACEDate, hp => hp.RFACETime, hp => hp.RFAHCRGDate, hp => hp.RFASignedByPhysician, hp => hp.RFATreatmentRequestClear, hp => hp.RFADiscrepancies, hp => hp.EvaluatedBy, hp => hp.EvaluationDate, hp => hp.Credentials, hp => hp.InternalAppeal);
        }

        public int AssignNewReferralToRequest(DLModel.RFARequest _rfaRequest)
        {
            return _rfaRequestRepo.Update(_rfaRequest, hp => hp.RFAReferralID, hp => hp.RFAStatus, hp => hp.DecisionDate);
        }

        public int updateRFARequestAndDecision(DLModel.RFARequest _rfaRequest)
        {
            return _rfaRequestRepo.Update(_rfaRequest, hp => hp.RFAReferralID, hp => hp.RFAStatus, hp => hp.RFARequestedTreatment, hp => hp.RFAClinicalReasonforDecision, hp => hp.RFAGuidelinesUtilized, hp => hp.RFARelevantPortionUtilized, hp => hp.RFANotes, hp => hp.RFAFrequency, hp => hp.RFADuration, hp => hp.RFADurationTypeID, hp => hp.DecisionDate);
        }
        public int saveRFARequestModify(DLModel.RFARequestModify _rfaRequestModify)
        {
            if (_rfaRequestModifyRepo.GetAll(rs => rs.RFARequestID == _rfaRequestModify.RFARequestID).Count() > 0)
                return _rfaRequestModifyRepo.Update(_rfaRequestModify);
            else
                return _rfaRequestModifyRepo.Add(_rfaRequestModify).RFARequestModifyID;
        }
        public DLModel.RFARequestModify getRFARequestModify(int _rfaRequestID)
        {
            return _rfaRequestModifyRepo.GetAll(rs => rs.RFARequestID == _rfaRequestID).SingleOrDefault();
        }
        public int updatePhysicianInReferral(int _physicianID, int _rfaReferralID)
        {
            return _rfaReferralRepo.Update(new RFAReferral { RFAReferralID = _rfaReferralID, PhysicianID = _physicianID }, hp => hp.PhysicianID);
        }
        public RFAReferral getReferralByID(int Id)
        {
            return _rfaReferralRepo.GetById(Id);
        }
        public BLModel.Paged.IncompleteReferrals getAllIncompleteReferrals(int _skip, int _take)
        {
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.IncompleteReferrals
            {
                IncompleteReferralsDetails = _spImpl.getAllIncompleteReferrals(_skip, _take, GlobalConst.ProcessLevel.RecordSplit),
                TotalCount = _spImpl.getReferralsTotalCountInComplete(GlobalConst.ProcessLevel.RecordSplit)
            };
        }
        #endregion

        #region RFAReferralFile
        public DLModel.RFAReferralFile addReferralFileIntake(string filename, int userid)
        {
            int rfaReferralID = AddReferral();
            return _rfaReferralFileRepo.Add(new DLModel.RFAReferralFile { RFAReferralFileName = rfaReferralID + "_" + filename, RFAFileTypeID = GlobalConst.FileTypes.IntakeUpload, RFAReferralID = rfaReferralID, RFAFileUserID = userid, RFAFileCreationDate = System.DateTime.Now });
        }
        public int addReferralFile(DLModel.RFAReferralFile _rfaReferralFile)
        {
            if (_rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.IMRApplication)
                _rfaReferralFileRepo.Delete(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAFileTypeID == GlobalConst.FileTypes.IMRApplication);            
            else if (_rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.ProofofService)
                _rfaReferralFileRepo.Delete(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAFileTypeID == GlobalConst.FileTypes.ProofofService);
            else if (_rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.TimeExtensionProofOfService)
                _rfaReferralFileRepo.Delete(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAFileTypeID == GlobalConst.FileTypes.TimeExtensionProofOfService);
            else if (_rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.RFIPreparationLetter)
                _rfaReferralFileRepo.Delete(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAFileTypeID == GlobalConst.FileTypes.RFIPreparationLetter);
            else if (_rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.DeterminationLetter)
                _rfaReferralFileRepo.Delete(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAFileTypeID == GlobalConst.FileTypes.DeterminationLetter);
            else if (_rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.IMRSplitContent)
                _rfaReferralFileRepo.Delete(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAFileTypeID == GlobalConst.FileTypes.IMRSplitContent);
            else if (_rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.IMRSplitBarcode)
                _rfaReferralFileRepo.Delete(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAFileTypeID == GlobalConst.FileTypes.IMRSplitBarcode);
            else if (_rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.WithdrawnUpload)
                _rfaReferralFileRepo.Delete(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAReferralFileName == _rfaReferralFile.RFAReferralFileName && fl.RFAFileTypeID == GlobalConst.FileTypes.WithdrawnUpload);
            else if (_rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.ClientAuthorizedUpload)
                _rfaReferralFileRepo.Delete(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAReferralFileName == _rfaReferralFile.RFAReferralFileName && fl.RFAFileTypeID == GlobalConst.FileTypes.ClientAuthorizedUpload);


            if (_rfaReferralFileRepo.GetAll(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAFileTypeID == GlobalConst.FileTypes.TimeExtensionPN && _rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.TimeExtensionPN).Count() > 0)
                return 0;
            else if (_rfaReferralFileRepo.GetAll(fl => fl.RFAReferralID == _rfaReferralFile.RFAReferralID && fl.RFAFileTypeID == GlobalConst.FileTypes.TimeExtension && _rfaReferralFile.RFAFileTypeID == GlobalConst.FileTypes.TimeExtension).Count() > 0)
                return 0;
            else
                return _rfaReferralFileRepo.Add(_rfaReferralFile).RFAReferralFileID;     
           
        }
        public int updateReferralFile(DLModel.RFAReferralFile _rfaReferralFile)
        {
            return _rfaReferralFileRepo.Update(_rfaReferralFile);
        }
        public DLModel.RFAReferralFile getReferralFileByID(int Id)
        {
            return _rfaReferralFileRepo.GetById(Id);
        }

        public DLModel.RFAReferralFile getReferralFileByIntake(int _rfaReferralID)
        {
            return _rfaReferralFileRepo.GetAll(hp => hp.RFAReferralID == _rfaReferralID && hp.RFAFileTypeID == GlobalConst.FileTypes.IntakeUpload).FirstOrDefault();
        }

        public IEnumerable<DLModel.RFAReferralFile> getReferralFileByRFAReferralID(int _rfaReferralID)
        {
            return _rfaReferralFileRepo.GetAll(hp => hp.RFAReferralID == _rfaReferralID);
        }

        public IEnumerable<BLModel.RFAReferralFile> getReferralFileByRFAReferralIDandFileType(int _rfaReferralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getReferralFileByRFAReferralIDandFileType(_rfaReferralID);           
        
        }
        public IEnumerable<BLModel.RFAReferralFile> getRFAReferralFilesAccToReferralIDInPreparationCase(int _rfaReferralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getRFAReferralFilesAccToReferralIDInPreparationCase(_rfaReferralID);
        }


        public IEnumerable<DLModel.RFAReferralFile> getReferralFileByRFAReferralIDAndFileTypeID(int _rfaReferralID, int _fileTypeID)
        {
            return _rfaReferralFileRepo.GetAll(rs => rs.RFAReferralID == _rfaReferralID && rs.RFAFileTypeID == _fileTypeID).ToList();
        
        }

        public void AddRFIReportAdditionalRecordByRFAReferralFileID(int _rFAReferralFileID,int UserID)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.AddRFIReportAdditionalRecordByRFAReferralFileID(_rFAReferralFileID, UserID);

        }

        public void AddRFIReportAdditionalRecordByRFARequestID(int _rFAReferralFileID,int _rFARequestID, int UserID)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.AddRFIReportAdditionalRecordByRFARequestID(_rFAReferralFileID, _rFARequestID, UserID);

        }
        public IEnumerable<DLModel.PreviousReferralFromCurrentReferral> getPreviousReferralIDFromCurrentReferralInDuplicate(int referralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getPreviousReferralIDFromCurrentReferralInDuplicate(referralID);
        }
        #endregion
        
        #region RFARequests
        private System.DateTime GetDueDate(int _requesttypeID, System.DateTime _rfaRequestDate)
        {
            int adddays = 0;
            switch (_requesttypeID)
            {
                case 1:
                    adddays = GlobalConst.RequestTypes.Prospective;
                    break;
                case 2:
                    adddays = GlobalConst.RequestTypes.Retrospective;
                    break;
                case 3:
                    adddays = GlobalConst.RequestTypes.Expedite;
                    break;
                case 4:
                    adddays = GlobalConst.RequestTypes.Concurrent;
                    break;
                default:
                    break;
            }
            SPImpl _spImpl = new SPImpl();
           return _spImpl.GetRFARequestLatestDueDate(adddays, _rfaRequestDate);
        }
        public System.DateTime UpdateRFARequestLatestDueDateByRefferalID(int _RFAReferralID, int _AddDays, int _UserID)
        {
            SPImpl _spImpl = new SPImpl();
           return _spImpl.UpdateRFARequestLatestDueDateByRefferalID(_RFAReferralID, _AddDays, _UserID);
        }
        public int addRFARequest(BLModel.RFARequest _rfaRequest)
        {
            DLModel.RFARequest _rfaRequestDL = Mapper.Map<DLModel.RFARequest>(_rfaRequest);
            _rfaRequestDL.RFARequestDate = System.DateTime.Now;
            _rfaRequestDL.RFALatestDueDate = GetDueDate(_rfaRequest.RequestTypeID.Value, _rfaRequestDL.RFARequestDate.Value);
            int _RFARequestID = _rfaRequestRepo.Add(_rfaRequestDL).RFARequestID;
            if ((_rfaRequest.RFACPT_NDC != null) && (_rfaRequest.RFACPT_NDC != ""))
            {
                if (_rfaRequest.RFACPT_NDC.Trim().Contains(","))
                {
                    string[] arrcptndcodes;
                    arrcptndcodes = _rfaRequest.RFACPT_NDC.Split(',');
                    foreach (var _arrcptndcodes in arrcptndcodes)
                    {
                        if (_arrcptndcodes.ToString() != "")
                        {
                            DLModel.RFARequestCPTCode _RFARequestCPTCode = new DLModel.RFARequestCPTCode();
                            _RFARequestCPTCode.RFARequestID = _RFARequestID;
                            _RFARequestCPTCode.CPT_NDCCode = _arrcptndcodes.ToString();
                            addRFARequestCPTCode(_RFARequestCPTCode);
                        }
                    }

                }
                else
                {
                    DLModel.RFARequestCPTCode _RFARequestCPTCode = new DLModel.RFARequestCPTCode();
                    _RFARequestCPTCode.RFARequestID = _RFARequestID;
                    _RFARequestCPTCode.CPT_NDCCode = _rfaRequest.RFACPT_NDC.ToString().Trim();
                    addRFARequestCPTCode(_RFARequestCPTCode);
                }
            }
            //UpdateRFAReqCertificationNumberByID
            // SPImpl _SPImpl = new SPImpl();
            // _SPImpl.UpdateRFAReqCertificationNumberByID(_RFARequestID);
            return _RFARequestID;
        }
        public int updateRFARequest(BLModel.RFARequest _rfaRequest)
        {
            DLModel.RFARequest _rfaRequestDL = Mapper.Map<DLModel.RFARequest>(_rfaRequest);
            _rfaRequestDL.RFARequestDate = System.DateTime.Now;
            _rfaRequestDL.RFALatestDueDate = GetDueDate(_rfaRequest.RequestTypeID.Value, _rfaRequestDL.RFARequestDate.Value);
            //_rfaRequestRepo.Update(_rfaRequestDL);
            _rfaRequestRepo.Update(_rfaRequestDL, hp => hp.RFAReferralID, hp => hp.RequestTypeID, hp => hp.RFARequestedTreatment, hp => hp.TreatmentCategoryID, hp => hp.TreatmentTypeID, hp => hp.RFAFrequency, hp => hp.RFADuration, hp => hp.RFADurationTypeID, hp => hp.RFAQuantity);

            RFARequestModify _rFARequestModify = _rfaRequestModifyRepo.GetAll(rk => rk.RFARequestID == _rfaRequestDL.RFARequestID).SingleOrDefault();
            if (_rFARequestModify != null)
            {
                _rFARequestModify.RFARequestedTreatment = _rfaRequestDL.RFARequestedTreatment;
                _rfaRequestModifyRepo.Update(_rFARequestModify);
            }
            int _RFARequestID = _rfaRequest.RFARequestID;
            _rfaReferralCPTCodeRepo.Delete(req => req.RFARequestID == _RFARequestID);

            if ((_rfaRequest.RFACPT_NDC != null) && (_rfaRequest.RFACPT_NDC != ""))
            {
                if (_rfaRequest.RFACPT_NDC.Trim().Contains(","))
                {
                    string[] arrcptndcodes;
                    arrcptndcodes = _rfaRequest.RFACPT_NDC.Split(',');
                    foreach (var _arrcptndcodes in arrcptndcodes)
                    {
                        if (_arrcptndcodes.ToString() != "")
                        {
                            DLModel.RFARequestCPTCode _RFARequestCPTCode = new DLModel.RFARequestCPTCode();
                            _RFARequestCPTCode.RFARequestID = _RFARequestID;
                            _RFARequestCPTCode.CPT_NDCCode = _arrcptndcodes.ToString();
                            addRFARequestCPTCode(_RFARequestCPTCode);
                        }
                    }
                }
                else
                {
                    DLModel.RFARequestCPTCode _RFARequestCPTCode = new DLModel.RFARequestCPTCode();
                    _RFARequestCPTCode.RFARequestID = _RFARequestID;
                    _RFARequestCPTCode.CPT_NDCCode = _rfaRequest.RFACPT_NDC.ToString().Trim();
                    addRFARequestCPTCode(_RFARequestCPTCode);
                }
            }
            // UpdateRFAReqCertificationNumberByID
            //SPImpl _SPImpl = new SPImpl();
            //_SPImpl.UpdateRFAReqCertificationNumberByID(_RFARequestID);
            return _RFARequestID;
        }
        public void deleteRFARequest(int _rfaRequestID)
        {
            _rfaReferralCPTCodeRepo.Delete(rk => rk.RFARequestID == _rfaRequestID);
            _rfaRequestRepo.Delete(_rfaRequestID);
        }
        public void deleteRFADelatedRequest(int _rfaRequestID)
        {
            SPImpl _SPImpl = new SPImpl();
            _SPImpl.MoveRFARequestRecordToRFADeletedRequest(_rfaRequestID);
        }
        public BLModel.RFARequest getRFARequestByID(int _rfaRequestID)
        {
            BLModel.RFARequest _BLRFARequest = Mapper.Map<BLModel.RFARequest>(_rfaRequestRepo.GetById(_rfaRequestID));
            SPImpl _SPImpl = new SPImpl();
            _BLRFARequest.RFACPT_NDC = _SPImpl.getRequestCPTNDCCodesByRFARequestID(_rfaRequestID);
            return _BLRFARequest;
        }
        public IEnumerable<BLModel.RFARequest> getRFARequestByReferralID(int _rfaReferralId)
        {
            //return       
            IEnumerable<BLModel.RFARequest> _BLRFARequest = _rfaRequestRepo.GetAll(hp => hp.RFAReferralID == _rfaReferralId).Select(icr => new BLModel.RFARequest().InjectFrom(icr)).Cast<BLModel.RFARequest>().ToList();
            SPImpl _SPImpl = new SPImpl();
            foreach (var bLRFARequest in _BLRFARequest)
            {
                _SPImpl = new SPImpl();
                bLRFARequest.RFACPT_NDC = _SPImpl.getRequestCPTNDCCodesByRFARequestID(bLRFARequest.RFARequestID);
                bLRFARequest.RFARequestedTreatment = _SPImpl.getRequestedTreatmentByRFARequestID(bLRFARequest.RFARequestID);
            }
            return _BLRFARequest;
        }
        public IEnumerable<BLModel.RFARequest> getRFARequestByClaimID(int _claimID)
        {
            SPImpl _SPImpl = new SPImpl();
            return _SPImpl.getRFARequestByClaimID(_claimID);
        }
        public void addRFARequestCPTCode(RFARequestCPTCode _RFARequestCPTCode)
        {
            _rfaReferralCPTCodeRepo.Add(_RFARequestCPTCode);
        }
        public int addRFARequestAdditionalInfo(DLModel.RFARequestAdditionalInfo _rfaRequestAdditionalInfo)
        {

            return _rfaRequestAdditionalInfoRepo.Add(_rfaRequestAdditionalInfo).RFARequestAdditionalInfoID;
        }
        public int SaveRFARequestAdditionalInfo(RFARequestAdditionalInfo _rfaRequestAdditionalInfo)
        {
            var model = _rfaRequestAdditionalInfoRepo.GetAll(rs => rs.RFARequestID == _rfaRequestAdditionalInfo.RFARequestID).SingleOrDefault();
            if (model != null)
            {
                _rfaRequestAdditionalInfo.RFARequestAdditionalInfoID = model.RFARequestAdditionalInfoID;
                return updateRFARequestAdditionalInfo(_rfaRequestAdditionalInfo);
            }
            else
                return addRFARequestAdditionalInfo(_rfaRequestAdditionalInfo);

        }
        public int updateRFARequestAdditionalInfo(DLModel.RFARequestAdditionalInfo _rfaRequestAdditionalInfo)
        {

            return _rfaRequestAdditionalInfoRepo.Update(_rfaRequestAdditionalInfo);
        }
        public void addRFAReferralAdditionalInfo(DLModel.RFAReferralAdditionalInfo _rfaReferralAdditionalInfo)
        {
            SPImpl _SPImpl = new SPImpl();
            _SPImpl.SaveUpdateRFAReferralAdditionalInfo(_rfaReferralAdditionalInfo);
         
        }
        public BLModel.RFARequestAdditionalInfoDetail GetRFARequestAdditionalInfo(int _rfaRequestID)
        {
            MMCDbContext _mMCDbContext = new MMCDbContext();
            var rfaRequestAdditionalInfoDetail = (from rfaRequest in _mMCDbContext.rfaRequests
                                                  join rfaRequestAdditional in _mMCDbContext.RFARequestAdditionalInfoes
                                                  on rfaRequest.RFARequestID equals rfaRequestAdditional.RFARequestID
                                                  where rfaRequestAdditional.RFARequestID == _rfaRequestID
                                                  select new
                                                  {
                                                      rfaRequestAdditional.RFARequestAdditionalInfoID,
                                                      rfaRequestAdditional.RFARequestID,
                                                      rfaRequestAdditional.URIncompleteRFAForm,
                                                      rfaRequestAdditional.URNoRFAForm,
                                                      rfaRequestAdditional.URDeclineInternalAppeal,
                                                      rfaRequestAdditional.OriginalRFARequestID,
                                                      rfaRequest.DecisionID
                                                  }).ToList();
            return rfaRequestAdditionalInfoDetail.Select(AdditionalInfo => new BLModel.RFARequestAdditionalInfoDetail().InjectFrom(AdditionalInfo)).Cast<BLModel.RFARequestAdditionalInfoDetail>().SingleOrDefault();
        }
        public void UpdateRFAReqCertificationNumberByID(int RFAReferralID)
        {
            var _RFARequest = _rfaRequestRepo.GetAll(req => req.RFAReferralID == RFAReferralID).AsEnumerable();
            foreach (var _rFARequest in _RFARequest)
            {
                SPImpl _SPImpl = new SPImpl();
                _SPImpl.UpdateRFAReqCertificationNumberByID(_rFARequest.RFARequestID);
            }
        }
        public void UpdateRFAReferralRequestDecisionByID(int RFAReferralID)
        {
            SPImpl _SPImpl = new SPImpl();
            _SPImpl.UpdateRFAReferralRequestDecisionByID(RFAReferralID);
        }
        public void UpdateRFARequestDecisionAndRFAStatusByReferralID(int RFAReferralID, string DecisionID, string RFAStatus)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.UpdateRFARequestDecisionAndRFAStatusByReferralID(RFAReferralID, DecisionID, RFAStatus);
        }
        public string GetSignaturePathAndDescriptionByReferralID(int RFAReferralID)
        {
            SPImpl _spImpl = new SPImpl();
           return _spImpl.GetSignaturePathAndDescriptionByReferralID(RFAReferralID);
        }
       
        #endregion

        #region RFARequestBilling
        public int addRFARequestBilling(IEnumerable<DLModel.RFARequestBilling> _rfaRequestBilling)
        {
            try
            {
                foreach (RFARequestBilling _billing in _rfaRequestBilling)
                {
                    int a = _rfaRequestBillingRepo.Add(_billing).RFARequestBillingID;
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region RFAReferralCPTCodes
        public int addRFAReferralCPTCodes(RFARequestCPTCode _rfaReferralCPTCode)
        {
            return _rfaReferralCPTCodeRepo.Add(_rfaReferralCPTCode).RFACPTCodeID;
        }
        public int updateRFAReferralCPTCodes(RFARequestCPTCode _rfaReferralCPTCode)
        {
            return _rfaReferralCPTCodeRepo.Update(_rfaReferralCPTCode);
        }
        public void deleteRFAReferralCPTCodes(int _rfaCPTCodeID)
        {
            _rfaReferralCPTCodeRepo.Delete(_rfaCPTCodeID);
        }
        public RFARequestCPTCode getRFAReferralCPTCodesByID(int _rfaCPTCodeID)
        {
            return _rfaReferralCPTCodeRepo.GetById(_rfaCPTCodeID);
        }
        //public IEnumerable<RFARequestCPTCode> getRFAReferralCPTCodesByReferralID(int _rfaReferralId)
        //{
        //    return _rfaReferralCPTCodeRepo.GetAll(hp => hp.RFAReferralID == _rfaReferralId);
        //}
        #endregion

        #region RFARecord Splitting
        public int addRFARecordSplitting(RFARecordSplitting _rfaRecordSplitting)
        {
            int _RFARecSpltID = _rfaRecordSplittingRepo.Add(_rfaRecordSplitting).RFARecSpltID;
            if (_rfaRecordSplitting.RFARequestID != null)
            {
                _rfaRecordSplitting.RFAReferralID = _rfaRequestRepo.GetById(_rfaRecordSplitting.RFARequestID.Value).RFAReferralID.Value;
                _rfaRecordSplitting.RFARecSpltID = _RFARecSpltID;
                _rfaRecordSplittingRepo.Update(_rfaRecordSplitting, (rk => rk.RFAReferralID), (rk => rk.RFARecSpltID));
            }
            return _RFARecSpltID;
        }
        public int udateRFARecordSplitting(RFARecordSplitting _rfaRecordSplitting)
        {
            return _rfaRecordSplittingRepo.Update(_rfaRecordSplitting);
        }
        public int udateRFARecordSplittingClaimIDByReferralID(int rfaReferralID, int claimID)
        {
            var rfaRecSplts = _rfaRecordSplittingRepo.GetAll(hp => hp.RFAReferralID == rfaReferralID).ToList();
            foreach (var hp in rfaRecSplts)
            {
                hp.PatientClaimID = claimID;
                _rfaRecordSplittingRepo.Update(hp, hp1 => hp1.PatientClaimID);
            }
            return rfaRecSplts.Count();
        }

        public int udateRFARecordSplittingRecordForMedicalRec(RFARecordSplitting _rfaRecordSplitting)
        {
            return _rfaRecordSplittingRepo.Update(_rfaRecordSplitting, hp => hp.DocumentTypeID, hp => hp.RFARecDocumentName, hp => hp.RFARecDocumentDate, hp => hp.RFARecSpltSummary, hp => hp.AuthorName);
        }

        public void deleteRFARecordSplitting(int rfaRecSpltID)
        {
            _rfaRecordSplittingRepo.Delete(rfaRecSpltID);
        }
        public RFARecordSplitting getRFARecordSplittingByID(int rfaRecSpltID)
        {
            return _rfaRecordSplittingRepo.GetById(rfaRecSpltID);
        }
        public IEnumerable<RFARecordSplitting> getRFARecordSplittingByReferralID(int _rfaReferralId)
        {
            return _rfaRecordSplittingRepo.GetAll(hp => hp.RFAReferralID == _rfaReferralId);
        }
        #endregion

        #region Referral Intake Patient Claim
        public int udateReferralIntakePatientClaimByID(int _patientClaimID, int _rfaReferralId)
        {
            RFAReferral _rFAReferral = new RFAReferral();
            _rFAReferral.PatientClaimID = _patientClaimID;
            _rFAReferral.RFAReferralID = _rfaReferralId;
            return _rfaReferralRepo.Update(_rFAReferral, (rFAReferrals => rFAReferrals.PatientClaimID), (rFAReferrals => rFAReferrals.RFAReferralID));
        }
        public PatientDetailsByReferralID getPatientDetailsByReferralID(int _rfaReferralId)
        {
            int? _patientClaimID = getReferralByID(_rfaReferralId).PatientClaimID;
            MMCDbContext _mMCDbContext = new MMCDbContext();
            if (_patientClaimID != null)
            {
                var _PatientDetailsByReferralID = (from pat in _mMCDbContext.patients
                                                   join patclm in _mMCDbContext.patientClaims
                                                   on pat.PatientID equals patclm.PatientID
                                                   where patclm.PatientClaimID == _patientClaimID.Value
                                                   select new { PatientID = pat.PatientID, PatientName = (pat.PatFirstName + " " + pat.PatLastName), PatClaimNumber = patclm.PatClaimNumber, PatDOI = patclm.PatDOI, PatientClaimID = _patientClaimID.Value, RFAReferralID = _rfaReferralId }).AsEnumerable();
                return _PatientDetailsByReferralID.Select(icr => new BLModel.PatientDetailsByReferralID().InjectFrom(icr)).Cast<BLModel.PatientDetailsByReferralID>().SingleOrDefault();
            }
            else
            {
                PatientDetailsByReferralID _patientDetailsByReferralID = new PatientDetailsByReferralID();
                _patientDetailsByReferralID.PatientID = 0;
                _patientDetailsByReferralID.PatientClaimID = 0;
                _patientDetailsByReferralID.RFAReferralID = _rfaReferralId;
                _patientDetailsByReferralID.PatClaimNumber = null;
                _patientDetailsByReferralID.PatientName = null;
                _patientDetailsByReferralID.PatDOI = null;
                return _patientDetailsByReferralID;
            }
        }
        #endregion

        #region  RFADiagnosis
        public BLModel.Paged.RFADiagnosis getRFADiagnosisByReferralID(int _rfaReferralId, int skip, int take)
        {
           
            //var RFADiagnosis = (from patClaimDiagnoses in _mMCDbContext.patientClaimDiagnoses
            //                    join rfaReferral in _mMCDbContext.rfaReferrals
            //                    on patClaimDiagnoses.PatientClaimID equals rfaReferral.PatientClaimID
            //                    join icd9Codes in _mMCDbContext.iCD9Codes
            //                    on patClaimDiagnoses.icdICD9Number equals icd9Codes.icdICD9Number
            //                    where rfaReferral.RFAReferralID == _rfaReferralId && icd9Codes.ICD9Type == "1"
            //                    select new
            //                    {
            //                        patClaimDiagnoses.PatientClaimID,
            //                        patClaimDiagnoses.icdICD9Number,
            //                        rfaReferral.RFAReferralID,
            //                        icd9Codes.ICD9Description,
            //                        patClaimDiagnoses.PatientClaimDiagnosisID
            //                    }).OrderByDescending(Dgnosis => Dgnosis.PatientClaimID).Skip(skip).Take(take).ToList();
            //return new BLModel.Paged.RFADiagnosis
            //{
            //    RFADiagnosisDetails = RFADiagnosis.Select(Dgnosis => new BLModel.RFADiagnosis().InjectFrom(Dgnosis)).Cast<BLModel.RFADiagnosis>().ToList(),
            //    TotalCount = getTotoalCountofRFADiagnosisByReferralID(_rfaReferralId)
            //};
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.RFADiagnosis
            {
                RFADiagnosisDetails = _spImpl.GetRFADiagnosisByReferralID(_rfaReferralId,skip,take),
                TotalCount = _spImpl.GetRFADiagnosisByReferralIDCount(_rfaReferralId)
            };

        }
        //public int getTotoalCountofRFADiagnosisByReferralID(int _rfaReferralId)
        //{
        //    MMCDbContext _mMCDbContext = new MMCDbContext();
        //    var RFADiagnosis = (from patClaimDiagnoses in _mMCDbContext.patientClaimDiagnoses
        //                        join rfaReferral in _mMCDbContext.rfaReferrals
        //                        on patClaimDiagnoses.PatientClaimID equals rfaReferral.PatientClaimID
        //                        join icd9Codes in _mMCDbContext.iCD9Codes
        //                        on patClaimDiagnoses.icdICD9Number equals icd9Codes.icdICD9Number
        //                        where rfaReferral.RFAReferralID == _rfaReferralId && icd9Codes.ICD9Type == "1"
        //                        select 1).Count();
        //    return RFADiagnosis;
        //}
        #endregion

        #region RFAPatMedicalRecordReview
        public int addRFAPatMedicalRecordReview(DLModel.RFAPatMedicalRecordReview _rfaPatMedicalRecordReview)
        {
            return _rfaPatMedicalRecordReviewRepo.Add(_rfaPatMedicalRecordReview).RFAPatMedicalRecordReviewedID;
        }
        //public BLModel.Paged.RFAPatMedicalRecordReview getRFAPatMedicalRecordReview(int _rfaReferralId, int _skip, int _take)
        //{
        //    MMCDbContext _mMCDbContext = new MMCDbContext();
        //    var PatMedicalRecordReviews = (from rfaReferral in _mMCDbContext.rfaReferrals
        //                                   join rfaRecordspliting in _mMCDbContext.rfaRecordSplitings
        //                                   on rfaReferral.RFAReferralID equals rfaRecordspliting.RFAReferralID
        //                                   join rfaMedicalRecordReview in _mMCDbContext.rfaPatMedicalRecordReviews
        //                                   on rfaRecordspliting.RFARecSpltID equals rfaMedicalRecordReview.RFARecSpltID
        //                                   join physician in _mMCDbContext.physicians
        //                                   on rfaReferral.PhysicianID equals physician.PhysicianId
        //                                   where rfaRecordspliting.RFAReferralID == _rfaReferralId
        //                                   select new
        //                                   {
        //                                       RFAReferralID = rfaReferral.RFAReferralID,
        //                                       PatMRDocumentName = rfaRecordspliting.RFARecDocumentName.Replace(".pdf", "").Replace(".PDF", ""),
        //                                       PatientClaimID= rfaReferral.PatientClaimID.Value,
        //                                       PhysicianID =  rfaReferral.PhysicianID.Value,
        //                                       PhysicianName = physician.PhysFirstName + " " + physician.PhysLastName,
        //                                       rfaRecordspliting.RFARecSpltID,
        //                                       PatMRDocumentDate = rfaRecordspliting.RFARecDocumentDate
        //                                   }).OrderByDescending(rfaReferral => rfaReferral.PatientClaimID).Skip(_skip).Take(_take).ToList();
        //    return new BLModel.Paged.RFAPatMedicalRecordReview
        //    {
        //        RFAPatMedicalRecordReviewDetails = PatMedicalRecordReviews.Select(Dgnosis => new BLModel.RFAPatMedicalRecordReviewDetail().InjectFrom(Dgnosis)).Cast<BLModel.RFAPatMedicalRecordReviewDetail>().ToList(),
        //        TotalCount = (from rfaRecordspliting in _mMCDbContext.rfaRecordSplitings
        //                      join rfaMedicalRecordReview in _mMCDbContext.rfaPatMedicalRecordReviews
        //                      on rfaRecordspliting.RFARecSpltID equals rfaMedicalRecordReview.RFARecSpltID
        //                      where rfaRecordspliting.RFAReferralID == _rfaReferralId
        //                      select 1).Count()
        //    };
        //}
        public BLModel.Paged.RFAPatMedicalRecordReview getRFAPatMedicalRecordReviewByPatientID(int _patientID, int _referalId, int _skip, int _take)
        {
            SPImpl _spImpl = new SPImpl();

            return new BLModel.Paged.RFAPatMedicalRecordReview
            {

                RFAPatMedicalRecordReviewDetails = _spImpl.getRFAPatMedicalRecordReviewByPatientID(_patientID, _referalId, _skip, _take),
                TotalCount = _spImpl.getRFAPatMedicalRecordReviewByPatientIDCount(_patientID, _referalId)
            };
        }
        #endregion

        #region RFA- URHistory
        public BLModel.Paged.PatientHistory getPatientHistoryByPatientID(int _patientID, int _skip, int _take, string sortBy, string order)
        {
            SPImpl _SPImpl = new SPImpl();
            return new BLModel.Paged.PatientHistory
            {
                PatientHistoryDetails = _SPImpl.getPatientHistoryByPatientID(_patientID, _skip, _take, sortBy, order),
                TotalCount = _SPImpl.getPatientHistoryByPatientIDCount(_patientID)
            };

        }
        #endregion

        #region RequestHistory
        public BLModel.Paged.RequestHistory getRequestHistoryByRFARequestID(int _requestID, int _skip, int _take)
        {
            SPImpl _SPImpl = new SPImpl();
            return new BLModel.Paged.RequestHistory
            {
                RequestHistoryDetails = _SPImpl.getRequestHistoryByRFARequestID(_requestID, _skip, _take),
                TotalCount = _SPImpl.getRequestHistoryByRFARequestIDCount(_requestID)
            };

        }
        #endregion

        #region Duplicate  case
        public BLModel.Paged.RequestByReferralID GetRequestForDuplicateDecision(int _PatientClaimID, int _skip, int _take)
        {
            SPImpl _SPImpl = new SPImpl();
            return new BLModel.Paged.RequestByReferralID
            {
                RequestDetails = _SPImpl.getRequestForDuplicateDecision(_PatientClaimID, _skip, _take),
                TotalCount = _SPImpl.getRequestForDuplicateDecisionCount(_PatientClaimID)
            };
        }
        #endregion

        #region Notes
        public int addNotes(DLModel.Note _note)
        {
            return _noteRepo.Add(_note).NoteID;
        }
        public int updateNotes(DLModel.Note _note)
        {
            return _noteRepo.Update(_note, s => s.NoteID,s=>s.Notes);
        }
        #endregion

        #region DeterminationLetter
        public string getDeterminationLetterDecisionDesc(int _rfaReferralID)
        {
            SPImpl _SPImpl = new SPImpl();
            return _SPImpl.getDeterminationLetterDecisionDesc(_rfaReferralID);
        }
        #endregion

        #region RFARequestTimeExtension
        public int addRFARequestTimeExtensionInfo(DLModel.RFARequestTimeExtension _RFARequestTimeExtension)
        {
            return _rfaRequestTimeExtensionRepo.Add(_RFARequestTimeExtension).RFARequestTimeExtensionID;
        }
        public void DeleteRFARequestTimeExtensionInfo(int _rfarefferalId)
        {
            _rfaRequestTimeExtensionRepo.Delete(x => x.RFAReferralID == _rfarefferalId);
        }
        #endregion

        #region RFAReferral

        public void AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID(RFAReferralAdditionalLink _rfaReferralAdditionalLink)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID(_rfaReferralAdditionalLink);
        }
        public void AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID(RFAReferralAdditionalLink _rfaReferralAdditionalLink)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID(_rfaReferralAdditionalLink);
        }

        #endregion

        #region RFARequestBodyPart
        public int addRFARequestBodyPart(DLModel.RFARequestBodyPart _rfaRequestBodyPart)
        {
            return _rfaRequestBodyPartRepo.Add(_rfaRequestBodyPart).RFARequestBodyPartID;
        }
        public int updateRFARequestBodyPart(DLModel.RFARequestBodyPart _rfaRequestBodyPart)
        {
            return _rfaRequestBodyPartRepo.Update(_rfaRequestBodyPart);
        }

        public IEnumerable<RFARequestBodyPart> getRFARequestBodyPartByRequestID(int _requestID)
        {
            return _rfaRequestBodyPartRepo.GetAll(rs => rs.RFARequestID == _requestID).ToList();
        }
        public void deleteRFARequestBodyPartByReqID(int _rfaRequestID)
        {
            _rfaRequestBodyPartRepo.Delete(rk => rk.RFARequestID == _rfaRequestID);          
        }
        #endregion
    }


}