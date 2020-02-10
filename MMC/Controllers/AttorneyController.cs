using AutoMapper;
using MMCApp.Domain.Models.AttorneyModel;
using MMCApp.Domain.ViewModels.AttorneyViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class AttorneyController : Controller
    {
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public AttorneyController()
        {
            _iPaticipantService = new MMCService.PaticipantService.PaticipantServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
        }
        // GET: Attorney
        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }
        [HttpPost]
        public ActionResult GetAttorneyFirmByName(string _searchText, int? _skip)
        {
            var _attorneyFirmDetails = _iPaticipantService.getAttorneyAndAttorneyFirmByName(_searchText.Trim(), _skip.Value, GlobalConst.Records.LandingTake);
            AttorneyFirmViewModel objAttorneyFirmViewModelList = new AttorneyFirmViewModel();
            objAttorneyFirmViewModelList.AttorneyFirmDetails = Mapper.Map<IEnumerable<AttorneyFirm>>(_attorneyFirmDetails.AttorneyFirmDetails);
            objAttorneyFirmViewModelList.TotalCount = _attorneyFirmDetails.TotalCount;
            return Json(objAttorneyFirmViewModelList, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult GetAttorneyByAttorneyFirmID(int _attorneyFirmID, int? _skip)
        {
            var _attorneyDetails = _iPaticipantService.getAttorneyByAttorneyFirmID(_attorneyFirmID, _skip.Value, GlobalConst.Records.LandingTake);
            AttorneyViewModel objAttorneyViewModelList = new AttorneyViewModel();
            objAttorneyViewModelList.AttorneyDetails = Mapper.Map<IEnumerable<Attorney>>(_attorneyDetails.AttorneyDetails);
            objAttorneyViewModelList.TotalCount = _attorneyDetails.TotalCount;
            return Json(objAttorneyViewModelList, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult GetAttorneyByID(int _attorneyID)
        {
            var _attorneyDetails = _iPaticipantService.getAttorneyByID(_attorneyID);
            Attorney objAttorneyModel = new Attorney();
            objAttorneyModel = Mapper.Map<Attorney>(_attorneyDetails);
            return Json(objAttorneyModel);
        }
        [HttpGet]
        public ActionResult SaveAttorneyFirmDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            AttorneyFirmSearchViewModel objAttorneyFirmModel = new AttorneyFirmSearchViewModel();
            try
            {
                if (id != null)
                {
                    //var _AttorneyFirmSearchResult = _iPaticipantService.getAttorneyFirmByID(id.Value);
                    objAttorneyFirmModel.AttorneyFirmDetails = Mapper.Map<AttorneyFirm>(_iPaticipantService.getAttorneyFirmByID(id.Value));
                    var _AttorneySearchResult = _iPaticipantService.getAttorneyByAttorneyFirmID(id.Value, GlobalConst.Records.Skip, GlobalConst.Records.Take);
                    objAttorneyFirmModel.AttorneyDetails = Mapper.Map<IEnumerable<Attorney>>(_AttorneySearchResult.AttorneyDetails);
                    objAttorneyFirmModel.TotalCount = _AttorneySearchResult.TotalCount;
                }
            }
            catch
            {
            }
            return View(objAttorneyFirmModel);
        }
        [HttpPost]
        public ActionResult SaveAttorneyFirmDetail(AttorneyFirm objAttorneyFirmModel)
        {


            var _Result = GlobalConst.ConstantChar.Zero;
            var _arr = new List<int>();
            try
            {
                objAttorneyFirmModel.AttorneyFirmName = objAttorneyFirmModel.AttorneyFirmName.Trim();
                if (objAttorneyFirmModel.AttorneyFirmID == GlobalConst.ConstantChar.Zero)
                {
                    _Result = _iPaticipantService.addAttorneyFirm(Mapper.Map<MMCService.PaticipantService.AttorneyFirm>(objAttorneyFirmModel));
                    _arr.Add(_Result);
                    _arr.Add(GlobalConst.ConstantChar.One);
                }
                else
                {
                    _Result = _iPaticipantService.updateAttorneyFirm(Mapper.Map<MMCService.PaticipantService.AttorneyFirm>(objAttorneyFirmModel));
                    _arr.Add(objAttorneyFirmModel.AttorneyFirmID);
                    _arr.Add(GlobalConst.ConstantChar.Two);
                }
            }
            catch
            {
                _arr.Add(0);
                _arr.Add(GlobalConst.ConstantChar.Zero);
            }
            return Json(_arr);


        }
        [HttpPost]
        public ActionResult SaveAttorneyDetail(Attorney objAttorneyModel)
        {
            var _message = GlobalConst.ConstantChar.StringBlank;
            try
            {

                if (objAttorneyModel.AttorneyID == GlobalConst.ConstantChar.Zero)
                {
                    _iPaticipantService.addAttorney(Mapper.Map<MMCService.PaticipantService.Attorney>(objAttorneyModel));
                    _message = GlobalConst.Message.SaveMessage;
                }
                else
                {
                    _iPaticipantService.updateAttorney(Mapper.Map<MMCService.PaticipantService.Attorney>(objAttorneyModel));
                    _message = GlobalConst.Message.UpdateMessage;
                }

            }
            catch
            {
                _message = GlobalConst.Message.ErrorMessage;
            }
            return Json(_message);
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