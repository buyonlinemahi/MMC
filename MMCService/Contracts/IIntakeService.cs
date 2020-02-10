using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;
namespace MMCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIntake" in both code and config file together.
    [ServiceContract]
    public interface IIntakeService
    {
        [OperationContract]
        int addRFAReferral(RFAReferral _rfaReferral);
        [OperationContract]
        int updateClaimInReferral(int _claimID, int _rfaReferralID);
        [OperationContract]
        int updateRFAInReferral(RFAReferral _rfaReferral);
        [OperationContract]
        int updatePhysicianInReferral(int _physicianID, int _rfaReferralID);
        [OperationContract]
        RFAReferral getReferralByID(int Id);
        [OperationContract]
        DTO.Paged.PagedIncompleteReferrals getAllIncompleteReferrals(int _skip, int _take);
        [OperationContract]
        int addRFASplittedReferralHistory(DTO.RFASplittedReferralHistory _rfaSplittedReferralHistory);
        [OperationContract]
        int addRFAMergedReferralHistory(DTO.RFAMergedReferralHistory _rfaMergedReferralHistory);
        [OperationContract]
        int UploadSignature(RFAReferral _rfaReferral);
        [OperationContract]
        int SaveSignatureDescription(RFAReferral _rfaReferral);

        #region RFARferralFiles

        [OperationContract]
        RFAReferralFile addReferralFileIntake(string filename, int userid);
        [OperationContract]
        int addReferralFile(RFAReferralFile _rfaReferralFile);
        [OperationContract]
        int updateReferralFile(RFAReferralFile _rfaReferralFile);
        [OperationContract]
        RFAReferralFile getReferralFileByID(int _rfaReferralFileID);
        [OperationContract]
        RFAReferralFile getReferralFileByIntake(int _rfaReferralID);
        [OperationContract]
        IEnumerable<RFAReferralFile> getReferralFileByRFAReferralID(int _rfaReferralID);
        [OperationContract]
        IEnumerable<RFAReferralFile> getReferralFileByRFAReferralIDandFileType(int _rfaReferralID);
        [OperationContract]
        IEnumerable<DTO.RFAReferralFile> getReferralFileByRFAReferralIDAndFileTypeID(int _rfaReferralID, int _fileTypeID);
        [OperationContract]
        string GetSignaturePathAndDescriptionByReferralID(int RFAReferralID);
        [OperationContract]
        void AddRFIReportAdditionalRecordByRFAReferralFileID(int _rFAReferralFileID,int UserID);
        [OperationContract]
        void AddRFIReportAdditionalRecordByRFARequestID(int _rFAReferralFileID, int _rFARequestID, int UserID);
        [OperationContract]
        IEnumerable<DTO.PreviousReferralFromCurrentReferral> getPreviousReferralIDFromCurrentReferralInDuplicate(int referralID);
        [OperationContract]
        IEnumerable<DTO.RFAReferralFile> getRFAReferralFilesAccToReferralIDInPreparationCase(int _rfaReferralID);
        #endregion
        #region RFARequests
        [OperationContract]
        int addRFARequest(RFARequest _rfaRequest);
        [OperationContract]
        int AssignNewReferralToRequest(RFARequest _rfaRequest);
        [OperationContract]
        int updateRFARequestAndDecision(RFARequest _rfaRequest);
        [OperationContract]
        int updateRFARequest(RFARequest _rfaRequest);
        [OperationContract]
        void deleteRFARequest(int _rfaRequestID);
        [OperationContract]
        void deleteRFADelatedRequest(int _rfaRequestID);
        [OperationContract]
        RFARequest getRFARequestByID(int _rfaRequestID);
        [OperationContract]
        IEnumerable<RFARequest> getRFARequestByReferralID(int _rfaReferralId);
        [OperationContract]
        int addRFARequestAdditionalInfo(DTO.RFARequestAdditionalInfo _rfaRequestAdditionalInfo);
        [OperationContract]
        int updateRFARequestAdditionalInfo(DTO.RFARequestAdditionalInfo _rfaRequestAdditionalInfo);
        [OperationContract]
        void addRFAReferralAdditionalInfo(DTO.RFAReferralAdditionalInfo _rfaReferralAdditionalInfo);
        [OperationContract]
        DTO.RFARequestAdditionalInfoDetail GetRFARequestAdditionalInfo(int _rfaRequestID);
        [OperationContract]
        int SaveRFARequestAdditionalInfo(DTO.RFARequestAdditionalInfo _rfaRequestAdditionalInfo);
        [OperationContract]
        void UpdateRFAReqCertificationNumberByID(int RFAReferralID);
        [OperationContract]
        void UpdateRFAReferralRequestDecisionByID(int RFAReferralID);
        [OperationContract]
        void UpdateRFARequestDecisionAndRFAStatusByReferralID(int RFAReferralID, string DecisionID, string RFAStatus);
        [OperationContract]
        System.DateTime UpdateRFARequestLatestDueDateByRefferalID(int _RFAReferralID, int _AddDays, int _UserID);
        #endregion
        #region RFAReferralCPTCodes
        [OperationContract]
        int addRFAReferralCPTCodes(RFARequestCPTCode _rfaReferralCPTCode);
        [OperationContract]
        int updateRFAReferralCPTCodes(RFARequestCPTCode _rfaReferralCPTCode);
        [OperationContract]
        void deleteRFAReferralCPTCodes(int _rfaCPTCodeID);
        [OperationContract]
        RFARequestCPTCode getRFAReferralCPTCodesByID(int _rfaCPTCodeID);
        //[OperationContract]
        //IEnumerable<RFAReferralCPTCode> getRFAReferralCPTCodesByReferralID(int _rfaReferralId);
        #endregion
        #region RFARecordSplitting
        [OperationContract]
        int addRFARecordSplitting(RFARecordSplitting _rfaRecordSplitting);
        [OperationContract]
        int udateRFARecordSplitting(RFARecordSplitting _rfaRecordSplitting);
        [OperationContract]
        int udateRFARecordSplittingClaimIDByReferralID(int rfaReferralID, int claimID);
        [OperationContract]
        int udateRFARecordSplittingRecordForMedicalRec(RFARecordSplitting _rfaRecordSplitting);
        [OperationContract]
        void deleteRFARecordSplitting(int rfaRecSpltID);
        [OperationContract]
        RFARecordSplitting getRFARecordSplittingByID(int rfaRecSpltID);
        [OperationContract]
        IEnumerable<RFARecordSplitting> getRFARecordSplittingByReferralID(int _rfaReferralId);
        #endregion
        #region Referral Intake Patient Claim
        [OperationContract]
        int udateReferralIntakePatientClaimByID(int _patientClaimID, int _rfaReferralId);
        [OperationContract]
        PatientDetailsByReferralID getPatientDetailsByReferralID(int _rfaReferralId);
        #endregion
        #region  RFADiagnosis
        [OperationContract]
        DTO.Paged.PagedRFADiagnosis getRFADiagnosisByReferralID(int _rfaReferralId, int skip, int take);
        #endregion
        #region RFAPatMedicalRecordReview
        [OperationContract]
        int addRFAPatMedicalRecordReview(MMC.Core.Data.Model.RFAPatMedicalRecordReview _rfaPatMedicalRecordReview);
        //[OperationContract]
        //DTO.Paged.PagedRFAPatMedicalRecordReview getRFAPatMedicalRecordReview(int _rfaReferralId, int _skip, int _take);
        [OperationContract]
        DTO.Paged.PagedRFAPatMedicalRecordReview getRFAPatMedicalRecordReviewByPatientID(int _PatientID, int _ReferralId, int _skip, int _take);
        #endregion
        #region RFA- URHistory
        [OperationContract]
        DTO.Paged.PagedPatientHistory getPatientHistoryByPatientID(int _patientID, int _skip, int _take, string sortBy, string order);
        #endregion
        #region  RequestHistory
        [OperationContract]
        DTO.Paged.PagedRequestHistory getRequestHistoryByRFARequestID(int _requestID, int _skip, int _take);
        #endregion
        #region RequestModify
        [OperationContract]
        int saveRFARequestModify(DTO.RFARequestModify _rfaRequestModify);
        [OperationContract]
        DTO.RFARequestModify getRFARequestModify(int _rfaRequestID);
        #endregion
        #region Duplicate  case
        [OperationContract]
        DTO.Paged.PagedRequestByReferralID GetRequestForDuplicateDecision(int _PatientClaimID, int _skip, int _take);
        #endregion

        #region Note
        [OperationContract]
        int addNotes(DTO.Note _note);
        [OperationContract]
        int updateNotes(DTO.Note _note);
        #endregion
        #region RFARequestBilling
        [OperationContract]
        int addRFARequestBilling(IEnumerable<DTO.RFARequestBilling> _rfaRequestBilling);
        #endregion
        #region DeterminationLetter
        [OperationContract]
        string getDeterminationLetterDecisionDesc(int _rfaReferralID);
        #endregion
        #region RFARequestTimeExtension
        [OperationContract]
        int addRFARequestTimeExtensionInfo(DTO.RFARequestTimeExtension _RFARequestTimeExtension);
        [OperationContract]
        void DeleteRFARequestTimeExtensionInfo(int _rfarefferalId);
        #endregion

        #region RFAReferralAdditionalLink
        [OperationContract]
        void AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID(RFAReferralAdditionalLink _rfaReferralAdditionalLink);
        [OperationContract]
        void AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID(RFAReferralAdditionalLink _rfaReferralAdditionalLink);
        #endregion

        #region RFARequestBodyPart
        [OperationContract]
        int addRFARequestBodyPart(RFARequestBodyPart _rFARequestBodyPart);
        [OperationContract]
        int updateRFARequestBodyPart(RFARequestBodyPart _rFARequestBodyPart);
        [OperationContract]
        IEnumerable<RFARequestBodyPart> getRFARequestBodyPartByRequestID(int _requestID);
        [OperationContract]
        void deleteRFARequestBodyPartByReqID(int _rfaRequestID);
        #endregion

    }
}