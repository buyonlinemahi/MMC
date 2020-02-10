using AutoMapper;
using MMCApp.Domain.Models.VendorModel;
using MMCApp.Domain.ViewModels.VendorViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;
namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class VendorController : Controller
    {
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public VendorController()
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
        public ActionResult GetVenderByName(string _searchText, int? skip)
        {
            var _vendorDetails = _iPaticipantService.getVendorsByName(_searchText.Trim(), skip.Value, GlobalConst.Records.LandingTake);
            VendorSearchViewModel objVendorList = new VendorSearchViewModel();
            objVendorList.VendorDetails = Mapper.Map<IEnumerable<Vendor>>(_vendorDetails.VendorDetails);
            objVendorList.TotalCount = _vendorDetails.TotalCount;
            return Json(objVendorList);
        }

        public ActionResult SaveVendorDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            if (id != null)
            {
                return View(Mapper.Map<Vendor>(_iPaticipantService.getVendorByID(id.Value)));
            }
            else
                return View();
        }
        [HttpPost]
        public ActionResult SaveVendorDetail(Vendor _vendor)
        {
            var message = GlobalConst.ConstantChar.StringBlank;
            try
            {

                _vendor.VendorName = _vendor.VendorName.Trim();
                if (_vendor.VendorID != GlobalConst.ConstantChar.Zero)
                {
                    if (_iPaticipantService.updateVendor(Mapper.Map<MMCService.PaticipantService.Vendor>(_vendor)) > GlobalConst.ConstantChar.Zero)
                        message = GlobalConst.Message.UpdateMessage;
                    else
                        message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    if (_iPaticipantService.addVendor(Mapper.Map<MMCService.PaticipantService.Vendor>(_vendor)) > GlobalConst.ConstantChar.Zero)
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
        public ActionResult DeleteVendorById(int id)
        {
            try
            {
                _iPaticipantService.deleteVendor(id);
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
