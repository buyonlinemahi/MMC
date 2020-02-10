using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIMRService" in both code and config file together.
    [ServiceContract]
    public interface IIMRService
    {
        [OperationContract]
        DTO.Paged.PagedRequestIMRRecord getRequestIMRRecordAll(int _skip, int _take);
        [OperationContract]
        DTO.Paged.PagedRequestIMRRecord getRequestIMRRecordByPatientClaim(string _searchText, int _skip, int _take);
        [OperationContract]
        void SaveRequestIMRRecord(string[] _arrRequestID, int UserID);
        [OperationContract]
        IEnumerable<DTO.RFAReferralFile> getMedicalAndLegalDocsByReferralID(int ReferralID);
        [OperationContract]
        int UpdateRFAIMRReferenceNumberByReferralID(DTO.RFAReferral _rFAReferral);
        [OperationContract]
        int UpdateRFAIMRHistoryRationaleByReferralID(DTO.RFAReferral _rFAReferral);

        #region IMR Details
         [OperationContract]
        int addIMRRFAReferral(DTO.IMRRFAReferral _IMRRFAReferral);
         [OperationContract]
         int updateIMRRFAReferral(DTO.IMRRFAReferral _IMRRFAReferral);
         [OperationContract]
        void deleteIMRRFAReferral(int _IMRRFAReferralId);
         [OperationContract]
         DTO.IMRRFAReferral getIMRRFAReferralByRefID(int _referralID);
         [OperationContract]
         IEnumerable<Physician> getPhysicianByReferralID(int _referralID);
        #endregion

         [OperationContract]
         IEnumerable<RFAReferralFile> GetIMRLetters(int RFAReferralID);

         [OperationContract]
         IMRDecisionReferralDetails getIMRDecisionPageDetailsByReferralID(int RFAReferralID);

         [OperationContract]
         IEnumerable<IMRDecisionRequestDetails> getIMRDecisionPageRequestDetailsByReferralID(int RFAReferralID);

         [OperationContract]
         int updateIMRRFAReferralByValues(IMRRFAReferral _IMRRFAReferral);

         [OperationContract]
         int addIMRDecisionRequestDetail(IEnumerable<IMRRFARequest> _IMRRFARequest);

         [OperationContract]
         int updateIMRDecisionRequestDetail(IMRRFARequest _IMRRFARequest);

         [OperationContract]
         IEnumerable<DTO.IMRDecision> getIMRDecisionList();
    }
}
