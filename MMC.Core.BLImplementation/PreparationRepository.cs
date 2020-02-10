using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
namespace MMC.Core.BLImplementation
{
    public class PreparationRepository : IPreparationRepository
    {
        private readonly BaseRepository<RFAProcessLevels> _rfaProcessLevelRepo;
        private readonly BaseRepository<RFAAdditionalInfo> _rfaAdditionalInfoRepo;
        private readonly BaseRepository<RFAReferralFile> _rFAReferralFileRepo;
        public PreparationRepository()
        {
            _rfaProcessLevelRepo = new BaseRepository<RFAProcessLevels>();
            _rfaAdditionalInfoRepo = new BaseRepository<RFAAdditionalInfo>();
            _rFAReferralFileRepo = new BaseRepository<RFAReferralFile>();
        }
        public BLModel.Paged.ClinicalTriage getReferralByProcessLevel(int skip, int take, int processLevel)
        {
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.ClinicalTriage
            {
                ClinicalTriages = _spImpl.getReferralByProcessLevel(skip, take, processLevel).ToList(),
                TotalCount = _spImpl.GetReferralCountByProcessLevel(processLevel)
            };
        }
        public BLModel.PatientAndRequestModel getPatientAndRequestByReferralId(int _referralID)
        {
            SPImpl _spImpl = new SPImpl();
            var varRequestdetails = _spImpl.getRequestByReferralId(_referralID).ToList();
            return new BLModel.PatientAndRequestModel
            {
                Patients = _spImpl.getPatientByReferralId(_referralID),
                RequestDetail = varRequestdetails,
                RemainingDecision = varRequestdetails.Count(s => (s.RFAStatus != "SendToClinical" && s.RFAStatus != "Peer Review") && s.DecisionID == 0),
                TimeExtensionPNCount = _rFAReferralFileRepo.GetAll(rk => rk.RFAReferralID == _referralID && rk.RFAFileTypeID == Global.GlobalConst.FileTypes.TimeExtensionPN).Count()
            };
        }
        public int getRemainingRequest(int _referralID)
        {
            SPImpl _spImpl = new SPImpl();
            var varRequestdetails = _spImpl.getRequestByReferralId(_referralID).ToList();
            return varRequestdetails.Count(s => (s.RFAStatus == "SendToClinical" || s.RFAStatus == null) && s.DecisionID == 0);
        }
        public int getRFANewReferralIDFromRFAOldReferralID(int RFAReferralID, int DecisionID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getRFANewReferralIDFromRFAOldReferralID(RFAReferralID, DecisionID);
        }

        public int DeleteRFAReferralIDFromReferralFiles(int ReferralID, int RFAFileTypeID)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.DeleteRFAReferralIDFromReferralFiles(ReferralID, RFAFileTypeID);
            return 1;
        }

