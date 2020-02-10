
using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;


namespace MMC.Core.BL
{
    public interface IPreparationRepository
    {
        #region Preparation
        BLModel.Paged.ClinicalTriage getReferralByProcessLevel(int skip, int take, int processLevel);
        BLModel.PatientAndRequestModel getPatientAndRequestByReferralId(int _referralID);
        int getRemainingRequest(int _referralID);
        int updateProcessLevel(int _referralID, int _processLevel);
        int addRFAAdditionalInfo(RFAAdditionalInfo rfaAddiotionalInfo);
        int updateRFAAdditionalInfo(RFAAdditionalInfo rfaAddiotionalInfo);
        BLModel.Paged.RFAAdditionalInfo getAllRFAAdditionalInfo(int ReferralID,int skip,int take);
        RFAAdditionalInfo getRFAAdditionalInfoById(int id);
        int updateDecisionByRequestID(RFARequest rfaRequest);
        BLModel.PatientAndRequestModel getPatientAndRequestForNotificationByReferralId(int _referralID,int _skip,int _take);
        IEnumerable<BLModel.RequestByReferralID> getAllRequestByReferralID(int ReferralID);
        BLModel.Paged.PatientMedicalRecordByPatientID getReferralMedicalRecordByPatientID(int _patientID, int _skip, int _take);
        void AddRFITemplateRecordByRFARequestID(int _rFAReferralFileID, int _userID);
        void AddRFARequestTimeExtensionRecordByRFARequestID(int _rFAReferralFileID, int _userID);
        int getRFANewReferralIDFromRFAOldReferralID(int RFAReferralID, int DecisionID);
        int DeleteRFAReferralIDFromReferralFiles(int ReferralID, int RFAFileTypeID);
        void UpdateRFAAdditionalInfoDetailByRequestID(int OldRFAReferralID, int rFARequestID);
        #endregion

        #region BodyParts by ReferralID
        string getAcceptedBodyPartsByReferralID(int _referralID);
        string getDeniedBodyPartsByReferralID(int _referralID);
        string getDignosisByReferralID(int _referralID);
        string getDelayedBodyPartByReferralID(int _referralID);
        #endregion
    }
}


