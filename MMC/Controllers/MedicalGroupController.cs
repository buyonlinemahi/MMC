using AutoMapper;
using MMCApp.Domain.Models.MedicalGroup;
using MMCApp.Domain.ViewModels.MedicalGroupViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class MedicalGroupController : Controller
    {
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public MedicalGroupController()
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
        public ActionResult GetMedicalGroupByName(string _searchText, int? skip)
        {
            var _medicalGroupDetails = _iPaticipantService.getMedicalGroupsByName(_searchText.Trim(), skip.Value, GlobalConst.Records.LandingTake);
            MedicalGroupSearchViewModel objMGList = new MedicalGroupSearchViewModel();
            objMGList.MedicalGroupDetails = Mapper.Map<IEnumerable<MedicalGroup>>(_medicalGroupDetails.MedicalGroupDetails);
            objMGList.TotalCount = _medicalGroupDetails.TotalCount;
            return Json(objMGList);
        }

        [HttpPost]
        public ActionResult DeleteMedicalGroupById(int id)
        {
            try
            {
                _iPaticipantService.deleteMedicalGroup(id);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }

        public ActionResult SaveMedicalGroupDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            if (id != null)
            {
                return View(Mapper.Map<MedicalGroup>(_iPaticipantService.getMedicalGroupByID(id.Value)));
            }
            else
                return View();
        }
        [HttpPost]
        public ActionResult SaveMedicalGroupDetail(MedicalGroup _medicalGroup)
        {
            var message = GlobalConst.ConstantChar.StringBlank;
            try
            {

                _medicalGroup.MedicalGroupName = _medicalGroup.MedicalGroupName.Trim();
                if (_medicalGroup.MedicalGroupID != GlobalConst.ConstantChar.Zero)
                {
                    int i = _iPaticipantService.updateMedicalGroup(Mapper.Map<MMCService.PaticipantService.MedicalGroup>(_medicalGroup));
                    if (i > GlobalConst.ConstantChar.Zero)
                        message = GlobalConst.Message.UpdateMessage;
                    else
                        message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    int i = _iPaticipantService.addMedicalGroup(Mapper.Map<MMCService.PaticipantService.MedicalGroup>(_medicalGroup));
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
