using AutoMapper;
using MMCApp.Domain.Models.EmployerModel;
using MMCApp.Domain.ViewModels.EmployerViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;


namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class EmployerController : Controller
    {
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantServiceClient;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public EmployerController()
        {
            _iPaticipantServiceClient = new MMCService.PaticipantService.PaticipantServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
        }
        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }

        [HttpPost]
        public ActionResult GetEmployerByName(string _searchText, int? _skip)
        {
            var _employerDetails = _iPaticipantServiceClient.getEmployersByName(_searchText, _skip.Value, GlobalConst.Records.LandingTake);
            EmployerSearchViewModel objEmployerList = new EmployerSearchViewModel();
            objEmployerList.EmployerDetails = Mapper.Map<IEnumerable<Employer>>(_employerDetails.EmployerDetails);
            objEmployerList.TotalCount = _employerDetails.TotalCount;
            return Json(objEmployerList, GlobalConst.ContentTypes.TextHtml);
        }
        
        [HttpGet]
        public ActionResult SaveEmployerDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            EmployerSubsidiarySearchViewModel objEmployerModel = new EmployerSubsidiarySearchViewModel();
            if (id != null)
            {
                //var _EmployerResult = _iPaticipantServiceClient.getEmployerByID(id.Value);
                objEmployerModel.EmployerResult = Mapper.Map<Employer>(_iPaticipantServiceClient.getEmployerByID(id.Value));

                var _EmployerSubsidiaryResult = _iPaticipantServiceClient.getEmployerSubsidiariesByEmployerID(id.Value, GlobalConst.Records.Skip, GlobalConst.Records.Take);
                objEmployerModel.EmployerSubsidiaryDetails = Mapper.Map<IEnumerable<EmployerSubsidiary>>(_EmployerSubsidiaryResult.EmployerSubsidiaryDetails);
                objEmployerModel.TotalCount = _EmployerSubsidiaryResult.TotalCount;
            }
            return View(objEmployerModel);
        }

    
        [HttpPost]
        public ActionResult SaveEmployerDetail(Employer _objEmployer)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            var _arr = new List<int>();
            try
            {
                _objEmployer.EmpName = _objEmployer.EmpName.Trim();
                if (_objEmployer.EmployerID == GlobalConst.ConstantChar.Zero)
                {
                    _Result = _iPaticipantServiceClient.addEmployer(Mapper.Map<MMCService.PaticipantService.Employer>(_objEmployer));
                    _arr.Add(_Result);
                    _arr.Add(GlobalConst.ConstantChar.One);
                }
                else
                {
                    _Result = _iPaticipantServiceClient.updateEmployer(Mapper.Map<MMCService.PaticipantService.Employer>(_objEmployer));
                    _arr.Add(_objEmployer.EmployerID);
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
        public ActionResult DeleteEmployerByID(int _employerID)
        {
            try
            {
                _iPaticipantServiceClient.deleteEmployer(_employerID);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch 
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
            
        }

        [HttpGet]
        public JsonResult GetAllEmployers() {
            /*EmployerSearchViewModel ObjEmployerAll = new EmployerSearchViewModel();
            ObjEmployerAll.EmployerDetails = Mapper.Map<IEnumerable<Employer>>(_iPaticipantServiceClient.getEmployersAll());*/
            return Json(Mapper.Map<IEnumerable<Employer>>(_iPaticipantServiceClient.getEmployersAll()), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);            
        }

        [HttpPost]
        public ActionResult SaveEmployerSubsidiaryDetail(EmployerSubsidiary _objEmployerSubsidiary)
        {
            var _message = GlobalConst.ConstantChar.StringBlank;
            try
            {
                if (_objEmployerSubsidiary.EMPSubsidiaryID == GlobalConst.ConstantChar.Zero)
                {
                    int a = _iPaticipantServiceClient.addEmployerSubsidiaries(Mapper.Map<MMCService.PaticipantService.EmployerSubsidiary>(_objEmployerSubsidiary));
                    _message = GlobalConst.Message.SaveMessage;
                }
                else
                {
                    int a = _iPaticipantServiceClient.updateEmployerSubsidiaries(Mapper.Map<MMCService.PaticipantService.EmployerSubsidiary>(_objEmployerSubsidiary));
                    _message = GlobalConst.Message.UpdateMessage;
                }
            }
            catch
            {
                _message = GlobalConst.Message.ErrorMessage;
            }

            return Json(_message, GlobalConst.ContentTypes.TextHtml);
        }

      
        [HttpPost]
        public ActionResult GetEmployerSubsidiariesByEmployer(int id, int _skip)
        {
            EmployerSubsidiarySearchViewModel objEmployerModel = new EmployerSubsidiarySearchViewModel();
            if (id != 0)
            {
                var _EmployerSubsidiaryResult = _iPaticipantServiceClient.getEmployerSubsidiariesByEmployerID(id, _skip, GlobalConst.Records.Take);
                objEmployerModel.EmployerSubsidiaryDetails = Mapper.Map<IEnumerable<EmployerSubsidiary>>(_EmployerSubsidiaryResult.EmployerSubsidiaryDetails);
                objEmployerModel.TotalCount = _EmployerSubsidiaryResult.TotalCount;
            }
            return Json(objEmployerModel,GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult DeleteEmployerSubsidiaryByID(int _empSubsidiaryID)
        {
            try
            {
                _iPaticipantServiceClient.deleteEmployerSubsidiaries(_empSubsidiaryID);
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
