using MMC.Core.BL.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;
using DLModel = MMC.Core.Data.Model;

namespace MMC.Core.BL
{
    public interface IIMRRepository
    {
        #region IMR Details

        BLModel.Paged.RequestIMRRecord getRequestIMRRecordAll(int _skip, int _take);
        BLModel.Paged.RequestIMRRecord getRequestIMRRecordByPatientClaim(string _searchText, int _skip, int _take);
        void SaveRequestIMRRecord(string[] _arrRequestID, int UserID);
        IEnumerable<BLModel.RFAReferralFile> getMedicalAndLegalDocsByReferralID(int ReferralID);
        
        int UpdateRFAIMRReferenceNumberByReferralID(DLModel.RFAReferral _rFAReferral);
        int UpdateRFAIMRHistoryRationaleByReferralID(DLModel.RFAReferral _rFAReferral);
        
        #endregion
        #region IMR Details

        int addIMRRFAReferral(DLModel.IMRRFAReferral _IMRRFAReferral);
        int updateIMRRFAReferral(DLModel.IMRRFAReferral _IMRRFAReferral);
        void deleteIMRRFAReferral(int _IMRRFAReferralId);     
        DLModel.IMRRFAReferral getIMRRFAReferralByRefID(int _referralID);
        IEnumerable<DLModel.Physician> getPhysicianByReferralID(int _referralID);
        #endregion

        IEnumerable<RFAReferralFile> getIMRLetters(int RFAReferralID);
        
        #region IMRDecisionPage
        IMRDecisionReferralDetails getIMRDecisionPageDetailsByReferralID(int RFAReferralID);
        IEnumerable<IMRDecisionRequestDetails> getIMRDecisionPageRequestDetailsByReferralID(int RFAReferralID);
        int updateIMRRFAReferralByValues(DLModel.IMRRFAReferral _IMRRFAReferral);
        int addIMRDecisionRequestDetail(IEnumerable<DLModel.IMRRFARequest> _IMRRFARequest);
        int updateIMRDecisionRequestDetail(DLModel.IMRRFARequest _IMRRFARequest);
        IEnumerable<DLModel.IMRDecision> getIMRDecisionList();
        #endregion 
    }
}
