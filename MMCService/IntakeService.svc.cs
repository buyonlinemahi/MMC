using AutoMapper;
using MMC.Core.BL;
using MMCService.CustomServiceBehaviors;
using System.Collections.Generic;
using System.ServiceModel;
using DLModel = MMC.Core.Data.Model;
using BLModel = MMC.Core.BL.Model;
using MMCService.DTO;
namespace MMCService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AutoMapperServiceBehavior]
    public class IntakeService : IIntakeService
    {
        private readonly IIntakeRepository _iIntakeRepository;
        public IntakeService(IIntakeRepository iIntakeRepository)
        {
            _iIntakeRepository = iIntakeRepository;
        }
        public int addRFAReferral(DTO.RFAReferral _rfaReferral)
        {
            return _iIntakeRepository.addRFAReferral(Mapper.Map<DTO.RFAReferral, MMC.Core.Data.Model.RFAReferral>(_rfaReferral));
        }
        public int updateClaimInReferral(int _claimID, int _rfaReferralID)
        {
            return _iIntakeRepository.updateClaimInReferral(_claimID, _rfaReferralID);
        }
        public int updateRFAInReferral(DTO.RFAReferral _rfaReferral)
        {
            return _iIntakeRepository.updateRFAInReferral(Mapper.Map<DTO.RFAReferral, MMC.Core.Data.Model.RFAReferral>(_rfaReferral));
        }
        public int updatePhysicianInReferral(int _physicianID, int _rfaReferralID)
        {
            return _iIntakeRepository.updatePhysicianInReferral(_physicianID, _rfaReferralID);
        }
        public DTO.RFAReferral getReferralByID(int Id)
        {
            return Mapper.Map<DTO.RFAReferral>(_iIntakeRepository.getReferralByID(Id));
        }
        public DTO.Paged.PagedIncompleteReferrals getAllIncompleteReferrals(int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedIncompleteReferrals>(_iIntakeRepository.getAllIncompleteReferrals(_skip, _take));
        }
        public int addRFASplittedReferralHistory(DTO.RFASplittedReferralHistory _rfaSplittedReferralHistory)
        {
            return _iIntakeRepository.addRFASplittedReferralHistory(Mapper.Map<DTO.RFASplittedReferralHistory, MMC.Core.Data.Model.RFASplittedReferralHistory>(_rfaSplittedReferralHistory));
        }
        public int addRFAMergedReferralHistory(DTO.RFAMergedReferralHistory _rfaMergedReferralHistory)
        {
            return _iIntakeRepository.addRFAMergedReferralHistory(Mapper.Map<DTO.RFAMergedReferralHistory, MMC.Core.Data.Model.RFAMergedReferralHistory>(_rfaMergedReferralHistory));
        }
        public int UploadSignature(DTO.RFAReferral _rfaReferral)
        {
            return _iIntakeRepository.UploadSignature(Mapper.Map<DTO.RFAReferral, MMC.Core.Data.Model.RFAReferral>(_rfaReferral));
        }
        public int SaveSignatureDescription(DTO.RFAReferral _rfaReferral)
        {
            return _iIntakeRepository.SaveSignatureDescription(Mapper.Map<DTO.RFAReferral, MMC.Core.Data.Model.RFAReferral>(_rfaReferral));
        }
        public void UpdateRFARequestDecisionAndRFAStatusByReferralID(int RFAReferralID, string DecisionID, string RFAStatus)
        {
            _iIntakeRepository.UpdateRFARequestDecisionAndRFAStatusByReferralID(RFAReferralID, DecisionID, RFAStatus);
        }
        public System.DateTime UpdateRFARequestLatestDueDateByRefferalID(int _RFAReferralID, int _AddDays, int _UserID)
        {
            return _iIntakeRepository.UpdateRFARequestLatestDueDateByRefferalID(_RFAReferralID, _AddDays, _UserID);
        }


        #region RFARferralFiles
        public DTO.RFAReferralFile addReferralFileIntake(string filename, int userid)
        {
            return Mapper.Map<DTO.RFAReferralFile>(_iIntakeRepository.addReferralFileIntake(filename, userid));
        }
        public int addReferralFile(DTO.RFAReferralFile _rfaReferralFile)
        {
            return _iIntakeRepository.addReferralFile(Mapper.Map<DTO.RFAReferralFile, MMC.Core.Data.Model.RFAReferralFile>(_rfaReferralFile));
        }
        public int updateReferralFile(DTO.RFAReferralFile _rfaReferralFile)
        {
            return _iIntakeRepository.updateReferralFile(Mapper.Map<DTO.RFAReferralFile, MMC.Core.Data.Model.RFAReferralFile>(_rfaReferralFile));
        }
        public DTO.RFAReferralFile getReferralFileByID(int _rfaReferralFileID)
        {
            return Mapper.Map<DTO.RFAReferralFile>(_iIntakeRepository.getReferralFileByID(_rfaReferralFileID));
        }
        public DTO.RFAReferralFile getReferralFileByIntake(int _rfaReferralID)
        {
            return Mapper.Map<DTO.RFAReferralFile>(_iIntakeRepository.getReferralFileByIntake(_rfaReferralID));
        }
        public IEnumerable<DTO.RFAReferralFile> getReferralFileByRFAReferralID(int _rfaReferralID)
        {
            return Mapper.Map<IEnumerable<DTO.RFAReferralFile>>(_iIntakeRepository.getReferralFileByRFAReferralID(_rfaReferralID));
        }
        public IEnumerable<DTO.RFAReferralFile> getReferralFileByRFAReferralIDandFileType(int _rfaReferralID)
        {
            return Mapper.Map<IEnumerable<DTO.RFAReferralFile>>(_iIntakeRepository.getReferralFileByRFAReferralIDandFileType(_rfaReferralID));
        }
        public IEnumerable<DTO.RFAReferralFile> getRFAReferralFilesAccToReferralIDInPreparationCase(int _rfaReferralID)
        {
            return Mapper.Map<IEnumerable<DTO.RFAReferralFile>>(_iIntakeRepository.getRFAReferralFilesAccToReferralIDInPreparationCase(_rfaReferralID));
        }
        public IEnumerable<DTO.RFAReferralFile> getReferralFileByRFAReferralIDAndFileTypeID(int _rfaReferralID, int _fileTypeID)
        {
            return Mapper.Map<IEnumerable<DTO.RFAReferralFile>>(_iIntakeRepository.getReferralFileByRFAReferralIDAndFileTypeID(_rfaReferralID, _fileTypeID));
        }
        public string GetSignaturePathAndDescriptionByReferralID(int RFAReferralID)
        {
            return _iIntakeRepository.GetSignaturePathAndDescriptionByReferralID(RFAReferralID);
        }
        public void AddRFIReportAdditionalRecordByRFAReferralFileID(int _rFAReferralFileID, int UserId)
        {
            _iIntakeRepository.AddRFIReportAdditionalRecordByRFAReferralFileID(_rFAReferralFileID, UserId);
        }
        public void AddRFIReportAdditionalRecordByRFARequestID(int _rFAReferralFileID, int _rFARequestID, int UserID)
        {
            _iIntakeRepository.AddRFIReportAdditionalRecordByRFARequestID(_rFAReferralFileID, _rFARequestID, UserID);
        }
        public IEnumerable<DTO.PreviousReferralFromCurrentReferral> getPreviousReferralIDFromCurrentReferralInDuplicate(int referralID)
        {
            return Mapper.Map<IEnumerable<DTO.PreviousReferralFromCurrentReferral>>(_iIntakeRepository.getPreviousReferralIDFromCurrentReferralInDuplicate(referralID));
        }
        #endregion
        #region RFARequests
        public int addRFARequest(DTO.RFARequest _rfaRequest)
        {
            return _iIntakeRepository.addRFARequest(Mapper.Map<BLModel.RFARequest>(_rfaRequest));
        }
        public int updateRFARequest(DTO.RFARequest _rfaRequest)
        {
            return _iIntakeRepository.updateRFARequest(Mapper.Map<BLModel.RFARequest>(_rfaRequest));
        }
        public int AssignNewReferralToRequest(DTO.RFARequest _rfaRequest)
        {
            return _iIntakeRepository.AssignNewReferralToRequest(Mapper.Map<DLModel.RFARequest>(_rfaRequest));
        }
        public int updateRFARequestAndDecision(DTO.RFARequest _rfaRequest)
        {
            return _iIntakeRepository.updateRFARequestAndDecision(Mapper.Map<DLModel.RFARequest>(_rfaRequest));
        }
        public void deleteRFARequest(int _rfaRequestID)
        {
            _iIntakeRepository.deleteRFARequest(_rfaRequestID);
        }
        public void deleteRFADelatedRequest(int _rfaRequestID)
        {
            _iIntakeRepository.deleteRFADelatedRequest(_rfaRequestID);
        }
        public DTO.RFARequest getRFARequestByID(int _rfaRequestID)
        {
            return Mapper.Map<DTO.RFARequest>(_iIntakeRepository.getRFARequestByID(_rfaRequestID));
        }
        public IEnumerable<DTO.RFARequest> getRFARequestByReferralID(int _rfaReferralId)
        {
            return Mapper.Map<IEnumerable<DTO.RFARequest>>(_iIntakeRepository.getRFARequestByReferralID(_rfaReferralId));
        }
        public int addRFARequestAdditionalInfo(DTO.RFARequestAdditionalInfo _rfaRequestAdditionalInfo)
        {
            return _iIntakeRepository.addRFARequestAdditionalInfo(Mapper.Map<DLModel.RFARequestAdditionalInfo>(_rfaRequestAdditionalInfo));
        }

        public void addRFAReferralAdditionalInfo(DTO.RFAReferralAdditionalInfo _rfaReferralAdditionalInfo)
        {
            _iIntakeRepository.addRFAReferralAdditionalInfo(Mapper.Map<DLModel.RFAReferralAdditionalInfo>(_rfaReferralAdditionalInfo));
        }
        public int updateRFARequestAdditionalInfo(DTO.RFARequestAdditionalInfo _rfaRequestAdditionalInfo)
        {
            return _iIntakeRepository.updateRFARequestAdditionalInfo(Mapper.Map<DLModel.RFARequestAdditionalInfo>(_rfaRequestAdditionalInfo));
        }
        public DTO.RFARequestAdditionalInfoDetail GetRFARequestAdditionalInfo(int _rfaRequestID)
        {
            return Mapper.Map<DTO.RFARequestAdditionalInfoDetail>(_iIntakeRepository.GetRFARequestAdditionalInfo(_rfaRequestID));
        }
        public int SaveRFARequestAdditionalInfo(DTO.RFARequestAdditionalInfo _rfaRequestAdditionalInfo)
        {
            return _iIntakeRepository.SaveRFARequestAdditionalInfo(Mapper.Map<DLModel.RFARequestAdditionalInfo>(_rfaRequestAdditionalInfo));
        }

        public void UpdateRFAReqCertificationNumberByID(int RFAReferralID)
        {
            _iIntakeRepository.UpdateRFAReqCertificationNumberByID(RFAReferralID);
        }

        public void UpdateRFAReferralRequestDecisionByID(int RFAReferralID)
        {
            _iIntakeRepository.UpdateRFAReferralRequestDecisionByID(RFAReferralID);
        }

        #endregion
        #region RFAReferralCPTCodes
        public int addRFAReferralCPTCodes(DTO.RFARequestCPTCode _rfaReferralCPTCode)
        {
            return _iIntakeRepository.addRFAReferralCPTCodes(Mapper.Map<DLModel.RFARequestCPTCode>(_rfaReferralCPTCode));
        }
        public int updateRFAReferralCPTCodes(DTO.RFARequestCPTCode _rfaReferralCPTCode)
        {
            return _iIntakeRepository.updateRFAReferralCPTCodes(Mapper.Map<DLModel.RFARequestCPTCode>(_rfaReferralCPTCode));
        }
        public void deleteRFAReferralCPTCodes(int _rfaCPTCodeID)
        {
            _iIntakeRepository.deleteRFAReferralCPTCodes(_rfaCPTCodeID);
        }
        public DTO.RFARequestCPTCode getRFAReferralCPTCodesByID(int _rfaCPTCodeID)
        {
            return Mapper.Map<DTO.RFARequestCPTCode>(_iIntakeRepository.getRFAReferralCPTCodesByID(_rfaCPTCodeID));
        }
        //public IEnumerable<DTO.RFAReferralCPTCode> getRFAReferralCPTCodesByReferralID(int _rfaReferralId)
        //{
        //    return Mapper.Map<IEnumerable<DTO.RFAReferralCPTCode>>(_iIntakeRepository.getRFAReferralCPTCodesByReferralID(_rfaReferralId));
        //}
        #endregion
        #region RFARecordSplitting
        public int addRFARecordSplitting(DTO.RFARecordSplitting _rfaRecordSplitting)
        {
            return _iIntakeRepository.addRFARecordSplitting(Mapper.Map<DLModel.RFARecordSplitting>(_rfaRecordSplitting));
        }
        public int udateRFARecordSplitting(DTO.RFARecordSplitting _rfaRecordSplitting)
        {
            return _iIntakeRepository.udateRFARecordSplitting(Mapper.Map<DLModel.RFARecordSplitting>(_rfaRecordSplitting));
        }
        public int udateRFARecordSplittingClaimIDByReferralID(int rfaReferralID, int claimID)
        {
            return _iIntakeRepository.udateRFARecordSplittingClaimIDByReferralID(rfaReferralID, claimID);
        }
        public int udateRFARecordSplittingRecordForMedicalRec(DTO.RFARecordSplitting _rfaRecordSplitting)
        {
            return _iIntakeRepository.udateRFARecordSplittingRecordForMedicalRec(Mapper.Map<DLModel.RFARecordSplitting>(_rfaRecordSplitting));
        }
        public void deleteRFARecordSplitting(int rfaRecSpltID)
        {
            _iIntakeRepository.deleteRFARecordSplitting(rfaRecSpltID);
        }
        public DTO.RFARecordSplitting getRFARecordSplittingByID(int rfaRecSpltID)
        {
            return Mapper.Map<DTO.RFARecordSplitting>(_iIntakeRepository.getRFARecordSplittingByID(rfaRecSpltID));
        }
        public IEnumerable<DTO.RFARecordSplitting> getRFARecordSplittingByReferralID(int _rfaReferralId)
        {
            return Mapper.Map<IEnumerable<DTO.RFARecordSplitting>>(_iIntakeRepository.getRFARecordSplittingByReferralID(_rfaReferralId));
        }
        #endregion
        #region Referral Intake Patient Claim
        public int udateReferralIntakePatientClaimByID(int _patientClaimID, int _rfaReferralId)
        {
            return _iIntakeRepository.udateReferralIntakePatientClaimByID(_patientClaimID, _rfaReferralId);
        }
        public DTO.PatientDetailsByReferralID getPatientDetailsByReferralID(int _rfaReferralId)
        {
            return Mapper.Map<DTO.PatientDetailsByReferralID>(_iIntakeRepository.getPatientDetailsByReferralID(_rfaReferralId));
        }
        #endregion
        #region  RFADiagnosis
        public DTO.Paged.PagedRFADiagnosis getRFADiagnosisByReferralID(int _rfaReferralId, int skip, int take)
        {
            return Mapper.Map<DTO.Paged.PagedRFADiagnosis>(_iIntakeRepository.getRFADiagnosisByReferralID(_rfaReferralId, skip, take));
        }
        #endregion
        #region RFAPatMedicalRecordReview
        //public DTO.Paged.PagedRFAPatMedicalRecordReview getRFAPatMedicalRecordReview(int _rfaReferralId, int _skip, int _take)
        //{
        //    return Mapper.Map<DTO.Paged.PagedRFAPatMedicalRecordReview>(_iIntakeRepository.getRFAPatMedicalRecordReview(_rfaReferralId, _skip, _take));
        //}
        public DTO.Paged.PagedRFAPatMedicalRecordReview getRFAPatMedicalRecordReviewByPatientID(int _PatientID, int _ReferralId, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedRFAPatMedicalRecordReview>(_iIntakeRepository.getRFAPatMedicalRecordReviewByPatientID(_PatientID, _ReferralId, _skip, _take));
        }
        public int addRFAPatMedicalRecordReview(MMC.Core.Data.Model.RFAPatMedicalRecordReview _rfaPatMedicalRecordReview)
        {
            return _iIntakeRepository.addRFAPatMedicalRecordReview(_rfaPatMedicalRecordReview);
        }
        #endregion
        #region RFA- URHistory
        public DTO.Paged.PagedPatientHistory getPatientHistoryByPatientID(int _patientID, int _skip, int _take, string sortBy, string order)
        {
            return Mapper.Map<DTO.Paged.PagedPatientHistory>(_iIntakeRepository.getPatientHistoryByPatientID(_patientID, _skip, _take, sortBy, order));
        }
        #endregion

        #region  RequestHistory
        public DTO.Paged.PagedRequestHistory getRequestHistoryByRFARequestID(int _requestID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedRequestHistory>(_iIntakeRepository.getRequestHistoryByRFARequestID(_requestID, _skip, _take));
        }

        #endregion
        #region RequestModify
        public int saveRFARequestModify(DTO.RFARequestModify _rfaRequestModify)
        {
            return _iIntakeRepository.saveRFARequestModify(Mapper.Map<DTO.RFARequestModify, MMC.Core.Data.Model.RFARequestModify>(_rfaRequestModify));
        }
        public DTO.RFARequestModify getRFARequestModify(int _rfaRequestID)
        {
            return Mapper.Map<DTO.RFARequestModify>(_iIntakeRepository.getRFARequestModify(_rfaRequestID));
        }
        #endregion
        #region Duplicate  case
        public DTO.Paged.PagedRequestByReferralID GetRequestForDuplicateDecision(int _PatientClaimID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedRequestByReferralID>(_iIntakeRepository.GetRequestForDuplicateDecision(_PatientClaimID, _skip, _take));
        }
        #endregion
        #region notes
        public int addNotes(DTO.Note _note)
        {
            return _iIntakeRepository.addNotes(Mapper.Map<DTO.Note, MMC.Core.Data.Model.Note>(_note));
        }
        public int updateNotes(DTO.Note _note)
        {
            return _iIntakeRepository.updateNotes(Mapper.Map<DTO.Note, MMC.Core.Data.Model.Note>(_note));
        }
        #endregion
        #region RFARequestBilling
        public int addRFARequestBilling(IEnumerable<DTO.RFARequestBilling> _rfaRequestBilling)
        {
            return _iIntakeRepository.addRFARequestBilling(Mapper.Map<IEnumerable<DTO.RFARequestBilling>, IEnumerable<MMC.Core.Data.Model.RFARequestBilling>>(_rfaRequestBilling));
        }
        #endregion
        #region DeterminationLetter
        public string getDeterminationLetterDecisionDesc(int _rfaReferralID)
        {
            return _iIntakeRepository.getDeterminationLetterDecisionDesc(_rfaReferralID);
        }
        #endregion
        #region RFARequestTimeExtension
        public int addRFARequestTimeExtensionInfo(DTO.RFARequestTimeExtension _RFARequestTimeExtension)
        {
            return _iIntakeRepository.addRFARequestTimeExtensionInfo(Mapper.Map<DTO.RFARequestTimeExtension, MMC.Core.Data.Model.RFARequestTimeExtension>(_RFARequestTimeExtension));
        }
        public void DeleteRFARequestTimeExtensionInfo(int _rfarefferalId)
        {
            _iIntakeRepository.DeleteRFARequestTimeExtensionInfo(_rfarefferalId);
        }

        #endregion

        #region RFAReferralAdditionalLink        
        public void AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID(RFAReferralAdditionalLink _rfaReferralAdditionalLink)
        {
            _iIntakeRepository.AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID(Mapper.Map<DTO.RFAReferralAdditionalLink, MMC.Core.Data.Model.RFAReferralAdditionalLink>(_rfaReferralAdditionalLink));
        }
     
        public void AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID(RFAReferralAdditionalLink _rfaReferralAdditionalLink)
        {
            _iIntakeRepository.AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID(Mapper.Map<DTO.RFAReferralAdditionalLink, MMC.Core.Data.Model.RFAReferralAdditionalLink>(_rfaReferralAdditionalLink));
        }
        #endregion

        #region RFARequestBodyPart
        public int addRFARequestBodyPart(RFARequestBodyPart _rFARequestBodyPart)
        {
            return _iIntakeRepository.addRFARequestBodyPart(Mapper.Map<DTO.RFARequestBodyPart, MMC.Core.Data.Model.RFARequestBodyPart>(_rFARequestBodyPart));
        }
        public int  updateRFARequestBodyPart(RFARequestBodyPart _rFARequestBodyPart)
        {
            return _iIntakeRepository.updateRFARequestBodyPart(Mapper.Map<DTO.RFARequestBodyPart, MMC.Core.Data.Model.RFARequestBodyPart>(_rFARequestBodyPart));
        }

        public IEnumerable<RFARequestBodyPart> getRFARequestBodyPartByRequestID(int _requestID)
         {
             return Mapper.Map<IEnumerable<DTO.RFARequestBodyPart>>(_iIntakeRepository.getRFARequestBodyPartByRequestID(_requestID));
         }

        public void deleteRFARequestBodyPartByReqID(int _rfaRequestID)
        {
             _iIntakeRepository.deleteRFARequestBodyPartByReqID(_rfaRequestID);
        }
        
        #endregion
    }
}