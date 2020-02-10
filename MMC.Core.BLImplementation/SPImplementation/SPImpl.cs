using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using MMC.Core.Data.SQLServer.Constant;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
using DLModel = MMC.Core.Data.Model;
 

namespace MMC.Core.BLImplementation.SPImplementation
{
    public class SPImpl
    {
        MMCDbContext db = new MMCDbContext();
        public IEnumerable<BLModel.ClinicalTriage> getReferralByProcessLevel(int skip, int take, int processLevel)
        {
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            SqlParameter _processLevel = new SqlParameter("@processLevel", processLevel);
            return db.Database.SqlQuery<BLModel.ClinicalTriage>(StoredProcedureConst.ReferralRepositoryProcedure.GetReferralByProcessLevel, _skip, _take, _processLevel);
        }

        public int GetReferralCountByProcessLevel(int processLevel)
        {
            SqlParameter _processLevel = new SqlParameter("@processLevel", processLevel);
            return db.Database.SqlQuery<int>(StoredProcedureConst.ReferralRepositoryProcedure.GetReferralCountByProcessLevel, _processLevel).SingleOrDefault();
        }
        #region Referral Method
        public IEnumerable<BLModel.RequestByReferralID> getRequestByReferralId(int referralID)
        {
            SqlParameter _referralID = new SqlParameter("@ReferralID", referralID);
            return db.Database.SqlQuery<BLModel.RequestByReferralID>(StoredProcedureConst.ReferralRepositoryProcedure.GetRequestByReferralID, _referralID).ToList();
        }

        public IEnumerable<BLModel.RequestByReferralID> getRequestForNotificationByReferralId(int referralID)
        {
            SqlParameter _referralID = new SqlParameter("@ReferralID", referralID);
            return db.Database.SqlQuery<BLModel.RequestByReferralID>(StoredProcedureConst.ReferralRepositoryProcedure.GetRequestForNotificationByReferralID, _referralID);
        }
        public BLModel.PatientByReferralID getPatientByReferralId(int referralID)
        {
            SqlParameter _referralID = new SqlParameter("@ReferralID", referralID);
            return db.Database.SqlQuery<BLModel.PatientByReferralID>(StoredProcedureConst.ReferralRepositoryProcedure.GetPatientByReferralID, _referralID).SingleOrDefault();
        }

        public IEnumerable<BLModel.RequestByReferralID> getRequestByReferralIdForPeerReview(int referralID)
        {
            SqlParameter _referralID = new SqlParameter("@ReferralID", referralID);
            return db.Database.SqlQuery<BLModel.RequestByReferralID>(StoredProcedureConst.ReferralRepositoryProcedure.GetRequestByReferralIDForPeerReview, _referralID).ToList();
        }

        public IEnumerable<BLModel.IncompleteReferrals> getAllIncompleteReferrals(int skip, int take, int processLevel)
        {
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            SqlParameter _processLevel = new SqlParameter("@processLevel", processLevel);
            return db.Database.SqlQuery<BLModel.IncompleteReferrals>(StoredProcedureConst.ReferralRepositoryProcedure.GetReferralsInComplete, _skip, _take, _processLevel).ToList();
        }

        public int getReferralsTotalCountInComplete(int processLevel)
        {
            SqlParameter _processLevel = new SqlParameter("@processLevel", processLevel);
            return db.Database.SqlQuery<int>(StoredProcedureConst.ReferralRepositoryProcedure.GetReferralsTotalCountInComplete, _processLevel).FirstOrDefault();
        }

        public int getRFANewReferralIDFromRFAOldReferralID(int RFAReferralID,int DecisionID)
        {
            SqlParameter _RFAReferralID = new SqlParameter("@ReferralID", RFAReferralID);
            SqlParameter _DecisionID = new SqlParameter("@DecisionID", DecisionID);
            if (DecisionID == 0)
            {
                return db.Database.SqlQuery<int>(StoredProcedureConst.ReferralRepositoryProcedure.GetRFANewReferralIDFromRFAOldReferralIdPeerReview, _RFAReferralID).FirstOrDefault();
            }
            else if (DecisionID == 13 || DecisionID == 12)
            {
                return db.Database.SqlQuery<int>(StoredProcedureConst.ReferralRepositoryProcedure.GetRFANewReferralIDFromRFAOldReferralIDForWithdrawn, _RFAReferralID, _DecisionID).FirstOrDefault();
            }
            else
            {
                return db.Database.SqlQuery<int>(StoredProcedureConst.ReferralRepositoryProcedure.GetRFANewReferralIDFromRFAOldReferralID, _RFAReferralID, _DecisionID).FirstOrDefault();
            }
        }
        
        public void DeleteRFAReferralIDFromReferralFiles(int ReferralID, int RFAFileTypeID)
        {
            SqlParameter _ReferralID = new SqlParameter("@ReferralID", ReferralID);
            SqlParameter _RFAFileTypeID = new SqlParameter("@RFAFileTypeID", RFAFileTypeID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.ReferralRepositoryProcedure.DeleteRFAReferralIDFromReferralFiles, _ReferralID, _RFAFileTypeID);
        }
        public IEnumerable<PreviousReferralFromCurrentReferral> getPreviousReferralIDFromCurrentReferralInDuplicate(int referralID)
        {
            SqlParameter _referralID = new SqlParameter("@ReferralID", referralID);
            return db.Database.SqlQuery<PreviousReferralFromCurrentReferral>(StoredProcedureConst.ReferralRepositoryProcedure.GetPreviousReferralIDFromCurrentReferralInDuplicate, _referralID).ToList(); 
        }

        public void updateRFAReferralRequestRFAClinicalReasonforDecisionByID(int rFAReferralID, string rFAClinicalReasonforDecision)
        {
            SqlParameter _rFAReferralID = new SqlParameter("@RFAReferralID", rFAReferralID);
            SqlParameter _rFAClinicalReasonforDecision = new SqlParameter("@RFAClinicalReasonforDecision", rFAClinicalReasonforDecision);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.ReferralRepositoryProcedure.UpdateRFAReferralRequestRFAClinicalReasonforDecisionByID, _rFAReferralID, _rFAClinicalReasonforDecision);
        }
        #endregion

