using MMC.Core.BL;
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
    public class IMRRepository : IIMRRepository
    {
        private readonly BaseRepository<DLModel.RFARequest> _rfaRequestRepo;
        private readonly BaseRepository<DLModel.RFAReferral> _rFAReferralRepo;
        private readonly BaseRepository<DLModel.IMRRFAReferral> _iMRRFAReferral;
        private readonly BaseRepository<DLModel.IMRRFARequest> _iMRRFARequest;
        private readonly BaseRepository<DLModel.IMRDecision> _iMRDecision;
        public IMRRepository()
        {
            _rfaRequestRepo = new BaseRepository<DLModel.RFARequest>();
            _rFAReferralRepo = new BaseRepository<DLModel.RFAReferral>();
            _iMRRFAReferral = new BaseRepository<DLModel.IMRRFAReferral>();
            _iMRRFARequest = new BaseRepository<DLModel.IMRRFARequest>();
            _iMRDecision = new BaseRepository<DLModel.IMRDecision>();
        }


        #region RequestIMRRecord

        public BLModel.Paged.RequestIMRRecord getRequestIMRRecordAll(int _skip, int _take)
        {

            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.RequestIMRRecord
            {
                RequestIMRRecordDetails = _spImpl.getIMRReferralByProcessLevel(_skip, _take, (GlobalConst.ProcessLevel.IMRResponse + "," + GlobalConst.ProcessLevel.IMRDesicion).ToString()).ToList(),
                TotalCount = _spImpl.getIMRReferralCountByProcessLevel((GlobalConst.ProcessLevel.IMRResponse + "," + GlobalConst.ProcessLevel.IMRDesicion).ToString())
                //_spImpl.getIMRReferralCountByProcessLevel(GlobalConst.ProcessLevel.IMRResponse)
            };

            /*MMCDbContext _MMCDbContext = new MMCDbContext();
            var _getIMRAllRecord = (from rfa in _MMCDbContext.rfaReferrals
                                    join req in _MMCDbContext.rfaRequests
                                    on rfa.RFAReferralID equals req.RFAReferralID
                                    join patclm in _MMCDbContext.patientClaims
                                    on rfa.PatientClaimID equals patclm.PatientClaimID
                                    join pat in _MMCDbContext.patients
                                    on patclm.PatientID equals pat.PatientID
                                    join rfapl in _MMCDbContext.rfaProcessLevels
                                    on rfa.RFAReferralID equals rfapl.RFAReferralID
                                    where ( rfapl.ProcessLevel == GlobalConst.ProcessLevel.IMR
                                     && !(from rfapc2 in _MMCDbContext.rfaProcessLevels
                                          where rfapc2.ProcessLevel > GlobalConst.ProcessLevel.IMR
                                          select rfapc2.RFAReferralID).Contains(rfa.RFAReferralID) 
                                         )
                                    select new { rfa.RFAReferralID, req.RFARequestID, pat.PatFirstName, pat.PatLastName, req.RFARequestedTreatment }).OrderByDescending(rk => rk.RFAReferralID).ToList();
            return new BLModel.Paged.RequestIMRRecord
            {
                RequestIMRRecordDetails = _getIMRAllRecord.Select(adj => new BLModel.RequestIMRRecord().InjectFrom(adj)).Cast<BLModel.RequestIMRRecord>().Skip(_skip).Take(_take),
                TotalCount = _getIMRAllRecord.Count()

            };*/

        }
        public BLModel.Paged.RequestIMRRecord getRequestIMRRecordByPatientClaim(string _searchText, int _skip, int _take)
        {

            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.RequestIMRRecord
            {
                RequestIMRRecordDetails = _spImpl.getIMRReferralByProcessLevelnPatientClaim(_skip, _take, GlobalConst.ProcessLevel.Billing, GlobalConst.ProcessLevel.FileStorage, _searchText).ToList(),
                TotalCount = _spImpl.getIMRReferralByProcessLevelnPatientClaimCount(GlobalConst.ProcessLevel.Billing, GlobalConst.ProcessLevel.FileStorage, _searchText)
            };

            //MMCDbContext _MMCDbContext = new MMCDbContext();
            //var _getIMRAllRecord = (from rfa in _MMCDbContext.rfaReferrals
            //                        join req in _MMCDbContext.rfaRequests
            //                        on rfa.RFAReferralID equals req.RFAReferralID
            //                        join patclm in _MMCDbContext.patientClaims
            //                        on rfa.PatientClaimID equals patclm.PatientClaimID
            //                        join pat in _MMCDbContext.patients
            //                        on patclm.PatientID equals pat.PatientID
            //                        join dec in _MMCDbContext.decisions
            //                        on req.DecisionID equals dec.DecisionID
            //                        join rfapl in _MMCDbContext.rfaProcessLevels
            //                        on rfa.RFAReferralID equals rfapl.RFAReferralID
            //                        where (patclm.PatClaimNumber.Equals(_searchText) && (req.DecisionID == 2 || req.DecisionID == 3) && (rfapl.ProcessLevel == GlobalConst.ProcessLevel.Billing || rfapl.ProcessLevel == GlobalConst.ProcessLevel.FileStorage)
            //                        && !(from rfapc2 in _MMCDbContext.rfaProcessLevels
            //                             where rfapc2.ProcessLevel == GlobalConst.ProcessLevel.IMR
            //                             select rfapc2.RFAReferralID).Contains(rfa.RFAReferralID)
            //                             )
            //                        select new { rfa.RFAReferralID, req.RFARequestID, pat.PatFirstName, pat.PatLastName, req.RFARequestedTreatment, dec.DecisionDesc, req.RFAStatus, pat.PatientID }).OrderBy(rk => rk.RFAReferralID).ToList();
            //return new BLModel.Paged.RequestIMRRecord
            //{
            //    RequestIMRRecordDetails = _getIMRAllRecord.Select(rk => new BLModel.RequestIMRRecord().InjectFrom(rk)).Cast<BLModel.RequestIMRRecord>().Skip(_skip).Take(_take),
            //    TotalCount = _getIMRAllRecord.Count()

            //};
        }
        public void SaveRequestIMRRecord(string[] _arrRequestID, int UserID)
        {

            int refID = _rfaRequestRepo.GetById(int.Parse(_arrRequestID[0])).RFAReferralID.Value;

            //MMCDbContext _MMCDbContext = new MMCDbContext();
            //var _checkTotalReferralCnt = (from rfa in _MMCDbContext.rfaReferrals
            //                              join req in _MMCDbContext.rfaRequests
            //                              on rfa.RFAReferralID equals req.RFAReferralID
            //                              join rfapl in _MMCDbContext.rfaProcessLevels
            //                              on rfa.RFAReferralID equals rfapl.RFAReferralID
            //                              where (rfa.RFAReferralID.Equals(refID) && (req.DecisionID == 2 || req.DecisionID == 3) && (rfapl.ProcessLevel == GlobalConst.ProcessLevel.Billing || rfapl.ProcessLevel == GlobalConst.ProcessLevel.FileStorage))
            //                              select new { req.RFARequestID }).Count();


            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _checkTotalReferralCnt = (from rfa in _MMCDbContext.rfaReferrals
                                          join req in _MMCDbContext.rfaRequests
                                          on rfa.RFAReferralID equals req.RFAReferralID
                                          where (rfa.RFAReferralID.Equals(refID) && (req.DecisionID == 2 || req.DecisionID == 3 || req.DecisionID == 8 || req.DecisionID == 9 || req.DecisionID == 10)) // && (rfapl.ProcessLevel == GlobalConst.ProcessLevel.Billing || rfapl.ProcessLevel == GlobalConst.ProcessLevel.FileStorage))
                                          select new { req.RFARequestID }).Count();



            int Flag = 0;

            string RFARequestIDs = "";
            if (_arrRequestID.Count() == _checkTotalReferralCnt)
                Flag = 1;

            for (int i = 0; i < _arrRequestID.Count(); i++)
            {
                RFARequestIDs += _arrRequestID[i].ToString() + "#";
            }

            SPImpl _spImpl = new SPImpl();
            _spImpl.CreateRFARefferalByRFARefferalID(refID, RFARequestIDs.Substring(0, RFARequestIDs.Length - 1), Flag);
        }
        public IEnumerable<BLModel.RFAReferralFile> getMedicalAndLegalDocsByReferralID(int ReferralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getMedicalAndLegalDocsByReferralID(ReferralID);
        }
        public int UpdateRFAIMRReferenceNumberByReferralID(DLModel.RFAReferral _rFAReferral)
        {
            if (_rFAReferralRepo.GetAll(rk => rk.RFAReferralID != _rFAReferral.RFAReferralID && rk.RFAIMRReferenceNumber == _rFAReferral.RFAIMRReferenceNumber).Count() > 0)
                return 0;
            else
            {
                _rFAReferralRepo.Update(_rFAReferral, (rFAReferral => rFAReferral.RFAIMRReferenceNumber), (rFAReferral => rFAReferral.RFAReferralID));
                return 2;
            }
        }

        public int UpdateRFAIMRHistoryRationaleByReferralID(DLModel.RFAReferral _rFAReferral)
        {
            _rFAReferralRepo.Update(_rFAReferral, (rFAReferral => rFAReferral.RFAIMRHistoryRationale), (rFAReferral => rFAReferral.RFAReferralID));
            return 2;
        }

        #endregion


        ///IMRREFERRAL
        ///
        public int addIMRRFAReferral(IMRRFAReferral _IMRRFAReferral)
        {
            if (_iMRRFAReferral.GetAll(p => p.RFAReferralID == _IMRRFAReferral.RFAReferralID).Count() > 0)
            {
                _iMRRFAReferral.Delete(p => p.RFAReferralID == _IMRRFAReferral.RFAReferralID);
            }
            return _iMRRFAReferral.Add(_IMRRFAReferral).IMRRFAReferralID;
        }

        public int updateIMRRFAReferral(IMRRFAReferral _IMRRFAReferral)
        {
            return _iMRRFAReferral.Update(_IMRRFAReferral);
        }
        
        public int updateIMRRFAReferralByValues(IMRRFAReferral _IMRRFAReferral)
        {
            IMRRFAReferral updateIMRRFAReferral = new IMRRFAReferral();
            updateIMRRFAReferral = _iMRRFAReferral.GetAll(p => p.RFAReferralID == _IMRRFAReferral.RFAReferralID).FirstOrDefault();
            if (_IMRRFAReferral.IMRResponseDate != null)
                updateIMRRFAReferral.IMRResponseDate = _IMRRFAReferral.IMRResponseDate;
            if (_IMRRFAReferral.IMRDecisionID != null)
                updateIMRRFAReferral.IMRDecisionID = _IMRRFAReferral.IMRDecisionID;
            if (_IMRRFAReferral.IMRDecisionReceivedDate != null)
                updateIMRRFAReferral.IMRDecisionReceivedDate = _IMRRFAReferral.IMRDecisionReceivedDate;

            return _iMRRFAReferral.Update(updateIMRRFAReferral);
        }

        public void deleteIMRRFAReferral(int _IMRRFAReferralid)
        {
            _iMRRFAReferral.Delete(_IMRRFAReferralid);
        }

        public IEnumerable<IMRRFAReferral> getIMRRFAReferralsAll()
        {
            return _iMRRFAReferral.GetAll();
        }

        public IMRRFAReferral getIMRRFAReferralByRefID(int _referralID)
        {
            return _iMRRFAReferral.GetAll(p => p.RFAReferralID == _referralID).FirstOrDefault();
        }

        public IEnumerable<DLModel.Physician> getPhysicianByReferralID(int _referralID)
        {
            MMCDbContext dbcontext = new MMCDbContext();
            var refclaim = (from refr in dbcontext.rfaReferrals where refr.RFAReferralID == _referralID select (refr.PatientClaimID)).SingleOrDefault();
            int refrclaim = System.Convert.ToInt32(refclaim);
            var phys = (from refr in dbcontext.rfaReferrals join phy in dbcontext.physicians on refr.PhysicianID equals phy.PhysicianId where refr.PatientClaimID == refrclaim select (phy)).ToList().Distinct();
            return phys.Select(emp => new DLModel.Physician().InjectFrom(emp)).Cast<DLModel.Physician>();

        }

        public IEnumerable<BLModel.RFAReferralFile> getIMRLetters(int RFAReferralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getIMRLetters(RFAReferralID);
        }

        public BLModel.IMRDecisionReferralDetails getIMRDecisionPageDetailsByReferralID(int RFAReferralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getIMRDecisionPageDetailsByReferralID(RFAReferralID);
        }

        public IEnumerable<BLModel.IMRDecisionRequestDetails> getIMRDecisionPageRequestDetailsByReferralID(int RFAReferralID)
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getIMRDecisionPageRequestDetailsByReferralID(RFAReferralID);
        }

        public int addIMRDecisionRequestDetail(IEnumerable<IMRRFARequest> _IMRRFARequest)
        {
            foreach(IMRRFARequest imrRFAReq in _IMRRFARequest)
            {
                if (imrRFAReq.IMRRFARequestID != 0)
                {
                    _iMRRFARequest.Update(imrRFAReq);
                }
                else
                {
                    _iMRRFARequest.Add(imrRFAReq);                    
                }
                
            }
            return 1;
        }

        public int updateIMRDecisionRequestDetail(IMRRFARequest _IMRRFARequest)
        {
            return _iMRRFARequest.Update(_IMRRFARequest);
        }

        public IEnumerable<IMRDecision> getIMRDecisionList()
        {
            return _iMRDecision.GetAll();
        }
    }
}