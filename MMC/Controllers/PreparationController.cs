using AutoMapper;
using MMCApp.Domain.Models.ClinicalTriage;
using MMCApp.Domain.Models.ClinicalTriageModel;
using MMCApp.Domain.Models.CommonModel;
using MMCApp.Domain.Models.DurationTypeModel;
using MMCApp.Domain.Models.EmailRecordModel;
using MMCApp.Domain.Models.IntakeModel;
using MMCApp.Domain.Models.MedicalRecordReview;
using MMCApp.Domain.Models.PatientModel;
using MMCApp.Domain.Models.RFADiagnosisModel;
using MMCApp.Domain.Models.RFAMergedReferralHistory;
using MMCApp.Domain.Models.RFASplittedReferralHistory;
using MMCApp.Domain.Models.StorageModel;
using MMCApp.Domain.Models.UserModel;
using MMCApp.Domain.ViewModels.ClinicalTriageViewModel;
using MMCApp.Domain.ViewModels.MedicalRecordReviewViewModel;
using MMCApp.Domain.ViewModels.PatientViewModel;
using MMCApp.Domain.ViewModels.RFADiagnosisViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.ApplicationServices;
using MMCApp.Infrastructure.Base;
using MMCApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using serviceModel = MMC.MMCService;
namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class PreparationController : BaseController
    {
        MMCService.PatientService.PatientServiceClient _iPatientService;
        MMCService.IntakeService.IntakeServiceClient _iIntakeService;
        MMCService.PreparationService.PreparationServiceClient _iPreparationService;
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.LetterService.LetterServiceClient _iLetterServiceClient;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        MMCService.UserService.UserServiceClient _iUserService;
        MMCService.EmailRecordAttachmentService.EmailRecordAttachmentServiceClient _iEmailRecordAttachmentService;


        public readonly StorageService _storageService;

        public readonly EMailService _mailService;
        public PreparationController()
        {
            _iPreparationService = new MMCService.PreparationService.PreparationServiceClient();
            _iPatientService = new MMCService.PatientService.PatientServiceClient();
            _iIntakeService = new MMCService.IntakeService.IntakeServiceClient();
            _storageService = new StorageService();
            _iPaticipantService = new MMCService.PaticipantService.PaticipantServiceClient();
            _iLetterServiceClient = new MMCService.LetterService.LetterServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
            _mailService = new EMailService();
            _iUserService = new MMCService.UserService.UserServiceClient();
            _iEmailRecordAttachmentService = new MMCService.EmailRecordAttachmentService.EmailRecordAttachmentServiceClient();
        }
        public ActionResult Index(int? skip)
        {
            getNoOfReferralAccordingToProcessLevels();

            if (skip == null)
            {
                var _clinicalTriageDetails = _iPreparationService.getReferralByProcessLevel(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, GlobalConst.ProcessLevel.Preparation);
                ClinicalTriageViewModel clinicalTriage = new ClinicalTriageViewModel();
                clinicalTriage.ClinicalTriages = Mapper.Map<IEnumerable<ClinicalTriage>>(_clinicalTriageDetails.ClinicalTriages);
                clinicalTriage.TotalCount = _clinicalTriageDetails.TotalCount;
                return View(clinicalTriage);
            }
            else
            {
                var _clinicalTriageDetails = _iPreparationService.getReferralByProcessLevel(skip.Value, GlobalConst.Records.LandingTake, GlobalConst.ProcessLevel.Preparation);
                ClinicalTriageViewModel clinicalTriage = new ClinicalTriageViewModel();
                clinicalTriage.ClinicalTriages = Mapper.Map<IEnumerable<ClinicalTriage>>(_clinicalTriageDetails.ClinicalTriages);
                clinicalTriage.TotalCount = _clinicalTriageDetails.TotalCount;
                return Json(clinicalTriage, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ClinicalTriage(int? skip)
        {
            getNoOfReferralAccordingToProcessLevels();

            var _clinicalTriageDetails = _iPreparationService.getReferralByProcessLevel((skip == null ? GlobalConst.Records.Skip : (int)skip), GlobalConst.Records.LandingTake, GlobalConst.ProcessLevel.Clinical);
            ClinicalTriageViewModel clinicalTriage = new ClinicalTriageViewModel();
            clinicalTriage.ClinicalTriages = Mapper.Map<IEnumerable<ClinicalTriage>>(_clinicalTriageDetails.ClinicalTriages);
            clinicalTriage.TotalCount = _clinicalTriageDetails.TotalCount;
            clinicalTriage.ProcessLevel = GlobalConst.ProcessLevel.Clinical;
            if (skip == null)
                return View(clinicalTriage);
            else
                return Json(clinicalTriage, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ClinicalAudit(int? skip)
        {
            getNoOfReferralAccordingToProcessLevels();

            var _clinicalAuditDetails = _iPreparationService.getReferralByProcessLevel((skip == null ? GlobalConst.Records.Skip : (int)skip), GlobalConst.Records.LandingTake, GlobalConst.ProcessLevel.ClinicalAudit);
            ClinicalTriageViewModel clinicalTriage = new ClinicalTriageViewModel();
            clinicalTriage.ClinicalTriages = Mapper.Map<IEnumerable<ClinicalTriage>>(_clinicalAuditDetails.ClinicalTriages);
            clinicalTriage.TotalCount = _clinicalAuditDetails.TotalCount;
            clinicalTriage.ClinicalTriages.ToList().ForEach(hp => hp.pageURL = createURLforClinicalAudit(hp.RFAReferralID, GlobalConst.ProcessLevel.ClinicalAudit));
            if (skip == null)
                return View(clinicalTriage);
            else
                return Json(clinicalTriage, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        private string createURLforClinicalAudit(int Id, int ProcessLevel)
        {
            return Url.Action(GlobalConst.Actions.PreparationController.ClinicalTriageAction, GlobalConst.Controllers.Preparation,
                         new { Id = Id, ProcessLevel = ProcessLevel });
        }
        public ActionResult PreparationAction(int id)
        {
            getNoOfReferralAccordingToProcessLevels();

            HttpCookie cookie = new HttpCookie(GlobalConst.ConstantName.PreviousAttachementLinks);
            Editor EditorNote = new Editor(System.Web.HttpContext.Current, GlobalConst.Richtexteditor.EditorNote);
            EditorNote.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            EditorNote.Width = Unit.Pixel(1050);
            EditorNote.Height = Unit.Pixel(660);
            EditorNote.ResizeMode = RTEResizeMode.Disabled;
            EditorNote.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            EditorNote.MvcInit();
            ViewData[GlobalConst.Richtexteditor.EditorNote] = EditorNote.MvcGetString();
            PatientAndRequestModel _patientAndRequestModel = new PatientAndRequestModel();
            _patientAndRequestModel = Mapper.Map<PatientAndRequestModel>(_iPreparationService.getPatientAndRequestByReferralId(id));
            _patientAndRequestModel.ReferralFileNotification = getReferralFileByRFAReferralIDAndFileTypeID(id, GlobalConst.FileType.RFIPreparationLetter);
            _patientAndRequestModel.ReferralFileTimeExtensionPNAndProofOfService = getAllRFAReferralFilesAccToReferralIDInTimeExtensionPNorProofOfService(id);
            //_patientAndRequestModel.ProcessLevelCheck = _iCommonService.getProcessLevelsByReferralID(id).Where(rk => rk.ProcessLevel <= GlobalConst.ProcessLevel.Notifications).Count() > 0 ? 1 : 0;
            return View(_patientAndRequestModel);
        }
        public ActionResult GetAllReferralFileByRFAReferralIDAndFileTypeID(int _rfarefferalid, int _filetypeid)
        {
            return Json(getReferralFileByRFAReferralIDAndFileTypeID(_rfarefferalid, _filetypeid), GlobalConst.ContentTypes.TextHtml);
        }
        private IEnumerable<RFAReferralFile> getReferralFileByRFAReferralIDAndFileTypeID(int _rfarefferalid, int _filetypeid)
        {
            IEnumerable<RFAReferralFile> ReferralFileNotification = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIntakeService.getReferralFileByRFAReferralIDAndFileTypeID(_rfarefferalid, GlobalConst.FileType.RFIPreparationLetter));
            if (ReferralFileNotification != null)
            {
                StorageModel _storageModel = new StorageModel();
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_rfarefferalid, GlobalConst.ConstantChar.Char_R));
                _storageModel.path = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
                _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                foreach (RFAReferralFile _RFAReferralFile in ReferralFileNotification)
                {
                    _RFAReferralFile.RFAReferralFileFullPath = _storageModel.path + _storageModel.ClientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.PatientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ClaimID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ReferralID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.FolderName + GlobalConst.ConstantChar.ForwardSlash + _RFAReferralFile.RFAReferralFileName;
                }
            }
            return ReferralFileNotification;
        }

        [HttpPost]
        public ActionResult GetAllRFAReferralFilesAccToReferralIDInTimeExtensionPNAndProofOfService(int _rfaReferralID)
        {
            return Json(getAllRFAReferralFilesAccToReferralIDInTimeExtensionPNorProofOfService(_rfaReferralID), GlobalConst.ContentTypes.TextHtml);
        }

        private IEnumerable<RFAReferralFile> getAllRFAReferralFilesAccToReferralIDInTimeExtensionPNorProofOfService(int _rfarefferalid)
        {
            IEnumerable<RFAReferralFile> ReferralFileTimeExtensionPNAndProofOfService = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIntakeService.getRFAReferralFilesAccToReferralIDInPreparationCase(_rfarefferalid));
            if (ReferralFileTimeExtensionPNAndProofOfService != null)
            {
                StorageModel _storageModel = new StorageModel();
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_rfarefferalid, GlobalConst.ConstantChar.Char_R));
                _storageModel.path = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
                _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                foreach (RFAReferralFile _RFAReferralFile in ReferralFileTimeExtensionPNAndProofOfService)
                {
                    _RFAReferralFile.RFAReferralFileFullPath = _storageModel.path + _storageModel.ClientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.PatientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ClaimID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ReferralID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.FolderName + GlobalConst.ConstantChar.ForwardSlash + _RFAReferralFile.RFAReferralFileName;
                }
            }
            return ReferralFileTimeExtensionPNAndProofOfService;
        }
        
        [HttpPost]
        public ActionResult SavePatientClaimDiagnose(PatientClaimDiagnose PatientClaimDiagnoseDetails)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                _Result = _iPatientService.updatePatientClaimDiagnose(Mapper.Map<MMCService.PatientService.PatientClaimDiagnose>(PatientClaimDiagnoseDetails));
            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(GlobalConst.Message.SaveMessage);
        }
        [HttpPost]
        public ActionResult PreparationActionDetail(int id)
        {
            PatientAndRequestModel _patientAndRequestModel = new PatientAndRequestModel();
            _patientAndRequestModel = Mapper.Map<PatientAndRequestModel>(_iPreparationService.getPatientAndRequestByReferralId(id));
            return Json(_patientAndRequestModel);
        }
        public JsonResult GetAcceptedBodyPartByReferralID(int _referralId)
        {
            return Json(_iPreparationService.getAcceptedBodyPartsByReferralID(_referralId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDeniedBodyPartByReferralID(int _referralId)
        {
            return Json(_iPreparationService.getDeniedBodyPartsByReferralID(_referralId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDelayedBodyPartByReferralID(int _referralId)
        {
            return Json(_iPreparationService.getDelayedBodyPartByReferralID(_referralId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDiagnosisByReferralID(int _referralId)
        {
            return Json(_iPreparationService.getDignosisByReferralID(_referralId), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult PreparationActionProcess(RFAReferral rfaReferral)
        {

            int _newDuplicateRFAReferralID = 0;
            int _newDeferralRFAReferralID = 0;
            int _newUnableToReviewRFAReferralID = 0;
            int _newClientAuthorizedRFAReferralID = 0;
            int _newWithdrawnRFAReferralID = 0;
            int newRFAReferralid = 0;
            List<string> _listURlDownloadInitial = new List<string>();
            List<string> _listTemp = new List<string>(new string[] {GlobalConst.ConstantName.DeferralInitial, GlobalConst.ConstantName.DuplicateInitial, GlobalConst.ConstantName.UnabletoReviewInitial, GlobalConst.ConstantName.ClientAuthorizedInitial});
            HttpCookie cookie = new HttpCookie(GlobalConst.ConstantName.PreviousAttachementLinks);

            IEnumerable<RequestByReferralID> requestByReferral = Mapper.Map<IEnumerable<RequestByReferralID>>(_iPreparationService.getAllRequestByReferralID(rfaReferral.RFAReferralID));
            var duplicateRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Duplicate);
            var DeferralRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Deferral);
            var UnabletoReview = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.UnabletoReview);
            var PeerReview = requestByReferral.Count(s => s.RFAStatus == GlobalConst.RFAStatus.PeerReview);
            var clientAuthorized = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.ClientAuthorized);
            var withdrawn = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Withdrawn);
            var otherRequest = (from request in requestByReferral
                                where request.DecisionID != GlobalConst.Decision.Duplicate && request.DecisionID != GlobalConst.Decision.UnabletoReview && request.DecisionID != GlobalConst.Decision.Deferral && request.RFAStatus != GlobalConst.RFAStatus.PeerReview
                                && request.DecisionID != GlobalConst.Decision.ClientAuthorized && request.DecisionID != GlobalConst.Decision.Withdrawn
                                select request).Count();

            if (requestByReferral.Count() == otherRequest)
                _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.Clinical);
            else if (requestByReferral.Count() == clientAuthorized)// US#2715 by mahi for Client Authorized
            {
                _newClientAuthorizedRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.ClientAuthorized);
                if (_newClientAuthorizedRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInWithdrawn(requestByReferral, rfaReferral);
                }
                else
                {
                    _iPreparationService.updateProcessLevel(_newClientAuthorizedRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessPreparationWithdrawnRequest(_newClientAuthorizedRFAReferralID, request.RFARequestID);

                    }
                }
                if (rfaReferral.RFAReferralID != newRFAReferralid && _newClientAuthorizedRFAReferralID != 0)
                {
                    ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                    ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                    UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID, "ClientAuthorized");
                }
            }
            else if (requestByReferral.Count() == PeerReview)
            {
                newRFAReferralid = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.ConstantChar.Zero);
                if (newRFAReferralid == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInPeerReview(requestByReferral, rfaReferral);
                }
                else
                {                       
                    _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.PeerReview);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessPreparationRequest(newRFAReferralid, request.RFARequestID);
                       
                    }
                }

                if (rfaReferral.RFAReferralID != newRFAReferralid)
                {
                    ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                    ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                }   
            }
            else if (requestByReferral.Count() == withdrawn) // Withdrawn
            {
                _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInWithdrawn(requestByReferral, rfaReferral);
                }
                else
                {
                    _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);

                    }
                }
                if (rfaReferral.RFAReferralID != newRFAReferralid && _newWithdrawnRFAReferralID != 0)
                {
                    ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                    ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                    UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                }
            }           
            else if (requestByReferral.Count() == duplicateRequest && requestByReferral.Count() == 1)
            {
                _newDuplicateRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Duplicate);
                if (_newDuplicateRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInNotification(requestByReferral, rfaReferral);
                }
                else
                {
                    Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DuplicateRFAReferralID] = _newDuplicateRFAReferralID.ToString();
                    rfaReferral.RFAReferralID = _newDuplicateRFAReferralID;
                    var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDuplicateRFAReferralID, GlobalConst.FileType.DuplicateLetter);
                    var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDuplicateRFAReferralID, GlobalConst.FileType.InitialNotification);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessOnlyOneUnableToReviewDuplicateDeferral(request, _newDuplicateRFAReferralID, request.RFAReferralID);
                    }
                }
                DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.DuplicateLetter);
                TempData[GlobalConst.ConstantName.DuplicateInitial] = DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.InitialNotification);
            }
            else if (requestByReferral.Count() == DeferralRequest && requestByReferral.Count() == 1)
            {
                _newDeferralRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Deferral);
                if (_newDeferralRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInNotification(requestByReferral, rfaReferral);
                }
                else
                {
                    Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DeferralRFAReferralID] = _newDeferralRFAReferralID.ToString();
                    rfaReferral.RFAReferralID = _newDeferralRFAReferralID;
                    var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDeferralRFAReferralID, GlobalConst.FileType.DeferralLetter);
                    var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDeferralRFAReferralID, GlobalConst.FileType.InitialNotification);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessOnlyOneUnableToReviewDuplicateDeferral(request, _newDeferralRFAReferralID, request.RFAReferralID);
                    }
                }
                DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.DeferralLetter);
                //new code
                TempData[GlobalConst.ConstantName.DeferralInitial] = DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.InitialNotification);

            }
            else if (requestByReferral.Count() == UnabletoReview && requestByReferral.Count() == 1)
            {
                _newUnableToReviewRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.UnabletoReview);
                if (_newUnableToReviewRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInNotification(requestByReferral, rfaReferral);
                }
                else
                {
                    Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.UnableToReviewRFAReferralID] = _newUnableToReviewRFAReferralID.ToString();
                    rfaReferral.RFAReferralID = _newUnableToReviewRFAReferralID;
                    var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newUnableToReviewRFAReferralID, GlobalConst.FileType.NoRFALetter);
                    var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newUnableToReviewRFAReferralID, GlobalConst.FileType.InitialNotification);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessOnlyOneUnableToReviewDuplicateDeferral(request, _newUnableToReviewRFAReferralID, request.RFAReferralID);
                    }
                }
                DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.NoRFALetter);
                TempData[GlobalConst.ConstantName.UnabletoReviewInitial] = DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.InitialNotification);
            }
            else if (requestByReferral.Count() == clientAuthorized && requestByReferral.Count() == 1)// US#2715 by mahi for Client Authorized
            {
                _newClientAuthorizedRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.ClientAuthorized);
                if (_newClientAuthorizedRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInWithdrawn(requestByReferral, rfaReferral);
                }
                else
                {
                    _iPreparationService.updateProcessLevel(_newClientAuthorizedRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessPreparationWithdrawnRequest(_newClientAuthorizedRFAReferralID, request.RFARequestID);

                    }
                }
                if (rfaReferral.RFAReferralID != _newClientAuthorizedRFAReferralID && _newClientAuthorizedRFAReferralID != 0)
                {
                    ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                    ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                    UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID, "ClientAuthorized");
                }         
            }
            else if (requestByReferral.Count() == withdrawn && requestByReferral.Count() == 1)// Withdrawn
            {
                _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInWithdrawn(requestByReferral, rfaReferral);
                }
                else
                {
                    _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);

                    }
                }
                if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID !=0)
                {
                    ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                    ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                    UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                }               
            }
            else if (otherRequest != 0)
                ProcessPrepationRequestwhenotherRequestNotzero(requestByReferral, rfaReferral);
            else if (DeferralRequest != 0)
                ProcessDefferalRequest(requestByReferral, rfaReferral, GlobalConst.ConstantChar.ScreenPrepration);
            else if (duplicateRequest != 0)
                ProcessDuplicateRequest(requestByReferral, rfaReferral, GlobalConst.ConstantChar.ScreenPrepration);
            else if (UnabletoReview != 0)
                ProcessUnableToReviewRequest(requestByReferral, rfaReferral, GlobalConst.ConstantChar.ScreenPrepration);
            else if (clientAuthorized != 0)
                ProcessClientAuthorizedRequest(requestByReferral, rfaReferral);

            foreach (var _nameUrl in _listTemp)// For attachement of email
            {
                string _urlData = Convert.ToString(TempData[_nameUrl]);
                _urlData = _urlData.Replace("(", "");
                _urlData = _urlData.Replace(")", "");
                if (_urlData != "")
                {
                    Tuple<string, string> t = CombinedLinkForAttachement(_urlData);
                    string fullPathFile = t.Item1 + "," + t.Item2;
                    _listURlDownloadInitial.Add(fullPathFile);
                    if (_nameUrl == GlobalConst.ConstantName.DeferralInitial)
                    {
                        Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCDeferral] = fullPathFile;
                    }
                    else if (_nameUrl == GlobalConst.ConstantName.DuplicateInitial)
                    {
                        Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCDuplicate] = fullPathFile;
                    }
                    else if (_nameUrl == GlobalConst.ConstantName.UnabletoReviewInitial)
                    {
                        Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCUnabletoReview] = fullPathFile;
                    }
                    else if (_nameUrl == GlobalConst.ConstantName.ClientAuthorizedInitial)
                    {
                        Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCClientAuthorized] = fullPathFile;
                    }
                }
            }
            return Json(_listURlDownloadInitial);
        }
        #region ClinicalTriageActionProcess
        [HttpPost]
        public ActionResult ClinicalTriageActionProcess(RFAReferral rfaReferral)
        {
            int _newDuplicateRFAReferralID = 0;
            int _newDeferralRFAReferralID = 0;
            int _newUnableToReviewRFAReferralID = 0;
            int newRFAReferralid = 0;
            int _newWithdrawnRFAReferralID = 0;
            bool IsNotificationProcessLevel = false;

            List<string> _listURlDownloadInitial = new List<string>();
            List<string> _listTemp = new List<string>(new string[] {GlobalConst.ConstantName.DeferralInitial, GlobalConst.ConstantName.DuplicateInitial, GlobalConst.ConstantName.UnabletoReviewInitial, GlobalConst.ConstantName.AuditInitial});
            HttpCookie cookie = new HttpCookie(GlobalConst.ConstantName.PreviousAttachementLinks);

            IEnumerable<RequestByReferralID> requestByReferral = Mapper.Map<IEnumerable<RequestByReferralID>>(_iPreparationService.getAllRequestByReferralID(rfaReferral.RFAReferralID));
            var PrepartionRequest = requestByReferral.Count(s => s.RFAStatus == GlobalConst.RFAStatus.SendToPreparation);
            var DeferralRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Deferral);
            var duplicateRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Duplicate);
            var UnabletoReview = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.UnabletoReview);
            var PeerReview = requestByReferral.Count(s => s.RFAStatus == GlobalConst.RFAStatus.PeerReview);
            var withdrawn = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Withdrawn);
            var otherRequest = (from request in requestByReferral
                                where request.RFAStatus != GlobalConst.RFAStatus.SendToPreparation && request.DecisionID != 0 && request.DecisionID != GlobalConst.Decision.Duplicate &&
                                request.DecisionID != GlobalConst.Decision.Deferral && request.DecisionID != GlobalConst.Decision.UnabletoReview && request.RFAStatus != GlobalConst.RFAStatus.PeerReview
                                && request.DecisionID != GlobalConst.Decision.Withdrawn
                                select request).Count();
            if (requestByReferral.Count() == PrepartionRequest)
            {
                _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.Preparation);
            }
            else if (requestByReferral.Count() == PeerReview)
            {
                newRFAReferralid = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.ConstantChar.Zero);
                if (newRFAReferralid == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInPeerReview(requestByReferral, rfaReferral);
                }
                else
                {
                    _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.PeerReview);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessPreparationRequest(newRFAReferralid, request.RFARequestID);
                    }
                    
                }

                if (rfaReferral.RFAReferralID != newRFAReferralid)
                {
                    ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                    ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                } 
            }
            else if (requestByReferral.Count() == withdrawn) // Withdrawn
            {
                _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInWithdrawn(requestByReferral, rfaReferral);
                }
                else
                {
                    _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);

                    }
                }
                if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID != 0)
                {
                    ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                    ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                    UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                }
            }    
            else if (otherRequest == requestByReferral.Count())
            {
                foreach (RequestByReferralID request in requestByReferral)
                {
                    if (request.DecisionDate == null && GlobalConst.ProcessLevel.ClinicalAudit == rfaReferral.ProcessLevel)
                        request.DecisionDate = DateTime.Now;
                    _iPreparationService.updateDecisionByRequestID(Mapper.Map<MMCService.PreparationService.RFARequest>(request));
                }
                if (rfaReferral.ProcessLevel == GlobalConst.ProcessLevel.Clinical)
                    _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.ClinicalAudit);
                else
                {
                    Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.AuditRFAReferralID] = rfaReferral.RFAReferralID.ToString();
                    _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.Notifications);
                    IsNotificationProcessLevel = true;
                    TempData[GlobalConst.ConstantName.AuditInitial] = DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.InitialNotification);
                }
            }
            else if (requestByReferral.Count() == duplicateRequest && requestByReferral.Count() == 1)
            {
                 _newDuplicateRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Duplicate);
                 if (_newDuplicateRFAReferralID == GlobalConst.ConstantChar.Zero)
                 {
                     ProcessRefferalInNotification(requestByReferral, rfaReferral);
                 }
                 else
                 {

                     Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DuplicateRFAReferralID] = _newDuplicateRFAReferralID.ToString();
                     var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDuplicateRFAReferralID, GlobalConst.FileType.DuplicateLetter);
                     var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDuplicateRFAReferralID, GlobalConst.FileType.InitialNotification);
                     foreach (RequestByReferralID request in requestByReferral)
                     {
                         ProcessOnlyOneUnableToReviewDuplicateDeferral(request, _newDuplicateRFAReferralID, request.RFAReferralID);
                     }
                 }
                DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.DuplicateLetter);
                TempData[GlobalConst.ConstantName.DuplicateInitial] = DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.InitialNotification);
            }
            else if (requestByReferral.Count() == DeferralRequest && requestByReferral.Count() == 1)
            {
                _newDeferralRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Deferral);
                if (_newDeferralRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInNotification(requestByReferral, rfaReferral);
                }
                else
                {
                    Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DeferralRFAReferralID] = _newDeferralRFAReferralID.ToString();
                    var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDeferralRFAReferralID, GlobalConst.FileType.DeferralLetter);
                    var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDeferralRFAReferralID, GlobalConst.FileType.InitialNotification);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessOnlyOneUnableToReviewDuplicateDeferral(request, _newDeferralRFAReferralID, request.RFAReferralID);
                    }
                }
                DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.DeferralLetter);
                //new code
                TempData[GlobalConst.ConstantName.DeferralInitial] = DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.InitialNotification);
            }
            else if (requestByReferral.Count() == UnabletoReview && requestByReferral.Count() == 1)
            {
                _newUnableToReviewRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.UnabletoReview);
                if (_newUnableToReviewRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInNotification(requestByReferral, rfaReferral);
                }
                else
                {
                    Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.UnableToReviewRFAReferralID] = _newUnableToReviewRFAReferralID.ToString();
                    var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newUnableToReviewRFAReferralID, GlobalConst.FileType.NoRFALetter);
                    var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newUnableToReviewRFAReferralID, GlobalConst.FileType.InitialNotification);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessOnlyOneUnableToReviewDuplicateDeferral(request, _newUnableToReviewRFAReferralID, request.RFAReferralID);
                    }
                }
                DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.NoRFALetter);
                TempData[GlobalConst.ConstantName.UnabletoReviewInitial] = DownloadLetter(rfaReferral.RFAReferralID, GlobalConst.FileType.InitialNotification);
            }
            else if (requestByReferral.Count() == withdrawn && requestByReferral.Count() == 1)// Withdrawn
            {
                _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                {
                    ProcessRefferalInWithdrawn(requestByReferral, rfaReferral);
                }
                else
                {
                    _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                    foreach (RequestByReferralID request in requestByReferral)
                    {
                        ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);

                    }
                }
                if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID != 0)
                {
                    ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                    ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                    UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID,"Withdrawn");
                }
            }

            else if (otherRequest != 0)
            {
                ProcessRequestwhenotherRequestNotzero(requestByReferral, rfaReferral);
            }
            else if (PrepartionRequest != 0 && otherRequest == 0)
            {
                ProcessRequestwhenotherRequestzero(requestByReferral, rfaReferral);
            }
            else if (DeferralRequest != 0)
            {
                ProcessDefferalRequest(requestByReferral, rfaReferral, GlobalConst.ConstantChar.ScreenClinical);
            }
            else if (duplicateRequest != 0)
            {
                ProcessDuplicateRequest(requestByReferral, rfaReferral, GlobalConst.ConstantChar.ScreenClinical);
            }
            else if (UnabletoReview != 0)
            {
                ProcessUnableToReviewRequest(requestByReferral, rfaReferral, GlobalConst.ConstantChar.ScreenClinical);
            }
                       
            foreach (var _nameUrl in _listTemp)// For attachement of email
            {
                string _urlData = Convert.ToString(TempData[_nameUrl]);
                _urlData = _urlData.Replace("(", "");
                _urlData = _urlData.Replace(")", "");
                if (_urlData != "")
                {
                    Tuple<string, string> t = CombinedLinkForAttachement(_urlData);
                    string fullPathFile = t.Item1 + "," + t.Item2;
                    _listURlDownloadInitial.Add(fullPathFile);
                    if (_nameUrl == GlobalConst.ConstantName.DeferralInitial)
                    {
                        Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCDeferral] = fullPathFile;
                    }
                    else if (_nameUrl == GlobalConst.ConstantName.DuplicateInitial)
                    {
                        Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCDuplicate] = fullPathFile;
                    }
                    else if (_nameUrl == GlobalConst.ConstantName.UnabletoReviewInitial)
                    {
                        Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCUnabletoReview] = fullPathFile;
                    }
                    else if (_nameUrl == GlobalConst.ConstantName.AuditInitial)
                    {
                        Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCAudit] = fullPathFile;
                    }
                }
            }
            return Json(_listURlDownloadInitial);
        }
        #endregion
        private void ProcessRefferalInNotification(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral)
        {
            Array.ForEach(requestByReferral.ToArray(), subarray => subarray.DecisionDate = DateTime.Now);
            _iPreparationService.updateDecisionByRequestID(Mapper.Map<MMCService.PreparationService.RFARequest>(requestByReferral.First()));
            _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.Notifications);
        }

        private void ProcessRefferalInPeerReview(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral)
        {
            _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.PeerReview);
        }

        private void ProcessRefferalInWithdrawn(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral)
        {
            Array.ForEach(requestByReferral.ToArray(), subarray => subarray.DecisionDate = DateTime.Now);
            _iPreparationService.updateDecisionByRequestID(Mapper.Map<MMCService.PreparationService.RFARequest>(requestByReferral.First()));
            _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.FileStorage);
        }

        private void ProcessRequestwhenotherRequestzero(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral)
        {
            int SendToPreparation = 0;
            int PeerReview = 0;
            int WithDrawn = 0;
            int _newDuplicateRFAReferralID = 0;
            int _newDeferralRFAReferralID = 0;
            int _newUnableToReviewRFAReferralID = 0;
            int _newWithdrawnRFAReferralID = 0;
            int IsDownload = 1;
            int newRFAReferralid = 0;
            int newRFAReferralidForPeerReview = 0;
            int duplicateRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Duplicate);
            int DeferralRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Deferral);
            int UnabletoReview = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.UnabletoReview);
            int sendToPrep = requestByReferral.Count(s => s.RFAStatus == GlobalConst.RFAStatus.SendToPreparation);
            int sendToPeerReview = requestByReferral.Count(s => s.RFAStatus == GlobalConst.RFAStatus.PeerReview);

            RFAReferral _oldRFAReferral = Mapper.Map<RFAReferral>(_iIntakeService.getReferralByID(rfaReferral.RFAReferralID));
            foreach (RequestByReferralID request in requestByReferral)
            {
                if (request.RFAStatus == GlobalConst.RFAStatus.SendToPreparation)
                {
                    if (SendToPreparation == 0)
                    {
                        newRFAReferralid = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        SendToPreparation++;
                    }
                    sendToPrep--;
                    if (rfaReferral.RFAReferralID != newRFAReferralid)
                    {
                        if (sendToPrep == 0)
                        {
                            _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.Preparation);
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                        }
                    }
                    ProcessPreparationRequest(newRFAReferralid, request.RFARequestID);
                }
                else if (request.RFAStatus == GlobalConst.RFAStatus.PeerReview)
                {
                    if (PeerReview == 0)
                    {
                        newRFAReferralidForPeerReview = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.ConstantChar.Zero);
                        if (newRFAReferralidForPeerReview == GlobalConst.ConstantChar.Zero)
                            newRFAReferralidForPeerReview = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        PeerReview++;
                    }
                    sendToPeerReview--;
                    if (rfaReferral.RFAReferralID != newRFAReferralidForPeerReview)
                    {
                        if (sendToPeerReview == 0)
                        {
                            _iPreparationService.updateProcessLevel(newRFAReferralidForPeerReview, GlobalConst.ProcessLevel.PeerReview);
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralidForPeerReview);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralidForPeerReview);
                        }
                    }
                    ProcessPreparationRequest(newRFAReferralidForPeerReview, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.Withdrawn)
                {
                    if (WithDrawn == 0)
                    {
                        _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                        if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newWithdrawnRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID !=0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                        }
                        WithDrawn++;
                    }
                    ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);
                }
                else
                {
                    if (request.DecisionID == GlobalConst.Decision.Duplicate)
                    {
                        if (_newDuplicateRFAReferralID == 0)
                        {
                            _newDuplicateRFAReferralID = DuplicateProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);                           
                        }
                        duplicateRequest--;
                        IsDownload = duplicateRequest;
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDuplicateRFAReferralID, request.RFAReferralID, IsDownload);
                    }
                    else if (request.DecisionID == GlobalConst.Decision.Deferral)
                    {
                        if (_newDeferralRFAReferralID == 0)
                        {
                            _newDeferralRFAReferralID = DeferralProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);
                        }
                        DeferralRequest--;
                        IsDownload = DeferralRequest;
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDeferralRFAReferralID, request.RFAReferralID, IsDownload);
                    }
                    else if (request.DecisionID == GlobalConst.Decision.UnabletoReview)
                    {
                        if (_newUnableToReviewRFAReferralID == 0)
                        {
                            _newUnableToReviewRFAReferralID = UnableToReviewProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);                            
                        }
                        UnabletoReview--;
                        IsDownload = UnabletoReview;
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newUnableToReviewRFAReferralID, request.RFAReferralID, IsDownload);
                    }
                    
                    IsDownload = 1;
                }
            }
        }
        private void ProcessRequestwhenotherRequestNotzero(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral)
        {
            RFAReferral _oldRFAReferral = Mapper.Map<RFAReferral>(_iIntakeService.getReferralByID(rfaReferral.RFAReferralID));
            int SendToPreparation = 0;
            int newRFAReferralid = 0;
            int WithDrawn = 0;
            int _newDuplicateRFAReferralID = 0;
            int _newDeferralRFAReferralID = 0;
            int _newUnableToReviewRFAReferralID = 0;
            int otherStaus = 1;
            int PeerReview = 0;
            int newRFAReferralidForPeerReview = 0;
            int _newWithdrawnRFAReferralID = 0;
            int IsDownload = 1;
            int duplicateRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Duplicate);
            int DeferralRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Deferral);
            int UnabletoReview = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.UnabletoReview);
            int sendToPrep = requestByReferral.Count(s => s.RFAStatus == GlobalConst.RFAStatus.SendToPreparation);
            int sendToPeerReview = requestByReferral.Count(s => s.RFAStatus == GlobalConst.RFAStatus.PeerReview);
            foreach (RequestByReferralID request in requestByReferral)
            {
                if (request.RFAStatus == GlobalConst.RFAStatus.SendToPreparation)
                {
                    if (SendToPreparation == 0)
                    {
                        newRFAReferralid = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));     
                        SendToPreparation++;
                    }
                    sendToPrep--;
                    if (rfaReferral.RFAReferralID != newRFAReferralid)
                    {
                        if (sendToPrep == 0)
                        {
                            _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.Preparation);
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                        }
                    }
                    ProcessPreparationRequest(newRFAReferralid, request.RFARequestID);
                }
                else if (request.RFAStatus == GlobalConst.RFAStatus.PeerReview)
                {
                    if (PeerReview == 0)
                    {
                        newRFAReferralidForPeerReview = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.ConstantChar.Zero);
                        if (newRFAReferralidForPeerReview == GlobalConst.ConstantChar.Zero)
                            newRFAReferralidForPeerReview = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));    
                        PeerReview++;
                    }
                    sendToPeerReview--;
                    if (rfaReferral.RFAReferralID != newRFAReferralidForPeerReview)
                    {
                        if (sendToPeerReview == 0)
                        {
                            _iPreparationService.updateProcessLevel(newRFAReferralidForPeerReview, GlobalConst.ProcessLevel.PeerReview);
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralidForPeerReview);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralidForPeerReview);
                        }
                    }
                    ProcessPreparationRequest(newRFAReferralidForPeerReview, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.Withdrawn)
                {
                    if (WithDrawn == 0)
                    {
                        _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                        if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newWithdrawnRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID != 0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                        }
                        WithDrawn++;
                    }
                    ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.Duplicate)
                {                  
                    if (_newDuplicateRFAReferralID == 0)
                    {
                        _newDuplicateRFAReferralID = DuplicateProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);                       
                    }
                    duplicateRequest--;
                    IsDownload = duplicateRequest;
                    ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDuplicateRFAReferralID, request.RFAReferralID, IsDownload);
                   
                }
                else if (request.DecisionID == GlobalConst.Decision.Deferral)
                {                    
                    if (_newDeferralRFAReferralID == 0)
                    {
                        _newDeferralRFAReferralID = DeferralProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);                       
                    }
                    DeferralRequest--;
                    IsDownload = DeferralRequest;
                    ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDeferralRFAReferralID, request.RFAReferralID, IsDownload);
                   
                }
                else if (request.DecisionID == GlobalConst.Decision.UnabletoReview)
                {                    
                    if (_newUnableToReviewRFAReferralID == 0)
                    {
                        _newUnableToReviewRFAReferralID = UnableToReviewProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);
                    }
                    UnabletoReview--;
                    IsDownload = UnabletoReview;
                    ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newUnableToReviewRFAReferralID, request.RFAReferralID, IsDownload);
                }
                else
                    updateDecisionDate(request.DecisionDate, rfaReferral.ProcessLevel, request);
                if (otherStaus == requestByReferral.Count())
                {
                    if (rfaReferral.ProcessLevel == GlobalConst.ProcessLevel.Clinical)
                        _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.ClinicalAudit);
                    else
                    {
                        _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.Notifications);
                        Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.AuditRFAReferralID] = request.RFAReferralID.ToString();
                        TempData[GlobalConst.ConstantName.AuditInitial] = DownloadLetter(request.RFAReferralID, GlobalConst.FileType.InitialNotification);
                    }
                }
                otherStaus++;
                IsDownload = 1;
            }
        }
        private void ProcessPrepationRequestwhenotherRequestNotzero(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral)
        {
            RFAReferral _oldRFAReferral = Mapper.Map<RFAReferral>(_iIntakeService.getReferralByID(rfaReferral.RFAReferralID));
            int otherStaus = 0;
            int PeerReview = 0;
            int WithDrawn = 0;
            int clientAuthorized = 0;
            int IsDownload = 1;
            int newRFAReferralid = 0;
            int _newDuplicateRFAReferralID = 0;
            int _newDeferralRFAReferralID = 0;
            int _newUnableToReviewRFAReferralID = 0;
            int _newClientAuthorizedRFAReferralID = 0;
            int _newWithdrawnRFAReferralID = 0;
            int duplicateRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Duplicate);
            int DeferralRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Deferral);
            int UnabletoReview = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.UnabletoReview);
            foreach (RequestByReferralID request in requestByReferral)
            {
                if (request.DecisionID == GlobalConst.Decision.Duplicate)
                {
                    if (_newDuplicateRFAReferralID == 0)
                    {
                        _newDuplicateRFAReferralID = DuplicateProcessOne(request.RFAReferralID, _oldRFAReferral);                      
                    }
                    duplicateRequest--;
                    IsDownload = duplicateRequest;
                    ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDuplicateRFAReferralID, request.RFAReferralID, IsDownload);                    
                }
                else if (request.DecisionID == GlobalConst.Decision.Deferral)
                {
                    if (_newDeferralRFAReferralID == 0)
                    {
                        _newDeferralRFAReferralID = DeferralProcessOne(request.RFAReferralID, _oldRFAReferral);                     
                    }
                    DeferralRequest--;
                    IsDownload = DeferralRequest;
                    ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDeferralRFAReferralID, request.RFAReferralID, IsDownload);
                }
                else if (request.DecisionID == GlobalConst.Decision.UnabletoReview)
                {
                    if (_newUnableToReviewRFAReferralID == 0)
                    {
                        _newUnableToReviewRFAReferralID = UnableToReviewProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);
                    }
                    UnabletoReview--;
                    IsDownload = UnabletoReview;
                    ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newUnableToReviewRFAReferralID, request.RFAReferralID, IsDownload);                    
                }
                else if (request.RFAStatus == GlobalConst.RFAStatus.PeerReview)
                {
                    if (PeerReview == 0)
                    {
                        newRFAReferralid = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.ConstantChar.Zero);
                        if (newRFAReferralid == GlobalConst.ConstantChar.Zero)
                            newRFAReferralid = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.PeerReview);
                        if (rfaReferral.RFAReferralID != newRFAReferralid)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                        }
                        PeerReview++;
                    }
                    ProcessPreparationRequest(newRFAReferralid, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.ClientAuthorized)
                {  
                    if (clientAuthorized == 0)
                    {
                        _newClientAuthorizedRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.ClientAuthorized);
                        if (_newClientAuthorizedRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newClientAuthorizedRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newClientAuthorizedRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newClientAuthorizedRFAReferralID && _newClientAuthorizedRFAReferralID != 0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID, "ClientAuthorized");
                        }
                        clientAuthorized++;
                    }
                    ProcessPreparationWithdrawnRequest(_newClientAuthorizedRFAReferralID, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.Withdrawn)
                {
                    if (WithDrawn == 0)
                    {
                        _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                        if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newWithdrawnRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID !=0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                        }
                        WithDrawn++;
                    }
                    ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);
                }
                else
                {
                    if (otherStaus == 0)
                    {
                        _iPreparationService.updateProcessLevel(rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.Clinical);
                        otherStaus++;
                    }
                }
                IsDownload = 1;
            }
        }

        private void ProcessDefferalRequest(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral, string Screen)
        {
            int Defferal = 0;
            int PeerReview = 0;
            int WithDrawn = 0;
            int clientAuthorized = 0;
            int IsDownload = 1;
            int newRFAReferralid = 0;
            int _newDuplicateRFAReferralID = 0;
            int _newDeferralRFAReferralID = 0;
            int _newUnableToReviewRFAReferralID = 0;
            int _newClientAuthorizedRFAReferralID = 0;
            int _newWithdrawnRFAReferralID = 0;
            
            int duplicateRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Duplicate);
            int DeferralRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Deferral);
            int UnabletoReview = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.UnabletoReview);

            RFAReferral _oldRFAReferral = Mapper.Map<RFAReferral>(_iIntakeService.getReferralByID(rfaReferral.RFAReferralID));
            foreach (RequestByReferralID request in requestByReferral)
            {
                if (request.DecisionID == GlobalConst.Decision.Deferral)
                {
                    if (Defferal == 0)
                    {
                        _newDeferralRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(request.RFAReferralID, GlobalConst.Decision.Deferral);
                        if (_newDeferralRFAReferralID == GlobalConst.ConstantChar.Zero)
                        {
                            if (Screen == GlobalConst.ConstantChar.ScreenPrepration)
                            {
                                _newDeferralRFAReferralID = request.RFAReferralID;                                
                            }
                            else
                            {
                                _newDeferralRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));                                
                            }
                        }
                        else
                        {                           
                            var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDeferralRFAReferralID, GlobalConst.FileType.DeferralLetter);
                            var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDeferralRFAReferralID, GlobalConst.FileType.InitialNotification);
                        }                        
                        Defferal++;
                        DeferralRequest--;
                        IsDownload = DeferralRequest;
                        if (IsDownload==0)
                            Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DeferralRFAReferralID] = _newDeferralRFAReferralID.ToString();
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDeferralRFAReferralID, request.RFAReferralID, IsDownload); 
                    }
                    else
                    {
                        if (_newDeferralRFAReferralID == 0)
                        {
                            _newDeferralRFAReferralID = DeferralProcessOne(request.RFAReferralID, _oldRFAReferral);
                        }
                        DeferralRequest--;
                        IsDownload = DeferralRequest;
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDeferralRFAReferralID, request.RFAReferralID, IsDownload);                       
                    }                  
                }//new code
                else if (request.RFAStatus == GlobalConst.RFAStatus.PeerReview)
                {

                    if (PeerReview == 0)
                    {
                        newRFAReferralid = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.ConstantChar.Zero);
                        if (newRFAReferralid == GlobalConst.ConstantChar.Zero)
                            newRFAReferralid = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.PeerReview);
                        if (rfaReferral.RFAReferralID != newRFAReferralid)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                        }
                        PeerReview++;
                    }
                    ProcessPreparationRequest(newRFAReferralid, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.Withdrawn)
                {
                    if (WithDrawn == 0)
                    {
                        _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                        if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newWithdrawnRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID !=0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                        }
                        WithDrawn++;
                    }
                    ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);
                }
                else if(request.DecisionID == GlobalConst.Decision.ClientAuthorized)
                {
                    if (clientAuthorized == 0)
                    {
                        _newClientAuthorizedRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.ClientAuthorized);
                        if (_newClientAuthorizedRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newClientAuthorizedRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newClientAuthorizedRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newClientAuthorizedRFAReferralID && _newClientAuthorizedRFAReferralID != 0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID, "ClientAuthorized");
                        }
                        clientAuthorized++;
                    }
                    ProcessPreparationWithdrawnRequest(_newClientAuthorizedRFAReferralID, request.RFARequestID);
                }
                else
                {
                    if (request.DecisionID == GlobalConst.Decision.Duplicate)
                    {
                        if (_newDuplicateRFAReferralID == 0)
                        {
                            _newDuplicateRFAReferralID = DuplicateProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);                           
                        }
                        duplicateRequest--;
                        IsDownload = duplicateRequest;
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDuplicateRFAReferralID, request.RFAReferralID, IsDownload);

                    }                   
                    else if (request.DecisionID == GlobalConst.Decision.UnabletoReview)
                    {
                        if (_newUnableToReviewRFAReferralID == 0)
                        {
                            _newUnableToReviewRFAReferralID = UnableToReviewProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);                            
                        }
                        UnabletoReview--;
                        IsDownload = UnabletoReview;
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newUnableToReviewRFAReferralID, request.RFAReferralID, IsDownload);
                    }                   
                }
                IsDownload = 1;
            }
        }
        private void ProcessDuplicateRequest(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral, string Screen)
        {
            int Duplicate = 0;
            int PeerReview = 0;
            int WithDrawn = 0;
            int newRFAReferralid = 0;
            int clientAuthorized = 0;
            int IsDownload = 1;
            int _newDuplicateRFAReferralID = 0;
            int _newUnableToReviewRFAReferralID = 0;
            int _newClientAuthorizedRFAReferralID = 0;
            int _newWithdrawnRFAReferralID = 0;

            int duplicateRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.Duplicate);
            int UnabletoReview = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.UnabletoReview);

            RFAReferral _oldRFAReferral = Mapper.Map<RFAReferral>(_iIntakeService.getReferralByID(rfaReferral.RFAReferralID));
            foreach (RequestByReferralID request in requestByReferral)
            {
                if (request.DecisionID == GlobalConst.Decision.Duplicate)
                {
                    if (Duplicate == 0)
                    {
                        _newDuplicateRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(request.RFAReferralID, GlobalConst.Decision.Duplicate);
                        if (_newDuplicateRFAReferralID == GlobalConst.ConstantChar.Zero)
                        {
                            if (Screen == GlobalConst.ConstantChar.ScreenPrepration)
                            {
                                _newDuplicateRFAReferralID = request.RFAReferralID;
                            }
                            else
                            {
                                _newDuplicateRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                            }
                        }
                        else
                        {
                            request.RFAReferralID = _newDuplicateRFAReferralID;
                            var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDuplicateRFAReferralID, GlobalConst.FileType.DuplicateLetter);
                            var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDuplicateRFAReferralID, GlobalConst.FileType.InitialNotification);
                        }
                                               
                        Duplicate++;
                        duplicateRequest--;
                        IsDownload = duplicateRequest;
                        if (IsDownload == 0)
                            Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DuplicateRFAReferralID] = _newDuplicateRFAReferralID.ToString();
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDuplicateRFAReferralID, request.RFAReferralID, IsDownload); 
                    }
                    else
                    {
                        if (_newDuplicateRFAReferralID == 0)
                        {
                            _newDuplicateRFAReferralID = DuplicateProcessOne(request.RFAReferralID, _oldRFAReferral);                                                    
                        }
                        duplicateRequest--;
                        IsDownload = duplicateRequest;
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newDuplicateRFAReferralID, request.RFAReferralID, IsDownload);     
                    }                  
                }//new code
                else if (request.RFAStatus == GlobalConst.RFAStatus.PeerReview)
                {

                    if (PeerReview == 0)
                    {
                        newRFAReferralid = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.ConstantChar.Zero);
                        if (newRFAReferralid == GlobalConst.ConstantChar.Zero)
                            newRFAReferralid = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.PeerReview);
                        if (rfaReferral.RFAReferralID != newRFAReferralid)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                        }
                        PeerReview++;
                    }
                    ProcessPreparationRequest(newRFAReferralid, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.Withdrawn)
                {
                    if (WithDrawn == 0)
                    {
                        _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                        if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newWithdrawnRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID != 0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                        }
                        WithDrawn++;
                    }
                    ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.ClientAuthorized)
                {
                     if (clientAuthorized == 0)
                    {
                        _newClientAuthorizedRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.ClientAuthorized);
                        if (_newClientAuthorizedRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newClientAuthorizedRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newClientAuthorizedRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newClientAuthorizedRFAReferralID && _newClientAuthorizedRFAReferralID != 0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID, "ClientAuthorized");
                        }
                        clientAuthorized++;
                    }
                    ProcessPreparationWithdrawnRequest(_newClientAuthorizedRFAReferralID, request.RFARequestID);
                }
                else
                {
                    if (request.DecisionID == GlobalConst.Decision.UnabletoReview)
                    {
                        if (_newUnableToReviewRFAReferralID == 0)
                        {
                            _newUnableToReviewRFAReferralID = UnableToReviewProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);                           
                        }
                        UnabletoReview--;
                        IsDownload = UnabletoReview;
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newUnableToReviewRFAReferralID, request.RFAReferralID, IsDownload);
                    }
                   
                }
                IsDownload = 1;
            }
        }
        private void ProcessUnableToReviewRequest(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral, string Screen)
        {
            int UnableToReview = 0;
            int PeerReview = 0;
            int WithDrawn = 0;
            int clientAuthorized = 0;
            int newRFAReferralid = 0;
            int IsDownload = 1;
            int _newUnableToReviewRFAReferralID = 0;
            int _newClientAuthorizedRFAReferralID = 0;
            int _newWithdrawnRFAReferralID = 0;
            int UnabletoReviewReq = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.UnabletoReview);           

            RFAReferral _oldRFAReferral = Mapper.Map<RFAReferral>(_iIntakeService.getReferralByID(rfaReferral.RFAReferralID));
            foreach (RequestByReferralID request in requestByReferral)
            {
                if (request.DecisionID == GlobalConst.Decision.UnabletoReview)
                {
                    if (UnableToReview == 0)
                    {
                        _newUnableToReviewRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.UnabletoReview);
                        if (_newUnableToReviewRFAReferralID == GlobalConst.ConstantChar.Zero)
                        {
                            if (Screen == GlobalConst.ConstantChar.ScreenPrepration)
                            {
                                _newUnableToReviewRFAReferralID = request.RFAReferralID;
                            }
                            else
                            {
                                _newUnableToReviewRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                            }
                        }
                        else
                        {
                            request.RFAReferralID = _newUnableToReviewRFAReferralID;
                            var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newUnableToReviewRFAReferralID, GlobalConst.FileType.NoRFALetter);
                            var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newUnableToReviewRFAReferralID, GlobalConst.FileType.InitialNotification);
                        }
                                               
                         UnableToReview++;
                         UnabletoReviewReq--;
                         IsDownload = UnabletoReviewReq;
                         if (IsDownload == 0)
                             Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.UnableToReviewRFAReferralID] = _newUnableToReviewRFAReferralID.ToString();
                         ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newUnableToReviewRFAReferralID, request.RFAReferralID, IsDownload); 
                    }
                    else
                    {
                        if (_newUnableToReviewRFAReferralID == 0)
                        {
                            _newUnableToReviewRFAReferralID = UnableToReviewProcessOne(rfaReferral.RFAReferralID, _oldRFAReferral);                            
                        }
                        UnabletoReviewReq--;
                        IsDownload = UnabletoReviewReq;
                        ProcesUnableToreviewDeferralDuplicateNewFlow(request, _newUnableToReviewRFAReferralID, request.RFAReferralID, IsDownload);
                    }
                    
                }
                //new code
                else if (request.RFAStatus == GlobalConst.RFAStatus.PeerReview)
                {
                    if (PeerReview == 0)
                    {
                        newRFAReferralid = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.ConstantChar.Zero);
                        if (newRFAReferralid == GlobalConst.ConstantChar.Zero)
                            newRFAReferralid = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.PeerReview);
                        if (rfaReferral.RFAReferralID != newRFAReferralid)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                        }
                        PeerReview++;
                    }
                    ProcessPreparationRequest(newRFAReferralid, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.Withdrawn)
                {
                    if (WithDrawn == 0)
                    {
                        _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                        if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newWithdrawnRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID != 0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                        }
                        WithDrawn++;
                    }
                    ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.ClientAuthorized)
                {
                    if (clientAuthorized == 0)
                    {
                        _newClientAuthorizedRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.ClientAuthorized);
                        if (_newClientAuthorizedRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newClientAuthorizedRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newClientAuthorizedRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newClientAuthorizedRFAReferralID && _newClientAuthorizedRFAReferralID != 0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID, "ClientAuthorized");
                        }
                        clientAuthorized++;
                    }
                    ProcessPreparationWithdrawnRequest(_newClientAuthorizedRFAReferralID, request.RFARequestID);
                }
                IsDownload = 1;
            }
        }
        private void ProcesUnableToreviewDeferralDuplicate(RequestByReferralID request, RFAReferral _oldRFAReferral, int RFAReferralID)
        {
            int newRFAReferralid = 0;
            newRFAReferralid = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
            AssNewRefferalToRequest(newRFAReferralid, request.RFARequestID, request.DecisionDate);
            _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.Notifications);
            ManageRFASplittedReferralHistory(RFAReferralID, newRFAReferralid);
            if (request.DecisionID == GlobalConst.Decision.Duplicate)
                DownloadLetter(newRFAReferralid, GlobalConst.FileType.DuplicateLetter);
            if (request.DecisionID == GlobalConst.Decision.Deferral)
            {
                DownloadLetter(newRFAReferralid, GlobalConst.FileType.DeferralLetter);
                //new code
                DownloadLetter(newRFAReferralid, GlobalConst.FileType.InitialNotification);
            }
            if (request.DecisionID == GlobalConst.Decision.UnabletoReview)
                DownloadLetter(newRFAReferralid, GlobalConst.FileType.NoRFALetter);
        }
        private void ProcessClientAuthorizedRequest(IEnumerable<RequestByReferralID> requestByReferral, RFAReferral rfaReferral)
        {
            int clientAuthorized = 0;// US#2715 by mahi for Client Authorized
            int PeerReview = 0;
            int WithDrawn = 0;
            int newRFAReferralid = 0;
            int IsDownload = 0;
            int _newClientAuthorizedRFAReferralID = 0;
            int _newWithdrawnRFAReferralID = 0;
            
            var _clientAuthorizedRequest = requestByReferral.Count(s => s.DecisionID == GlobalConst.Decision.ClientAuthorized);
            RFAReferral _oldRFAReferral = Mapper.Map<RFAReferral>(_iIntakeService.getReferralByID(rfaReferral.RFAReferralID));
            foreach (RequestByReferralID request in requestByReferral)
            {
                if (request.DecisionID == GlobalConst.Decision.ClientAuthorized)
                {
                    if (clientAuthorized == 0)
                    {
                        _newClientAuthorizedRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.ClientAuthorized);
                        if (_newClientAuthorizedRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newClientAuthorizedRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newClientAuthorizedRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newClientAuthorizedRFAReferralID && _newClientAuthorizedRFAReferralID != 0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newClientAuthorizedRFAReferralID, "ClientAuthorized");
                        }
                        clientAuthorized++;
                    }
                    ProcessPreparationWithdrawnRequest(_newClientAuthorizedRFAReferralID, request.RFARequestID);
                }//new code
                else if (request.RFAStatus == GlobalConst.RFAStatus.PeerReview)
                {

                    if (PeerReview == 0)
                    {
                        newRFAReferralid = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.ConstantChar.Zero);
                        if (newRFAReferralid == GlobalConst.ConstantChar.Zero)
                            newRFAReferralid = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.PeerReview);
                        if (rfaReferral.RFAReferralID != newRFAReferralid)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, newRFAReferralid);
                        }
                        PeerReview++;
                    }
                    ProcessPreparationRequest(newRFAReferralid, request.RFARequestID);
                }
                else if (request.DecisionID == GlobalConst.Decision.Withdrawn)
                {
                    if (WithDrawn == 0)
                    {
                        _newWithdrawnRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(rfaReferral.RFAReferralID, GlobalConst.Decision.Withdrawn);
                        if (_newWithdrawnRFAReferralID == GlobalConst.ConstantChar.Zero)
                            _newWithdrawnRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                        _iPreparationService.updateProcessLevel(_newWithdrawnRFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                        if (rfaReferral.RFAReferralID != _newWithdrawnRFAReferralID && _newWithdrawnRFAReferralID !=0)
                        {
                            ManageRFASplittedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            ManageRFAMergedReferralHistory(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID);
                            UpdateReferralIDAccordingToWithdrawnReferralID(rfaReferral.RFAReferralID, _newWithdrawnRFAReferralID, "Withdrawn");
                        }
                        WithDrawn++;
                    }
                    ProcessPreparationWithdrawnRequest(_newWithdrawnRFAReferralID, request.RFARequestID);
                }
            }
        }
        private void ProcessPreparationRequest(int newRFAReferralid, int RFARequestID)
        {
            int OldRFAReferralID = _iIntakeService.getRFARequestByID(RFARequestID).RFAReferralID.Value;

            RFARequest RFARequest = new MMCApp.Domain.Models.IntakeModel.RFARequest();
            RFARequest.RFAReferralID = newRFAReferralid;
            RFARequest.RFAStatus = null;
            RFARequest.RFARequestID = RFARequestID;
            RFARequest.DecisionDate = null;
            _iIntakeService.AssignNewReferralToRequest(Mapper.Map<MMCService.IntakeService.RFARequest>(RFARequest));

            _iPreparationService.UpdateRFAAdditionalInfoDetailByRequestID(OldRFAReferralID, RFARequestID);

            
        }

        private void ProcessPreparationWithdrawnRequest(int newRFAReferralid, int RFARequestID)
        {
            int OldRFAReferralID = _iIntakeService.getRFARequestByID(RFARequestID).RFAReferralID.Value;

            RFARequest RFARequest = new MMCApp.Domain.Models.IntakeModel.RFARequest();
            RFARequest.RFAReferralID = newRFAReferralid;
            RFARequest.RFAStatus = null;
            RFARequest.RFARequestID = RFARequestID;
            RFARequest.DecisionDate = System.DateTime.Now;
            _iIntakeService.AssignNewReferralToRequest(Mapper.Map<MMCService.IntakeService.RFARequest>(RFARequest));

            _iPreparationService.UpdateRFAAdditionalInfoDetailByRequestID(OldRFAReferralID, RFARequestID);

        }

        private void updateDecisionDate(DateTime? DecisionDate, int ProcessLevel, RequestByReferralID request)
        {
            if (DecisionDate == null && ProcessLevel == GlobalConst.ProcessLevel.ClinicalAudit)
            {
                request.DecisionDate = DateTime.Now;
                _iPreparationService.updateDecisionByRequestID(Mapper.Map<MMCService.PreparationService.RFARequest>(request));
            }
        }
        private void AssNewRefferalToRequest(int RfaReferralID, int RFARequestID, DateTime? DecisionDate)
        {
            RFARequest RFARequest = new MMCApp.Domain.Models.IntakeModel.RFARequest();
            RFARequest.RFAReferralID = RfaReferralID;
            RFARequest.RFAStatus = null;
            RFARequest.RFARequestID = RFARequestID;
            if (DecisionDate == null)
                RFARequest.DecisionDate = DateTime.Now;
            _iIntakeService.AssignNewReferralToRequest(Mapper.Map<MMCService.IntakeService.RFARequest>(RFARequest));
        }
        private void ManageRFASplittedReferralHistory(int RFAOldReferralID, int RFANewReferralID)
        {
            RFASplittedReferralHistory _rfaSplittedReferralHistory = new RFASplittedReferralHistory();
            _rfaSplittedReferralHistory.RFAOldReferralID = RFAOldReferralID;
            _rfaSplittedReferralHistory.RFANewReferralID = RFANewReferralID;
            _rfaSplittedReferralHistory.RFASplittedReferralDate = System.DateTime.Now;
            _iIntakeService.addRFASplittedReferralHistory(Mapper.Map<MMCService.IntakeService.RFASplittedReferralHistory>(_rfaSplittedReferralHistory));
            RFAReferralFile _rfaReferralFile = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIntakeService.getReferralFileByRFAReferralIDAndFileTypeID(RFAOldReferralID, GlobalConst.FileType.IntakeUpload)).SingleOrDefault();
            if (_rfaReferralFile != null)
            {
                _rfaReferralFile.RFAReferralID = RFANewReferralID;
                _rfaReferralFile.RFAFileCreationDate = System.DateTime.Now;
                int fileid = _iIntakeService.addReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_rfaReferralFile));
            }
        }
        [HttpPost]
        public ActionResult GetPatientMedicalRecordByPatientID(int _patientID, int _skip)
        {
            var _s = GetPatientMedicalRecord(_patientID, _skip);
            return Json(GetPatientMedicalRecord(_patientID, _skip), GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult UploadSignature(RFAReferral _RFAReferral)
        {
            string Filename = "";
            if (_RFAReferral.RFASignatureFile != null)
            {
                StorageModel _storageModel = new StorageModel();
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_RFAReferral.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                _storageModel.FolderName = GlobalConst.FolderName.Signature;
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                Filename = GlobalConst.FolderName.Signature + "_" + _RFAReferral.RFAReferralID + Path.GetExtension(_RFAReferral.RFASignatureFile.FileName);
                string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
                if (System.IO.File.Exists(savePath))
                    System.IO.File.Delete(savePath);
                _RFAReferral.RFASignatureFile.SaveAs(savePath);
                _RFAReferral.RFASignature = Filename;
                _iIntakeService.UploadSignature(Mapper.Map<MMCService.IntakeService.RFAReferral>(_RFAReferral));
                _RFAReferral.RFASignature = GlobalConst.ConstantChar.ForwardSlash + GlobalConst.VirtualPathFolders.Storage + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ClientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.PatientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ClaimID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ReferralID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.FolderName + GlobalConst.ConstantChar.ForwardSlash + _RFAReferral.RFASignature;
                //   _objUser.UserSignatureFileName = GlobalConst.VirtualPathFolders.UserSignature + Path.GetExtension(_objUser.UserSignature.FileName);
            }

            return Json(_RFAReferral.RFASignature, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult UploadSignatureAndDescription(int _RFAReferralID, string SignatureSelect)
        {
            RFAReferral _RFAReferral = new RFAReferral();
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_RFAReferralID, GlobalConst.ConstantChar.Char_R));
            _storageModel.FolderName = GlobalConst.FolderName.Signature;
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string Filename = "";
            string OfficalSignaturepath = "";
            string decisionDesc = "";
            decisionDesc = _iIntakeService.getDeterminationLetterDecisionDesc(_RFAReferralID);
            if (SignatureSelect == "MD")
            {
                OfficalSignaturepath = GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Content + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.img + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.OfficalSignature + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.OfficalSignatureName.RickChavez;
                _RFAReferral.RFASignatureDescription = GlobalConst.OfficalSignatureDescription.RickChavezSignatureDescription; ;
            }
            else
            {
                OfficalSignaturepath = GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Content + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.img + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.OfficalSignature + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.OfficalSignatureName.VickySummogum;
                _RFAReferral.RFASignatureDescription = GlobalConst.OfficalSignatureDescription.VickySummogumSignatureDescription;
            }
            Filename = GlobalConst.FolderName.Signature + "_" + _RFAReferralID + Path.GetExtension(OfficalSignaturepath);
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
            if (System.IO.File.Exists(savePath))
            {
                System.IO.File.SetAttributes(savePath, FileAttributes.Normal);
                System.IO.File.Delete(savePath);

            }
            System.IO.File.Copy(Server.MapPath(OfficalSignaturepath), savePath);
            _RFAReferral.RFASignature = Filename;
            _RFAReferral.RFAReferralID = _RFAReferralID;
            _iIntakeService.UploadSignature(Mapper.Map<MMCService.IntakeService.RFAReferral>(_RFAReferral));
            _iIntakeService.SaveSignatureDescription(Mapper.Map<MMCService.IntakeService.RFAReferral>(_RFAReferral));
            _RFAReferral.RFASignature = GlobalConst.ConstantChar.ForwardSlash + GlobalConst.VirtualPathFolders.Storage + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ClientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.PatientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ClaimID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ReferralID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.FolderName + GlobalConst.ConstantChar.ForwardSlash + _RFAReferral.RFASignature;


            return Json(_RFAReferral, GlobalConst.ContentTypes.TextHtml);
        }


        private RFAReferral GetSignatureAndDescription(int _rfaReferralID)
        {
            RFAReferral objRFAReferral = new RFAReferral();
            objRFAReferral = Mapper.Map<RFAReferral>(_iIntakeService.getReferralByID(_rfaReferralID));
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_rfaReferralID, GlobalConst.ConstantChar.Char_R));
            _storageModel.FolderName = GlobalConst.FolderName.Signature;

            if (objRFAReferral.RFASignature != null)
                objRFAReferral.RFASignature = GlobalConst.ConstantChar.ForwardSlash + GlobalConst.VirtualPathFolders.Storage + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ClientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.PatientID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ClaimID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.ReferralID + GlobalConst.ConstantChar.ForwardSlash + _storageModel.FolderName + GlobalConst.ConstantChar.ForwardSlash + objRFAReferral.RFASignature;
            return objRFAReferral;
        }


        [HttpPost]
        public ActionResult SaveSignatureDescription(RFAReferral _RFAReferral)
        {
            _iIntakeService.SaveSignatureDescription(Mapper.Map<MMCService.IntakeService.RFAReferral>(_RFAReferral));
            return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);
        }
        public PatientMedicalRecordViewModel GetPatientMedicalRecord(int _patientID, int _skip)
        {
            var _patientMedicalRecordByPatientID = _iPreparationService.getReferralMedicalRecordByPatientID(_patientID, _skip, GlobalConst.Records.Take5);
            PatientMedicalRecordViewModel objPatientMedicalRecordViewModel = new PatientMedicalRecordViewModel();
            objPatientMedicalRecordViewModel.PatientMedicalRecordByPatientIDDetails = Mapper.Map<IEnumerable<PatientMedicalRecordByPatientID>>(_patientMedicalRecordByPatientID.PatientMedicalRecordByPatientIDDetails);
            objPatientMedicalRecordViewModel.TotalCount = _patientMedicalRecordByPatientID.TotalCount;
            objPatientMedicalRecordViewModel.PagedRecords = GlobalConst.Records.Take5;
            return objPatientMedicalRecordViewModel;
        }
        [HttpPost]
        public ActionResult GetRequestForDuplicateDecision(int _patientClaimID, int _skip)
        {
            var _DuplicateRequest = _iIntakeService.GetRequestForDuplicateDecision(_patientClaimID, _skip, GlobalConst.Records.Take5);
            PatientAndRequestModel _PatientAndRequestModel = new PatientAndRequestModel();
            _PatientAndRequestModel.RequestDetail = Mapper.Map<IEnumerable<RequestByReferralID>>(_DuplicateRequest.RequestDetails);
            _PatientAndRequestModel.RequestCount = _DuplicateRequest.TotalCount;
            return Json(_PatientAndRequestModel, GlobalConst.ContentTypes.TextHtml);
        }
        public ActionResult ClinicalTriageAction(int id, int ProcessLevel)
        {
            var _NoOFReferral = _iCommonService.getNoOfReferralCountAccToProcessLevel();

            ViewBag.IntakeCount = _NoOFReferral[0].IntakeCount;
            ViewBag.PreparationCount = _NoOFReferral[0].PreparationCount;
            ViewBag.ClinicalTriageCount = _NoOFReferral[0].ClinicalTriageCount;
            ViewBag.ClinicalAuditCount = _NoOFReferral[0].ClinicalAuditCount;
            ViewBag.NotificationCount = _NoOFReferral[0].NotificationCount;
            ViewBag.BillingCount = _NoOFReferral[0].BillingCount;
            ViewBag.PeerReviewCount = _NoOFReferral[0].PeerReviewCount;
            ViewBag.IMRCount = _NoOFReferral[0].IMRCount;

            HttpCookie cookie = new HttpCookie(GlobalConst.ConstantName.PreviousAttachementLinks);
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;
            Editor1.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            Editor1.MvcInit();
            ViewData["Editor1"] = Editor1.MvcGetString();
            Editor Editor2 = new Editor(System.Web.HttpContext.Current, "Editor2");
            Editor2.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            Editor2.Width = Unit.Pixel(1050);
            Editor2.Height = Unit.Pixel(660);
            Editor2.ResizeMode = RTEResizeMode.Disabled;
            Editor2.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            Editor2.MvcInit();
            ViewData["Editor2"] = Editor2.MvcGetString();
            Editor Editor3 = new Editor(System.Web.HttpContext.Current, "Editor3");
            Editor3.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            Editor3.Width = Unit.Pixel(1050);
            Editor3.Height = Unit.Pixel(660);
            Editor3.ResizeMode = RTEResizeMode.Disabled;
            Editor3.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            Editor3.MvcInit();
            ViewData["Editor3"] = Editor3.MvcGetString();
            Editor Editor4 = new Editor(System.Web.HttpContext.Current, "Editor4");
            Editor4.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            Editor4.Width = Unit.Pixel(1050);
            Editor4.Height = Unit.Pixel(660);
            Editor4.ResizeMode = RTEResizeMode.Disabled;
            Editor4.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            Editor4.MvcInit();
            ViewData["Editor4"] = Editor4.MvcGetString();
            Editor EditorNote = new Editor(System.Web.HttpContext.Current, GlobalConst.Richtexteditor.EditorNote);
            EditorNote.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            EditorNote.Width = Unit.Pixel(1050);
            EditorNote.Height = Unit.Pixel(660);
            EditorNote.ResizeMode = RTEResizeMode.Disabled;
            EditorNote.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            EditorNote.MvcInit();
            ViewData["EditorNote"] = EditorNote.MvcGetString();
            ClinicalTriageActionViewModel clinicalTriageActionViewModel = new ClinicalTriageActionViewModel();
            clinicalTriageActionViewModel.patientAndRequestModel = Mapper.Map<PatientAndRequestModel>(_iPreparationService.getPatientAndRequestByReferralId(id));
            clinicalTriageActionViewModel.patientAndRequestModel.RemainingDecision = clinicalTriageActionViewModel.patientAndRequestModel.RequestDetail.Count(s => s.RFAStatus == GlobalConst.RFAStatus.SendToClinical && s.DecisionID == 0);
            if (clinicalTriageActionViewModel.patientAndRequestModel.Patients != null)
            {
                int _patientID = clinicalTriageActionViewModel.patientAndRequestModel.Patients.PatientID;
                clinicalTriageActionViewModel.PatientMedicalRecordViewModels = GetPatientMedicalRecord(_patientID, GlobalConst.Records.Skip);
                clinicalTriageActionViewModel.rfaDiagnosisViewModel = getRFADiagnosis(id, GlobalConst.Records.Skip);
                clinicalTriageActionViewModel.AcceptedBodyParts = _iPreparationService.getAcceptedBodyPartsByReferralID(id);
                clinicalTriageActionViewModel.DeniedBodyParts = _iPreparationService.getDeniedBodyPartsByReferralID(id);
                clinicalTriageActionViewModel.DelayedBodyParts = _iPreparationService.getDelayedBodyPartByReferralID(id);
                clinicalTriageActionViewModel.Diagnosis = _iPreparationService.getDignosisByReferralID(id);
                clinicalTriageActionViewModel.CombroidCondition = _iCommonService.getPatientComorbidConditionsByPatientID(_patientID).Replace("&nbsp;", " ");
                clinicalTriageActionViewModel.rfaPatMedicalRecordReviewViewModels = getRFAPatMedicalRecordReview(_patientID, id, GlobalConst.Records.Skip);
                clinicalTriageActionViewModel.ProcessLevel = ProcessLevel;
                clinicalTriageActionViewModel.PreviousProcessLevel = _iCommonService.getPreviousProcessLevelsByReferralID(id).ProcessLevel;
                if (ProcessLevel == GlobalConst.ProcessLevel.ClinicalAudit)
                    clinicalTriageActionViewModel.RFAReferral = GetSignatureAndDescription(id);
            }
            return View(clinicalTriageActionViewModel);
        }

        [HttpPost]
        public ActionResult SaveAddtionalInfo(IEnumerable<RFAAdditionalInfo> rfaAdditionalInfo)
        {
            int returnValue = 0;
            if (rfaAdditionalInfo.Select(a => a.RFAAdditionalInfoID).First() == 0)
            {
                rfaAdditionalInfo = rfaAdditionalInfo.Where(a => a.RFAAdditionalInfoRecord != null).ToList();
                for (int i = 0; i < rfaAdditionalInfo.Count(); i++)
                {
                    returnValue = _iPreparationService.addRFAAdditionalInfo(Mapper.Map<MMCService.PreparationService.RFAAdditionalInfo>(rfaAdditionalInfo.ElementAt(i)));
                }
            }
            else
            {
                rfaAdditionalInfo.ElementAt(0).RFAAdditionalInfoRecordDate = _iPreparationService.getRFAAdditionalInfoById(rfaAdditionalInfo.ElementAt(0).RFAAdditionalInfoID).RFAAdditionalInfoRecordDate;
                returnValue = _iPreparationService.updateRFAAdditionalInfo(Mapper.Map<MMCService.PreparationService.RFAAdditionalInfo>(rfaAdditionalInfo.ElementAt(0)));
            }
            return Json(returnValue);
        }
        [HttpPost]
        public ActionResult SaveRFARequestBilling(IEnumerable<RFARequestBilling> RFARequestBilling)
        {
            int a = _iIntakeService.addRFARequestBilling(Mapper.Map<IEnumerable<RFARequestBilling>, IEnumerable<MMCService.IntakeService.RFARequestBilling>>(RFARequestBilling).ToArray());
            return Json(a);
        }

        [HttpPost]
        public ActionResult getDurationType()
        {
            IEnumerable<DurationType> durationTypes = Mapper.Map<IEnumerable<DurationType>>(_iCommonService.getDurationTypesAll());
            return Json(durationTypes, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult getAllRFAAdditionalInfo(int ReferralID, int skip)
        {
            var rfaAdditionalInfo = _iPreparationService.getAllRFAAdditionalInfo(ReferralID, skip, GlobalConst.Records.Take);
            RFAAdditionalInfoViewModel rfaViewModel = new RFAAdditionalInfoViewModel();
            rfaViewModel.RFAAdditionalInfoDetails = Mapper.Map<IEnumerable<RFAAdditionalInfo>>(rfaAdditionalInfo.RFAAdditionalInfoDetails);
            rfaViewModel.TotalCount = rfaAdditionalInfo.TotalCount;
            return Json(rfaViewModel);
        }
        [HttpPost]
        public ActionResult getDeniedRationaleDetail(int ReferralID)
        {
            PatientClaimStatus _PatientClaimStatus = new PatientClaimStatus();
            _PatientClaimStatus = Mapper.Map<PatientClaimStatus>(_iPatientService.getPatientClaimStatusByRefferalID(ReferralID));
            return Json(_PatientClaimStatus, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult getUnableToReviewLettersDetail(int RequestID)
        {
            RFARequestAdditionalInfoDetail _RFARequestAdditionalInfo = new RFARequestAdditionalInfoDetail();
            _RFARequestAdditionalInfo = Mapper.Map<RFARequestAdditionalInfoDetail>(_iIntakeService.GetRFARequestAdditionalInfo(RequestID));
            return Json(_RFARequestAdditionalInfo, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult updateDecisionByRequestID(RFARequest rfaRequest)
        {
            int i = 0;
            if (rfaRequest.ProcessLevel == GlobalConst.ProcessLevel.ClinicalAudit && rfaRequest.DecisionID != null)
                rfaRequest.DecisionDate = System.DateTime.Now;
            i = _iPreparationService.updateDecisionByRequestID(Mapper.Map<MMCService.PreparationService.RFARequest>(rfaRequest));
            if (rfaRequest.DecisionID == GlobalConst.Decision.Modify && rfaRequest.IsModify)
            {
                RFARequestModify _RFARequestModify = new RFARequestModify();
                _RFARequestModify.RFARequestID = rfaRequest.RFARequestID;
                _RFARequestModify.RFARequestedTreatment = rfaRequest.RFARequestedTreatment;
                _RFARequestModify.RFAFrequency = rfaRequest.RFAFrequency;
                _RFARequestModify.RFADuration = rfaRequest.RFADuration;
                _RFARequestModify.RFADurationTypeID = rfaRequest.RFADurationTypeID.Value;
                _RFARequestModify.RFARequestModifyID = rfaRequest.RFARequestModifyID;
                _iIntakeService.saveRFARequestModify(Mapper.Map<MMCService.IntakeService.RFARequestModify>(_RFARequestModify));
            }
            else if (rfaRequest.DecisionID == GlobalConst.Decision.Deferral && rfaRequest.IsDeniedRationale)
            {
                PatientClaimStatus _PatientClaimStatus = new PatientClaimStatus
                {
                    PatientClaimStatusID = rfaRequest.PatientClaimStatusID,
                    DeniedRationale = rfaRequest.DeniedRationale
                };
                _iPatientService.updateDeniedRationale(Mapper.Map<MMCService.PatientService.PatientClaimStatus>(_PatientClaimStatus));
            }
            else if (rfaRequest.DecisionID == GlobalConst.Decision.UnabletoReview && rfaRequest.IsUnableToLetter)
            {
                RFARequestAdditionalInfo _RFARequestAdditionalInfo = new RFARequestAdditionalInfo();
                _RFARequestAdditionalInfo.RFARequestAdditionalInfoID = rfaRequest.RFARequestAdditionalInfoID;
                _RFARequestAdditionalInfo.URDeclineInternalAppeal = rfaRequest.URDeclineInternalAppeal;
                _RFARequestAdditionalInfo.URIncompleteRFAForm = rfaRequest.URIncompleteRFAForm;
                _RFARequestAdditionalInfo.URNoRFAForm = rfaRequest.URNoRFAForm;
                _RFARequestAdditionalInfo.RFARequestID = rfaRequest.RFARequestID;
                if (rfaRequest.RFARequestAdditionalInfoID != 0)
                    _iIntakeService.updateRFARequestAdditionalInfo(Mapper.Map<MMCService.IntakeService.RFARequestAdditionalInfo>(_RFARequestAdditionalInfo));
                else
                    _iIntakeService.addRFARequestAdditionalInfo(Mapper.Map<MMCService.IntakeService.RFARequestAdditionalInfo>(_RFARequestAdditionalInfo));
            }
            else if (rfaRequest.DecisionID == GlobalConst.Decision.Duplicate && rfaRequest.IsDuplicate)
            {
                RFARequestAdditionalInfo _RFARequestAdditionalInfo = new RFARequestAdditionalInfo();
                _RFARequestAdditionalInfo.OriginalRFARequestID = rfaRequest.OriginalRFARequestID;
                _RFARequestAdditionalInfo.RFARequestID = rfaRequest.RFARequestID;
                _iIntakeService.SaveRFARequestAdditionalInfo(Mapper.Map<MMCService.IntakeService.RFARequestAdditionalInfo>(_RFARequestAdditionalInfo));
            }
            return Json(i);
        }
        public RFADiagnosisViewModel getRFADiagnosis(int _rfaReferralId, int skip)
        {
            RFADiagnosisViewModel RFADiagnosisViewModel = new RFADiagnosisViewModel();
            var RFADiagnosis = _iIntakeService.getRFADiagnosisByReferralID(_rfaReferralId, skip, GlobalConst.Records.Take5);
            RFADiagnosisViewModel.RFADiagnosisDetails = Mapper.Map<IEnumerable<RFADiagnosis>>(RFADiagnosis.RFADiagnosisDetails);
            RFADiagnosisViewModel.TotalCount = RFADiagnosis.TotalCount;
            return RFADiagnosisViewModel;
        }
        public RFAPatMedicalRecordReviewViewModel getRFAPatMedicalRecordReview(int _patientID, int _rfaReferralId, int skip)
        {
            RFAPatMedicalRecordReviewViewModel _rfaPatMedicalRecordReviewViewModel = new RFAPatMedicalRecordReviewViewModel();
            var rfaPatMedicalRecordReview = _iIntakeService.getRFAPatMedicalRecordReviewByPatientID(_patientID, _rfaReferralId, skip, GlobalConst.Records.Take5);
            _rfaPatMedicalRecordReviewViewModel.RFAPatMedicalRecordReviewDetails = Mapper.Map<IEnumerable<RFAPatMedicalRecordReviewDetail>>(rfaPatMedicalRecordReview.RFAPatMedicalRecordReviewDetails);
            _rfaPatMedicalRecordReviewViewModel.TotalCount = rfaPatMedicalRecordReview.TotalCount;
            return _rfaPatMedicalRecordReviewViewModel;
        }
        [HttpPost]
        public ActionResult getRFADiagnosisByReferralID(int _rfaReferralId, int _skip)
        {
            return Json(getRFADiagnosis(_rfaReferralId, _skip), GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult getRemainingRequest(int _rfaReferralId)
        {
            return Json(_iPreparationService.getRemainingRequest(_rfaReferralId), GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult getPatMedicalRecordReview(int _patientID, int _rfaReferralId, int _skip)
        {
            return Json(getRFAPatMedicalRecordReview(_patientID, _rfaReferralId, _skip), GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult getAllPatMedicalRecord(int _patientid)
        {
            return Json(GetPatientMedicalRecord(_patientid, 0), GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult SavePatMedicalRecordReview(IEnumerable<RFAPatMedicalRecordReview> RFAPatMedicalRecordReview)
        {
            foreach (RFAPatMedicalRecordReview _patMedicalRecordReview in RFAPatMedicalRecordReview)
            {
                if (_patMedicalRecordReview.IsChecked)
                    _patMedicalRecordReview.RFAPatMedicalRecordReviewedID = _iIntakeService.addRFAPatMedicalRecordReview(Mapper.Map<MMCService.IntakeService.RFAPatMedicalRecordReview>(_patMedicalRecordReview));
            }
            return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult SaveTimeExtensionDetail(IEnumerable<RFAPatMedicalRecordReview> RFAPatMedicalRecordReview, RFARequestTimeExtension RFARequestTimeExtension)
        {
            _iIntakeService.DeleteRFARequestTimeExtensionInfo(RFARequestTimeExtension.RFAReferralID);
            var list_RequestID = Mapper.Map<IEnumerable<RFARequest>>(_iIntakeService.getRFARequestByReferralID(RFARequestTimeExtension.RFAReferralID)).Where(x => x.DecisionID == null && (x.RFAStatus == null || x.RFAStatus == "SendToPreparation")).Select(x => x.RFARequestID).ToList();
            foreach (var requestid in list_RequestID)
            {
                foreach (RFAPatMedicalRecordReview _patMedicalRecordReview in RFAPatMedicalRecordReview)
                {
                    if (_patMedicalRecordReview.IsChecked)
                    {
                        RFARequestTimeExtension.RFARequestID = requestid;
                        RFARequestTimeExtension.RFARecSpltID = _patMedicalRecordReview.RFARecSpltID;
                        if (RFARequestTimeExtension.TimeExtension == 1)
                        {
                            RFARequestTimeExtension.RFIRecords = "RFIRecords";
                            RFARequestTimeExtension.AdditionalExamination = null;
                            RFARequestTimeExtension.SpecialtyConsult = null;
                        }
                        else if (RFARequestTimeExtension.TimeExtension == 2)
                        {
                            RFARequestTimeExtension.RFIRecords = null;
                            RFARequestTimeExtension.SpecialtyConsult = null;
                        }
                        else
                        {
                            RFARequestTimeExtension.RFIRecords = null;
                            RFARequestTimeExtension.AdditionalExamination = null;
                        }
                        _iIntakeService.addRFARequestTimeExtensionInfo(Mapper.Map<MMCService.IntakeService.RFARequestTimeExtension>(RFARequestTimeExtension));
                    }
                }
            }
            return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);

        }

        [HttpPost]
        public ActionResult UpdateRFARequestLatestDueDate(int _rfaReferralId, string DownloadToken)
        {
            DateTime duedate = _iIntakeService.UpdateRFARequestLatestDueDateByRefferalID(_rfaReferralId, GlobalConst.TimeExtension.TimeExtensionDays, MMCUser.UserId);
            Tuple<string, string> t = DownloadLetter(_rfaReferralId, GlobalConst.FileType.TimeExtensionPNLetter);
            Response.AppendCookie(new HttpCookie(GlobalConst.HttpCookieValue.fileDownloadToken, DownloadToken));
            Response.AppendCookie(new HttpCookie(GlobalConst.HttpCookieValue.RFADueDate, duedate.ToString("MM/dd/yyyy")));

            string[] strPath = System.Text.RegularExpressions.Regex.Split(t.Item1, System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualPathFolders.Storage].ToString());

            Response.AppendCookie(new HttpCookie("TimeExtensionFilePath", "/Storage" + strPath[1].ToString()));

            Response.AppendCookie(new HttpCookie("TimeExtensionFilePathName", t.Item2));


            return File(t.Item1, GlobalConst.FileDownloadExtension.Application_Pdf, t.Item2);
        }
        public FileResult GenerateLetter(int _referralID, string DownloadToken)
        {
            Tuple<string, string> t = DownloadLetter(_referralID, GlobalConst.FileType.RFIPreparationLetter);
            Response.AppendCookie(new HttpCookie(GlobalConst.HttpCookieValue.fileDownloadToken, DownloadToken));
            return File(t.Item1, GlobalConst.FileDownloadExtension.Application_Pdf, t.Item2);

        }
        [HttpPost]
        public ActionResult GenerateTimeExtensionLetter(int _referralID, string DownloadToken)
        {
            Tuple<string, string> t = DownloadLetter(_referralID, GlobalConst.FileType.TimeExtensionLetter);
            Response.AppendCookie(new HttpCookie("TimeExtensionDownloadToken", DownloadToken));
            string[] strPath = System.Text.RegularExpressions.Regex.Split(t.Item1, System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualPathFolders.Storage].ToString());
            Response.AppendCookie(new HttpCookie("TimeExtensionFilePath", "/Storage" + strPath[1].ToString()));
            Response.AppendCookie(new HttpCookie("TimeExtensionFilePathName", t.Item2));
            return File(t.Item1, GlobalConst.FileDownloadExtension.Application_Pdf, t.Item2);

        }
        public Tuple<string, string> DownloadLetter(int _referralID, int LetterName)
        {
            string Filename = "";
            string reportURL = "";
            string hostURL = GlobalConst.Extension.http + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.ConstantChar.ForwardSlash;
            
            StorageModel _storageModel = new StorageModel();
            MMCApp.Domain.Models.IntakeModel.RFAReferralFile _rfaReferralFile = new MMCApp.Domain.Models.IntakeModel.RFAReferralFile();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_referralID, GlobalConst.ConstantChar.Char_R));
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
            switch (LetterName)
            {
                case 7:
                    Filename = _referralID.ToString() + "_" + GlobalConst.VirtualPathFolders.RFI + "_" + DateTime.Now.ToString("ddMMyyyyss") + GlobalConst.Extension.pdf;
                    _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RFILetter], _referralID, hostURL, GlobalConst.Extension.PDF);
                    _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.RFIPreparationLetter;



                    break;
                case 2:
                    Filename = _referralID.ToString() + "_" + GlobalConst.VirtualPathFolders.InitialNotificationLetter + System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_").Trim() + GlobalConst.Extension.pdf;

                    _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptInitialNotification], _referralID, hostURL, GlobalConst.Extension.PDF);
                    _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.InitialNotification;
                    break;
                case 8:
                    Filename = _referralID.ToString() + "_" + GlobalConst.VirtualPathFolders.NoRFALetter + System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_").Trim() + GlobalConst.Extension.pdf;
                    _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptNoRFAForm], _referralID, hostURL, GlobalConst.Extension.PDF);
                    _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.NoRFALetter;
                    break;
                case 9:
                    Filename = _referralID.ToString() + "_" + GlobalConst.VirtualPathFolders.DuplicateLetter + System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_").Trim() + GlobalConst.Extension.pdf;
                    _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptRFADuplicate], _referralID, hostURL, GlobalConst.Extension.PDF);
                    _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.DuplicateLetter;
                    break;
                case 10:
                    Filename = _referralID.ToString() + "_" + GlobalConst.VirtualPathFolders.DeferralLetter + System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_").Trim() + GlobalConst.Extension.pdf;
                    _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptRFADeferral], _referralID, hostURL, GlobalConst.Extension.PDF);
                    _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.DeferralLetter;
                    break;
                case 11:
                    Filename = _referralID.ToString() + "_" + GlobalConst.VirtualPathFolders.TimeExtensionPNLetter + GlobalConst.Extension.pdf;
                    _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptRFATimeExtensionPN], _referralID, hostURL, GlobalConst.Extension.PDF);
                    _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.TimeExtensionPNLetter;
                    break;
                case 15:
                    Filename = _referralID.ToString() + "_" + GlobalConst.VirtualPathFolders.TimeExtensionLetter + GlobalConst.Extension.pdf;
                    _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptRFATimeExtension], _referralID, hostURL, GlobalConst.Extension.PDF);
                    _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.TimeExtensionLetter;
                    break;
            }
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
            if (System.IO.File.Exists(savePath))
            {
                System.IO.File.Delete(savePath);
            }
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.DownloadFile(reportURL, savePath);
            _rfaReferralFile.RFAReferralID = _referralID;
            _rfaReferralFile.RFAReferralFileName = Filename;
            _rfaReferralFile.RFAFileCreationDate = DateTime.Now;
            _rfaReferralFile.RFAFileUserID = MMCUser.UserId;
            int RFAReferralFileID = _iIntakeService.addReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_rfaReferralFile));

            // link the RFI Template with the Request whose desician and status is null
            if (LetterName == GlobalConst.FileType.RFIPreparationLetter)
            {
                _iPreparationService.AddRFITemplateRecordByRFARequestID(RFAReferralFileID, MMCUser.UserId);
            }
            else if ((LetterName == GlobalConst.FileType.TimeExtensionPNLetter) || (LetterName == GlobalConst.FileType.TimeExtensionLetter))
            {
                _iPreparationService.AddRFARequestTimeExtensionRecordByRFARequestID(RFAReferralFileID, MMCUser.UserId);
            }

            _storageModel.path = null;
            savePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString() + Path.Combine(_storageModel.ClientID.ToString(), _storageModel.PatientID.ToString(), _storageModel.ClaimID.ToString(), _storageModel.ReferralID.ToString(), _storageModel.FolderName) + GlobalConst.ConstantChar.DoubleBackSlash + Filename);
            Tuple<string, string> savePathWithFileName = new Tuple<string, string>(savePath, Filename);
            if (LetterName == 11 || LetterName == 15)
            {
                string _timeExtension = Regex.Replace(savePathWithFileName.ToString(), "[()|{}]", "");
                Tuple<string, string> t = CombinedLinkForAttachement(_timeExtension);
                string fullPathFile = t.Item1 + "," + t.Item2;
                if (LetterName == 11)
                {
                    fullPathFile = Regex.Replace(fullPathFile, "[()|{}]", "");
                    Response.AppendCookie(new HttpCookie("TimeExtensionPNFullFilePath", fullPathFile));
                }
                else
                {
                    fullPathFile = Regex.Replace(fullPathFile, "[()|{}]", "");
                    Response.AppendCookie(new HttpCookie("TimeExtensionFullFilePath", fullPathFile));
                }
            }
            return savePathWithFileName;

        }
        [HttpPost]
        public ActionResult SendRFIReportEmailWithAttachment(RFIReport _rfaReport)
        {
            string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()) + _rfaReport.RFAReportDocumentPath.Trim().Replace("Storage", "").Replace("/", "\\");
            var myList = new List<string>();
            myList.Add(path);
            var myListReferralIDorFileID = GetRFAReferralFileIDOrReferralID(_rfaReport.popUpID);   // 2 popUp ID for TimeExtension or TimeExtensionPN  or 1 for RFI 
            _mailService.SendEmail(_rfaReport.RFAReportMailContent, _rfaReport.RFAReportEmailTo, _rfaReport.RFAReportEmailCC, _rfaReport.RFAReportSubject, myList.ToArray());
            EmailRecordStorage(_rfaReport.RFAReportEmailTo, _rfaReport.RFAReportEmailCC, _rfaReport.RFAReportSubject, _rfaReport.RFAReportMailContent, myList, _rfaReport.RFAReferralFileID, _rfaReport.popUpID, myListReferralIDorFileID,0);
            return Json(GlobalConst.Message.EmailSendMessage, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult UpdateRFAAdditionalInfoRecordDateByID(int rFAAdditionalInfoID, string cDate, int _rFARequestID)
        {
            RFAAdditionalInfo _rFAAdditionalInfo = new RFAAdditionalInfo();
            _rFAAdditionalInfo = Mapper.Map<RFAAdditionalInfo>(_iPreparationService.getRFAAdditionalInfoById(rFAAdditionalInfoID));
            _rFAAdditionalInfo.RFAAdditionalInfoRecordDate = Convert.ToDateTime(cDate);
            _iPreparationService.updateRFAAdditionalInfo(Mapper.Map<serviceModel.PreparationService.RFAAdditionalInfo>(_rFAAdditionalInfo));
            if (_rFARequestID > 0)
                _iIntakeService.AddRFIReportAdditionalRecordByRFARequestID(rFAAdditionalInfoID, _rFARequestID, MMCUser.UserId);
            else
                _iIntakeService.AddRFIReportAdditionalRecordByRFAReferralFileID(rFAAdditionalInfoID, MMCUser.UserId);

            return Json(GlobalConst.Message.RFAFileCreationDateSuccessfully, GlobalConst.ContentTypes.TextHtml);
        } 

        #region New Process Preperation by Mahi
        private void ProcesUnableToreviewDeferralDuplicateNewFlow(RequestByReferralID request, int newRFAReferralid, int RFAReferralID,int IsDownload)
        {            
            AssNewRefferalToRequest(newRFAReferralid, request.RFARequestID, request.DecisionDate);
            if (IsDownload == 0)
            {
                _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.Notifications);
                if (RFAReferralID != newRFAReferralid)
                {
                    ManageRFASplittedReferralHistory(RFAReferralID, newRFAReferralid);
                    ManageRFAMergedReferralHistory(RFAReferralID, newRFAReferralid);
                }
            }
            if (request.DecisionID == GlobalConst.Decision.Duplicate)
            {
                if (IsDownload == 0)
                {
                    DownloadLetter(newRFAReferralid, GlobalConst.FileType.DuplicateLetter);
                    TempData[GlobalConst.ConstantName.DuplicateInitial] = DownloadLetter(newRFAReferralid, GlobalConst.FileType.InitialNotification);
                }
            }
            if (request.DecisionID == GlobalConst.Decision.Deferral)
            {
                if (IsDownload == 0)
                {
                    TempData[GlobalConst.ConstantName.DeferralInitial] = DownloadLetter(newRFAReferralid, GlobalConst.FileType.InitialNotification);
                    DownloadLetter(newRFAReferralid, GlobalConst.FileType.DeferralLetter);
                                     
                }
            }
            if (request.DecisionID == GlobalConst.Decision.UnabletoReview)
            {
                if (IsDownload == 0)
                {
                    DownloadLetter(newRFAReferralid, GlobalConst.FileType.NoRFALetter);
                    TempData[GlobalConst.ConstantName.UnabletoReviewInitial] = DownloadLetter(newRFAReferralid, GlobalConst.FileType.InitialNotification);
                }
            }
          
           // int OldRFAReferralID = _iIntakeService.getRFARequestByID(request.RFARequestID).RFAReferralID.Value;
            _iPreparationService.UpdateRFAAdditionalInfoDetailByRequestID(RFAReferralID, request.RFARequestID);
        }

        private int RequestTypeAccToDecision(int DecisionID)
        {
            int _decision = 0;

            switch (DecisionID)
            {
                case 8:
                    _decision = GlobalConst.FileType.NoRFALetter;
                    break;
                case 9:
                    _decision = GlobalConst.FileType.DuplicateLetter;
                    break;
                default:
                    _decision = GlobalConst.FileType.DeferralLetter;
                    break;
            }

            return _decision;
        }

        private void ManageRFAMergedReferralHistory(int RFAOldReferralID, int RFANewReferralID)
        {
            RFAMergedReferralHistory _rfaMergeReferralHistory = new RFAMergedReferralHistory();
            _rfaMergeReferralHistory.RFAOldReferralID = RFAOldReferralID;
            _rfaMergeReferralHistory.RFANewReferralID = RFANewReferralID;
            _rfaMergeReferralHistory.RFAMergedReferralDate = System.DateTime.Now;
            _iIntakeService.addRFAMergedReferralHistory(Mapper.Map<MMCService.IntakeService.RFAMergedReferralHistory>(_rfaMergeReferralHistory));
            
        }

        private void ProcessOnlyOneUnableToReviewDuplicateDeferral(RequestByReferralID request, int newRFAReferralid, int RFAReferralID)
        {
            AssNewRefferalToRequest(newRFAReferralid, request.RFARequestID, request.DecisionDate);
            _iPreparationService.updateProcessLevel(newRFAReferralid, GlobalConst.ProcessLevel.Notifications);
            if (RFAReferralID != newRFAReferralid)
            {
                ManageRFASplittedReferralHistory(RFAReferralID, newRFAReferralid);
                ManageRFAMergedReferralHistory(RFAReferralID, newRFAReferralid);
            }
            int OldRFAReferralID = _iIntakeService.getRFARequestByID(request.RFARequestID).RFAReferralID.Value;
            _iPreparationService.UpdateRFAAdditionalInfoDetailByRequestID(OldRFAReferralID, request.RFARequestID);
        }

        private int DeferralProcessOne(int RFAReferralID,RFAReferral _oldRFAReferral)
        {
            int _newDeferralRFAReferralID = 0;
            _newDeferralRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(RFAReferralID, GlobalConst.Decision.Deferral);
            if (_newDeferralRFAReferralID == GlobalConst.ConstantChar.Zero)
            {
                _newDeferralRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DeferralRFAReferralID] = _newDeferralRFAReferralID.ToString();
            }
            else
            {
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DeferralRFAReferralID] = _newDeferralRFAReferralID.ToString();
                var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDeferralRFAReferralID, GlobalConst.FileType.DeferralLetter);
                var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDeferralRFAReferralID, GlobalConst.FileType.InitialNotification);
            }
            return _newDeferralRFAReferralID;
        }

        private int DuplicateProcessOne(int RFAReferralID,RFAReferral _oldRFAReferral)
        {
            int _newDuplicateRFAReferralID = 0;
            _newDuplicateRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(RFAReferralID, GlobalConst.Decision.Duplicate);
            if (_newDuplicateRFAReferralID == GlobalConst.ConstantChar.Zero)
            {
                _newDuplicateRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DuplicateRFAReferralID] = _newDuplicateRFAReferralID.ToString();
            }
            else
            {
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.DuplicateRFAReferralID] = _newDuplicateRFAReferralID.ToString();
                var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDuplicateRFAReferralID, GlobalConst.FileType.DuplicateLetter);
                var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newDuplicateRFAReferralID, GlobalConst.FileType.InitialNotification);
            }
            return _newDuplicateRFAReferralID;
        }

        private int UnableToReviewProcessOne(int RFAReferralID,RFAReferral _oldRFAReferral)
        {
            int _newUnableToReviewRFAReferralID = 0;
            _newUnableToReviewRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(RFAReferralID, GlobalConst.Decision.UnabletoReview);
            if (_newUnableToReviewRFAReferralID == GlobalConst.ConstantChar.Zero)
            {
                _newUnableToReviewRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.UnableToReviewRFAReferralID] = _newUnableToReviewRFAReferralID.ToString();
            }
            else
            {
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.UnableToReviewRFAReferralID] = _newUnableToReviewRFAReferralID.ToString();
                var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newUnableToReviewRFAReferralID, GlobalConst.FileType.NoRFALetter);
                var _delIntial = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newUnableToReviewRFAReferralID, GlobalConst.FileType.InitialNotification);
            }
            return _newUnableToReviewRFAReferralID;
        }

        private int ClientAuthorizedProcessOne(int RFAReferralID,RFAReferral _oldRFAReferral)
        {
            int _newClientAuthorizedRFAReferralID = 0;
            _newClientAuthorizedRFAReferralID = _iPreparationService.getRFANewReferralIDFromRFAOldReferralID(RFAReferralID, GlobalConst.Decision.ClientAuthorized);
            if (_newClientAuthorizedRFAReferralID == GlobalConst.ConstantChar.Zero)
            {
                _newClientAuthorizedRFAReferralID = _iIntakeService.addRFAReferral(Mapper.Map<MMCService.IntakeService.RFAReferral>(_oldRFAReferral));
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.ClientAuthorizedRFAReferralID] = _newClientAuthorizedRFAReferralID.ToString();
            }
            else
            {
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.ClientAuthorizedRFAReferralID] = _newClientAuthorizedRFAReferralID.ToString();
                var _del = _iPreparationService.DeleteRFAReferralIDFromReferralFiles(_newClientAuthorizedRFAReferralID, GlobalConst.FileType.InitialNotification);
            }
            return _newClientAuthorizedRFAReferralID;
        }

        private Tuple<string,string> CombinedLinkForAttachement(string urlPathData)
        {
            string URL = "";
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();           
            string[] paths = urlPathData.Split(',');
            foreach (string path in paths)
            {
                URL = path.Replace(toSearched, toReplace);
                break;
            }
            Tuple<string, string> savePathWithFileName = new Tuple<string, string>(urlPathData, URL);
            return savePathWithFileName;
        }
        #endregion

        #region Email & Email Record Attachment        
        [HttpPost]
        public ActionResult EmailSendMultipleDoc(EmailMultipleAttachment objEmailAttached)
        {
            try
            {
                int _checkCommingFromTEProofOfService = 0;
                int popID = 0;
                var myListReferralIDorFileID = new List<string>();
                var myList = new List<string>();
                if (objEmailAttached.AttachmentForEmailArray != null)
                {
                    foreach (var multipath in objEmailAttached.AttachmentForEmailArray)
                    {
                        myList.Add(multipath.AttachmentLink);

                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(multipath.AttachmentLink);
                        var _checkForList = fileNameWithoutExtension.Split('_');
                        string _timeExtension = _checkForList[1];
                        if (_timeExtension == "TimeExtensionProofOfService")
                        {
                            _checkCommingFromTEProofOfService = 1;
                        }
                    }
                }
                if (_checkCommingFromTEProofOfService == 1)
                {
                    myListReferralIDorFileID = GetRFAReferralFileIDOrReferralID(2);
                    popID = 2;
                }
                else
                {
                     myListReferralIDorFileID = GetRFAReferralFileIDOrReferralID(0);  // Zero popup Id for  Preperation,Clincal Triage,Clincal Audit Email Popup
                }
                
                _mailService.SendEmail(objEmailAttached.SendEmailText,objEmailAttached.SendEMailTo,objEmailAttached.SendEMailCc,objEmailAttached.Sendsubject, myList.ToArray());
                EmailRecordStorage(objEmailAttached.SendEMailTo, objEmailAttached.SendEMailCc, objEmailAttached.Sendsubject, objEmailAttached.SendEmailText, myList, 0, popID, myListReferralIDorFileID, objEmailAttached.rflID);
                return Json(GlobalConst.Message.EmailSendMessage, GlobalConst.ContentTypes.TextHtml);
            }
            catch(Exception ex)
            {
                return Json(GlobalConst.Message.ErrorMessage, GlobalConst.ContentTypes.TextHtml);
            }
        }
        public void EmailRecordStorage(string EMailTo, string EMailCc, string subject, string EmailText, List<string> myList, int RFAReferralFileID, int popUpID, List<string> myListReferralIDorFileID,int RFAReferralID)
        {           
                EmailRecord _emailRecord = new EmailRecord();
                _emailRecord.EmRecTo = EMailTo;
                _emailRecord.EmRecCC = EMailCc;
                _emailRecord.EmRecSubject = subject;
                _emailRecord.EmRecBody = EmailText;
                _emailRecord.EmailRecDate = DateTime.Now;
                _emailRecord.UserID = MMCUser.UserId;

                int _emailRecordID = _iEmailRecordAttachmentService.addEmailRecord(Mapper.Map<MMCService.EmailRecordAttachmentService.EmailRecord>(_emailRecord));
                
                if (popUpID!=0)
                {
                    if (popUpID == 2)// Time Extension or TimeExtension PN
                    {

                        _iEmailRecordAttachmentService.AddEmailRecordAndRFARequestLinkByRFITimeExtension(RFAReferralID, Convert.ToInt32(myListReferralIDorFileID[0]), _emailRecordID);

                    }
                    else
                    {   // 1 popUpID for RFI
                        _iEmailRecordAttachmentService.AddEmailRecordAndRFARequestLinkByRFITimeExtension(0,RFAReferralFileID, _emailRecordID);
                    }                                    
                }
                else
                {
                    foreach (var _id in myListReferralIDorFileID)
                    {
                        _iEmailRecordAttachmentService.AddEmailRecordAndRFARequestLinkByRFAReferralID(Convert.ToInt32(_id), _emailRecordID);
                    }
                }               
                foreach (var _list in myList)
                {
                    EmailRecordAttachment _emailRecordAttachment = new EmailRecordAttachment();
                    string URL = "";
                    string urlPathData = _list.ToString();
                    string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
                    URL = urlPathData.Replace(toSearched, toReplace);
                    Tuple<string, string> savePathWithDownloadPath = new Tuple<string, string>(urlPathData, URL);
                    string _urlData = savePathWithDownloadPath.ToString();
                    _urlData = _urlData.Replace("(", "");
                    _urlData = _urlData.Replace(")", "");

                    string _fileName = Path.GetFileName(urlPathData);
                    _emailRecordAttachment.EmailRecordId = _emailRecordID;
                    _emailRecordAttachment.DocumentName = _fileName;
                    _emailRecordAttachment.DocumentPath = _urlData;
                    int _emailRecordAttachmentID = _iEmailRecordAttachmentService.addEmailRecordAttachment(Mapper.Map<MMCService.EmailRecordAttachmentService.EmailRecordAttachment>(_emailRecordAttachment));
                }
        }       
        public List<string> GetRFAReferralFileIDOrReferralID(int popUpID)
        {
            var myList = new List<string>();

            if (popUpID != 0)
            {
                var _referralID = Request.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.RFAReferralFileID];
                myList.Add(_referralID);
            }
            else
            {             
                List<string> cookieNameLists = new List<string>(new string[] { GlobalConst.ConstantName.DeferralRFAReferralID, GlobalConst.ConstantName.DuplicateRFAReferralID, GlobalConst.ConstantName.UnableToReviewRFAReferralID, GlobalConst.ConstantName.ClientAuthorizedRFAReferralID, GlobalConst.ConstantName.AuditRFAReferralID });
                foreach (var _name in cookieNameLists)
                {
                    if (Request.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][_name] != null)
                    {
                        var _referralID = Request.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][_name];
                        myList.Add(_referralID);
                    }
                }
                
            }
            return myList;
        }
        #endregion

        #region Withdrawn Decision OR Client Authorized Method
        [HttpPost]
        public ActionResult UploadFileOnWithdrawnORClientAuthorizedDecision(RFAReferralFile _rfaReferralFile, int uploadRFARequestID,string uploadTypeCheck)
        {
            int _uploadType = 0;
            string _uploadFileNamedynamic = "";
            if (uploadTypeCheck == "Withdrawn")
            {
                _uploadType = GlobalConst.FileType.WithdrawnUpload;
                _uploadFileNamedynamic = "_Withdrawn";
            }
            else
            {
                _uploadType = GlobalConst.FileType.ClientAuthorizedUpload;
                _uploadFileNamedynamic = "_ClientAuthorized";
            }

            try
            {
                StorageModel _storageModel = new StorageModel();
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_rfaReferralFile.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                string filename = uploadRFARequestID + _uploadFileNamedynamic + GlobalConst.Extension.pdf;
                string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + filename;
                _rfaReferralFile.rfaReferralFile.SaveAs(savePath);

                MMCApp.Domain.Models.IntakeModel.RFAReferralFile _ReferralFileObj = new MMCApp.Domain.Models.IntakeModel.RFAReferralFile();
                _ReferralFileObj.RFAReferralID = _rfaReferralFile.RFAReferralID;
                _ReferralFileObj.RFAReferralFileName = filename;
                _ReferralFileObj.RFAFileCreationDate = DateTime.Now;
                _ReferralFileObj.RFAFileUserID = MMCUser.UserId;
                _ReferralFileObj.RFAFileTypeID = _uploadType;
                int RFAReferralFileID = _iIntakeService.addReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_ReferralFileObj));
                return Json(GlobalConst.Message.UploadedSucessfully);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }

        public void UpdateReferralIDAccordingToWithdrawnReferralID(int OldRFAReferralID,int newRFAReferralID,string CheckUploadType)
        {
            int _uploadType = 0; 
            if (CheckUploadType == "ClientAuthorized")
            {
                _uploadType = GlobalConst.FileType.ClientAuthorizedUpload;
            }
            else
            {
               _uploadType = GlobalConst.FileType.WithdrawnUpload;
            }

            var ReferralFileNotification = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIntakeService.getReferralFileByRFAReferralIDAndFileTypeID(OldRFAReferralID, _uploadType));

           foreach (RFAReferralFile referralData in ReferralFileNotification)
           {

               StorageModel _storageModel = new StorageModel();
               _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(OldRFAReferralID, GlobalConst.ConstantChar.Char_R));
               _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
               _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
               string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + referralData.RFAReferralFileName;


               StorageModel _storageModelNew = new StorageModel();
               _storageModelNew = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(newRFAReferralID, GlobalConst.ConstantChar.Char_R));
               _storageModelNew.FolderName = GlobalConst.FolderName.LegalDocs;
               _storageModelNew.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
               string savePathNew = _storageService.GeneateStorage(_storageModelNew) + GlobalConst.ConstantChar.DoubleBackSlash + referralData.RFAReferralFileName;
                           
               WebClient client = new WebClient();
               client.Credentials = CredentialCache.DefaultNetworkCredentials;
               client.DownloadFile(savePath, savePathNew);

               if (System.IO.File.Exists(savePath))
               {
                   System.IO.File.Delete(savePath);
               }

               MMCApp.Domain.Models.IntakeModel.RFAReferralFile _ReferralFileObj = new MMCApp.Domain.Models.IntakeModel.RFAReferralFile();
               _ReferralFileObj.RFAReferralFileID = referralData.RFAReferralFileID;
               _ReferralFileObj.RFAReferralID = newRFAReferralID;
               _ReferralFileObj.RFAReferralFileName = referralData.RFAReferralFileName;
               _ReferralFileObj.RFAFileCreationDate = DateTime.Now;
               _ReferralFileObj.RFAFileUserID = MMCUser.UserId;
               _ReferralFileObj.RFAFileTypeID = _uploadType;
               int RFAReferralFileID = _iIntakeService.updateReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_ReferralFileObj));
           }
        }
        #endregion 
        #region Proof of Service Generate Report
        [HttpPost]
        public ActionResult GenerateProofOfService(int id)
        {
            HttpCookie cookie = new HttpCookie(GlobalConst.ConstantName.PreviousAttachementLinks);
            StorageModel _storageModel = new StorageModel();
            MMCApp.Domain.Models.IntakeModel.RFAReferralFile _rfaReferralFile = new MMCApp.Domain.Models.IntakeModel.RFAReferralFile();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string Filename = id.ToString() + GlobalConst.ReportName.TimeExtensionProofOfService + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;    
            User objUserModel = new User();
            var _UserResult = _iUserService.getUserByID(MMCUser.UserId);
            objUserModel = Mapper.Map<User>(_UserResult);
            string uSignURL = GlobalConst.Extension.http + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.VirtualPathFolders.StorageUserPath + objUserModel.UserId + GlobalConst.ConstantChar.ForwardSlash + objUserModel.UserSignatureFileName;

            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptProofOfService], id, objUserModel.UserFirstName + ' ' + objUserModel.UserLastName, uSignURL, GlobalConst.Extension.PDF);
            
            if (System.IO.File.Exists(savePath))
            {
                System.IO.File.Delete(savePath);
            }
            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.DownloadFile(reportURL, savePath);
                client.Dispose();
            }

            Tuple<string, string> savePathWithFileName = new Tuple<string, string>(savePath, Filename);
            string _proofOfService = Regex.Replace(savePathWithFileName.ToString(), "[()|{}]", "");
            Tuple<string, string> t = CombinedLinkForAttachement(_proofOfService);
            string fullPathFile = t.Item1 + "," + t.Item2;
            Response.AppendCookie(new HttpCookie("ProofOfServiceFullFilePath", fullPathFile));

            _rfaReferralFile.RFAReferralID = id;
            _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.TimeExtensionProofOfService;
            _rfaReferralFile.RFAReferralFileName = Filename;
            _rfaReferralFile.RFAFileCreationDate = DateTime.Now;
            _rfaReferralFile.RFAFileUserID = MMCUser.UserId;
            _iIntakeService.addReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_rfaReferralFile));
            return Json("");
        }

        [HttpGet]
        public FileResult GetProofOfServiceFile(int id)
        {
            StorageModel _storageModel = new StorageModel();            
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string Filename = id.ToString() + GlobalConst.ReportName.TimeExtensionProofOfService + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
            return File(savePath, GlobalConst.FileDownloadExtension.Application_Pdf, Filename);
        }

        [HttpPost]
        public ActionResult GetAllLinksFromTimeExtensionPNorProofOfService(string _timeExtensionPN, string _timeExtension, string _proofOfService,int _RfaReferralID)
        {            
            List<string> _listURlDownloadInitial = new List<string>();
            HttpCookie cookie = new HttpCookie(GlobalConst.ConstantName.PreviousAttachementLinks);

            if (_timeExtensionPN != "")
            {
                _listURlDownloadInitial.Add(_timeExtensionPN);
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCTimeExtensionPN] = _timeExtensionPN;
            }
            else
            {
                StorageModel _storageModelPN = new StorageModel();
                _storageModelPN = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_RfaReferralID, GlobalConst.ConstantChar.Char_R));
                _storageModelPN.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                _storageModelPN.FolderName = GlobalConst.FolderName.LegalDocs;

                string FilenamePN = _RfaReferralID.ToString() + "_" + GlobalConst.VirtualPathFolders.TimeExtensionPNLetter + GlobalConst.Extension.pdf;
                string savePathPN = _storageService.GeneateStorage(_storageModelPN) + GlobalConst.ConstantChar.DoubleBackSlash + FilenamePN;

                Tuple<string, string> savePathWithFileNamePN = new Tuple<string, string>(savePathPN, FilenamePN);
                string _TimePN = Regex.Replace(savePathWithFileNamePN.ToString(), "[()|{}]", "");
                Tuple<string, string> tPN = CombinedLinkForAttachement(_TimePN);
                string fullPathFilePN = tPN.Item1 + "," + tPN.Item2;

                _listURlDownloadInitial.Add(fullPathFilePN);
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCTimeExtensionPN] = fullPathFilePN;

            }

            if (_timeExtension != "")
            {
                _listURlDownloadInitial.Add(_timeExtension);
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCTimeExtension] = _timeExtension;
            }
            else
            {
                StorageModel _storageModel = new StorageModel();
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_RfaReferralID, GlobalConst.ConstantChar.Char_R));
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

                string Filename = _RfaReferralID.ToString() + "_" + GlobalConst.VirtualPathFolders.TimeExtensionLetter + GlobalConst.Extension.pdf;
                string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;

                Tuple<string, string> savePathWithFileName = new Tuple<string, string>(savePath, Filename);
                string _TimeExten = Regex.Replace(savePathWithFileName.ToString(), "[()|{}]", "");
                Tuple<string, string> t = CombinedLinkForAttachement(_TimeExten);
                string fullPathFile = t.Item1 + "," + t.Item2;

                _listURlDownloadInitial.Add(fullPathFile);
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCTimeExtension] = fullPathFile;
            }

            if (_proofOfService != "")
            {               
                _listURlDownloadInitial.Add(_proofOfService);
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCProofOfService] = _proofOfService;
            }
            return Json(_listURlDownloadInitial);
        }

        [HttpPost]
        public ActionResult ClinicalTriageActionAfterUpdateRequest(int id, int ProcessLevel)
        {
            HttpCookie cookie = new HttpCookie(GlobalConst.ConstantName.PreviousAttachementLinks);
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;
            Editor1.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            Editor1.MvcInit();
            ViewData["Editor1"] = Editor1.MvcGetString();
            Editor Editor2 = new Editor(System.Web.HttpContext.Current, "Editor2");
            Editor2.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            Editor2.Width = Unit.Pixel(1050);
            Editor2.Height = Unit.Pixel(660);
            Editor2.ResizeMode = RTEResizeMode.Disabled;
            Editor2.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            Editor2.MvcInit();
            ViewData["Editor2"] = Editor2.MvcGetString();
            Editor Editor3 = new Editor(System.Web.HttpContext.Current, "Editor3");
            Editor3.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            Editor3.Width = Unit.Pixel(1050);
            Editor3.Height = Unit.Pixel(660);
            Editor3.ResizeMode = RTEResizeMode.Disabled;
            Editor3.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            Editor3.MvcInit();
            ViewData["Editor3"] = Editor3.MvcGetString();
            Editor Editor4 = new Editor(System.Web.HttpContext.Current, "Editor4");
            Editor4.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            Editor4.Width = Unit.Pixel(1050);
            Editor4.Height = Unit.Pixel(660);
            Editor4.ResizeMode = RTEResizeMode.Disabled;
            Editor4.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            Editor4.MvcInit();
            ViewData["Editor4"] = Editor4.MvcGetString();
            Editor EditorNote = new Editor(System.Web.HttpContext.Current, GlobalConst.Richtexteditor.EditorNote);
            EditorNote.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            EditorNote.Width = Unit.Pixel(1050);
            EditorNote.Height = Unit.Pixel(660);
            EditorNote.ResizeMode = RTEResizeMode.Disabled;
            EditorNote.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            EditorNote.MvcInit();
            ViewData["EditorNote"] = EditorNote.MvcGetString();
            ClinicalTriageActionViewModel clinicalTriageActionViewModel = new ClinicalTriageActionViewModel();
            clinicalTriageActionViewModel.patientAndRequestModel = Mapper.Map<PatientAndRequestModel>(_iPreparationService.getPatientAndRequestByReferralId(id));
            clinicalTriageActionViewModel.patientAndRequestModel.RemainingDecision = clinicalTriageActionViewModel.patientAndRequestModel.RequestDetail.Count(s => s.RFAStatus == GlobalConst.RFAStatus.SendToClinical && s.DecisionID == 0);
            if (clinicalTriageActionViewModel.patientAndRequestModel.Patients != null)
            {
                int _patientID = clinicalTriageActionViewModel.patientAndRequestModel.Patients.PatientID;
                clinicalTriageActionViewModel.PatientMedicalRecordViewModels = GetPatientMedicalRecord(_patientID, GlobalConst.Records.Skip);
                clinicalTriageActionViewModel.rfaDiagnosisViewModel = getRFADiagnosis(id, GlobalConst.Records.Skip);
                clinicalTriageActionViewModel.AcceptedBodyParts = _iPreparationService.getAcceptedBodyPartsByReferralID(id);
                clinicalTriageActionViewModel.DeniedBodyParts = _iPreparationService.getDeniedBodyPartsByReferralID(id);
                clinicalTriageActionViewModel.DelayedBodyParts = _iPreparationService.getDelayedBodyPartByReferralID(id);
                clinicalTriageActionViewModel.Diagnosis = _iPreparationService.getDignosisByReferralID(id);
                clinicalTriageActionViewModel.CombroidCondition = _iCommonService.getPatientComorbidConditionsByPatientID(_patientID).Replace("&nbsp;", " ");
                clinicalTriageActionViewModel.rfaPatMedicalRecordReviewViewModels = getRFAPatMedicalRecordReview(_patientID, id, GlobalConst.Records.Skip);
                clinicalTriageActionViewModel.ProcessLevel = ProcessLevel;
                clinicalTriageActionViewModel.PreviousProcessLevel = _iCommonService.getPreviousProcessLevelsByReferralID(id).ProcessLevel;
                if (ProcessLevel == GlobalConst.ProcessLevel.ClinicalAudit)
                    clinicalTriageActionViewModel.RFAReferral = GetSignatureAndDescription(id);
            }
            return Json(clinicalTriageActionViewModel);
        }
        #endregion 

        [NonAction]
        public void getNoOfReferralAccordingToProcessLevels()
        {
            var _NoOFReferral = _iCommonService.getNoOfReferralCountAccToProcessLevel();

            ViewBag.IntakeCount = _NoOFReferral[0].IntakeCount;
            ViewBag.PreparationCount = _NoOFReferral[0].PreparationCount;
            ViewBag.ClinicalTriageCount = _NoOFReferral[0].ClinicalTriageCount;
            ViewBag.ClinicalAuditCount = _NoOFReferral[0].ClinicalAuditCount;
            ViewBag.NotificationCount = _NoOFReferral[0].NotificationCount;
            ViewBag.BillingCount = _NoOFReferral[0].BillingCount;
            ViewBag.PeerReviewCount = _NoOFReferral[0].PeerReviewCount;
            ViewBag.IMRCount = _NoOFReferral[0].IMRCount;
        }
    }
}
