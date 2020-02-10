using AutoMapper;
using MMCApp.Domain.Models.ManagedCareCompanyModel;
using MMCApp.Domain.ViewModels.ManagedCareCompanyViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;
namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class ManagedCareCompanyController : Controller
    {
        //
        // GET: /ManagedCareCompany/

        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public ManagedCareCompanyController()
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
        public ActionResult GetManagedCareCompanyByName(string _searchText, int? _skip)
        {

            ManagedCareCompanySearchViewModel objManagedCareCompany = new ManagedCareCompanySearchViewModel();
            try
            {
                var _managedCareCompanyList = _iPaticipantService.getManagedCareCompanyByName(_searchText.Trim(), _skip.Value, GlobalConst.Records.LandingTake);
                objManagedCareCompany.ManagedCareCompanyDetails = Mapper.Map<IEnumerable<ManagedCareCompany>>(_managedCareCompanyList.ManagedCareCompanyDetails);
                objManagedCareCompany.TotalCount = _managedCareCompanyList.TotalCount;
            }
            catch
            {

            }
            return Json(objManagedCareCompany, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpGet]
        public ActionResult SaveCompanyDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            ManagedCareCompany objManagedCareCompany = new ManagedCareCompany();
            try
            {
                if (id != null)
                {
                    objManagedCareCompany = Mapper.Map<ManagedCareCompany>(_iPaticipantService.getManagedCareCompanyByID(id.Value));
                }
            }
            catch
            {
            }
            return View(objManagedCareCompany);
        }


        [HttpPost]
        public ActionResult SaveCareCompanyDetail(ManagedCareCompany _objManagedCareCompany)
        {
            var _message = GlobalConst.ConstantChar.StringBlank;
            try
            {
                if (ModelState.IsValid)
                {

                    _objManagedCareCompany.CompName = _objManagedCareCompany.CompName.Trim();
                    if (_objManagedCareCompany.CompanyId == GlobalConst.ConstantChar.Zero)
                    {
                        _iPaticipantService.addManagedCareCompany(Mapper.Map<MMCService.PaticipantService.ManagedCareCompany>(_objManagedCareCompany));
                        _message = GlobalConst.Message.SaveMessage;
                    }
                    else
                    {
                        _iPaticipantService.updateManagedCareCompany(Mapper.Map<MMCService.PaticipantService.ManagedCareCompany>(_objManagedCareCompany));
                        _message = GlobalConst.Message.UpdateMessage;
                    }
                }
                else
                {
                    _message = GlobalConst.Message.ErrorMessage;
                }
            }
            catch
            {
                _message = GlobalConst.Message.ErrorMessage;
            }
            return Json(_message);
        }

        [HttpPost]
        public ActionResult DeleteManagedCareCompanyByID(int _companyID)
        {
            var _IsDeleted = GlobalConst.ConstantChar.Zero;
            try
            {
                _iPaticipantService.deleteManagedCareCompany(_companyID);
                _IsDeleted = GlobalConst.ConstantChar.One;
            }
            catch
            {
                _IsDeleted = GlobalConst.ConstantChar.Zero;
            }
            return Json(_IsDeleted);
        }
        [HttpPost]
        public ActionResult GetManagedCareCompanyAll()
        {
            try
            {
                return Json(_iPaticipantService.getManagedCareCompanyAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public ActionResult GetManagedCareCompanyList()
        {
            try
            {
                return Json(_iPaticipantService.getManagedCareCompanyAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("", GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
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
