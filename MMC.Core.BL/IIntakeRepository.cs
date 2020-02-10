using MMC.Core.BL.Model;
using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;
using DLModel = MMC.Core.Data.Model;
namespace MMC.Core.BL
{
    public interface IIntakeRepository
    {
        int updateClaimInReferral(int _claimID, int _rfaReferralID);
        int updateRFAInReferral(RFAReferral _rfaReferral);

        #region RFARferral
        
        int addRFAReferral(RFAReferral _rfaReferral);
        int updatePhysicianInReferral(int _physicianID, int _rfaReferralID);
        RFAReferral getReferralByID(int Id);
        BLModel.Paged.IncompleteReferrals getAllIncompleteReferrals(int _skip, int _take);
        int addRFASplittedReferralHistory(RFASplittedReferralHistory _rfaSplittedReferralHistory);
        int addRFAMergedReferralHistory(RFAMergedReferralHistory _rfaMergedReferralHistory);
        int UploadSignature(RFAReferral _rfaReferral);
        int SaveSignatureDescription(RFAReferral _rfaReferral);

        #endregion

        #region RFARferralFiles

        DLModel.RFAReferralFile addReferralFileIntake(string filename,int userid);
        int addReferralFile(DLModel.RFAReferralFile _rfaReferralFile);
        int updateReferralFile(DLModel.RFAReferralFile _rfaReferralFile);
        DLModel.RFAReferralFile getReferralFileByID(int Id);
        DLModel.RFAReferralFile getReferralFileByIntake(int _rfaReferralID);
        IEnumerable<DLModel.RFAReferralFile> getReferralFileByRFAReferralID(int _rfaReferralID);
        IEnumerable<BLModel.RFAReferralFile> getReferralFileByRFAReferralIDandFileType(int _rfaReferralID);
        IEnumerable<DLModel.RFAReferralFile> getReferralFileByRFAReferralIDAndFileTypeID(int _rfaReferralID,int _fileTypeID);
        string GetSignaturePathAndDescriptionByReferralID(int RFAReferralID);
        void AddRFIReportAdditionalRecordByRFAReferralFileID(int _rFAReferralFileID, int UserID);
        void AddRFIReportAdditionalRecordByRFARequestID(int _rFAReferralFileID, int _rFARequestID, int UserID);
        //void UpdateRFIReportAdditionalRecordByRFAReferralFileID(int _rFAReferralFileID);
        IEnumerable<DLModel.PreviousReferralFromCurrentReferral> getPreviousReferralIDFromCurrentReferralInDuplicate(int referralID);
        IEnumerable<BLModel.RFAReferralFile> getRFAReferralFilesAccToReferralIDInPreparationCase(int _rfaReferralID);

        #endregion

        #region RFARequests
        int addRFARequest(BLModel.RFARequest _rfaRequest);

        int updateRFARequest(BLModel.RFARequest _rfaRequest);

        int AssignNewReferralToRequest(DLModel.RFARequest _rfaRequest);

        int updateRFARequestAndDecision(DLModel.RFARequest _rfaRequest);

        void deleteRFARequest(int _rfaRequestID);

        void deleteRFADelatedRequest(int _rfaRequestID);


        BLModel.RFARequest getRFARequestByID(int _rfaRequestID);

        IEnumerable<BLModel.RFARequest> getRFARequestByReferralID(int _rfaReferralId);
        IEnumerable<BLModel.RFARequest> getRFARequestByClaimID(int _claimID);
      
        int saveRFARequestModify(DLModel.RFARequestModify _rfaRequestModify);

        DLModel.RFARequestModify getRFARequestModify(int _rfaRequestID);

        int addRFARequestAdditionalInfo(DLModel.RFARequestAdditionalInfo _rfaRequestAdditionalInfo);

        int updateRFARequestAdditionalInfo(DLModel.RFARequestAdditionalInfo _rfaRequestAdditionalInfo);
        int SaveRFARequestAdditionalInfo(DLModel.RFARequestAdditionalInfo _rfaRequestAdditionalInfo);

        BLModel.RFARequestAdditionalInfoDetail GetRFARequestAdditionalInfo(int _rfaRequestID);
        void UpdateRFAReqCertificationNumberByID(int RFAReferralID);
        void UpdateRFAReferralRequestDecisionByID(int RFAReferralID);
        void UpdateRFARequestDecisionAndRFAStatusByReferralID(int RFAReferralID, string DecisionID, string RFAStatus);

