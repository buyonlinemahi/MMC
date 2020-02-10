using AutoMapper;
using MMCApp.Domain.Models.ThirdPartyAdministratorBranchModel;
using MMCApp.Domain.Models.ThirdPartyAdministratorModel;
using MMCApp.Domain.ViewModels.ThirdPartyAdministratorBranchViewModel;
using MMCApp.Domain.ViewModels.ThirdPartyAdministratorViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class ThirdPartyAdministratorController : Controller
    {
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public ThirdPartyAdministratorController()
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
        public ActionResult GetTPAByName(string _searchText, int? skip)
        {
            var _tpaDetails = _iPaticipantService.getThirdPartyAdministratorsByName(_searchText.Trim(), skip.Value, GlobalConst.Records.LandingTake);
            ThirdPartyAdministratorViewModel objTPAList = new ThirdPartyAdministratorViewModel();
            objTPAList.ThirdPartyAdministratorDetails = Mapper.Map<IEnumerable<ThirdPartyAdministrator>>(_tpaDetails.ThirdPartyAdministratorDetails);
            objTPAList.TotalCount = _tpaDetails.TotalCount;
            return Json(objTPAList);
        }
        public ActionResult SaveTPADetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            if (id != null)
            {
                ThirdPartyAdministrator _thirdPartyAdministrator = Mapper.Map<ThirdPartyAdministrator>(_iPaticipantService.getThirdPartyAdministratorByID(id.Value));
                return View(_thirdPartyAdministrator);
            }
            else
                return View();
        }
        [HttpPost]
        public ActionResult SaveTPADetail(ThirdPartyAdministrator _thirdPartyAdministrator)
        {
            var message = GlobalConst.ConstantChar.StringBlank;
            int i = GlobalConst.ConstantChar.Zero;
            try
            {

                _thirdPartyAdministrator.TPAName = _thirdPartyAdministrator.TPAName.Trim();
                if (_thirdPartyAdministrator.TPAID != GlobalConst.ConstantChar.Zero)
                {
                    i = _iPaticipantService.updateThirdPartyAdministrator(Mapper.Map<MMCService.PaticipantService.ThirdPartyAdministrator>(_thirdPartyAdministrator));
                    if (i > GlobalConst.ConstantChar.Zero)
                    {
                        message = GlobalConst.Message.UpdateMessage;
                        i = _thirdPartyAdministrator.TPAID;
                    }
                    else
                        message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    i = _iPaticipantService.addThirdPartyAdministrator(Mapper.Map<MMCService.PaticipantService.ThirdPartyAdministrator>(_thirdPartyAdministrator));
                    if (i > GlobalConst.ConstantChar.Zero)
                        message = GlobalConst.Message.SaveMessage;
                    else
                        message = GlobalConst.Message.ErrorMessage;
                }
            }
            catch
            {
                message = GlobalConst.Message.ErrorMessage;
            }
            object[] obj = { message, i };
            return Json(obj);
        }
        [HttpPost]
        public ActionResult DeleteTPAById(int id)
        {
            try
            {
                _iPaticipantService.deleteThirdPartyAdministrator(id);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }

        [HttpPost]
        public ActionResult SaveTPABranchDetail(ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch)
        {
            var message = GlobalConst.ConstantChar.StringBlank;
            try
            {
                if (_thirdPartyAdministratorBranch.TPABranchID != GlobalConst.ConstantChar.Zero)
                {
                    int i = _iPaticipantService.updateThirdPartyAdministratorBranch(Mapper.Map<MMCService.PaticipantService.ThirdPartyAdministratorBranch>(_thirdPartyAdministratorBranch));
                    if (i > GlobalConst.ConstantChar.Zero)
                        message = GlobalConst.Message.UpdateMessage;
                    else
                        message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    int i = _iPaticipantService.addThirdPartyAdministratorBranch(Mapper.Map<MMCService.PaticipantService.ThirdPartyAdministratorBranch>(_thirdPartyAdministratorBranch));
                    if (i > GlobalConst.ConstantChar.Zero)
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
        [HttpPost]
        public ActionResult GetTPABranchByTPAID(int _TPAID, int skip)
        {
            var _tpaBranchDetails = _iPaticipantService.getThirdPartyAdministratorBranchesByTPAID(_TPAID, skip, GlobalConst.Records.LandingTake);
            ThirdPartyAdministratorBranchViewModel objTPABranchList = new ThirdPartyAdministratorBranchViewModel();
            objTPABranchList.ThirdPartyAdministratorBranchDetails = Mapper.Map<IEnumerable<ThirdPartyAdministratorBranch>>(_tpaBranchDetails.ThirdPartyAdministratorBranchDetails);
            objTPABranchList.TotalCount = _tpaBranchDetails.TotalCount;
            return Json(objTPABranchList);
        }
        [HttpPost]
        public ActionResult DeleteTPABranchByID(int id)
        {
            try
            {
                _iPaticipantService.deleteThirdPartyAdministratorBranch(id);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }
        [HttpPost]
        public ActionResult GetTPABranchByID(int id)
        {
            ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch = Mapper.Map<ThirdPartyAdministratorBranch>(_iPaticipantService.getThirdPartyAdministratorBranchByID(id));
            return Json(_thirdPartyAdministratorBranch);
        }
        [HttpPost]
        public ActionResult GetThirdPartyAdministratorsAll()
        {
            IEnumerable<ThirdPartyAdministrator> _thirdPartyAdministratorBranch = Mapper.Map<IEnumerable<ThirdPartyAdministrator>>(_iPaticipantService.getThirdPartyAdministratorsAll());
            return Json(_thirdPartyAdministratorBranch);
        }
        [HttpGet]
        public ActionResult GetThirdPartyAdministratorsList()
        {
            IEnumerable<ThirdPartyAdministrator> _thirdPartyAdministratorBranch = Mapper.Map<IEnumerable<ThirdPartyAdministrator>>(_iPaticipantService.getThirdPartyAdministratorsAll());
            return Json(_thirdPartyAdministratorBranch, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetThirdPartyAdministratorsAllByTPAID(int _TPAID)
        {
            IEnumerable<ThirdPartyAdministratorBranch> _thirdPartyAdministratorBranch = Mapper.Map<IEnumerable<ThirdPartyAdministratorBranch>>(_iPaticipantService.getThirdPartyAdministratorBranchesAllByTPAID(_TPAID));
            return Json(_thirdPartyAdministratorBranch, GlobalConst.ContentTypes.TextHtml);
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
