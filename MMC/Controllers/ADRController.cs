using AutoMapper;
using MMCApp.Domain.Models.ADRModel;
using MMCApp.Domain.ViewModels.ADRViewModel;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MMC.Controllers
{
    public class ADRController : Controller
    {
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public ADRController()
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
        public ActionResult GetADRByName(string _searchText, int? skip)
        {
            var _ADRDetails = _iPaticipantService.getADRsByName(_searchText.Trim(), skip.Value, GlobalConst.Records.LandingTake);
            ADRViewModel objADRList = new ADRViewModel();
            objADRList.ADRDetails = Mapper.Map<IEnumerable<ADR>>(_ADRDetails.ADRDetails);
            objADRList.TotalCount = _ADRDetails.TotalCount;
            return Json(objADRList);
        }
        public ActionResult SaveADRDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            if (id != null)
            {
                return View(Mapper.Map<ADR>(_iPaticipantService.getADRByID(id.Value)));
            }
            else
                return View();
        }
        [HttpPost]
        public ActionResult SaveADRDetail(ADR _ADR)
        {
            var message = GlobalConst.ConstantChar.StringBlank;
            try
            {

                _ADR.ADRName = _ADR.ADRName.Trim();
                if (_ADR.ADRID != GlobalConst.ConstantChar.Zero)
                {
                    if (_iPaticipantService.updateADR(Mapper.Map<MMCService.PaticipantService.ADR>(_ADR)) > GlobalConst.ConstantChar.Zero)
                        message = GlobalConst.Message.UpdateMessage;
                    else
                        message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    if (_iPaticipantService.addADR(Mapper.Map<MMCService.PaticipantService.ADR>(_ADR)) > GlobalConst.ConstantChar.Zero)
                        message = GlobalConst.Message.SaveMessage;
                    else
                        message = GlobalConst.Message.ErrorMessage;
                }
            }
            catch
            {
                message = GlobalConst.Message.ErrorMessage;
            }
            return Json(message);
        }

        public JsonResult getAllADR()
        {
            return Json(_iPaticipantService.getADRsAll(), JsonRequestBehavior.AllowGet);
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