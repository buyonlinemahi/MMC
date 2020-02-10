using AutoMapper;
using MMCApp.Domain.Models.PhysicianModel;
using MMCApp.Domain.ViewModels.PhysicianViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class PhysicianController : Controller
    {
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public PhysicianController()
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
        public ActionResult GetPhysicianByID(int _physicianID)
        {
            /*Physician objphysicanList = new Physician();
            var physicanList = _iPaticipantService.getPhysicianByID(_physicianID);
            objphysicanList = Mapper.Map<Physician>(_iPaticipantService.getPhysicianByID(_physicianID));*/
            return Json(Mapper.Map<Physician>(_iPaticipantService.getPhysicianByID(_physicianID)));
        }

        [HttpPost]
        public ActionResult GetPhysicianByName(string _searchText, int? _skip)
        {
            PhysicianSearchViewModel objphysicanList = new PhysicianSearchViewModel();
            var physicanList = _iPaticipantService.GetPhysicianByName(_searchText.Trim(), _skip.Value, GlobalConst.Records.LandingTake);
            objphysicanList.PhysicianDetails = Mapper.Map<IEnumerable<Physician>>(physicanList.PhysicianDetails);
            objphysicanList.TotalCount = physicanList.TotalCount;
            return Json(objphysicanList, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult GetPhysicianByNamePatient(string _searchText, int? _skip)
        {
            PhysicianSearchViewModel objphysicanList = new PhysicianSearchViewModel();
            var physicanList = _iPaticipantService.GetPhysicianByName(_searchText.Trim(), _skip.Value, GlobalConst.Records.Take);
            objphysicanList.PhysicianDetails = Mapper.Map<IEnumerable<Physician>>(physicanList.PhysicianDetails);
            objphysicanList.TotalCount = physicanList.TotalCount;
            return Json(objphysicanList, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult GetPhysicianByNPI(string _searchText, int? _skip)
        {
            PhysicianSearchViewModel objphysicanList = new PhysicianSearchViewModel();
            var physicanList = _iPaticipantService.GetPhysicianByNPI(_searchText.Trim(), _skip.Value, GlobalConst.Records.LandingTake);
            objphysicanList.PhysicianDetails = Mapper.Map<IEnumerable<Physician>>(physicanList.PhysicianDetails);
            objphysicanList.TotalCount = physicanList.TotalCount;
            return Json(objphysicanList, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult GetPhysicianBySpeciality(int _searchID, int? _skip)
        {
            PhysicianSearchViewModel objphysicanList = new PhysicianSearchViewModel();
            var physicanList = _iPaticipantService.GetPhysicianBySpeciality(_searchID, _skip.Value, GlobalConst.Records.LandingTake);
            objphysicanList.PhysicianDetails = Mapper.Map<IEnumerable<Physician>>(physicanList.PhysicianDetails);
            objphysicanList.TotalCount = physicanList.TotalCount;
            return Json(objphysicanList, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpGet]
        public ActionResult SavePhysicianDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            Physician objPhysicianModel = new Physician();
            try
            {
                if (id != null)
                {
                    //var _PhysicianResult = _iPaticipantService.getPhysicianByID(id.Value);
                    objPhysicianModel = Mapper.Map<Physician>(_iPaticipantService.getPhysicianByID(id.Value));
                }
            }
            catch
            {
            }
            return View(objPhysicianModel);
        }

        [HttpPost]
        public ActionResult SavePhysicianDetail(Physician _objPhysician)
        {
            var _message = GlobalConst.ConstantChar.StringBlank;
            try
            {
                if (ModelState.IsValid)
                {
                    if (_objPhysician.PhysicianId == GlobalConst.ConstantChar.Zero)
                    {
                        _iPaticipantService.addPhysician(Mapper.Map<MMCService.PaticipantService.Physician>(_objPhysician));
                        _message = GlobalConst.Message.SaveMessage;
                    }
                    else
                    {
                        _iPaticipantService.updatePhysician(Mapper.Map<MMCService.PaticipantService.Physician>(_objPhysician));
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
        public ActionResult DeletePhysicianByID(int _physicianID)
        {
            var _IsDeleted = GlobalConst.ConstantChar.Zero;
            try
            {
                _iPaticipantService.deletePhysician(_physicianID);
                _IsDeleted = GlobalConst.ConstantChar.One;
            }
            catch 
            {
                _IsDeleted = GlobalConst.ConstantChar.Zero;
            }
            return Json(_IsDeleted);
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
