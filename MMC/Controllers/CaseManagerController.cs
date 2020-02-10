using AutoMapper;
using MMCApp.Domain.Models.CaseManagerModel;
using MMCApp.Domain.ViewModels.CaseManagerViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class CaseManagerController : Controller
    {
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public CaseManagerController()
        {
            _iPaticipantService = new MMCService.PaticipantService.PaticipantServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
        }
        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }
        [HttpPost]
        public ActionResult GetCaseManagerByName(string _searchText, int? _skip)
        {
            var _CaseManagerDetails = _iPaticipantService.getCaseManagerByName(_searchText, _skip.Value, GlobalConst.Records.LandingTake);
            CaseManagerSearchViewModel objCaseManagerList = new CaseManagerSearchViewModel();
            objCaseManagerList.CaseManagerDetails = Mapper.Map<IEnumerable<CaseManager>>(_CaseManagerDetails.CaseManagerDetails);
            objCaseManagerList.TotalCount = _CaseManagerDetails.TotalCount;
            return Json(objCaseManagerList, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpGet]
        public ActionResult SaveCaseManagerDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            CaseManager objCaseManagerModel = new CaseManager();
            if (id != null)
            {
                //var _CaseManagerResult = _iPaticipantService.getCaseManagerByID(id.Value);
                objCaseManagerModel = Mapper.Map<CaseManager>(_iPaticipantService.getCaseManagerByID(id.Value));
            }
            return View(objCaseManagerModel);
        }

        [HttpPost]
        public ActionResult SaveCaseManagerDetail(CaseManager _objCaseManager)
        {
            var _message = GlobalConst.ConstantChar.StringBlank;
            if (ModelState.IsValid)
            {


                _objCaseManager.CMFirstName = _objCaseManager.CMFirstName.Trim();
                _objCaseManager.CMLastName= _objCaseManager.CMLastName.Trim();

                if (_objCaseManager.CaseManagerID == GlobalConst.ConstantChar.Zero)
                {
                    if (_iPaticipantService.addCaseManager(Mapper.Map<MMCService.PaticipantService.CaseManager>(_objCaseManager)) > 0)
                        _message = GlobalConst.Message.SaveMessage;
                    else
                        _message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    if (_iPaticipantService.updateCaseManager(Mapper.Map<MMCService.PaticipantService.CaseManager>(_objCaseManager)) > 0)
                        _message = GlobalConst.Message.UpdateMessage;
                    else
                        _message = GlobalConst.Message.ErrorMessage;
                }
            }
            else
            {
                _message = GlobalConst.Message.ModelErrorMessage;
            }
            return Json(_message, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult DeleteCaseManagerByID(int _caseManagerID)
        {
            try
            {
                _iPaticipantService.deleteCaseManager(_caseManagerID);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }
        [HttpGet]
        public JsonResult getCaseManagerAll()
        {
            IEnumerable<CaseManager> objCaseManager = Mapper.Map<IEnumerable<CaseManager>>(_iPaticipantService.getCaseManagerAll());
            foreach (var _objCaseManager in objCaseManager)
            {
                _objCaseManager.CaseManagerName = _objCaseManager.CMFirstName + " " + _objCaseManager.CMLastName;
            }
            return Json(objCaseManager, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
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