        #region RFADiagnosis
        public IEnumerable<BLModel.RFADiagnosis> GetRFADiagnosisByReferralID(int RFAReferralID, int skip, int take)
        {
            SqlParameter _RFAReferralID = new SqlParameter("@RFAReferralID", RFAReferralID);
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            return db.Database.SqlQuery<BLModel.RFADiagnosis>(StoredProcedureConst.ReferralRepositoryProcedure.GetRFADiagnosisByReferralID, _RFAReferralID, _skip, _take).ToList();
        }
        public int GetRFADiagnosisByReferralIDCount(int RFAReferralID)
        {
            SqlParameter _RFAReferralID = new SqlParameter("@RFAReferralID", RFAReferralID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.ReferralRepositoryProcedure.GetRFADiagnosisByReferralIDCount, _RFAReferralID).FirstOrDefault();
        }
        #endregion
        

        public string getRequestCPTNDCCodesByRFARequestID(int _rfaRequestID)
        {
            SqlParameter _rFARequestID = new SqlParameter("@RFARequestID", _rfaRequestID);
            return db.Database.SqlQuery<string>(StoredProcedureConst.IntakeRepositoryProcedure.GetRequestCPTNDCCodesByRFARequestID, _rFARequestID).FirstOrDefault();
        }


        public string getRequestedTreatmentByRFARequestID(int _rfaRequestID)
        {
            SqlParameter _rFARequestID = new SqlParameter("@RFARequestID", _rfaRequestID);
            return db.Database.SqlQuery<string>(StoredProcedureConst.IntakeRepositoryProcedure.GetRequestedTreatmentByRFARequestID, _rFARequestID).FirstOrDefault();
        }

        public void updatePatientClaimPleadBodyPartByPatientClaimID(int _patientClaimID, int _bodyPartStatusID)
        {
            SqlParameter _PatientClaimID = new SqlParameter("@PatientClaimID", _patientClaimID);
            SqlParameter _BodyPartStatusID = new SqlParameter("@BodyPartStatusID", _bodyPartStatusID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.PatientRepositoryProcedure.UpdatePatientClaimPleadBodyPartByPatientClaimID, _PatientClaimID, _BodyPartStatusID);
        }

        public void UpdateRFAReqCertificationNumberByID(int _RFARequestID)
        {
            SqlParameter _rFARequestID = new SqlParameter("@RFARequestID", _RFARequestID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.UpdateRFAReqCertificationNumberByID, _rFARequestID);
        }
        public void UpdateRFAReferralRequestDecisionByID(int RFAReferralID)
        {
            SqlParameter _rFAReferralID = new SqlParameter("@RFAReferralID", RFAReferralID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.UpdateRFAReferralRequestDecisionByID, _rFAReferralID);
        }

        public void AddRFIReportAdditionalRecordByRFAReferralFileID(int RFAReferralFileID, int UserID)
        {
            SqlParameter _rFAReferralFileID = new SqlParameter("@RFAReferralFileID", RFAReferralFileID);
            SqlParameter _userID = new SqlParameter("@UserID", UserID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.AddRFIReportAdditionalRecordByRFAReferralFileID, _rFAReferralFileID,_userID);
        }

        public void AddRFIReportAdditionalRecordByRFARequestID(int RFAReferralFileID,int RFARequestID, int UserID)
        {
            SqlParameter _rFAReferralFileID = new SqlParameter("@RFAReferralFileID", RFAReferralFileID);
            SqlParameter _rFARequestID = new SqlParameter("@RFARequestID", RFARequestID);
            SqlParameter _userID = new SqlParameter("@UserID", UserID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.AddRFIReportAdditionalRecordByRFARequestID, _rFAReferralFileID, _rFARequestID, _userID);

        }

        public IEnumerable<BLModel.RFARequest> getRFARequestByClaimID(int claimID)
        {
            SqlParameter _claimID = new SqlParameter("@PatientClaimID", claimID);
            return db.Database.SqlQuery<BLModel.RFARequest>(StoredProcedureConst.IntakeRepositoryProcedure.getRFARequestByClaimID, _claimID);
        }

        public IEnumerable<BLModel.RFAReferralFile> getReferralFileByRFAReferralIDandFileType(int RFAReferralID)
        {
            SqlParameter _RFAReferralID = new SqlParameter("@referralID", RFAReferralID);
            return db.Database.SqlQuery<BLModel.RFAReferralFile>(StoredProcedureConst.IntakeRepositoryProcedure.GetReferralFileByRFAReferralID, _RFAReferralID).ToList();
        }

        public IEnumerable<BLModel.RFAReferralFile> getRFAReferralFilesAccToReferralIDInPreparationCase(int RFAReferralID)
        {
            SqlParameter _RFAReferralID = new SqlParameter("@RFAReferralID", RFAReferralID);
            return db.Database.SqlQuery<BLModel.RFAReferralFile>(StoredProcedureConst.IntakeRepositoryProcedure.GetRFAReferralFilesAccToReferralIDInPreparationCase, _RFAReferralID).ToList();
        }

