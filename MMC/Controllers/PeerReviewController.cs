using AutoMapper;
using MMCApp.Domain.Models.ClinicalTriageModel;
using MMCApp.Domain.Models.IntakeModel;
using MMCApp.Domain.Models.PeerReviewModel;
using MMCApp.Domain.ViewModels.ClinicalTriageViewModel;
using MMCApp.Domain.ViewModels.PeerReviewViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using RTE;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class PeerReviewController : Controller
    {
             MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
             MMCService.PreparationService.PreparationServiceClient _iPreparationService;
             MMCService.IntakeService.IntakeServiceClient _IntakeService;
             MMCService.CommonService.CommonServiceClient _iCommonService;
        public PeerReviewController()
        {
            _iPaticipantService = new MMCService.PaticipantService.PaticipantServiceClient();
            _iPreparationService = new MMCService.PreparationService.PreparationServiceClient();
            _IntakeService = new MMCService.IntakeService.IntakeServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
        }
        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }

        [HttpPost]
        public ActionResult GetPeerReviewByName(string _searchText, int? _skip)
        {
            var _PeerReviewDetails = _iPaticipantService.getPeerReviewsByName(_searchText.Trim(), _skip.Value, GlobalConst.Records.LandingTake);
            PeerReviewSearchViewModel objPeerReviewList = new PeerReviewSearchViewModel();
            objPeerReviewList.PeerReviewDetails = Mapper.Map<IEnumerable<PeerReview>>(_PeerReviewDetails.PeerReviewDetails);
            objPeerReviewList.TotalCount = _PeerReviewDetails.TotalCount;
            return Json(objPeerReviewList, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpGet]
        public ActionResult SavePeerReviewDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            if (id != null)
            {
                PeerReview _objPeerReviewModel = Mapper.Map<PeerReview>(_iPaticipantService.getPeerReviewByID(id.Value));
                return View(_objPeerReviewModel);
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult SavePeerReviewDetail(PeerReview _objPeerReview)
        {
            var _message = GlobalConst.ConstantChar.StringBlank;

                _objPeerReview.PeerReviewName = _objPeerReview.PeerReviewName.Trim();
              
                if (_objPeerReview.PeerReviewID == GlobalConst.ConstantChar.Zero)
                {
                    if (_iPaticipantService.addPeerReview(Mapper.Map<MMCService.PaticipantService.PeerReview>(_objPeerReview)) > 0)
                        _message = GlobalConst.Message.SaveMessage;
                    else
                        _message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    if (_iPaticipantService.updatePeerReview(Mapper.Map<MMCService.PaticipantService.PeerReview>(_objPeerReview)) > 0)
                        _message = GlobalConst.Message.UpdateMessage;
                    else
                        _message = GlobalConst.Message.ErrorMessage;
                }
          
            return Json(_message, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult DeletePeerReviewByID(int _PeerReviewID)
        {
            try
            {
                _iPaticipantService.deletePeerReview(_PeerReviewID);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }

        [HttpGet]
        public ActionResult PeerReviewAction(int id)
         {
             getNoOfReferralAccordingToProcessLevels();

            Editor EditorNote = new Editor(System.Web.HttpContext.Current, GlobalConst.Richtexteditor.EditorNote);
            EditorNote.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            EditorNote.Width = Unit.Pixel(1050);
            EditorNote.Height = Unit.Pixel(660);
            EditorNote.ResizeMode = RTEResizeMode.Disabled;
            EditorNote.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            EditorNote.MvcInit();
            ViewData[GlobalConst.Richtexteditor.EditorNote] = EditorNote.MvcGetString();

            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = "/richtexteditor/";
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;
            Editor1.DisabledItems = "save, help";
            Editor1.MvcInit();
            ViewData["Editor1"] = Editor1.MvcGetString();

            PatientAndRequestModel _PatientAndRequestModel = new PatientAndRequestModel();
            _PatientAndRequestModel = Mapper.Map<PatientAndRequestModel>(_iPaticipantService.getPatientAndPeerReviewRequestByReferralId(id));
            return View(_PatientAndRequestModel);
        }

        [HttpPost]
        public ActionResult SaveRequestClinicalReason(string reason, int requestID, int referralID)//, int requestID
        {
            RFARequest request = new RFARequest();
            request.RFARequestID = requestID;
            request.RFAStatus = "Peer Review";
            request.RFAClinicalReasonforDecision = reason;
            _iPreparationService.updateDecisionByRequestID(Mapper.Map<MMCService.PreparationService.RFARequest>(request));
            return Json(CheckClinicalReasonFilled(referralID));
        }

        public bool CheckClinicalReasonFilled(int referralID)
        {
            PatientAndRequestModel _PatientAndRequestModel = new PatientAndRequestModel();
            _PatientAndRequestModel = Mapper.Map<PatientAndRequestModel>(_iPaticipantService.getPatientAndPeerReviewRequestByReferralId(referralID));
            foreach (var item in _PatientAndRequestModel.RequestDetail)
            {
                if (item.RFAClinicalReasonforDecision == "" || item.RFAClinicalReasonforDecision == null)
                {
                    return false;
                }
            }
            
            return true;
        }
        [HttpGet]
        public ActionResult PeerReviewLanding(int? skip)
        {
            getNoOfReferralAccordingToProcessLevels();

            var _clinicalTriageDetails = _iPreparationService.getReferralByProcessLevel(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, 125);
            ClinicalTriageViewModel clinicalTriage = new ClinicalTriageViewModel();
            clinicalTriage.ClinicalTriages = Mapper.Map<IEnumerable<ClinicalTriage>>(_clinicalTriageDetails.ClinicalTriages);
            clinicalTriage.TotalCount = _clinicalTriageDetails.TotalCount;
            if (skip == null)
                return View(clinicalTriage);
            else
                return Json(clinicalTriage, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);            
        }

        [HttpPost]
        public ActionResult UpdateProcessLevel(int RFAReferralID)
        {
            _iPreparationService.updateProcessLevel(RFAReferralID, GlobalConst.ProcessLevel.ClinicalAudit);
            _IntakeService.UpdateRFARequestDecisionAndRFAStatusByReferralID(RFAReferralID, null, null);
            return Json("Referral Moved to Clinical Audit");
        }

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