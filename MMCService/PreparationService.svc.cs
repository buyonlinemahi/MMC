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
    public class PreparationService : IPreparationService
    {
        private readonly IPreparationRepository _preparationRepository;
        public PreparationService(IPreparationRepository preparationRepository)
        {
            _preparationRepository = preparationRepository;
        }

        #region ClinicalTriage
        public DTO.Paged.PagedClinicalTriage getReferralByProcessLevel(int skip, int take, int processLevel)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.ClinicalTriage, DTO.Paged.PagedClinicalTriage>(_preparationRepository.getReferralByProcessLevel(skip,take,processLevel));
        }
        #endregion

        #region ReferralProcess
        public DTO.PatientAndRequestModel getPatientAndRequestForNotificationByReferralId(int _referralID,int _skip,int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.PatientAndRequestModel, DTO.PatientAndRequestModel>(_preparationRepository.getPatientAndRequestForNotificationByReferralId(_referralID, _skip,_take));
        }
        public DTO.PatientAndRequestModel getPatientAndRequestByReferralId(int _referralID)
        {
            return Mapper.Map<MMC.Core.BL.Model.PatientAndRequestModel, DTO.PatientAndRequestModel>(_preparationRepository.getPatientAndRequestByReferralId(_referralID));
        }
        public int updateProcessLevel(int _referralID, int _processLevel)
        {
            return _preparationRepository.updateProcessLevel(_referralID, _processLevel);
        }
        public IEnumerable<DTO.RequestByReferralID> getAllRequestByReferralID(int _referralID)
        {
            return Mapper.Map<IEnumerable<DTO.RequestByReferralID >>(_preparationRepository.getAllRequestByReferralID(_referralID));
        }
        public int getRemainingRequest(int _referralID)
        {
            return _preparationRepository.getRemainingRequest(_referralID);
        }
        public int getRFANewReferralIDFromRFAOldReferralID(int RFAReferralID, int DecisionID)
        {
            return _preparationRepository.getRFANewReferralIDFromRFAOldReferralID(RFAReferralID, DecisionID);
        }
        public int DeleteRFAReferralIDFromReferralFiles(int ReferralID, int RFAFileTypeID)
        {
            return _preparationRepository.DeleteRFAReferralIDFromReferralFiles(ReferralID, RFAFileTypeID);
        }
        #endregion

        #region AdditionalInfo
        public int addRFAAdditionalInfo(DTO.RFAAdditionalInfo rfaAddiotionalInfo)
        {
            return _preparationRepository.addRFAAdditionalInfo(Mapper.Map<DTO.RFAAdditionalInfo,MMC.Core.Data.Model.RFAAdditionalInfo>(rfaAddiotionalInfo));
        }
        public int updateRFAAdditionalInfo(DTO.RFAAdditionalInfo rfaAddiotionalInfo)
        {
            return _preparationRepository.updateRFAAdditionalInfo(Mapper.Map<DTO.RFAAdditionalInfo,MMC.Core.Data.Model.RFAAdditionalInfo>(rfaAddiotionalInfo));
        }
        public DTO.Paged.PagedRFAAdditionalInfo getAllRFAAdditionalInfo(int ReferralID,int skip,int take)
        {
            return Mapper.Map<DTO.Paged.PagedRFAAdditionalInfo>(_preparationRepository.getAllRFAAdditionalInfo(ReferralID, skip, take));
        }
        public DTO.RFAAdditionalInfo getRFAAdditionalInfoById(int id)
        {
            return Mapper.Map<DTO.RFAAdditionalInfo>(_preparationRepository.getRFAAdditionalInfoById(id));
        }
        public void UpdateRFAAdditionalInfoDetailByRequestID(int oldRFAReferralID, int rFARequestID)
        {
            _preparationRepository.UpdateRFAAdditionalInfoDetailByRequestID(oldRFAReferralID, rFARequestID);
        }
        #endregion

        #region RFADecision
        public int updateDecisionByRequestID(DTO.RFARequest rfaRequest)
        {
            return _preparationRepository.updateDecisionByRequestID(Mapper.Map<MMC.Core.Data.Model.RFARequest>(rfaRequest));
        }
        //public int updateNotesByReferralID(DTO.RFAReferral rfaReferral)
        //{
        //    return _preparationRepository.updateNotesByReferralID(Mapper.Map < MMC.Core.Data.Model.RFAReferral>(rfaReferral));
        //}

        public void AddRFITemplateRecordByRFARequestID(int _rFAReferralFileID, int _userID)
        {
            _preparationRepository.AddRFITemplateRecordByRFARequestID(_rFAReferralFileID, _userID);
        }

        public void AddRFARequestTimeExtensionRecordByRFARequestID(int _rFAReferralFileID, int _userID)
        {
            _preparationRepository.AddRFARequestTimeExtensionRecordByRFARequestID(_rFAReferralFileID, _userID);
        } 

        #endregion

        #region BodyParts
        public string getAcceptedBodyPartsByReferralID(int _referralID)
        {
            return _preparationRepository.getAcceptedBodyPartsByReferralID(_referralID);
        }
        
        public string getDeniedBodyPartsByReferralID(int _referralID)
        {
            return _preparationRepository.getDeniedBodyPartsByReferralID(_referralID);
        }
        public string getDelayedBodyPartByReferralID(int _referralID)
        {
            return _preparationRepository.getDelayedBodyPartByReferralID(_referralID);
        }
        public string getDignosisByReferralID(int _referralID)
        {
            return _preparationRepository.getDignosisByReferralID(_referralID);
        }
        #endregion
        
        #region ReferralMedicalRecord

        public DTO.Paged.PagedPatientMedicalRecordByPatientID getReferralMedicalRecordByPatientID(int _patientID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedPatientMedicalRecordByPatientID>(_preparationRepository.getReferralMedicalRecordByPatientID(_patientID, _skip, _take));
        }

        #endregion
    }
}