        public void UpdateRFARequestDecisionAndRFAStatusByReferralID(int RFAReferralID, string DecisionID, string RFAStatus)
        {   
            SqlParameter _RFAReferralID = new SqlParameter("@RFAReferralID", RFAReferralID);
            SqlParameter _DecisionID = new SqlParameter("@DecisionID", DecisionID == null ? (object)DBNull.Value : DecisionID);
            SqlParameter _RFAStatus = new SqlParameter("@RFAStatus", RFAStatus == null ? (object)DBNull.Value : DecisionID);

            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.UpdateRFARequestDecisionAndRFAStatusByReferralID, _RFAReferralID, _DecisionID, _RFAStatus);
        }

        public string GetSignaturePathAndDescriptionByReferralID(int RFAReferralID)
        {
            SqlParameter _RFAReferralID = new SqlParameter("@RFAReferralID", RFAReferralID);

            return db.Database.SqlQuery<string>(StoredProcedureConst.IntakeRepositoryProcedure.GetSignaturePathAndDescriptionByReferralID, _RFAReferralID).SingleOrDefault();
        }
        public DateTime GetRFARequestLatestDueDate(int addDays, DateTime RFARequestDate)
        {
            SqlParameter _addDays = new SqlParameter("@AddDay", addDays);
            SqlParameter _RFARequestDate = new SqlParameter("@RFARequestDate", RFARequestDate);
            return db.Database.SqlQuery<System.DateTime>(StoredProcedureConst.IntakeRepositoryProcedure.GetRFARequestLatestDueDate, _addDays, _RFARequestDate).SingleOrDefault();
        }
        public DateTime UpdateRFARequestLatestDueDateByRefferalID(int RFAReferralID, int AddDays, int UserID)
        {
            SqlParameter _AddDay = new SqlParameter("@AddDay", AddDays);            
            SqlParameter _RFAReferralID = new SqlParameter("@rfaRefferalID", RFAReferralID);
            SqlParameter _Createdby = new SqlParameter("@Createdby", UserID);
            return db.Database.SqlQuery<System.DateTime>(StoredProcedureConst.IntakeRepositoryProcedure.UpdateRFARequestLatestDueDateByRefferalID, _RFAReferralID, _AddDay, _Createdby).SingleOrDefault();
        }

        public void AddRFITemplateRecordByRFARequestID(int rFAReferralFileID, int userID)
        {
            SqlParameter _rFAReferralFileID = new SqlParameter("@RFAReferralFileID", rFAReferralFileID);
            SqlParameter _userID = new SqlParameter("@UserID", userID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.AddRFITemplateRecordByRFARequestID, _rFAReferralFileID, _userID);
        }

        public void AddRFARequestTimeExtensionRecordByRFARequestID(int rFAReferralFileID, int userID)
        {
            SqlParameter _rFAReferralFileID = new SqlParameter("@RFAReferralFileID", rFAReferralFileID);
            SqlParameter _userID = new SqlParameter("@UserID", userID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.AddRFARequestTimeExtensionRecordByRFARequestID, _rFAReferralFileID, _userID);
        }

        public void MoveRFARequestRecordToRFADeletedRequest(int rfaRequestID)
        {
            SqlParameter _rfaRequestID = new SqlParameter("@RFARequestID", rfaRequestID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.MoveRFARequestRecordToRFADeletedRequest, _rfaRequestID);
        }
         
        public void UpdateRFAAdditionalInfoDetailByRequestID(int oldRFAReferralID, int rFARequestID)
        {
            SqlParameter _oldRFAReferralID = new SqlParameter("@OldRFAReferralID", oldRFAReferralID);
            SqlParameter _rfaRequestID = new SqlParameter("@RFARequestID", rFARequestID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.UpdateRFAAdditionalInfoDetailByRequestID, _oldRFAReferralID, _rfaRequestID);
        }



        #region Billing
        public IEnumerable<BLModel.Billing> GetBillingDetails(int skip, int take)
        {
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);

            return db.Database.SqlQuery<BLModel.Billing>(StoredProcedureConst.BillingRepositoryProcedure.GetDetailsForBillingLanding, _skip, _take).ToList();
        }

        public int GetBillingDetailsCount()
        {
            return db.Database.SqlQuery<int>(StoredProcedureConst.BillingRepositoryProcedure.GetDetailsForBillingLandingCount).SingleOrDefault();
        }

        public IEnumerable<BLModel.Billing> GetBillingDetailsByClientName(string ClientName,int skip, int take)
        {
            SqlParameter _clientName = new SqlParameter("@ClientName", ClientName);
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);

            return db.Database.SqlQuery<BLModel.Billing>(StoredProcedureConst.BillingRepositoryProcedure.GetDetailsForBillingLandingByClientName, _clientName, _skip, _take).ToList();
        }

        public int GetBillingDetailsByClientNameCount(string ClientName)
        {
            SqlParameter _clientName = new SqlParameter("@ClientName", ClientName);
            return db.Database.SqlQuery<int>(StoredProcedureConst.BillingRepositoryProcedure.GetDetailsForBillingLandingByClientNameCount, _clientName).SingleOrDefault();
        }

        public IEnumerable<BLModel.BillingAccountReceivables> GetAccountReceivablesByClientName(string ClientName, int skip, int take)
        {
            SqlParameter _clientName = new SqlParameter("@ClientName", ClientName);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);

            return db.Database.SqlQuery<BLModel.BillingAccountReceivables>(StoredProcedureConst.BillingRepositoryProcedure.GetAccountReceivablesByClientName, _clientName, _skip, _take).ToList();
        }

        public int GetAccountReceivablesByClientNameCount(string ClientName)
        {
            SqlParameter _clientName = new SqlParameter("@ClientName", ClientName);
            return db.Database.SqlQuery<int>(StoredProcedureConst.BillingRepositoryProcedure.GetAccountReceivablesByClientNameCount, _clientName).SingleOrDefault();
        }
        #endregion
        #region Email Record Attachment
        public IEnumerable<BLModel.EmailRecordAttachment> GetEmailRecordAttachmentByEmailRecordId(int emailRecordId)
        {
            SqlParameter _emailRecordId = new SqlParameter("@EmailRecordId", emailRecordId);
            return db.Database.SqlQuery<BLModel.EmailRecordAttachment>(StoredProcedureConst.EmailRecordAttachmentProcedure.GetEmailRecordAttachmentByEmailRecordId, _emailRecordId).ToList();
        }
        #endregion

        #region InitialNotification
        public BLModel.InitialNotificationLetter getInitialNotificationLetterDetails(int referralID)
        {
            SqlParameter _referralID = new SqlParameter("@referralID", referralID);
            return db.Database.SqlQuery<BLModel.InitialNotificationLetter>(StoredProcedureConst.LetterRepositoryProcedure.GetInitialNotificationLetterDetails, _referralID).FirstOrDefault();
        }

        public IEnumerable<BLModel.RequestInitialNotificationLetter> getRequestDetailsInitialNotificationLetter(int referralID)
        {
            SqlParameter _referralID = new SqlParameter("@referralID", referralID);
            return db.Database.SqlQuery<BLModel.RequestInitialNotificationLetter>(StoredProcedureConst.LetterRepositoryProcedure.GetRequestDetailsInitialNotificationLetter, _referralID).ToList();
        }

        public IEnumerable<BLModel.RequestInitialNotificationLetter> getCertifiedRequestDetailsInitialNotificationLetter(int referralID)
        {
            SqlParameter _referralID = new SqlParameter("@referralID", referralID);
            return db.Database.SqlQuery<BLModel.RequestInitialNotificationLetter>(StoredProcedureConst.LetterRepositoryProcedure.GetCertifiedRequestDetailsInitialNotificationLetter, _referralID).ToList();
        }

        public IEnumerable<BLModel.RequestInitialNotificationLetter> getDeniedRequestDetailsInitialNotificationLetter(int referralID)
        {
            SqlParameter _referralID = new SqlParameter("@referralID", referralID);
            return db.Database.SqlQuery<BLModel.RequestInitialNotificationLetter>(StoredProcedureConst.LetterRepositoryProcedure.GetDeniedRequestDetailsInitialNotificationLetter, _referralID).ToList();
        }
        #endregion

        #region Patient Medical Record

        public IEnumerable<BLModel.PatientMedicalRecordByPatientID> getPatientMedicalRecordByPatientID(int patientID, int skip, int take, string sortBy, string order)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            SqlParameter _sortBy = new SqlParameter("@sortBy", sortBy);
            SqlParameter _order = new SqlParameter("@order", order);

            return db.Database.SqlQuery<BLModel.PatientMedicalRecordByPatientID>(StoredProcedureConst.PatientRepositoryProcedure.GetPatientMedicalRecordByPatientID, _patientID, _skip, _take, _sortBy, _order);
        }
        public int GetPatientMedicalRecordByPatientIDCount(int patientID)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.PatientRepositoryProcedure.GetPatientMedicalRecordByPatientIDCount, _patientID).SingleOrDefault();
        }

        public IEnumerable<BLModel.PatientMedicalRecordByPatientID> getReferralMedicalRecordByPatientID(int patientID, int skip, int take)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);

            return db.Database.SqlQuery<BLModel.PatientMedicalRecordByPatientID>(StoredProcedureConst.ReferralRepositoryProcedure.GetReferralMedicalRecordByPatientID, _patientID, _skip, _take);
        }

        public int GetReferralMedicalRecordByPatientIDCount(int patientID)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.ReferralRepositoryProcedure.GetReferralMedicalRecordByPatientIDCount, _patientID).SingleOrDefault();
        }


        public BLModel.PatientMedicalRecordTemplateByPatientID GetPatientMedicalRecordTemplateByPatientID(int patientID)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            return db.Database.SqlQuery<BLModel.PatientMedicalRecordTemplateByPatientID>(StoredProcedureConst.PatientRepositoryProcedure.GetPatientMedicalRecordTemplateByPatientID, _patientID).SingleOrDefault();
        }

        public IEnumerable<BLModel.PatientMedicalRecordByPatientID> GetPatientMedicalRecordMultipleTemplateByPatientID(int patientID)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            return db.Database.SqlQuery<BLModel.PatientMedicalRecordByPatientID>(StoredProcedureConst.PatientRepositoryProcedure.GetPatientMedicalRecordMultipleTemplateByPatientID, _patientID).AsEnumerable();
        }

        public void UpdatePatientMedicareEligibleByID(int patientID, string mode, int currentMedicalConditionId)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            SqlParameter _mode = new SqlParameter("@Mode", mode);
            SqlParameter _currentMedicalConditionId = new SqlParameter("@CurrentMedicalConditionId", currentMedicalConditionId);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.PatientRepositoryProcedure.UpdatePatientMedicareEligibleByID, _patientID, _mode, _currentMedicalConditionId);
        }

        #endregion


        #region BodyParts
        public string getAcceptedBodyPartsByReferralID(int _referralID)
        {
            SqlParameter _ReferralID = new SqlParameter("@ReferralID", _referralID);
            return db.Database.SqlQuery<string>(StoredProcedureConst.ReferralRepositoryProcedure.GetAcceptedBodyPartByReferralID, _ReferralID).FirstOrDefault();
        }
        public string getDeniedBodyPartsByReferralID(int _referralID)
        {
            SqlParameter _ReferralID = new SqlParameter("@ReferralID", _referralID);
            return db.Database.SqlQuery<string>(StoredProcedureConst.ReferralRepositoryProcedure.GetDeniedBodyPartByReferralID, _ReferralID).FirstOrDefault();
        }
        public string getDelayedBodyPartByReferralID(int _referralID)
        {
            SqlParameter _ReferralID = new SqlParameter("@ReferralID", _referralID);
            return db.Database.SqlQuery<string>(StoredProcedureConst.ReferralRepositoryProcedure.GetDelayedBodyPartByReferralID, _ReferralID).FirstOrDefault();
        }
        public string getDignosisByReferralID(int _referralID)
        {
            SqlParameter _ReferralID = new SqlParameter("@ReferralID", _referralID);
            return db.Database.SqlQuery<string>(StoredProcedureConst.ReferralRepositoryProcedure.GetDiagnosisByReferralID, _ReferralID).FirstOrDefault();
        }
        public IEnumerable<BLModel.BodyPartDetail> getBodyPartsByClaimId(int _ClaimID, int _skip, int _take)
        {
            SqlParameter ClaimID = new SqlParameter("@ClaimID", _ClaimID);
            SqlParameter Skip = new SqlParameter("@Skip", _skip);
            SqlParameter Take = new SqlParameter("@Take", _take);
            return db.Database.SqlQuery<BLModel.BodyPartDetail>(StoredProcedureConst.PatientRepositoryProcedure.GetAllBodyPartsbyClaimID, ClaimID, Skip, Take).ToList();
        }

        public int getBodyPartsCountByClaimId(int _ClaimID)
        {
            SqlParameter ClaimID = new SqlParameter("@ClaimID", _ClaimID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.PatientRepositoryProcedure.GetAllBodyPartsCountbyClaimID, ClaimID).FirstOrDefault();
        }

        #endregion

        #region Patient Medical Record Review

        public IEnumerable<BLModel.RFAPatMedicalRecordReviewDetail> getRFAPatMedicalRecordReviewByPatientID(int patientID, int ReferralID, int skip, int take)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            SqlParameter _referralID = new SqlParameter("@ReferralID", ReferralID);
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            return db.Database.SqlQuery<BLModel.RFAPatMedicalRecordReviewDetail>(StoredProcedureConst.IntakeRepositoryProcedure.GetRFAPatMedicalRecordReviewByPatientID, _patientID, _referralID, _skip, _take).ToList();
        }
        public int getRFAPatMedicalRecordReviewByPatientIDCount(int patientID, int ReferralID)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            SqlParameter _referralID = new SqlParameter("@ReferralID", ReferralID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.IntakeRepositoryProcedure.GetRFAPatMedicalRecordReviewByPatientIDCount, _patientID, _referralID).SingleOrDefault();
        }
        #endregion


        #region Client 

        public BLModel.Client getClaimAdministratorByClientID(int ClientID)
        {
            SqlParameter _clientID = new SqlParameter("@ClientID", ClientID);
            return db.Database.SqlQuery<BLModel.Client>(StoredProcedureConst.ClientRepositoryProcedure.GetClaimAdministratorByClientID, _clientID).FirstOrDefault();
        }

        public IEnumerable<BLModel.Client> getClientDetailsByName(string SearchText, int Skip, int Take)
        {
            SqlParameter _searchText = new SqlParameter("@SearchText", SearchText);
            SqlParameter _skip = new SqlParameter("@Skip", Skip);
            SqlParameter _take = new SqlParameter("@Take", Take);
            return db.Database.SqlQuery<BLModel.Client>(StoredProcedureConst.ClientRepositoryProcedure.GetClientDetailsByName, _searchText, _skip, _take).AsEnumerable();
        }

        public IEnumerable<BLModel.Client> getAllClaimAdministrator()
        {
           return db.Database.SqlQuery<BLModel.Client>(StoredProcedureConst.ClientRepositoryProcedure.GetAllClaimAdministrator).ToList();
        }
        public IEnumerable<BLModel.ClaimAdministratorAllByClientID> getClaimAdministratorAllByClientID(int ClientID)
        {
            SqlParameter _clientID = new SqlParameter("@ClientID", ClientID);
            return db.Database.SqlQuery<BLModel.ClaimAdministratorAllByClientID>(StoredProcedureConst.ClientRepositoryProcedure.GetClaimAdministratorAllByClientID, _clientID).ToList();
        }

        public void UpdateClientManagedCareCompanyByClientID(DLModel.ClientManagedCareCompany _clientManagedCareCompany)
        {
            SqlParameter _clientID = new SqlParameter("@ClientID", _clientManagedCareCompany.ClientID);
            SqlParameter _CompanyId = new SqlParameter("@CompanyID", _clientManagedCareCompany.CompanyID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.ClientRepositoryProcedure.UpdateClientManagedCareCompanyByClientID, _clientID, _CompanyId);
        }

        public void UpdateClaimAdministratorResetDetailsByClientID(int _ClientID)
        {
            SqlParameter _clientID = new SqlParameter("@ClientID", _ClientID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.ClientRepositoryProcedure.UpdateClaimAdministratorResetDetailsByClientID, _clientID);
        }

        public IEnumerable<BLModel.ClientInsuranceBranch> getClientInsuranceBranchesAllByInsurerID(int _clientID, int _insurerID)
        {
            SqlParameter _clientId = new SqlParameter("@ClientID", _clientID);
            SqlParameter _insurerId = new SqlParameter("@InsurerID", _insurerID);
            return db.Database.SqlQuery<BLModel.ClientInsuranceBranch>(StoredProcedureConst.ClientRepositoryProcedure.GetClientInsuranceBranchesAllByInsurerID, _clientId, _insurerId).AsEnumerable();
        }

        public IEnumerable<BLModel.ClientThirdPartyAdministratorBranch> getClientTPABranchesAllByTPAID(int _clientID, int _tPAID)
        {
            SqlParameter _clientId = new SqlParameter("@ClientID", _clientID);
            SqlParameter _tPAId = new SqlParameter("@TPAID", _tPAID);
            return db.Database.SqlQuery<BLModel.ClientThirdPartyAdministratorBranch>(StoredProcedureConst.ClientRepositoryProcedure.GetClientTPABranchesAllByTPAID, _clientId, _tPAId).AsEnumerable();
        }
        

        #endregion

        #region Attorney
        public IEnumerable<BLModel.AttorneyFirm> getAttorneyFirmByAttorneyNameorFirm(string AttorneyFirmSearch, int skip, int take)
        {
            SqlParameter _AttorneyFirmSearch = new SqlParameter("@AttorneyFirmSearch", AttorneyFirmSearch);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return db.Database.SqlQuery<BLModel.AttorneyFirm>(StoredProcedureConst.AttorneyRepositoryProcedure.SearchAttorneyFirmByAttorneyORAttorneyFirmName, _AttorneyFirmSearch, _skip, _take);
        }

        public int getAttorneyFirmByAttorneyNameorFirmCOUNT(string AttorneyFirmSearch)
        {
            SqlParameter _AttorneyFirmSearch = new SqlParameter("@AttorneyFirmSearch", AttorneyFirmSearch);
            return db.Database.SqlQuery<int>(StoredProcedureConst.AttorneyRepositoryProcedure.SearchAttorneyFirmByAttorneyORAttorneyFirmNameCOUNT, _AttorneyFirmSearch).SingleOrDefault();
        }
        #endregion

        #region Notification


        public IEnumerable<BLModel.NotificationEmailSend> getAdjandPhyEmailByReferralID(int ReferralID)
        {
            SqlParameter _ReferralID = new SqlParameter("@ReferralID", ReferralID);
            return db.Database.SqlQuery<BLModel.NotificationEmailSend>(StoredProcedureConst.NotificationRepositoryProcedure.GetAdjandPhyEmailByReferralID, _ReferralID);
        }
        #endregion

        #region Common
        public string getPatientComorbidConditionsByPatientID(int PatientID)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", PatientID);
            return db.Database.SqlQuery<string>(StoredProcedureConst.CommonRepositoryProcedure.GetPatientComorbidConditionsByPatientID, _patientID).FirstOrDefault();
        }
        public BLModel.StorageModel GetStorageStuctureByID(int _id, char _by)
        {
            SqlParameter id = new SqlParameter("@ID", _id);
            SqlParameter by = new SqlParameter("@By", _by);
            return db.Database.SqlQuery<BLModel.StorageModel>(StoredProcedureConst.CommonRepositoryProcedure.GetStorageStuctureByID, id, by).FirstOrDefault();
        }
        public IEnumerable<ICDCode> getICDCodesByNumber(string ICDNumber, string ICDTab)
        {
            SqlParameter _ICDNumber = new SqlParameter("@ICDNumber", ICDNumber);
            SqlParameter _ICDTab = new SqlParameter("@ICDTab", ICDTab);
            return db.Database.SqlQuery<ICDCode>(StoredProcedureConst.CommonRepositoryProcedure.GetICDCodesByNumber, _ICDNumber, _ICDTab).ToList();
        }
        public IEnumerable<BLModel.ICDCode> getICDCodesByNumberOrDescription(string ICDNumberOrDescription, string ICD9Type, string ICDTab, int skip, int take)
        {
            SqlParameter _ICDNumberOrDescription = new SqlParameter("@ICDNumberOrDescription", ICDNumberOrDescription);
            SqlParameter _ICD9Type = new SqlParameter("@ICD9Type", ICD9Type);
            SqlParameter _ICDTab = new SqlParameter("@ICDTab", ICDTab);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return db.Database.SqlQuery<BLModel.ICDCode>(StoredProcedureConst.CommonRepositoryProcedure.GetICDCodesByNumberOrDescription, _ICDNumberOrDescription, _ICD9Type, _ICDTab, _skip, _take).ToList();
        }
        public int getICDCodesByNumberOrDescriptionCount(string ICDNumberOrDescription, string ICD9Type, string ICDTab)
        {
            SqlParameter _ICDNumberOrDescription = new SqlParameter("@ICDNumberOrDescription", ICDNumberOrDescription);
            SqlParameter _ICD9Type = new SqlParameter("@ICD9Type", ICD9Type);
            SqlParameter _ICDTab = new SqlParameter("@ICDTab", ICDTab);
            return db.Database.SqlQuery<int>(StoredProcedureConst.CommonRepositoryProcedure.GetICDCodesByNumberOrDescriptionCount, _ICDNumberOrDescription, _ICD9Type, _ICDTab).FirstOrDefault();
        }
        public IEnumerable<BLModel.AdditionalDocument> getAdditionalDocumentByPatientID(int _PatientID,int _PatientClaimID, int skip, int take)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", _PatientID);
            SqlParameter _patientClaimID = new SqlParameter("@PatientClaimID", _PatientClaimID);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return db.Database.SqlQuery<BLModel.AdditionalDocument>(StoredProcedureConst.CommonRepositoryProcedure.GetAdditionalDocumentByPatientID, _patientID, _patientClaimID, _skip, _take).ToList();
        }
        public int getAdditionalDocumentByPatientIDCount(int _PatientID, int _PatientClaimID)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", _PatientID);
            SqlParameter _patientClaimID = new SqlParameter("@PatientClaimID", _PatientClaimID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.CommonRepositoryProcedure.GetAdditionalDocumentByPatientIDCount, _patientID, _patientClaimID).SingleOrDefault();
        }
        public IEnumerable<NoOfReferralCount> getNoOfReferralCountForDifferentProcessLevel()
        {
            return db.Database.SqlQuery<NoOfReferralCount>(StoredProcedureConst.CommonRepositoryProcedure.GetReferralsForDifferentProcessLevels).ToList();
        }
        #endregion

        #region patent claim
        public IEnumerable<BLModel.PatientClaim> getPatientsClaimByName(string SearchText, int Skip, int Take)
        {
            SqlParameter _searchText = new SqlParameter("@SearchText", SearchText);
            SqlParameter _skip = new SqlParameter("@Skip", Skip);
            SqlParameter _take = new SqlParameter("@Take", Take);
            return db.Database.SqlQuery<BLModel.PatientClaim>(StoredProcedureConst.PatientRepositoryProcedure.GetPatientsClaimByName, _searchText, _skip, _take).AsEnumerable();
        }
        public int getPatientsClaimByNameCount(string SearchText)
        {
            SqlParameter _searchText = new SqlParameter("@SearchText", SearchText);
            return db.Database.SqlQuery<int>(StoredProcedureConst.PatientRepositoryProcedure.GetPatientsClaimByNameCount, _searchText).FirstOrDefault();
        }
        public IEnumerable<BLModel.PatientClaimDiagnose> getPatientClaimDiagnoseByPatientClaimId(int PatientClaimID, int Skip, int Take)
        {
            SqlParameter _patientClaimID = new SqlParameter("@PatientClaimID", PatientClaimID);
            SqlParameter _skip = new SqlParameter("@Skip", Skip);
            SqlParameter _take = new SqlParameter("@Take", Take);
            return db.Database.SqlQuery<BLModel.PatientClaimDiagnose>(StoredProcedureConst.PatientRepositoryProcedure.GetPatientClaimDiagnoseByPatientClaimId, _patientClaimID, _skip, _take).ToList();
        }
        public int getPatientClaimDiagnoseByPatientClaimIdCount(int PatientClaimID)
        {
            SqlParameter _patientClaimID = new SqlParameter("@PatientClaimID", PatientClaimID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.PatientRepositoryProcedure.GetPatientClaimDiagnoseByPatientClaimIdCount, _patientClaimID).FirstOrDefault();
        }
       
        #endregion

        #region RFA- URHistory
        public IEnumerable<BLModel.PatientHistory> getPatientHistoryByPatientID(int _PatientID, int skip, int take, string sortBy, string order)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", _PatientID);
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            SqlParameter _sortBy = new SqlParameter("@sortBy", sortBy);
            SqlParameter _order = new SqlParameter("@order", order);
            return db.Database.SqlQuery<BLModel.PatientHistory>(StoredProcedureConst.IntakeRepositoryProcedure.GetPatientHistoryByPatientID, _patientID, _skip, _take, _sortBy, _order).ToList();
        }
        public int getPatientHistoryByPatientIDCount(int patientID)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.IntakeRepositoryProcedure.GetPatientHistoryByPatientIDCount, _patientID).SingleOrDefault();
        }
        #endregion
        #region RequestHistory
        public IEnumerable<BLModel.RequestHistory> getRequestHistoryByRFARequestID(int _RequestID, int skip, int take)
        {
            SqlParameter _requestID = new SqlParameter("@RFARequestID", _RequestID);
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            return db.Database.SqlQuery<BLModel.RequestHistory>(StoredProcedureConst.IntakeRepositoryProcedure.GetRequestHistoryByRFARequestID, _requestID, _skip, _take).ToList();
        }
        public int getRequestHistoryByRFARequestIDCount(int _RequestID)
        {
            SqlParameter _requestID = new SqlParameter("@RFARequestID", _RequestID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.IntakeRepositoryProcedure.GetRequestHistoryByRFARequestIDCount, _requestID).SingleOrDefault();
        }
        #endregion
        #region Duplicate  case
        public IEnumerable<BLModel.RequestByReferralID> getRequestForDuplicateDecision(int patientClaimID, int skip, int take)
        {
            SqlParameter _patientClaimID = new SqlParameter("@patientClaimID", patientClaimID);
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            return db.Database.SqlQuery<BLModel.RequestByReferralID>(StoredProcedureConst.IntakeRepositoryProcedure.GetRequestForDuplicateDecision, _patientClaimID, _skip, _take).ToList();
        }
        public int getRequestForDuplicateDecisionCount(int patientClaimID)
        {
            SqlParameter _patientClaimID = new SqlParameter("@patientClaimID", patientClaimID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.IntakeRepositoryProcedure.GetRequestForDuplicateDecisionCount, _patientClaimID).SingleOrDefault();
        }
        #endregion

        #region Patient(s) Notes
        public IEnumerable<BLModel.NotesDetail> getNotesByPatientID(int patientID, int skip, int take)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            SqlParameter _skip = new SqlParameter("@Skip", skip);
            SqlParameter _take = new SqlParameter("@Take", take);
            return db.Database.SqlQuery<BLModel.NotesDetail>(StoredProcedureConst.PatientRepositoryProcedure.GetNotesByPatientID, _patientID, _skip, _take).ToList();
        }
        public int getNotesByPatientIDCount(int patientID)
        {
            SqlParameter _patientID = new SqlParameter("@PatientID", patientID);
            return db.Database.SqlQuery<int>(StoredProcedureConst.PatientRepositoryProcedure.GetNotesByPatientIDCount, _patientID).FirstOrDefault();
        }
        #endregion

        #region IMR
        public void CreateRFARefferalByRFARefferalID(int rFAReferralID, string rFARequestIDs, int flag)
        {

            SqlParameter _rFARequestID = new SqlParameter("@RFAReferralID", rFAReferralID);
            SqlParameter _rFARequestIDs = new SqlParameter("@RFARequestIDs", rFARequestIDs);
            SqlParameter _flag = new SqlParameter("@Flag", flag);
            db.Database.SqlQuery<BLModel.Client>(StoredProcedureConst.IMRRepositoryProcedure.CreateRFARefferalByRFARefferalID, _rFARequestID,_rFARequestIDs, _flag).FirstOrDefault();
        } 
        public IEnumerable<BLModel.RFAReferralFile> getMedicalAndLegalDocsByReferralID(int ReferralID)
        {
            SqlParameter _RFAReferralID = new SqlParameter("@RFAReferralID", ReferralID);
           return db.Database.SqlQuery<BLModel.RFAReferralFile>(StoredProcedureConst.IMRRepositoryProcedure.GetMedicalAndLegalDocsByReferralID, _RFAReferralID).ToList();
        }

        public IEnumerable<BLModel.RequestIMRRecord> getIMRReferralByProcessLevel(int skip, int take, string  processLevel)
        {
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            SqlParameter _processLevel = new SqlParameter("@processLevel", processLevel);
            return db.Database.SqlQuery<BLModel.RequestIMRRecord>(StoredProcedureConst.IMRRepositoryProcedure.GetIMRLandingReferralsByProcessLevels, _skip, _take, _processLevel);
        }
        public int getIMRReferralCountByProcessLevel(string processLevel)
        {
            SqlParameter _processLevel = new SqlParameter("@processLevel", processLevel);
            return db.Database.SqlQuery<int>(StoredProcedureConst.IMRRepositoryProcedure.GetIMRLandingReferralsByProcessLevelsCount, _processLevel).SingleOrDefault();
        }

        public IEnumerable<BLModel.RequestIMRRecord> getIMRReferralByProcessLevelnPatientClaim(int skip, int take, int processLevel,int processLevel2, string patientClaim)
        {
            SqlParameter _skip = new SqlParameter("@skip", skip);
            SqlParameter _take = new SqlParameter("@take", take);
            SqlParameter _processLevel = new SqlParameter("@processLevel", processLevel);
            SqlParameter _processLevel2 = new SqlParameter("@processLevel2", processLevel2);
            SqlParameter _patientClaim = new SqlParameter("@patientClaim", patientClaim);
            return db.Database.SqlQuery<BLModel.RequestIMRRecord>(StoredProcedureConst.IMRRepositoryProcedure.GetIMRReferralByProcessLevelnPatientClaim, _skip, _take, _processLevel,_processLevel2, _patientClaim);
        }
        public int getIMRReferralByProcessLevelnPatientClaimCount(int processLevel, int processLevel2, string patientClaim)
        {
            SqlParameter _processLevel = new SqlParameter("@processLevel", processLevel);
            SqlParameter _processLevel2 = new SqlParameter("@processLevel2", processLevel2);
            SqlParameter _patientClaim = new SqlParameter("@patientClaim", patientClaim);
            return db.Database.SqlQuery<int>(StoredProcedureConst.IMRRepositoryProcedure.GetIMRReferralByProcessLevelnPatientClaimCount, _processLevel, _processLevel2, _patientClaim).SingleOrDefault();
        }

        public IEnumerable<BLModel.RFAReferralFile> getIMRLetters(int RFAReferralID) 
        { 
            SqlParameter _referralID = new SqlParameter("@ReferralID",RFAReferralID);
            return db.Database.SqlQuery<BLModel.RFAReferralFile>(StoredProcedureConst.IMRRepositoryProcedure.GetIMRLetters, _referralID);
        }

        public BLModel.IMRDecisionReferralDetails getIMRDecisionPageDetailsByReferralID(int RFAReferralID)
        {
            SqlParameter _referralID = new SqlParameter("@RFAReferralID", RFAReferralID);
            return db.Database.SqlQuery<BLModel.IMRDecisionReferralDetails>(StoredProcedureConst.IMRRepositoryProcedure.GetIMRDecisionPageDetailsByReferralID, _referralID).SingleOrDefault();
        }

        public IEnumerable<BLModel.IMRDecisionRequestDetails> getIMRDecisionPageRequestDetailsByReferralID(int RFAReferralID)
        {
            SqlParameter _referralID = new SqlParameter("@RFAReferralID", RFAReferralID);
            return db.Database.SqlQuery<BLModel.IMRDecisionRequestDetails>(StoredProcedureConst.IMRRepositoryProcedure.GetIMRDecisionPageRequestDetailsByReferralID, _referralID).ToList();
        }
        #endregion

        #region DeterminationLetter
        public string getDeterminationLetterDecisionDesc(int rfaReferralID)
        {
            SqlParameter _rfaReferralID = new SqlParameter("@referralID", rfaReferralID);
            return db.Database.SqlQuery<string>(StoredProcedureConst.IntakeRepositoryProcedure.GetDeterminationLetterDecisionDesc, _rfaReferralID).SingleOrDefault();
        }
        #endregion

        #region RFAReferralAdditionalInfo
        public void SaveUpdateRFAReferralAdditionalInfo(RFAReferralAdditionalInfo _rfaReferralAdditionalInfo)
        {
            SqlParameter _rfaReferralID = new SqlParameter("@RFAReferralID", _rfaReferralAdditionalInfo.RFAReferralID);
            SqlParameter _originalRFAReferralID = new SqlParameter("@OriginalRFAReferralID", _rfaReferralAdditionalInfo.OriginalRFAReferralID);
            SqlParameter _userId = new SqlParameter("@UserId", _rfaReferralAdditionalInfo.UserId);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.IntakeRepositoryProcedure.SaveUpdateRFAReferralAdditionalInfo, _rfaReferralID, _originalRFAReferralID, _userId);
        }
        #endregion

        #region RFAReferralAdditionalLink
        public void AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID(RFAReferralAdditionalLink _RFAReferralAdditionalLink)
        {
            SqlParameter _rfaReferralID = new SqlParameter("@RFAReferralID", _RFAReferralAdditionalLink.RFAReferralID);
            SqlParameter _clientStatementID = new SqlParameter("@ClientStatementID", _RFAReferralAdditionalLink.ClientStatementID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.ReferralRepositoryProcedure.AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID, _rfaReferralID, _clientStatementID);
        }

        public void AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID(RFAReferralAdditionalLink _RFAReferralAdditionalLink)
        {
            SqlParameter _rfaReferralID = new SqlParameter("@RFAReferralID", _RFAReferralAdditionalLink.RFAReferralID);
            SqlParameter _rfareferralInvoiceID = new SqlParameter("@RFAReferralInvoiceID", _RFAReferralAdditionalLink.RFAReferralInvoiceID);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.ReferralRepositoryProcedure.AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID, _rfaReferralID, _rfareferralInvoiceID);
        }
        #endregion

        #region EmailRequest
        public void AddEmailRecordAndRFARequestLinkByRFAReferralID(int RFAReferralID, int EmailRecordId)
        {
            SqlParameter _rFAReferralID = new SqlParameter("@RFAReferralID", RFAReferralID);
            SqlParameter _emailRecordId = new SqlParameter("@EmailRecordId", EmailRecordId);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.EmailRecordAttachmentProcedure.AddEmailRecordAndRFARequestLinkByRFAReferralID, _rFAReferralID, _emailRecordId);
        }

        public void AddEmailRecordAndRFARequestLinkByRFITimeExtension(int RFAReferralID,int RFAReferralFileID, int EmailRecordId)
        {
            SqlParameter _rFAReferralID = new SqlParameter("@RFAReferralID", RFAReferralID);
            SqlParameter _rFAReferralFileID = new SqlParameter("@RFAReferralFileID", RFAReferralFileID);
            SqlParameter _emailRecordId = new SqlParameter("@EmailRecordId", EmailRecordId);
            db.Database.ExecuteSqlCommand(StoredProcedureConst.EmailRecordAttachmentProcedure.AddEmailRecordAndRFARequestLinkByRFITimeExtension, _rFAReferralID, _rFAReferralFileID, _emailRecordId);
        }
        #endregion









    }
}
