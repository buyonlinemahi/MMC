using AutoMapper;
using MMCApp.Domain.Models.AdjusterModel;
using MMCApp.Domain.ViewModels.AdjusterViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MMC.Controllers
{
     [AuthorizedUserCheck]
    public class AdjusterController : Controller
    {
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public AdjusterController()
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
        public ActionResult GetAdjusterByName(string _searchText, int? _skip)
        {
            var _adjusterDetails = _iPaticipantService.getadjustersByName(_searchText.Trim(), _skip.Value, GlobalConst.Records.LandingTake);
            AdjusterSearchViewModel objAdjusterList = new AdjusterSearchViewModel();
            objAdjusterList.AdjusterDetails = Mapper.Map<IEnumerable<Adjuster>>(_adjusterDetails.AdjusterDetails);
            objAdjusterList.TotalCount = _adjusterDetails.TotalCount;
            return Json(objAdjusterList, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpGet]
        public ActionResult SaveAdjusterDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            if (id != null)
            {
                Adjuster _objAdjusterModel = Mapper.Map<Adjuster>(_iPaticipantService.getAdjusterByID(id.Value));
                return View(_objAdjusterModel);
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult SaveAdjusterDetail(Adjuster _objAdjuster)
        {
            var _message = GlobalConst.ConstantChar.StringBlank;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _objAdjuster.AdjFirstName = _objAdjuster.AdjFirstName.Trim();
                _objAdjuster.AdjLastName = _objAdjuster.AdjLastName.Trim();

                if (_objAdjuster.AdjusterID.ToString() == GlobalConst.ConstantChar.StringBlank)
                {
                    if (_iPaticipantService.addAdjuster(Mapper.Map<MMCService.PaticipantService.Adjuster>(_objAdjuster)) > 0)
                        _message = GlobalConst.Message.SaveMessage;
                    else
                        _message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    if (_iPaticipantService.updateAdjuster(Mapper.Map<MMCService.PaticipantService.Adjuster>(_objAdjuster)) > 0)
                        _message = GlobalConst.Message.UpdateMessage;
                    else
                        _message = GlobalConst.Message.ErrorMessage;
                }
            }
            else
                _message = GlobalConst.Message.ModelErrorMessage;
            return Json(_message, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult DeleteAdjusterByID(int _adjusterID)
        {
            try
            {
                _iPaticipantService.deleteAdjuster(_adjusterID);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
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