        public BLModel.PatientAndRequestModel getPatientAndRequestForNotificationByReferralId(int _referralID, int _skip, int _take)
        {
            SPImpl _spImpl = new SPImpl();
            var varRequestdetails = _spImpl.getRequestForNotificationByReferralId(_referralID).Skip(_skip).Take(_take).ToList();
            return new BLModel.PatientAndRequestModel
            {
                Patients = _spImpl.getPatientByReferralId(_referralID),
                RequestDetail = varRequestdetails,
                RequestCount = _spImpl.getRequestForNotificationByReferralId(_referralID).Count()
            };
        }
        public int updateDecisionByRequestID(RFARequest rfaRequest)
        {
            MMCDbContext mmcDBcontext = new MMCDbContext();
            try
            {
                var qry = from p in mmcDBcontext.rfaRequests where p.RFARequestID == rfaRequest.RFARequestID select p;
                foreach (var q in qry)
                {
                    q.DecisionID = rfaRequest.DecisionID;
                    q.RFAStatus = rfaRequest.RFAStatus;
                    q.RFANotes = rfaRequest.RFANotes;
                    //   q.RFARequestedTreatment = rfaRequest.RFARequestedTreatment;
                    if (rfaRequest.DecisionID != 1)
                    {
                        q.RFAClinicalReasonforDecision = rfaRequest.RFAClinicalReasonforDecision;
                        q.RFAGuidelinesUtilized = rfaRequest.RFAGuidelinesUtilized;
                        q.RFARelevantPortionUtilized = rfaRequest.RFARelevantPortionUtilized;
                        if (rfaRequest.RFAClinicalReasonforDecision != null)
                        {
                            SPImpl _spImpl = new SPImpl();
                            _spImpl.updateRFAReferralRequestRFAClinicalReasonforDecisionByID(q.RFAReferralID.Value, q.RFAClinicalReasonforDecision);
                        }
                    }
                    else
                    {
                        q.RFAClinicalReasonforDecision = null;
                        q.RFAGuidelinesUtilized = null;
                        q.RFARelevantPortionUtilized = null;
                    }
                    q.DecisionDate = rfaRequest.DecisionDate;
                }
                mmcDBcontext.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int updateProcessLevel(int _referralID, int _processLevel)
        {
            RFAProcessLevels rfaProcessLevel = new RFAProcessLevels();
            rfaProcessLevel.RFAReferralID = _referralID;
            rfaProcessLevel.ProcessLevel = _processLevel;
            return _rfaProcessLevelRepo.Add(rfaProcessLevel).RFAProcessLevelID;
        }
        public int addRFAAdditionalInfo(MMC.Core.Data.Model.RFAAdditionalInfo rfaAddiotionalInfo)
        {
            return _rfaAdditionalInfoRepo.Add(rfaAddiotionalInfo).RFAAdditionalInfoID;
        }
        public int updateRFAAdditionalInfo(MMC.Core.Data.Model.RFAAdditionalInfo rfaAddiotionalInfo)
        {
            return _rfaAdditionalInfoRepo.Update(rfaAddiotionalInfo);
        }

        public BLModel.Paged.RFAAdditionalInfo getAllRFAAdditionalInfo(int ReferralID, int skip, int take)
        {
            return new BLModel.Paged.RFAAdditionalInfo
            {
                RFAAdditionalInfoDetails = (_rfaAdditionalInfoRepo.GetAll(p => p.RFAReferralID == ReferralID).OrderByDescending(p => p.RFAAdditionalInfoID)).Skip(skip).Take(take),
                TotalCount = _rfaAdditionalInfoRepo.GetAll(p => p.RFAReferralID == ReferralID).Count()
            };
        }
        public RFAAdditionalInfo getRFAAdditionalInfoById(int id)
        {
            return _rfaAdditionalInfoRepo.GetById(id);
        }

        public void UpdateRFAAdditionalInfoDetailByRequestID(int OldRFAReferralID, int rFARequestID)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.UpdateRFAAdditionalInfoDetailByRequestID(OldRFAReferralID, rFARequestID);
        }


        public BLModel.Paged.PatientMedicalRecordByPatientID getReferralMedicalRecordByPatientID(int _patientID, int _skip, int _take)
        {
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.PatientMedicalRecordByPatientID
            {
                PatientMedicalRecordByPatientIDDetails = _spImpl.getReferralMedicalRecordByPatientID(_patientID, _skip, _take),
                TotalCount = _spImpl.GetReferralMedicalRecordByPatientIDCount(_patientID)
            };
        }

        public void AddRFITemplateRecordByRFARequestID(int _rFAReferralFileID, int _userID)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.AddRFITemplateRecordByRFARequestID(_rFAReferralFileID, _userID);
        }

        public void AddRFARequestTimeExtensionRecordByRFARequestID(int _rFAReferralFileID, int _userID)
        {
            SPImpl _spImpl = new SPImpl();
            _spImpl.AddRFARequestTimeExtensionRecordByRFARequestID(_rFAReferralFileID, _userID);
        }

        #region BodyParts by ReferralID
        public string getAcceptedBodyPartsByReferralID(int _referralID)
        {
            SPImpl _smp = new SPImpl();
            return _smp.getAcceptedBodyPartsByReferralID(_referralID);
        }

        public string getDeniedBodyPartsByReferralID(int _referralID)
        {
            SPImpl _smp = new SPImpl();
            return _smp.getDeniedBodyPartsByReferralID(_referralID);
        }
        public string getDelayedBodyPartByReferralID(int _referralID)
        {
            SPImpl _smp = new SPImpl();
            return _smp.getDelayedBodyPartByReferralID(_referralID);
        }
        public string getDignosisByReferralID(int _referralID)
        {
            SPImpl _smp = new SPImpl();
            return _smp.getDignosisByReferralID(_referralID);
        }
        public IEnumerable<BLModel.RequestByReferralID> getAllRequestByReferralID(int ReferralID)
        {
            SPImpl _smp = new SPImpl();
            return _smp.getRequestByReferralId(ReferralID);
        }
        #endregion

    }
}