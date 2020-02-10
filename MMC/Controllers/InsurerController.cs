using AutoMapper;
using MMCApp.Domain.Models.InsurerModel;
using MMCApp.Domain.ViewModels.InsurerViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class InsurerController : Controller
    {
        //
        // GET: /Insurer/

        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public InsurerController()
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
        public ActionResult GetInsurerByName(string _searchText, int? _skip)
        {
            var _InsurerDetails = _iPaticipantService.getInsurersByName(_searchText.Trim(), _skip.Value, GlobalConst.Records.LandingTake);
            InsurerSearchViewModel objInsurerList = new InsurerSearchViewModel();
            objInsurerList.InsurerDetails = Mapper.Map<IEnumerable<Insurer>>(_InsurerDetails.InsurerDetails);
            objInsurerList.TotalCount = _InsurerDetails.TotalCount;
            return Json(objInsurerList, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpGet]
        public ActionResult SaveInsurerDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            Insurer objInsurerModel = new Insurer();
            if (id != null)
            {
                //var _InsurerResult = _iPaticipantService.getInsurerByID(id.Value);
                objInsurerModel = Mapper.Map<Insurer>(_iPaticipantService.getInsurerByID(id.Value));
            }
            return View(objInsurerModel);
        }

        [HttpPost]
        public ActionResult SaveInsurerDetail(Insurer _objInsurer)
        {
            //  var _message = GlobalConst.ConstantChar.StringBlank;
            if (ModelState.IsValid)
            {
                _objInsurer.InsName = _objInsurer.InsName.Trim();
                if (_objInsurer.InsurerID == GlobalConst.ConstantChar.Zero)
                {
                    int InsurerId = _iPaticipantService.addInsurer(Mapper.Map<MMCService.PaticipantService.Insurer>(_objInsurer));
                    if (InsurerId > 0)
                    {
                        _objInsurer.InsurerID = InsurerId;
                        _objInsurer.AlertMessage = GlobalConst.Message.SaveMessage;
                    }
                    else
                    {
                        _objInsurer.AlertMessage = GlobalConst.Message.ErrorMessage;
                    }
                }
                else
                {

                    if (_iPaticipantService.updateInsurer(Mapper.Map<MMCService.PaticipantService.Insurer>(_objInsurer)) > 0)
                        _objInsurer.AlertMessage = GlobalConst.Message.UpdateMessage;
                    else
                        _objInsurer.AlertMessage = GlobalConst.Message.ErrorMessage;
                }
            }
            else
            {
                _objInsurer.AlertMessage = GlobalConst.Message.ModelErrorMessage;
            }
            return Json(_objInsurer, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult DeleteInsurerByID(int _insurerID)
        {
            try
            {
                _iPaticipantService.deleteInsurer(_insurerID);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }

        [HttpGet]
        public JsonResult GetAllInsurers()
        {
            /*InsurerSearchViewModel ObjInsuranceAll = new InsurerSearchViewModel();
            ObjInsuranceAll.InsurerDetails = Mapper.Map<IEnumerable<Insurer>>(_iPaticipantService.getInsurersAll());                        */
            return Json(Mapper.Map<IEnumerable<Insurer>>(_iPaticipantService.getInsurersAll()), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult GetInsuranceBranchByInsurerID(int _insurerID, int? _skip)
        {
            var _InsuranceBranchDetails = _iPaticipantService.getInsuranceBranchesByInsurerID(_insurerID, Convert.ToInt32(_skip.Value), GlobalConst.Records.LandingTake);
            InsuranceBranchSearchViewModel objInsuranceList = new InsuranceBranchSearchViewModel();
            objInsuranceList.InsuranceBranchDetails = Mapper.Map<IEnumerable<InsuranceBranch>>(_InsuranceBranchDetails.InsuranceBranchDetails);
            objInsuranceList.TotalCount = _InsuranceBranchDetails.TotalCount;
            return Json(objInsuranceList, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult GetInsuranceBranchesAllByInsurerID(int _InsurerID)
        {
            //var _InsuranceBranchDetails = _iPaticipantService.getInsuranceBranchesAllByInsurerID(_InsurerID);
            return Json(_iPaticipantService.getInsuranceBranchesAllByInsurerID(_InsurerID), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveInsuranceBranchDetail(InsuranceBranch _objInsuranceBranch)
        {
            var _message = GlobalConst.ConstantChar.StringBlank;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {

                if (_objInsuranceBranch.InsuranceBranchID.ToString() == GlobalConst.ConstantChar.StringBlank)
                {
                    if (_iPaticipantService.addInsuranceBranch(Mapper.Map<MMCService.PaticipantService.InsuranceBranch>(_objInsuranceBranch)) > 0)
                        _message = GlobalConst.Message.SaveMessage;
                    else
                        _message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    if (_iPaticipantService.updateInsuranceBranch(Mapper.Map<MMCService.PaticipantService.InsuranceBranch>(_objInsuranceBranch)) > 0)
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
        public ActionResult DeleteInsuranceBranchByID(int _insuranceBranchID)
        {
            try
            {
                _iPaticipantService.deleteInsuranceBranch(_insuranceBranchID);
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
