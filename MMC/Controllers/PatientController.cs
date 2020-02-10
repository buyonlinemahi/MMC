using AutoMapper;
using MMCApp.Domain.Models.AttorneyModel;
using MMCApp.Domain.Models.ClientModel;
using MMCApp.Domain.Models.NoteModel;
using MMCApp.Domain.Models.PatientModel;
using MMCApp.Domain.Models.StorageModel;
using MMCApp.Domain.ViewModels.ClientModelViewModel;
using MMCApp.Domain.ViewModels.NoteViewModel;
using MMCApp.Domain.ViewModels.PatientViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.ApplicationServices;
using MMCApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MMCApp.Domain.ViewModels.AdditionalDocumentViewModel;
using MMCApp.Domain.Models.AdditionalDocumentModel;
using MMCApp.Domain.ViewModels.EmployerViewModel;
using MMCApp.Domain.Models.EmployerModel;


namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class PatientController : Controller
    {
        MMCService.PatientService.PatientServiceClient _iPatientService;
        MMCService.ClientService.ClientServiceClient _iClientService;
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        MMCService.UserService.UserServiceClient _iUserService;
        MMCService.PaticipantService.PaticipantServiceClient _iPaticipantServiceClient;
        StorageService _storageService;
        public PatientController()
        {
            _iPatientService = new MMCService.PatientService.PatientServiceClient();
            _iClientService = new MMCService.ClientService.ClientServiceClient();
            _iPaticipantService = new MMCService.PaticipantService.PaticipantServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
            _iUserService = new MMCService.UserService.UserServiceClient();
            _storageService = new StorageService();
            _iPaticipantServiceClient = new MMCService.PaticipantService.PaticipantServiceClient();
        }

        [HttpPost]
        public ActionResult GetPatientByName(string _searchText, int _skip)
        {
            var _patientDetails = _iPatientService.getPatientsByName(_searchText, _skip, GlobalConst.Records.LandingTake);
            PatientSearchViewModel objPatientList = new PatientSearchViewModel();
            objPatientList.PatientDetails = Mapper.Map<IEnumerable<Patient>>(_patientDetails.PatientDetails);
            objPatientList.TotalCount = _patientDetails.TotalCount;
            return Json(objPatientList, GlobalConst.ContentTypes.TextHtml);
        }

        public ActionResult Index(string s)
        {
            getNoOfReferralAccordingToProcessLevels();

            var _patientDetails = _iPatientService.getPatientsByName(s, 0, GlobalConst.Records.LandingTake);
            PatientSearchViewModel objPatientList = new PatientSearchViewModel();
            objPatientList.PatientDetails = Mapper.Map<IEnumerable<Patient>>(_patientDetails.PatientDetails);
            objPatientList.TotalCount = _patientDetails.TotalCount;
            ViewBag.SearchText = s;
            return View(objPatientList);
        }

        public ActionResult PatientDemographics(int? id, int id2, string id3)
        {
            getNoOfReferralAccordingToProcessLevels();
            Editor EditorNote = new Editor(System.Web.HttpContext.Current, GlobalConst.Richtexteditor.EditorNote);
            EditorNote.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            EditorNote.Width = Unit.Pixel(1050);
            EditorNote.Height = Unit.Pixel(660);
            EditorNote.ResizeMode = RTEResizeMode.Disabled;
            EditorNote.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            EditorNote.MvcInit();
            ViewData[GlobalConst.Richtexteditor.EditorNote] = EditorNote.MvcGetString();

            Editor EditorNoteUpdate = new Editor(System.Web.HttpContext.Current, "EditorNoteUpdate");
            EditorNoteUpdate.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            EditorNoteUpdate.Width = Unit.Pixel(1050);
            EditorNoteUpdate.Height = Unit.Pixel(660);
            EditorNoteUpdate.ResizeMode = RTEResizeMode.Disabled;
            EditorNoteUpdate.DisabledItems = GlobalConst.Richtexteditor.Save_help;
            EditorNoteUpdate.MvcInit();
            ViewData["EditorNoteUpdate"] = EditorNoteUpdate.MvcGetString();

            ViewBag.MedicalRecord = id3;
            PatientAddUpdateDetailsViewModel objPatientModel = new PatientAddUpdateDetailsViewModel();
            try
            {
                if (id != null)
                {
                    //var _patientResult = _iPatientService.getPatientByID(id.Value);
                    //objPatientModel.PatientDetails = Mapper.Map<Patient>(_patientResult);
                    objPatientModel.PatientDetails = Mapper.Map<Patient>(_iPatientService.getPatientByID(id.Value));
                    objPatientModel.PatientDetails.PatientClaimID = id2;
                    var _currentMedicalConditions = _iPatientService.getpatientCurrentMedicalConditionByPatientId(id.Value, GlobalConst.Records.Skip, GlobalConst.Records.Take);
                    objPatientModel.PatCurrentMedicalConditionsDetails = Mapper.Map<IEnumerable<PatientCurrentMedicalCondition>>(_currentMedicalConditions.PatientCurrentMedicalConditionDetails);
                    objPatientModel.TotalCount = _currentMedicalConditions.TotalCount;
                }
            }
            catch
            {
                objPatientModel = null;
            }
            return View(objPatientModel);
        }

        [HttpPost]
        public ActionResult GetPatientMedicalConditionDetail(int? id)
        {
            PatientAddUpdateDetailsViewModel objPatientModel = new PatientAddUpdateDetailsViewModel();
            try
            {
                if (id != null)
                {
                    var _currentMedicalConditions = _iPatientService.getpatientCurrentMedicalConditionByPatientId(id.Value, GlobalConst.Records.Skip, GlobalConst.Records.Take);
                    objPatientModel.PatCurrentMedicalConditionsDetails = Mapper.Map<IEnumerable<PatientCurrentMedicalCondition>>(_currentMedicalConditions.PatientCurrentMedicalConditionDetails);
                    objPatientModel.TotalCount = _currentMedicalConditions.TotalCount;
                }
            }
            catch
            {
                objPatientModel = null;
            }
            return Json(objPatientModel);
        }

        [HttpPost]
        public ActionResult GetPatientCurrentMedicalConditions(int _id, int _skip)
        {
            PatientAddUpdateDetailsViewModel objPatientModel = new PatientAddUpdateDetailsViewModel();
            try
            {
                var _currentMedicalConditions = _iPatientService.getpatientCurrentMedicalConditionByPatientId(_id, _skip, GlobalConst.Records.Take);
                objPatientModel.PatCurrentMedicalConditionsDetails = Mapper.Map<IEnumerable<PatientCurrentMedicalCondition>>(_currentMedicalConditions.PatientCurrentMedicalConditionDetails);
                objPatientModel.TotalCount = _currentMedicalConditions.TotalCount;
            }
            catch
            {
                objPatientModel = null;
            }
            return Json(objPatientModel);
        }

        [HttpPost]
        public ActionResult SavePatientDetail(Patient objPatientModel)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            var _arr = new List<int>();
            try
            {
                objPatientModel.PatFirstName = objPatientModel.PatFirstName.Trim();
                objPatientModel.PatLastName = objPatientModel.PatLastName.Trim();
                if (objPatientModel.PatientID == GlobalConst.ConstantChar.Zero)
                {
                    _Result = _iPatientService.addPatient(Mapper.Map<MMCService.PatientService.Patient>(objPatientModel));
                    objPatientModel.PatientID = _Result;
                    _arr.Add(_Result);
                    _arr.Add(GlobalConst.ConstantChar.One);
                }
                else
                {
                    _Result = _iPatientService.updatePatient(Mapper.Map<MMCService.PatientService.Patient>(objPatientModel));
                    _arr.Add(objPatientModel.PatientID);
                    _arr.Add(GlobalConst.ConstantChar.Two);
                }
                if (_iPatientService.getPatientCurrentMedicalConditionByPatientAndCMCID(objPatientModel.PatientID, GlobalConst.CurrentMedicalCondition.EndStageRenalDisease) > 0)
                {
                    _arr.Add(-1);
                    _iPatientService.UpdatePatientMedicareEligibleByID(objPatientModel.PatientID, GlobalConst.Mode.Add, GlobalConst.CurrentMedicalCondition.EndStageRenalDisease);
                }
                else
                    _arr.Add(-2);
            }
            catch
            {
                _arr.Add(0);
                _arr.Add(GlobalConst.ConstantChar.Zero);
            }
            return Json(_arr);
        }

        [HttpPost]
        public ActionResult SavePatientCurrentMedicalConditions(PatientCurrentMedicalCondition PatCurrentMedicalConditionsDetails)
        {
            string _Result = "0";
            try
            {
                int result = _iPatientService.addPatientCurrentMedicalCondition(Mapper.Map<MMCService.PatientService.PatientCurrentMedicalCondition>(PatCurrentMedicalConditionsDetails));

                if (PatCurrentMedicalConditionsDetails.CurrentMedicalConditionId == GlobalConst.CurrentMedicalCondition.EndStageRenalDisease)
                    _Result = result.ToString() + "#" + PatCurrentMedicalConditionsDetails.CurrentMedicalConditionId.ToString();
                else
                    _Result = result.ToString();
            }
            catch
            {
                _Result = "0";
            }
            return Json(_Result);
        }


        [HttpPost]
        public ActionResult DeletePatientCurrentMedicalConditions(int _patientCurrentMedicalConditionID)
        {
            string _Result = "0";
            try
            {
                PatientCurrentMedicalCondition _PatientCurrentMedicalCondition = Mapper.Map<PatientCurrentMedicalCondition>(_iPatientService.getpatientCurrentMedicalConditionByID(_patientCurrentMedicalConditionID));
                _iPatientService.deletePatientCurrentMedicalCondition(_patientCurrentMedicalConditionID);
                if (_iPatientService.getPatientCurrentMedicalConditionByPatientAndCMCID(_PatientCurrentMedicalCondition.PatientID, _PatientCurrentMedicalCondition.CurrentMedicalConditionId) > 0)
                    _Result = "y#";
                else
                    _Result = "n#";
                if (_Result.Contains("#"))
                    _Result += GlobalConst.ConstantChar.One.ToString();
                else
                    _Result = GlobalConst.ConstantChar.One.ToString();
            }
            catch
            {
                _Result = "0";
            }
            return Json(_Result);
        }

        [HttpPost]
        public ActionResult SavePatientClaimDetail(PatientClaim objPatientModel)
        {
            int _Result = GlobalConst.ConstantChar.Zero;
            int _ResultStatus = GlobalConst.ConstantChar.Zero;
            var _arr = new List<int>();

            try
            {
                //  objPatientModel.PatClaimAdministratorID = 1;
                string[] Insval = objPatientModel.PatInsValue.Split('-');
                string[] Tpaval = objPatientModel.PatTPAValue.Split('-');

                if (Insval[1].ToString().ToLower() == GlobalConst.TableAbbreviation.Ins)
                {
                    objPatientModel.PatInsurerID = Convert.ToInt32(Insval[0]);
                    objPatientModel.PatInsuranceBranchID = null;
                }
                else
                {
                    objPatientModel.PatInsuranceBranchID = Convert.ToInt32(Insval[0]);
                    objPatientModel.PatInsurerID = null;
                }

                if (Tpaval[1].ToString().ToLower() == GlobalConst.TableAbbreviation.Tpa)
                {
                    objPatientModel.PatTPAID = Convert.ToInt32(Tpaval[0]);
                    objPatientModel.PatTPABranchID = null;
                }
                else
                {
                    objPatientModel.PatTPABranchID = Convert.ToInt32(Tpaval[0]);
                    objPatientModel.PatTPAID = null;
                }

                objPatientModel.PatClaimNumber = objPatientModel.PatClaimNumber.Trim();

                if (objPatientModel.PatientClaimID == GlobalConst.ConstantChar.Zero)
                {
                    _Result = _iPatientService.addPatientClaim(Mapper.Map<MMCService.PatientService.PatientClaim>(objPatientModel));
                    if (_Result == GlobalConst.ConstantChar.MinusOne)
                    {
                        _arr.Add(_Result);
                        _arr.Add(GlobalConst.ConstantChar.MinusOne);
                    }
                    else
                    {
                        _arr.Add(_Result);
                        PatientClaimStatus PatientClaimStatusDetails = new PatientClaimStatus();
                        PatientClaimStatusDetails.ClaimStatusID = objPatientModel.ClaimStatusID;
                        if (objPatientModel.DeniedRationale == null)
                        {
                            PatientClaimStatusDetails.DeniedRationale = "";
                        }
                        else
                        {

                            PatientClaimStatusDetails.DeniedRationale = objPatientModel.DeniedRationale;
                        }
                        PatientClaimStatusDetails.PatientClaimID = _Result;
                        _ResultStatus = _iPatientService.addPatientClaimStatus(Mapper.Map<MMCService.PatientService.PatientClaimStatus>(PatientClaimStatusDetails));
                        if (objPatientModel.ClaimStatusID == GlobalConst.ClaimStatus.Delayed)
                            _iPatientService.updatePatientClaimPleadBodyPartByPatientClaimID(_Result, GlobalConst.BodyPartStatus.Unknown);
                        else if (objPatientModel.ClaimStatusID == GlobalConst.ClaimStatus.Denied)
                            _iPatientService.updatePatientClaimPleadBodyPartByPatientClaimID(_Result, GlobalConst.BodyPartStatus.Denied);
                        else if (objPatientModel.ClaimStatusID == GlobalConst.ClaimStatus.AcceptedinPart || objPatientModel.ClaimStatusID == GlobalConst.ClaimStatus.AcceptedinFull)
                            _iPatientService.updatePatientClaimPleadBodyPartByPatientClaimID(_Result, GlobalConst.BodyPartStatus.Accepted);
                        _arr.Add(GlobalConst.ConstantChar.One);
                        _arr.Add(_ResultStatus);

                    }
                }
                else
                {
                    _Result = _iPatientService.updatePatientClaim(Mapper.Map<MMCService.PatientService.PatientClaim>(objPatientModel));

                    if (_Result == GlobalConst.ConstantChar.MinusOne)
                    {
                        _arr.Add(_Result);
                        _arr.Add(GlobalConst.ConstantChar.MinusOne);

                    }
                    else
                    {

                        _arr.Add(objPatientModel.PatientClaimID);
                        PatientClaimStatus PatientClaimStatusDetails = new PatientClaimStatus();
                        PatientClaimStatusDetails.ClaimStatusID = objPatientModel.ClaimStatusID;
                        if (objPatientModel.DeniedRationale == null)
                            PatientClaimStatusDetails.DeniedRationale = "";
                        else
                            PatientClaimStatusDetails.DeniedRationale = objPatientModel.DeniedRationale;
                        if (objPatientModel.PatientClaimID == 0)
                            PatientClaimStatusDetails.PatientClaimID = _Result;
                        else
                            PatientClaimStatusDetails.PatientClaimID = objPatientModel.PatientClaimID;
                        PatientClaimStatusDetails.PatientClaimStatusID = objPatientModel.PatientClaimStatusID;
                        _ResultStatus = _iPatientService.updatePatientClaimStatus(Mapper.Map<MMCService.PatientService.PatientClaimStatus>(PatientClaimStatusDetails));
                        if (objPatientModel.ClaimStatusID == GlobalConst.ClaimStatus.Delayed)
                            _iPatientService.updatePatientClaimPleadBodyPartByPatientClaimID(objPatientModel.PatientClaimID, GlobalConst.BodyPartStatus.Unknown);
                        else if (objPatientModel.ClaimStatusID == GlobalConst.ClaimStatus.Denied)
                            _iPatientService.updatePatientClaimPleadBodyPartByPatientClaimID(objPatientModel.PatientClaimID, GlobalConst.BodyPartStatus.Denied);
                        else if (objPatientModel.ClaimStatusID == GlobalConst.ClaimStatus.AcceptedinPart || objPatientModel.ClaimStatusID == GlobalConst.ClaimStatus.AcceptedinFull)
                            _iPatientService.updatePatientClaimPleadBodyPartByPatientClaimID(objPatientModel.PatientClaimID, GlobalConst.BodyPartStatus.Accepted);
                        _arr.Add(GlobalConst.ConstantChar.Two);
                        _arr.Add(_ResultStatus);
                    }
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
        public ActionResult GetPatientClaimStatusByPatientClaimId(int _patientClaimID, int _skip)
        {
            PatientClaimAddUpdateDetails objPatientclaim = new PatientClaimAddUpdateDetails();
            try
            {
                var _patientClaimStatusByPatient = _iPatientService.getPatientClaimStatusByPatientClaimId(_patientClaimID, _skip, GlobalConst.Records.Take);
                objPatientclaim.PatientClaimStatusDetails = Mapper.Map<IEnumerable<PatientClaimStatus>>(_patientClaimStatusByPatient.PatientClaimStatustDetails);
                objPatientclaim.TotalCountClaimStatus = _patientClaimStatusByPatient.TotalCount;
            }
            catch
            {
                objPatientclaim = null;
            }
            return Json(objPatientclaim);
        }


        [HttpPost]
        public ActionResult SavePatientClaimStatus(PatientClaimStatus PatientClaimStatusDetails)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                if (PatientClaimStatusDetails.PatientClaimID != GlobalConst.ConstantChar.Zero)
                    _Result = _iPatientService.addPatientClaimStatus(Mapper.Map<MMCService.PatientService.PatientClaimStatus>(PatientClaimStatusDetails));
            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }


        [HttpPost]
        public ActionResult DeletePatientClaimStatus(int _patientClaimStatusID)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                _iPatientService.deletePatientClaimStatus(_patientClaimStatusID);
                _Result = GlobalConst.ConstantChar.One;

            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }


        [HttpPost]
        public ActionResult GetPatientClaimAddOnBodyPartByPatientClaimId(int _patientClaimID, int _skip)
        {
            PatientClaimAddUpdateDetails objPatientclaim = new PatientClaimAddUpdateDetails();
            try
            {
                var _PatientClaimAddOnBodyPart = _iPatientService.getPatientClaimAddOnBodyPartByPatientClaimId(_patientClaimID, _skip, GlobalConst.Records.Take);
                objPatientclaim.PatientClaimAddOnBodyPartDetails = Mapper.Map<IEnumerable<PatientClaimAddOnBodyPart>>(_PatientClaimAddOnBodyPart.PatientClaimAddOnBodyPartDetails);
                objPatientclaim.TotalCountClaimAddOnBodyPart = _PatientClaimAddOnBodyPart.TotalCount;
            }
            catch
            {
                objPatientclaim = null;
            }
            return Json(objPatientclaim);
        }



        [HttpPost]
        public ActionResult SavePatientClaimAddOnBodyPart(PatientClaimAddOnBodyPart PatientClaimAddOnBodyPartDetails)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                if (PatientClaimAddOnBodyPartDetails.PatientClaimAddOnBodyPartID == GlobalConst.ConstantChar.Zero)
                {
                    _Result = _iPatientService.addPatientClaimAddOnBodyPart(Mapper.Map<MMCService.PatientService.PatientClaimAddOnBodyPart>(PatientClaimAddOnBodyPartDetails));
                }
                else
                {
                    _iPatientService.updatePatientClaimAddOnBodyPart(Mapper.Map<MMCService.PatientService.PatientClaimAddOnBodyPart>(PatientClaimAddOnBodyPartDetails));
                    _Result = GlobalConst.ConstantChar.MinusOne;
                }
            }   
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }


        [HttpPost]
        public ActionResult DeletePatientClaimAddOnBodyPart(int _patientAddOnBodyPartID)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                _iPatientService.deletePatientClaimAddOnBodyPart(_patientAddOnBodyPartID);
                _Result = GlobalConst.ConstantChar.One;

            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }


        [HttpPost]
        public ActionResult GetPatientClaimPleadBodyPartByPatientClaimId(int _patientClaimID, int _skip)
        {
            PatientClaimAddUpdateDetails objPatientclaim = new PatientClaimAddUpdateDetails();
            try
            {
                var _PatientClaimPleadBodyPartByPatientClaimId = _iPatientService.getPatientClaimPleadBodyPartByPatientClaimId(_patientClaimID, _skip, GlobalConst.Records.Take);
                objPatientclaim.PatientClaimPleadBodyPartDetails = Mapper.Map<IEnumerable<PatientClaimPleadBodyPart>>(_PatientClaimPleadBodyPartByPatientClaimId.PatientClaimPleadBodyPartDetails);
                objPatientclaim.TotalCountClaimPleadBodyPart = _PatientClaimPleadBodyPartByPatientClaimId.TotalCount;
            }
            catch
            {
                objPatientclaim = null;
            }
            return Json(objPatientclaim);
        }



        [HttpPost]
        public ActionResult SavePatientClaimPleadBodyPart(PatientClaimPleadBodyPart PatientClaimPleadBodyPartDetails)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                if (PatientClaimPleadBodyPartDetails.PatientClaimPleadBodyPartID == GlobalConst.ConstantChar.Zero)
                {
                    _Result = _iPatientService.addPatientClaimPleadBodyPart(Mapper.Map<MMCService.PatientService.PatientClaimPleadBodyPart>(PatientClaimPleadBodyPartDetails));
                }
                else
                {
                    _iPatientService.updatePatientClaimPleadBodyPart(Mapper.Map<MMCService.PatientService.PatientClaimPleadBodyPart>(PatientClaimPleadBodyPartDetails));
                    _Result = GlobalConst.ConstantChar.MinusOne;
                }
            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }


        [HttpPost]
        public ActionResult DeletePatientClaimPleadBodyPart(int _patientPleadBodyPartID)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                _iPatientService.deletePatientClaimPleadBodyPart(_patientPleadBodyPartID);
                _Result = GlobalConst.ConstantChar.One;

            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }


        [HttpPost]
        public ActionResult GetPatientClaimDiagnoseByPatientClaimId(int _patientClaimID, int _skip)
        {
            PatientClaimAddUpdateDetails objPatientclaim = new PatientClaimAddUpdateDetails();
            try
            {
                var _PatientClaimDiagnoseByPatientClaimId = _iPatientService.getPatientClaimDiagnoseByPatientClaimId(_patientClaimID, _skip, GlobalConst.Records.Take);
                objPatientclaim.PatientClaimDiagnoseDetails = Mapper.Map<IEnumerable<PatientClaimDiagnose>>(_PatientClaimDiagnoseByPatientClaimId.PatientClaimDiagnoseDetails);
                objPatientclaim.TotalCountClaimDiagnose = _PatientClaimDiagnoseByPatientClaimId.TotalCount;
            }
            catch
            {
                objPatientclaim = null;
            }
            return Json(objPatientclaim);
        }

         
        [HttpPost]
        public ActionResult SavePatientClaimDiagnose(PatientClaimDiagnose PatientClaimDiagnoseDetails)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                if (PatientClaimDiagnoseDetails.PatientClaimID != GlobalConst.ConstantChar.Zero)
                {
                    _Result = _iPatientService.addPatientClaimDiagnose(Mapper.Map<MMCService.PatientService.PatientClaimDiagnose>(PatientClaimDiagnoseDetails));
                }
            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }


        [HttpPost]
        public ActionResult DeletePatientClaimDiagnose(int _patientClaimDiagnoseID)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                _iPatientService.deletePatientClaimDiagnose(_patientClaimDiagnoseID);
                _Result = GlobalConst.ConstantChar.One;
            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }

        [HttpPost]
        public ActionResult GetpatientClaimsByPatientID(int _patientID, int _skip)
        {
            var _patientDetails = _iPatientService.getpatientClaimsByPatientID(_patientID, _skip, GlobalConst.Records.LandingTake);
            PatientClaimAddUpdateDetails _objPatientClaimlist = new PatientClaimAddUpdateDetails();
            _objPatientClaimlist.PatientClaimResults = Mapper.Map<IEnumerable<PatientClaim>>(_patientDetails.PatientClaimDetails);
            _objPatientClaimlist.TotalCountClaim = _patientDetails.TotalCount;
            return Json(_objPatientClaimlist, GlobalConst.ContentTypes.TextHtml);
        }
         
        [HttpPost]
        public ActionResult getPatientClaimByID(int patientClaimNumberID)
        {
            PatientClaimAddUpdateDetails objPatientModel = new PatientClaimAddUpdateDetails();
            objPatientModel.PatientClaimDetails = Mapper.Map<PatientClaim>(_iPatientService.getPatientClaimByID(patientClaimNumberID));

            var _PatientClaimAddOnBodyPart = _iPatientService.getPatientClaimAddOnBodyPartByPatientClaimId(objPatientModel.PatientClaimDetails.PatientClaimID, GlobalConst.Records.Skip, GlobalConst.Records.Take);
            objPatientModel.PatientClaimAddOnBodyPartDetails = Mapper.Map<IEnumerable<PatientClaimAddOnBodyPart>>(_PatientClaimAddOnBodyPart.PatientClaimAddOnBodyPartDetails);
            objPatientModel.TotalCountClaimAddOnBodyPart = _PatientClaimAddOnBodyPart.TotalCount;

            var _PatientClaimPleadBodyPartByPatientClaimId = _iPatientService.getPatientClaimPleadBodyPartByPatientClaimId(objPatientModel.PatientClaimDetails.PatientClaimID, GlobalConst.Records.Skip, GlobalConst.Records.Take);
            objPatientModel.PatientClaimPleadBodyPartDetails = Mapper.Map<IEnumerable<PatientClaimPleadBodyPart>>(_PatientClaimPleadBodyPartByPatientClaimId.PatientClaimPleadBodyPartDetails);
            objPatientModel.TotalCountClaimPleadBodyPart = _PatientClaimPleadBodyPartByPatientClaimId.TotalCount;

            var _PatientClaimDiagnoseByPatientClaimId = _iPatientService.getPatientClaimDiagnoseByPatientClaimId(objPatientModel.PatientClaimDetails.PatientClaimID, GlobalConst.Records.Skip, GlobalConst.Records.Take);
            objPatientModel.PatientClaimDiagnoseDetails = Mapper.Map<IEnumerable<PatientClaimDiagnose>>(_PatientClaimDiagnoseByPatientClaimId.PatientClaimDiagnoseDetails);
            objPatientModel.TotalCountClaimDiagnose = _PatientClaimDiagnoseByPatientClaimId.TotalCount;

            return Json(objPatientModel);

        }

        [HttpPost]
        public ActionResult DeletePatientByID(int _patientID, int _patientClaimID)
        {
            try
            {
                if (_patientClaimID > 0)
                {
                    _iPatientService.deletePatientClaimAddOnBodyPartByPatientClaimID(_patientClaimID);
                    _iPatientService.deletePatientClaimDiagnosePatientClaimID(_patientClaimID);
                    _iPatientService.deletePatientClaimPleadBodyPartByPatientClaimID(_patientClaimID);
                    _iPatientService.deletePatientClaimStatusPatientClaimID(_patientClaimID);
                    _iPatientService.deletePatientClaim(_patientClaimID);
                }
                else
                {
                    _iPatientService.deletePatientCurrentMedicalConditionByPatientID(_patientID);
                    _iPatientService.deletePatient(_patientID);
                }
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }

        }

        ///***********************
        ///
        [HttpGet]
        public JsonResult getClientAll()
        {
            ClientViewModel objClientAll = new ClientViewModel();
            objClientAll.ClientDetails = Mapper.Map<IEnumerable<Client>>(_iClientService.getClientAll());
            return Json(objClientAll.ClientDetails, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getAttorneyByFirmTypeIdAll(int id)
        {
            //var objApplicantAttorney = Mapper.Map<IEnumerable<Attorney>>((_iPaticipantService.getAttorneyRecordsByFirmTypeID(id)).AttorneyDetails);
            //return Json(objApplicantAttorney, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
            return Json(Mapper.Map<IEnumerable<Attorney>>((_iPaticipantService.getAttorneyRecordsByFirmTypeID(id)).AttorneyDetails), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAdjusterByClientID(int clientid)
        {
            //var objAdjuster = Mapper.Map<IEnumerable<MMCApp.Domain.Models.AdjusterModel.Adjuster>>((_iClientService.getAdjusterByClientID(clientid)));
            //return Json(objAdjuster, GlobalConst.ContentTypes.TextHtml);
            return Json(Mapper.Map<IEnumerable<MMCApp.Domain.Models.AdjusterModel.Adjuster>>((_iClientService.getAdjusterByClientID(clientid))), GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public JsonResult getInsurerByClientIdAll(int clientID, int skip)
        {
            ClientInsurerViewModel objClientInsurerAll = new ClientInsurerViewModel();
            var ClientInsdetail = _iClientService.getClientInsurerByClientID(clientID, skip, GlobalConst.Records.Take5);
            objClientInsurerAll.ClientInsurerDetails = Mapper.Map<IEnumerable<ClientInsurer>>(ClientInsdetail.ClientInsurerDetails);
            objClientInsurerAll.TotalCount = ClientInsdetail.TotalCount;
            return Json(objClientInsurerAll, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public JsonResult getEmployerByClientIdAll(int clientID, int skip)
        {
            ClientEmployerViewModel objClientEmployerAll = new ClientEmployerViewModel();
            var ClientEmpdetail = _iClientService.getClientEmployerByClientID(clientID, skip, GlobalConst.Records.Take5);
            objClientEmployerAll.ClientEmployerDetails = Mapper.Map<IEnumerable<ClientEmployer>>(ClientEmpdetail.ClientEmployerDetails);
            objClientEmployerAll.TotalCount = ClientEmpdetail.TotalCount;
            return Json(objClientEmployerAll, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public JsonResult getThirdPartyAdministratorByClientIdAll(int clientID, int skip)
        {
            ClientThirdPartyAdministratorViewModel objClientTPAAll = new ClientThirdPartyAdministratorViewModel();
            var ClientTPA = _iClientService.getClientThirdPartyAdministratorByClientID(clientID, skip, GlobalConst.Records.Take5);
            objClientTPAAll.ClientTPADetails = Mapper.Map<IEnumerable<ClientThirdPartyAdministrator>>(ClientTPA.ClientThirdPartyAdministratorDetails);
            objClientTPAAll.TotalCount = ClientTPA.TotalCount;
            return Json(objClientTPAAll, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public JsonResult getAllInsurerByClientIdAll(int clientID)
        {
            ClientInsurerViewModel objClientInsurerAll = new ClientInsurerViewModel();
            var ClientInsdetail = _iClientService.getAllClientInsurerByClientID(clientID);
            objClientInsurerAll.ClientInsurerDetails = Mapper.Map<IEnumerable<ClientInsurer>>(ClientInsdetail);    
            foreach(var ins in objClientInsurerAll.ClientInsurerDetails )
            {
                ins.InsValue = ins.InsurerID + "-" + ins.InsType;
            }
            return Json(objClientInsurerAll, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public JsonResult getAllEmployerByClientIdAll(int clientID)
        {
            ClientEmployerViewModel objClientEmployerAll = new ClientEmployerViewModel();
            var ClientEmpdetail = _iClientService.getAllClientEmployerByClientID(clientID);
            objClientEmployerAll.ClientEmployerDetails = Mapper.Map<IEnumerable<ClientEmployer>>(ClientEmpdetail);         
            return Json(objClientEmployerAll, GlobalConst.ContentTypes.TextHtml);
        }


        [HttpPost]
        public JsonResult getAllEmployerSubsidiaryByEmployerID(int _employerID)
        {
            EmployerSubsidiarySearchViewModel objEmployerSubsidiaryAll = new EmployerSubsidiarySearchViewModel();
            var objEmployerSubsidiaries = _iPaticipantServiceClient.getAllEmployerSubsidiariesByEmployerID(_employerID);
            objEmployerSubsidiaryAll.EmployerSubsidiaryDetails = Mapper.Map<IEnumerable<EmployerSubsidiary>>(objEmployerSubsidiaries);
            return Json(objEmployerSubsidiaryAll, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public JsonResult getAllThirdPartyAdministratorByClientIdAll(int clientID)
        {
            ClientThirdPartyAdministratorViewModel objClientTPAAll = new ClientThirdPartyAdministratorViewModel();
            var ClientTPA = _iClientService.getAllClientThirdPartyAdministratorByClientID(clientID);
            objClientTPAAll.ClientTPADetails = Mapper.Map<IEnumerable<ClientThirdPartyAdministrator>>(ClientTPA);
            foreach (var tpa in objClientTPAAll.ClientTPADetails)
            {
                tpa.TPAValue = tpa.TPAID + "-" + tpa.TPAType;
            }
            return Json(objClientTPAAll, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public JsonResult getMCCByClientId(int clientID)
        {
            ClientManagedCareCompany objClientMCC = new ClientManagedCareCompany();
            var ClientMCC = _iClientService.getClientManagedCareCompanyByClientID(clientID, GlobalConst.Records.Skip, GlobalConst.Records.Take5);
            objClientMCC = Mapper.Map<ClientManagedCareCompany>(ClientMCC.ClientManagedCareCompanyDetails.FirstOrDefault());
            return Json(objClientMCC, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public JsonResult getClaimAdministratorByClientId(int clientID)
        {
            /*Client Client = new Client();
            Client = Mapper.Map<Client>(_iClientService.getClaimAdministratorByClientID(clientID));
            return Json(Client, GlobalConst.ContentTypes.TextHtml);*/
            return Json(Mapper.Map<Client>(_iClientService.getClaimAdministratorByClientID(clientID)), GlobalConst.ContentTypes.TextHtml);
            
        }


        [HttpPost]
        public ActionResult SavePatientPhysician(PatientClaim objPatientModel)
        {
            int _Result = GlobalConst.ConstantChar.Zero;
            var _arr = new List<int>();

            try
            {
                if (objPatientModel.PatientClaimID > GlobalConst.ConstantChar.Zero)
                {
                    _Result = _iPatientService.updatePatientPhysician(Mapper.Map<MMCService.PatientService.PatientClaim>(objPatientModel));
                    _arr.Add(_Result);
                }
            }
            catch
            {
                _arr.Add(0);
            }
            return Json(_arr);
        }

        //****************************

        [HttpPost]
        public JsonResult getAllpatientClaimsByPatientID(int _patientID)
        {
            /*var patientClaim = _iPatientService.getAllpatientClaimsByPatientID(_patientID);
            IEnumerable<PatientClaim> _patientClaim = Mapper.Map<IEnumerable<PatientClaim>>(patientClaim);
            return Json(_patientClaim, GlobalConst.ContentTypes.TextHtml);*/
            return Json(Mapper.Map<IEnumerable<PatientClaim>>(_iPatientService.getAllpatientClaimsByPatientID(_patientID)), GlobalConst.ContentTypes.TextHtml);
            
        }

        [HttpPost]
        public ActionResult getNotesByPatientID(int patientID, int? skip)
        {
            var noteDetail = _iPatientService.getNotesByPatientID(patientID, skip.Value, GlobalConst.Records.LandingTake);
            NoteDetailViewModel _objNote = new NoteDetailViewModel();
            _objNote.NotesDetails = Mapper.Map<IEnumerable<NoteDetail>>(noteDetail.NotesDetails);
            foreach (NoteDetail viewmodel in _objNote.NotesDetails)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                viewmodel.ShortNotes = regex.Replace(HttpUtility.HtmlDecode(viewmodel.Notes), String.Empty);
                if (viewmodel.ShortNotes.Length > 25)
                {
                    viewmodel.ShortNotes = regex.Replace(HttpUtility.HtmlDecode(viewmodel.Notes), String.Empty).Substring(0, 25) + "...";
                    viewmodel.ShortNotes = viewmodel.ShortNotes.Replace("&nbsp;", "") + "...";
                }
                else
                {
                    viewmodel.ShortNotes = viewmodel.ShortNotes.Replace("&nbsp;", "") + "...";
                }
            }
            _objNote.TotalCount = noteDetail.TotalCount;
            return Json(_objNote);
        }
     
        [HttpPost]
        public FileResult PrintNotes(int PatientID)
        {
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptPatientsNotes], PatientID, GlobalConst.Extension.PDF);
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(PatientID,'P'));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.PrintNote;
            string Filename = PatientID.ToString() + GlobalConst.ReportName.PrintNote;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
            client.DownloadFile(reportURL, savePath);
            client.Dispose();
            return File(savePath, GlobalConst.FileDownloadExtension.Application_Pdf, Filename);
        }

        #region Patient Occupational

        [HttpPost]
        public ActionResult SavePatientOccupational(PatientOccupational _objPatientOccupational)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            try
            {
                if (_objPatientOccupational.PatientID != GlobalConst.ConstantChar.Zero)
                    if (_objPatientOccupational.PatientOccupationalID != GlobalConst.ConstantChar.Zero)
                    _Result = _iPatientService.updatePatientOccupational(Mapper.Map<MMCService.PatientService.PatientOccupational>(_objPatientOccupational));
                    else
                        _Result = _iPatientService.addPatientOccupational(Mapper.Map<MMCService.PatientService.PatientOccupational>(_objPatientOccupational));
            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }

        [HttpPost]
        public ActionResult GetPatientOccupationalByPatientClaimID(int patientID)
        {
            try
            {
                PatientOccupational _patientOccupational = new PatientOccupational();
                var OccupationalDetails = _iPatientService.getPatientOccupationalByPatientClaimID(patientID);
                _patientOccupational = Mapper.Map<PatientOccupational>(OccupationalDetails);
                return Json(_patientOccupational);
            }
            catch (Exception ex)
            {
                return Json("");
            }
        }
        #endregion
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
   