        void addRFAReferralAdditionalInfo(DLModel.RFAReferralAdditionalInfo _rfaReferralAdditionalInfo);
        System.DateTime UpdateRFARequestLatestDueDateByRefferalID(int _RFAReferralID, int _AddDays, int _UserID);
        #endregion

        #region RFARequestBilling
        int addRFARequestBilling(IEnumerable<DLModel.RFARequestBilling> _rfaRequestBilling);        
        #endregion

        #region RFAReferralCPTCodes
        int addRFAReferralCPTCodes(RFARequestCPTCode _rfaReferralCPTCode);

        int updateRFAReferralCPTCodes(RFARequestCPTCode _rfaReferralCPTCode);

        void deleteRFAReferralCPTCodes(int _rfaCPTCodeID);

        RFARequestCPTCode getRFAReferralCPTCodesByID(int _rfaCPTCodeID);

        //IEnumerable<RFARequestCPTCode> getRFAReferralCPTCodesByReferralID(int _rfaReferralId);
        #endregion

        #region RFARecord Splitting
        int addRFARecordSplitting(RFARecordSplitting _rfaRecordSplitting);

        int udateRFARecordSplitting(RFARecordSplitting _rfaRecordSplitting);

        int udateRFARecordSplittingClaimIDByReferralID(int rfaReferralID, int claimID);

        int udateRFARecordSplittingRecordForMedicalRec(RFARecordSplitting _rfaRecordSplitting);

        void deleteRFARecordSplitting(int rfaRecSpltID);

        RFARecordSplitting getRFARecordSplittingByID(int rfaRecSpltID);

        IEnumerable<RFARecordSplitting> getRFARecordSplittingByReferralID(int _rfaReferralId);
        #endregion

        #region Referral Intake Patient Claim

        int udateReferralIntakePatientClaimByID(int _patientClaimID, int _rfaReferralId);

        PatientDetailsByReferralID getPatientDetailsByReferralID(int _rfaReferralId);


        #endregion

        #region  RFADiagnosis
        BLModel.Paged.RFADiagnosis getRFADiagnosisByReferralID(int _rfaReferralId,int skip,int take);
         #endregion 

        #region RFAPatMedicalRecordReview
        int addRFAPatMedicalRecordReview(MMC.Core.Data.Model.RFAPatMedicalRecordReview _rfaPatMedicalRecordReview);
        //BLModel.Paged.RFAPatMedicalRecordReview getRFAPatMedicalRecordReview(int _rfaReferralId, int _skip, int _take);
        BLModel.Paged.RFAPatMedicalRecordReview getRFAPatMedicalRecordReviewByPatientID(int _PatientID, int _referalId, int _skip, int _take);
        #endregion

        #region RFA- URHistory
        BLModel.Paged.PatientHistory getPatientHistoryByPatientID(int _patientID, int _skip, int _take, string sortBy, string order);
        #endregion

        #region  RequestHistory
        BLModel.Paged.RequestHistory getRequestHistoryByRFARequestID(int _requestID, int _skip, int _take);
        #endregion

        #region Duplicate
        BLModel.Paged.RequestByReferralID GetRequestForDuplicateDecision(int _PatientClaimID, int _skip, int _take); 
        #endregion

        #region Note
        int addNotes(DLModel.Note _note);
        int updateNotes(DLModel.Note _note);
        #endregion

        #region DeterminationLetter
        string getDeterminationLetterDecisionDesc(int _rfaReferralID);
        #endregion


        #region RFARequestTimeExtension
        int addRFARequestTimeExtensionInfo(DLModel.RFARequestTimeExtension _RFARequestTimeExtension);
        void DeleteRFARequestTimeExtensionInfo(int _rfarefferalId);
        #endregion

        #region RFAReferralAdditionalLink     
        void AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID(RFAReferralAdditionalLink _rfaReferralAdditionalLink);
        void AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID(RFAReferralAdditionalLink _rfaReferralAdditionalLink);
       
        #endregion

        #region RFARequestBodyPart
        int addRFARequestBodyPart(DLModel.RFARequestBodyPart _rFARequestBodyPart);
        int updateRFARequestBodyPart(DLModel.RFARequestBodyPart _rFARequestBodyPart);
        IEnumerable<RFARequestBodyPart> getRFARequestBodyPartByRequestID(int _requestID);
        void deleteRFARequestBodyPartByReqID(int _rfaRequestID);
        #endregion
    }

}

