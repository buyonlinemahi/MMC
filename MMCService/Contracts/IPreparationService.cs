using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MMCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPreparationService" in both code and config file together.
    [ServiceContract]
    public interface IPreparationService
    {
        #region ClinicalTriage
        [OperationContract]
        DTO.Paged.PagedClinicalTriage getReferralByProcessLevel(int skip, int take, int processLevel);
        #endregion

        #region ReferralProcess
        [OperationContract]
        DTO.PatientAndRequestModel getPatientAndRequestByReferralId(int _referralID);
        [OperationContract]
        IEnumerable<DTO.RequestByReferralID> getAllRequestByReferralID(int _referralID);
        [OperationContract]
        DTO.PatientAndRequestModel getPatientAndRequestForNotificationByReferralId(int _referralID, int _skip, int _take);
        [OperationContract]
        int updateProcessLevel(int _referralID, int _processLevel);
        [OperationContract]
        int getRFANewReferralIDFromRFAOldReferralID(int RFAReferralID, int DecisionID);
        [OperationContract]
        int DeleteRFAReferralIDFromReferralFiles(int ReferralID, int RFAFileTypeID);
        #endregion

        #region AdditionalInfo
        [OperationContract]
        int addRFAAdditionalInfo(DTO.RFAAdditionalInfo rfaAddiotionalInfo);
        [OperationContract]
        int updateRFAAdditionalInfo(DTO.RFAAdditionalInfo rfaAddiotionalInfo);
        [OperationContract]
        DTO.Paged.PagedRFAAdditionalInfo getAllRFAAdditionalInfo(int ReferralID, int skip, int take);
        [OperationContract]
        DTO.RFAAdditionalInfo getRFAAdditionalInfoById(int id);
        [OperationContract]
        int getRemainingRequest(int _referralID);
        [OperationContract]
        void UpdateRFAAdditionalInfoDetailByRequestID(int oldRFAReferralID, int rFARequestID);
        #endregion

        #region RFADecision
        [OperationContract]
        int updateDecisionByRequestID(DTO.RFARequest rfaRequest);
        //[OperationContract]
        //int updateNotesByReferralID(DTO.RFAReferral rfaReferral);   
        [OperationContract]
        void AddRFITemplateRecordByRFARequestID(int _rFAReferralFileID, int _userID);
        [OperationContract]
        void AddRFARequestTimeExtensionRecordByRFARequestID(int _rFAReferralFileID, int _userID);
        #endregion
        #region BodyParts
        [OperationContract]
        string getAcceptedBodyPartsByReferralID(int _referralID);
        [OperationContract]
        string getDeniedBodyPartsByReferralID(int _referralID);
        [OperationContract]
        string getDignosisByReferralID(int _referralID);
        [OperationContract]
        string getDelayedBodyPartByReferralID(int _referralID);
        #endregion

        #region ReferralMedicalRecord
        [OperationContract]
        DTO.Paged.PagedPatientMedicalRecordByPatientID getReferralMedicalRecordByPatientID(int _patientID, int _skip, int _take);
        #endregion
    }
}
