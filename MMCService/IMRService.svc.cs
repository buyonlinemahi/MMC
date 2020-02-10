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

    public class IMRService : IIMRService
    {
        private readonly IIMRRepository _iMRRepository;

        public IMRService(IIMRRepository iMRRepository)
        {
            _iMRRepository = iMRRepository;
        }

        public DTO.Paged.PagedRequestIMRRecord getRequestIMRRecordAll(int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedRequestIMRRecord>(_iMRRepository.getRequestIMRRecordAll(_skip, _take));
        }
        public DTO.Paged.PagedRequestIMRRecord getRequestIMRRecordByPatientClaim(string _searchtext, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedRequestIMRRecord>(_iMRRepository.getRequestIMRRecordByPatientClaim(_searchtext, _skip, _take));
        }

        public void SaveRequestIMRRecord(string[] _arrRequestID, int UserID)
        {
            _iMRRepository.SaveRequestIMRRecord(_arrRequestID, UserID);
        }
        public IEnumerable<DTO.RFAReferralFile> getMedicalAndLegalDocsByReferralID(int ReferralID)
        {
            return  Mapper.Map<IEnumerable<DTO.RFAReferralFile>>(_iMRRepository.getMedicalAndLegalDocsByReferralID(ReferralID));
        }

        public int UpdateRFAIMRReferenceNumberByReferralID(DTO.RFAReferral _rFAReferral)
        {
            return _iMRRepository.UpdateRFAIMRReferenceNumberByReferralID(Mapper.Map<DTO.RFAReferral, MMC.Core.Data.Model.RFAReferral>(_rFAReferral));
        }
        public int UpdateRFAIMRHistoryRationaleByReferralID(DTO.RFAReferral _rFAReferral)
        {
            return _iMRRepository.UpdateRFAIMRHistoryRationaleByReferralID(Mapper.Map<DTO.RFAReferral, MMC.Core.Data.Model.RFAReferral>(_rFAReferral));
        }

        #region IMRRFAReferral
        public int addIMRRFAReferral(IMRRFAReferral _IMRRFAReferral)
        {
            return _iMRRepository.addIMRRFAReferral(Mapper.Map<DTO.IMRRFAReferral, MMC.Core.Data.Model.IMRRFAReferral>(_IMRRFAReferral));
        }

        public int updateIMRRFAReferral(IMRRFAReferral _IMRRFAReferral)
        {
            return _iMRRepository.updateIMRRFAReferral(Mapper.Map<DTO.IMRRFAReferral, MMC.Core.Data.Model.IMRRFAReferral>(_IMRRFAReferral));
        }

        public void deleteIMRRFAReferral(int _IMRRFAReferralid)
        {
            _iMRRepository.deleteIMRRFAReferral(_IMRRFAReferralid);
        }

        public DTO.IMRRFAReferral getIMRRFAReferralByRefID(int _referralID)
        {
            return Mapper.Map<DTO.IMRRFAReferral>(_iMRRepository.getIMRRFAReferralByRefID(_referralID));
        }

        public IEnumerable<DTO.Physician> getPhysicianByReferralID(int ReferralID)
        {
            return Mapper.Map<IEnumerable<DTO.Physician>>(_iMRRepository.getPhysicianByReferralID(ReferralID));
        }
        #endregion

        public IEnumerable<RFAReferralFile> GetIMRLetters(int RFAReferralID)
        {
            return Mapper.Map<IEnumerable<DTO.RFAReferralFile>>(_iMRRepository.getIMRLetters(RFAReferralID));
        }

        public IMRDecisionReferralDetails getIMRDecisionPageDetailsByReferralID(int RFAReferralID)
        {
            return Mapper.Map<DTO.IMRDecisionReferralDetails>(_iMRRepository.getIMRDecisionPageDetailsByReferralID(RFAReferralID));
        }

        public IEnumerable<IMRDecisionRequestDetails> getIMRDecisionPageRequestDetailsByReferralID(int RFAReferralID)
        {
            return Mapper.Map<IEnumerable<DTO.IMRDecisionRequestDetails>>(_iMRRepository.getIMRDecisionPageRequestDetailsByReferralID(RFAReferralID));
        }

        public int updateIMRRFAReferralByValues(IMRRFAReferral _IMRRFAReferral)
        {
            return _iMRRepository.updateIMRRFAReferralByValues(Mapper.Map<DTO.IMRRFAReferral, MMC.Core.Data.Model.IMRRFAReferral>(_IMRRFAReferral));            
        }

        public int addIMRDecisionRequestDetail(IEnumerable<IMRRFARequest> _IMRRFARequest)
        {
            return _iMRRepository.addIMRDecisionRequestDetail(Mapper.Map<IEnumerable<DTO.IMRRFARequest>, IEnumerable<MMC.Core.Data.Model.IMRRFARequest>>(_IMRRFARequest));
        }

        public int updateIMRDecisionRequestDetail(IMRRFARequest _IMRRFARequest)
        {
            return _iMRRepository.updateIMRDecisionRequestDetail(Mapper.Map<DTO.IMRRFARequest, MMC.Core.Data.Model.IMRRFARequest>(_IMRRFARequest));
        }

        public IEnumerable<DTO.IMRDecision> getIMRDecisionList()
        {
            return Mapper.Map<IEnumerable<DTO.IMRDecision>>(_iMRRepository.getIMRDecisionList());
        }
    }
}
