using AutoMapper;
using MMCApp.Domain.Models.DurationTypeModel;
using MMCApp.Domain.Models.IntakeModel;
using MMCApp.Domain.Models.NoteModel;
using MMCApp.Domain.Models.PatientModel;
using MMCApp.Domain.Models.RequestTypeModel;
using MMCApp.Domain.Models.StorageModel;
using MMCApp.Domain.Models.TreatmentCategoryModel;
using MMCApp.Domain.Models.TreatmentTypeModel;
using MMCApp.Domain.ViewModels.BodyPartViewModel;
using MMCApp.Domain.ViewModels.IntakeViewModel;
using MMCApp.Domain.ViewModels.PatientViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.ApplicationServices;
using MMCApp.Infrastructure.Base;
using MMCApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AppModel = MMCApp.Domain.Models;
using serviceModel = MMC.MMCService;
namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class IntakeController : BaseController
    {
        public readonly PDFSplitterService _pdfSplitterService;
        public readonly StorageService _storageService;
        MMCService.IntakeService.IntakeServiceClient _intakeService;
        MMCService.CommonService.CommonServiceClient _commonService;
        MMCService.PatientService.IPatientService _patientService;
        public IntakeController()
        {
            _intakeService = new MMCService.IntakeService.IntakeServiceClient();
            _commonService = new serviceModel.CommonService.CommonServiceClient();
            _patientService = new MMCService.PatientService.PatientServiceClient();
            _pdfSplitterService = new PDFSplitterService();
            _storageService = new StorageService();
        }


        [HttpGet]
        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View(getIncompleteReferrals(GlobalConst.ConstantChar.Zero));
        }

        public ActionResult getIncompleteReferralsDetails(int _skip)
        {
            return Json(getIncompleteReferrals(_skip), GlobalConst.ContentTypes.TextHtml);
        }

        [NonAction]
        private IncompleteReferralsViewModel getIncompleteReferrals(int _skip)
        {
            
            var _incompletedRef = _intakeService.getAllIncompleteReferrals(_skip, GlobalConst.Records.LandingTake);
            IncompleteReferralsViewModel _IncompleteReferralsViewModel = new IncompleteReferralsViewModel();
            _IncompleteReferralsViewModel.IncompleteReferralsDetails = Mapper.Map<IEnumerable<MMCApp.Domain.Models.IntakeModel.IncompleteReferrals>>(_incompletedRef.IncompleteReferralsDetails);
            _IncompleteReferralsViewModel.TotalCount = _incompletedRef.TotalCount;
            return _IncompleteReferralsViewModel;
        }

        public ActionResult AddIntake()
        {
            return View(GlobalConst.Views.IntakeController.Intake, new Tuple<RFAReferral, RFAReferralFile>(new RFAReferral(), new RFAReferralFile()));
        }

        public ActionResult UpdateIntake(int Id)
        {
            getNoOfReferralAccordingToProcessLevels();

            Editor EditorNote = new Editor(System.Web.HttpContext.Current, "EditorNote");
            EditorNote.ClientFolder = "/richtexteditor/";
            EditorNote.Width = Unit.Pixel(1050);
            EditorNote.Height = Unit.Pixel(660);
            EditorNote.ResizeMode = RTEResizeMode.Disabled;
            EditorNote.DisabledItems = "save, help";
            EditorNote.MvcInit();
            ViewData["EditorNote"] = EditorNote.MvcGetString();
            return View(GlobalConst.Views.IntakeController.Intake, new Tuple<RFAReferral, RFAReferralFile>(getRFAReferral(Id), getRFAReferralFile(Id)));
        }

        #region Referralfile
        [NonAction]
        private RFAReferralFile getRFAReferralFile(int _rfaReferralID)
        {
            var _rfaRFAReferralFile = Mapper.Map<RFAReferralFile>(_intakeService.getReferralFileByIntake(_rfaReferralID));
            _rfaRFAReferralFile.RFAReferralFileFullPath = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString() + GlobalConst.VirtualPathFolders.IntakeUploadFiles + "\\" + _rfaRFAReferralFile.RFAReferralFileName;
            _rfaRFAReferralFile.TotalPages = _pdfSplitterService.getTotalPageofFileIntake(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath]), _rfaRFAReferralFile.RFAReferralFileName);
            _rfaRFAReferralFile.ProcessLevel = _commonService.getMaxProcessLevelByReferralID(_rfaReferralID);
            return _rfaRFAReferralFile;
        }

        [HttpPost]
        public ActionResult uploadIntakeFile(RFAReferralFile _rfaReferralFile)
        {
            string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString() + GlobalConst.VirtualPathFolders.IntakeUploadFiles);
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(_rfaReferralFile.rfaReferralFile.FileName);
            string fileToDelete = _rfaReferralFile.RFAReferralFileName;
            _rfaReferralFile.rfaReferralFile.SaveAs(path + "\\" + filename);

            _rfaReferralFile.RFAReferralFileName = filename;

            if (_rfaReferralFile.RFAReferralFileID == 0)
            {
                _rfaReferralFile = Mapper.Map<RFAReferralFile>(_intakeService.addReferralFileIntake(GlobalConst.VirtualPathFolders.IntakeUpload +  GlobalConst.Extension.pdf, MMCUser.UserId));
                _commonService.AddProcessLevelByReferralID(_rfaReferralFile.RFAReferralID, GlobalConst.ProcessLevel.Intake);
                _commonService.AddProcessLevelByReferralID(_rfaReferralFile.RFAReferralID, GlobalConst.ProcessLevel.ClaimVerification);
                System.IO.File.Move(path + "\\" + filename, path + "\\" + _rfaReferralFile.RFAReferralID + "_" + GlobalConst.VirtualPathFolders.IntakeUpload +  GlobalConst.Extension.pdf);
                _rfaReferralFile.Mode = "Add";

            }
            else
            {
                _rfaReferralFile.RFAFileTypeID = GlobalConst.FileTypes.IntakeUpload;
                if (System.IO.File.Exists(path + "\\" + _rfaReferralFile.RFAReferralID + "_" + GlobalConst.VirtualPathFolders.IntakeUpload + GlobalConst.Extension.pdf))
                    System.IO.File.Delete(path + "\\" + _rfaReferralFile.RFAReferralID + "_" + GlobalConst.VirtualPathFolders.IntakeUpload + GlobalConst.Extension.pdf);

                _rfaReferralFile.RFAReferralFileName = _rfaReferralFile.RFAReferralID + "_" + GlobalConst.VirtualPathFolders.IntakeUpload + GlobalConst.Extension.pdf;
                _rfaReferralFile.RFAFileUserID = MMCUser.UserId;
                _rfaReferralFile.RFAFileCreationDate = DateTime.Now;
                _intakeService.updateReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_rfaReferralFile));
                _pdfSplitterService.deleteIntakeUploadedFile(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath]), fileToDelete);
                System.IO.File.Move(path + "\\" + filename, path + "\\" + _rfaReferralFile.RFAReferralID + "_" + GlobalConst.VirtualPathFolders.IntakeUpload + GlobalConst.Extension.pdf);
                _rfaReferralFile.Mode = "Update";
            }
            filename = _rfaReferralFile.RFAReferralID + "_" + GlobalConst.VirtualPathFolders.IntakeUpload + GlobalConst.Extension.pdf;
            _rfaReferralFile.RFAReferralFileFullPath = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString() + GlobalConst.VirtualPathFolders.IntakeUploadFiles + "\\" + filename;
            _rfaReferralFile.rfaReferralFile = null;
            _rfaReferralFile.TotalPages = _pdfSplitterService.getTotalPageofFileIntake(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath]), filename);
            return Json(_rfaReferralFile, GlobalConst.ContentTypes.TextHtml);
        }
        #endregion

        #region RFA Referral
        [NonAction]
        private RFAReferral getRFAReferral(int _rfaReferralID)
        {
            var _rfaReferral = Mapper.Map<RFAReferral>(_intakeService.getReferralByID(_rfaReferralID));
            if (_rfaReferral.RFACETime != null)
            {
                string[] timesplit = _rfaReferral.RFACETime.Split(' ');
                _rfaReferral.RFACETime = timesplit[0].ToString();
                _rfaReferral.RFATimeValue = timesplit[1].ToString();
            }
           
            if (_rfaReferral.PatientClaimID != null && _rfaReferral.PatientClaimID > 0)
                _rfaReferral.PatientID = _patientService.getPatientClaimByID((int)_rfaReferral.PatientClaimID).PatientID;
            return _rfaReferral;
        }

        [HttpPost]
        public ActionResult GetpatientClaimByID(int claimID)
        {
            var PatClaim = _patientService.getPatientClaimByID(claimID).PatClaimNumber;
            return Json(PatClaim);
        }

        [HttpPost]
        public ActionResult SaveRFAIntake(RFAReferral _rfaReferral)
        {
            if (_rfaReferral.RFACETime == null)
                _rfaReferral.RFACETime = null;
            else
                _rfaReferral.RFACETime = _rfaReferral.RFACETime + " " + _rfaReferral.RFATimeValue;
            _intakeService.updateRFAInReferral(Mapper.Map<serviceModel.IntakeService.RFAReferral>(_rfaReferral));
            _commonService.AddProcessLevelByReferralID(_rfaReferral.RFAReferralID, GlobalConst.ProcessLevel.Physician);
            return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);
        }
        #endregion

        #region RFARequest
        [HttpGet]
        public ActionResult getRFARequestRecords(int _rfaReferralID)
        {
            RFARequestViewModel _rfaRequestViewModel = new RFARequestViewModel();
            _rfaRequestViewModel.rfaRequest = new RFARequest();
            _rfaRequestViewModel.rfaRequestsDetails = Mapper.Map<IEnumerable<RFARequestsDetails>>(_intakeService.getRFARequestByReferralID(_rfaReferralID));
            _rfaRequestViewModel.durationTypes = Mapper.Map<IEnumerable<DurationType>>(_commonService.getDurationTypesAll());
            _rfaRequestViewModel.reqeustTypes = Mapper.Map<IEnumerable<RequestType>>(_commonService.getRequestTypeAll());
            _rfaRequestViewModel.treatmentCategories = Mapper.Map<IEnumerable<TreatmentCategory>>(_commonService.getTreatmentCategoriesAll());
            _rfaRequestViewModel.treatementTypes = Mapper.Map<IEnumerable<TreatmentType>>(_commonService.getTreatmentTypesAll());
            //_rfaRequestViewModel.rfaReferralCPTCodes = Mapper.Map<IEnumerable<RFAReferralCPTCode>>(_intakeService.getRFAReferralCPTCodesByReferralID(_rfaReferralID));
            _rfaRequestViewModel.rfaRequestsDetails.ToList().ForEach(
                hp =>
                {
                    hp.TreatmentCategoryName = _rfaRequestViewModel.treatmentCategories.ToList().Find(hp1 => hp1.TreatmentCategoryID == hp.TreatmentCategoryID).TreatmentCategoryName;
                    hp.TreatmentTypeDesc = _rfaRequestViewModel.treatementTypes.ToList().Find(hp1 => hp1.TreatmentTypeID == hp.TreatmentTypeID).TreatmentTypeDesc;
                    hp.RequestTypeDesc = _rfaRequestViewModel.reqeustTypes.ToList().Find(hp1 => hp1.RequestTypeID == hp.RequestTypeID).RequestTypeDesc;
                });
            return Json(_rfaRequestViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult saveRFARequest(RFARequestsDetails rfaRequest)
        {
            if (rfaRequest.RFARequestID > 0)
            {
                _intakeService.updateRFARequest(Mapper.Map<serviceModel.IntakeService.RFARequest>(rfaRequest));
                _intakeService.deleteRFARequestBodyPartByReqID(rfaRequest.RFARequestID);
                var bodypart = rfaRequest.ReqBodyPart.Split(',');
                foreach (var reqbodypart in bodypart)
                {
                    RFARequestBodyPart rfaRequestBodyPart = new RFARequestBodyPart();
                    rfaRequestBodyPart.RFARequestID = rfaRequest.RFARequestID;
                    string[] reqBpart = reqbodypart.Split('-');
                    rfaRequestBodyPart.ClaimBodyPartID = Convert.ToInt32(reqBpart[0].ToString());
                    rfaRequestBodyPart.BodyPartType = (reqBpart[1].ToString());
                    _intakeService.addRFARequestBodyPart(Mapper.Map<serviceModel.IntakeService.RFARequestBodyPart>(rfaRequestBodyPart));
                }
            }
            else
            {
                rfaRequest.RFARequestID = _intakeService.addRFARequest(Mapper.Map<serviceModel.IntakeService.RFARequest>(rfaRequest));
                var bodypart = rfaRequest.ReqBodyPart.Split(',');
                foreach(var reqbodypart in bodypart)
                {
                    RFARequestBodyPart rfaRequestBodyPart = new RFARequestBodyPart();
                    rfaRequestBodyPart.RFARequestID = rfaRequest.RFARequestID;
                    string[] reqBpart = reqbodypart.Split('-');
                    rfaRequestBodyPart.ClaimBodyPartID = Convert.ToInt32(reqBpart[0].ToString());
                    rfaRequestBodyPart.BodyPartType = (reqBpart[1].ToString());
                    _intakeService.addRFARequestBodyPart(Mapper.Map<serviceModel.IntakeService.RFARequestBodyPart>(rfaRequestBodyPart));
                }
            }

            _commonService.AddProcessLevelByReferralID(rfaRequest.RFAReferralID, GlobalConst.ProcessLevel.RecordSplit);

            return Json(rfaRequest, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public void deleteRFARequest(int _rfaRequestID, int _flag)
        {
            if(_flag==0)
                _intakeService.deleteRFARequest(_rfaRequestID);
            else
                _intakeService.deleteRFADelatedRequest(_rfaRequestID);
        }

        //[HttpPost]
        //public ActionResult saveRFAReferralCPTCode(RFAReferralCPTCode rfaReferralCPTCode)
        //{
        //    if (rfaReferralCPTCode.RFACPTCodeID > 0)
        //        _intakeService.updateRFAReferralCPTCodes(Mapper.Map<serviceModel.IntakeService.RFAReferralCPTCode>(rfaReferralCPTCode));
        //    else
        //        rfaReferralCPTCode.RFACPTCodeID = _intakeService.addRFAReferralCPTCodes(Mapper.Map<serviceModel.IntakeService.RFAReferralCPTCode>(rfaReferralCPTCode));

        //    return Json(rfaReferralCPTCode, GlobalConst.ContentTypes.TextHtml);
        //}

        [HttpPost]
        public JsonResult getRFARequestBodyPartByRequestID(int _requestID)
        {
            var RFAReqBodyPart= _intakeService.getRFARequestBodyPartByRequestID(_requestID);
            List<string> list_SelectedBodyPart=new List<string>();
            foreach (var bodypart in RFAReqBodyPart)
            {
                //SelectedBodyPart += "'" + bodypart.ClaimBodyPartID + "-" + bodypart.BodyPartType + "'" + ",";
                list_SelectedBodyPart.Add( bodypart.ClaimBodyPartID + "-" + bodypart.BodyPartType);
            }

            return Json(list_SelectedBodyPart);
        }

        [HttpPost]
        public void deleteRFAReferralCPTCode(int _rfaCPTCodeID)
        {
            _intakeService.deleteRFAReferralCPTCodes(_rfaCPTCodeID);
        }
        #endregion

        #region intake physician
        [HttpPost]
        public ActionResult saveIntakePhysician(int PhysicianId, int RFAReferralID)
        {
            _commonService.AddProcessLevelByReferralID(RFAReferralID, GlobalConst.ProcessLevel.Requests);
            return Json(_intakeService.updatePhysicianInReferral(PhysicianId, RFAReferralID), GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult getPhysicianidByReferralID(int _rfaReferralID)
        {
            RFAReferral objRFAReferral = new RFAReferral();
            objRFAReferral = Mapper.Map<RFAReferral>(_intakeService.getReferralByID(_rfaReferralID));
            return Json(objRFAReferral);
        }
        #endregion

        #region RFARecordSplitting..hp
        [HttpGet]
        public ActionResult getRFARecordSplit(int _rfaReferralID)
        {
            RFARecordSplittingViewModel rfaRecSplt = new RFARecordSplittingViewModel();
            rfaRecSplt.documentCategories = Mapper.Map<IEnumerable<AppModel.DocumentCategoryModel.DocumentCategory>>(_commonService.getDocumentCategoriesAll());
            //rfaRecSplt.documentTypes = Mapper.Map<IEnumerable<AppModel.DocumentTypeModel.DocumentType>>(_commonService.getDocumentTypesAll());
            rfaRecSplt.rfaRecordSplitingDetails = Mapper.Map<IEnumerable<RFARecordSpliting>>(_intakeService.getRFARecordSplittingByReferralID(_rfaReferralID));
            int _patientID = _intakeService.getPatientDetailsByReferralID(_rfaReferralID).PatientID;
            rfaRecSplt.rfaRecordSplitingDetails.ToList().ForEach(
            hp =>
            {
                hp.DocumentCategoryName = rfaRecSplt.documentCategories.ToList().Find(hp1 => hp1.DocumentCategoryID == hp.DocumentCategoryID).DocumentCategoryName;
                hp.DocumentTypeDesc = _commonService.getDocumentTypesAll().ToList().Find(rk => rk.DocumentTypeID == hp.DocumentTypeID).DocumentTypeDesc;
                //hp.DocumentTypeDesc = rfaRecSplt.documentTypes.ToList().Find(hp1 => hp1.DocumentTypeID == hp.DocumentTypeID).DocumentTypeDesc;
                hp.DocumentUrl = createURLforViewFile(hp.RFAFileName, _patientID, hp.PatientClaimID);
            });

            return Json(rfaRecSplt, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult saveRFARecordSplitting(RFARecordSpliting rfaRecordSpliting)
        {
            rfaRecordSpliting.RFAFileName = rfaRecordSpliting.RFARecDocumentName + System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_").Trim();
            //create path...
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_commonService.GetStorageStuctureByID(rfaRecordSpliting.PatientClaimID, 'C'));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;

            string saveToPath = _storageService.GeneateStorage(_storageModel);
            //end path..
            rfaRecordSpliting.RFAFileName = _pdfSplitterService.splitPDFIntake(rfaRecordSpliting.RFARecPageStart, rfaRecordSpliting.RFARecPageEnd, Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath]), saveToPath, rfaRecordSpliting.RFAReferralFileName, rfaRecordSpliting.RFAFileName);
            rfaRecordSpliting.RFAUploadDate = DateTime.Now;
            rfaRecordSpliting.UserID = MMCUser.UserId;
            rfaRecordSpliting.RFARecSpltID = _intakeService.addRFARecordSplitting(Mapper.Map<serviceModel.IntakeService.RFARecordSplitting>(rfaRecordSpliting));
         
            rfaRecordSpliting.DocumentUrl = createURLforViewFile(rfaRecordSpliting.RFAFileName, rfaRecordSpliting.PatientID, rfaRecordSpliting.PatientClaimID);
            return Json(rfaRecordSpliting, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult updateProcessLevelRecSplt(int _rfaReferralID)
        {
            _commonService.AddProcessLevelByReferralID(_rfaReferralID, GlobalConst.ProcessLevel.Preparation);
            _intakeService.UpdateRFAReqCertificationNumberByID(_rfaReferralID);
            _intakeService.UpdateRFAReferralRequestDecisionByID(_rfaReferralID);
            return RedirectToActionPermanent(GlobalConst.Actions.PreparationController.Index, GlobalConst.Controllers.Preparation);
        }

      
        public void deleteRFARecordSplitting(int _rfaRecSpltID, int patientID, int claimID, string fileName)
        {
            //create path...
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_commonService.GetStorageStuctureByID(claimID, 'C'));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;

            string saveToPath = _storageService.GeneateStorage(_storageModel);
            //end path..
            _pdfSplitterService.deleteFileRFARecSplt(saveToPath + GlobalConst.ConstantChar.DoubleBackSlash + fileName);
            _intakeService.deleteRFARecordSplitting(_rfaRecSpltID);
        }
        [HttpPost]
        public JsonResult DeleteRFARecordSplittingrecords(int _rfaRecSpltID, int patientID, int claimID, string fileName)
        {
            deleteRFARecordSplitting(_rfaRecSpltID, patientID, claimID, fileName);
            return Json(GlobalConst.Message.DeleteMessage, GlobalConst.ContentTypes.TextHtml);
        }

        public FileResult viewRecSpltFile(string fileName, int patientID, int claimID)
        {
            //create path...
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_commonService.GetStorageStuctureByID(claimID, 'C'));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;

            //end path..
            string uploadPath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + fileName;
            return File(uploadPath, GlobalConst.ContentTypes.PDF, fileName);
        }

        [NonAction]
        private string createURLforViewFile(string _fileName, int _patientID, int _claimID)
        {
            return Url.Action(GlobalConst.Actions.IntakeController.viewRecSpltFile, GlobalConst.Controllers.Intake,
                         new { fileName = _fileName, patientID = _patientID, claimID = _claimID });
        }

        [NonAction]
        private void updateRecSpltClaimIDByReferralID(int rfaReferralID, int patientID, int claimID)
        {
            var _rfaRecSplt = Mapper.Map<IEnumerable<RFARecordSpliting>>(_intakeService.getRFARecordSplittingByReferralID(rfaReferralID));
            if (_rfaRecSplt.Count() > 0)
            {
                _intakeService.udateRFARecordSplittingClaimIDByReferralID(rfaReferralID, claimID);
                int opatientID = _patientService.getPatientClaimByID(_rfaRecSplt.FirstOrDefault().PatientClaimID).PatientID;
                movePatientMedicalRecordsByClaimID(_rfaRecSplt, opatientID, patientID, claimID);
            }

        }
        [NonAction]
        private void movePatientMedicalRecordsByClaimID(IEnumerable<RFARecordSpliting> rfaRecSplts, int opatientID, int patientID, int claimID)
        {
            string virStoragePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            //create path...
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_commonService.GetStorageStuctureByID(claimID, 'C'));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.PatientID = opatientID;
            _storageModel.ClaimID = rfaRecSplts.FirstOrDefault().PatientClaimID;
            _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;


            StorageModel _storageModel1 = new StorageModel()
            {
                ClientID = _storageModel.ClientID,
                path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()),
                PatientID = patientID,
                ClaimID = claimID,
                FolderName = GlobalConst.FolderName.MedicalRecords
            };
            //end path..
            rfaRecSplts.ToList().ForEach(hp =>
                _storageService.movePatientMedicalRecordIntakeByClaimID(_storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + hp.RFARecDocumentName, _storageService.GeneateStorage(_storageModel1) + GlobalConst.ConstantChar.DoubleBackSlash + hp.RFARecDocumentName));
        }
        #endregion

        #region Intake Claim Detail

        [HttpPost]
        public ActionResult IntakeClaimDetailByName(string _searchText, int? _skip)
        {
            var _patentDetail = _patientService.getpatientClaimsByName(_searchText, _skip.Value, GlobalConst.Records.Take);
            PatientClaimSearchViewModel _PatientClaimSearchViewModel = new PatientClaimSearchViewModel();
            _PatientClaimSearchViewModel.PatientClaimDetails = Mapper.Map<IEnumerable<PatientClaim>>(_patentDetail.PatientClaimDetails);
            _PatientClaimSearchViewModel.TotalCount = _patentDetail.TotalCount;
            return Json(_PatientClaimSearchViewModel);
        }

        [HttpPost]
        public ActionResult getClaimDiagnosis(int _claimID, int? _skip)
        {
            var objClaimDiagnosis = _patientService.getPatientClaimDiagnoseByPatientClaimId(_claimID, _skip.Value, GlobalConst.Records.Take);
            IntakePatientClaimDiagnoseViewModel _objClaimDiagnosesViewModel = new IntakePatientClaimDiagnoseViewModel();
            _objClaimDiagnosesViewModel.PatientClaimDiagnoseDetails = Mapper.Map<IEnumerable<PatientClaimDiagnose>>(objClaimDiagnosis.PatientClaimDiagnoseDetails);
            _objClaimDiagnosesViewModel.TotalCount = objClaimDiagnosis.TotalCount;
            return Json(_objClaimDiagnosesViewModel);
        }

        [HttpPost]
        public ActionResult getAllBodyPartsByClaimID(int _claimID, int? _skip)
        {
            var _bodyparts = _patientService.getAllBodyPartsByClaimId(_claimID, _skip.Value, GlobalConst.Records.Take);
            BodyPartDetailViewModel objBPViewmodel = new BodyPartDetailViewModel();
            objBPViewmodel.BodyPartDetails = Mapper.Map<IEnumerable<AppModel.BodyPartModel.BodyPartDetail>>(_bodyparts.BodyPartDetails);
            objBPViewmodel.TotalCount = _bodyparts.TotalCount;
            return Json(objBPViewmodel);
        }

        [HttpPost]
        public ActionResult getAllBodyPartsForReqByClaimID(int _claimID)
        {
            var _bodyparts = _patientService.getAllBodyPartsByClaimId(_claimID, 0, 0);
            BodyPartDetailViewModel objBPViewmodel = new BodyPartDetailViewModel();
            objBPViewmodel.BodyPartDetails = Mapper.Map<IEnumerable<AppModel.BodyPartModel.BodyPartDetail>>(_bodyparts.BodyPartDetails);
            objBPViewmodel.TotalCount = _bodyparts.TotalCount;
            return Json(objBPViewmodel);
        }

        [HttpPost]
        public ActionResult UdateReferralIntakePatientClaimByID(int _patientClaimID, int patientID, int _rfaReferralId)
        {
            _intakeService.udateReferralIntakePatientClaimByID(_patientClaimID, _rfaReferralId);
            _commonService.AddProcessLevelByReferralID(_rfaReferralId, GlobalConst.ProcessLevel.RFAVerification);
            updateRecSpltClaimIDByReferralID(_rfaReferralId, patientID, _patientClaimID);
            return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpGet]
        public ActionResult getPatientDetailsByReferralID(int _rfaReferralId)
        {
            return Json(_intakeService.getPatientDetailsByReferralID(_rfaReferralId), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region  Patient - UR History 
        [HttpPost]
        public ActionResult GetPatientHistoryByPatientID(int _patientID, int? _skip, string _sortBy, string _order)
        {
            var _patientHistory = _intakeService.getPatientHistoryByPatientID(_patientID, _skip.Value, GlobalConst.Records.Take, _sortBy, _order);
            PatientHistoryViewModel _PatientHistoryViewModel = new PatientHistoryViewModel();
            _PatientHistoryViewModel.PatientHistoryDetails = Mapper.Map<IEnumerable<PatientHistory>>(_patientHistory.PatientHistoryDetails);
            _PatientHistoryViewModel.TotalCount = _patientHistory.TotalCount;
            
            return Json(_PatientHistoryViewModel, GlobalConst.ContentTypes.TextHtml);
        }
         #endregion

        #region  Patient - Request History
        [HttpPost]
        public ActionResult GetRequestHistoryByRequestID(int _requestID, int? _skip)
        {
            //Note:- Only in case of Email Pop Up  emailRecordId act as RFARequestID & FileTypeId is -1 -----------------// 
            var _requestHistory = _intakeService.getRequestHistoryByRFARequestID(_requestID, _skip.Value, GlobalConst.Records.Take);
            string a = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();// +"\\" + _rfaRFAReferralFile.RFAReferralFileName;
            StorageModel _storageModel = new StorageModel();
           
            RequestHistoryViewModel _RequestHistoryViewModel = new RequestHistoryViewModel();
            _RequestHistoryViewModel.RequestHistoryDetails = Mapper.Map<IEnumerable<RequestHistory>>(_requestHistory.RequestHistoryDetails);
            foreach (var reqhistory in _RequestHistoryViewModel.RequestHistoryDetails)
            {
                if (reqhistory.FileTypeId != -1)
                {
                    if (reqhistory.FileTypeId == 6)
                    {
                        string path = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
                        reqhistory.FileFullPath = path + GlobalConst.ConstantChar.DoubleBackSlash + "IntakeUploadFiles" + GlobalConst.ConstantChar.DoubleBackSlash + reqhistory.filename;
                    }
                    else
                    {
                        _storageModel = Mapper.Map<StorageModel>(_commonService.GetStorageStuctureByID(reqhistory.RFAReferralID, 'R'));
                        _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                        _storageModel.path = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
                        reqhistory.FileFullPath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + reqhistory.filename;
                    }
                }
            }
            _RequestHistoryViewModel.TotalCount = _requestHistory.TotalCount;

            int _rFAReferralID = _intakeService.getRFARequestByID(_requestID).RFAReferralID.Value;

            _RequestHistoryViewModel.NotificationProcessLevelCheck = _commonService.getProcessLevelsByReferralID(_rFAReferralID).Where(rk => rk.ProcessLevel > GlobalConst.ProcessLevel.Notifications).Count() > 0 ? 1 : 0;
            _RequestHistoryViewModel.CliniclAuditProcessLevelCheck = _commonService.getProcessLevelsByReferralID(_rFAReferralID).Where(rk => rk.ProcessLevel == GlobalConst.ProcessLevel.Notifications).Count() > 0 ? 1 : 0;



            return Json(_RequestHistoryViewModel, GlobalConst.ContentTypes.TextHtml);
        }
        #endregion

        [HttpPost]
        public JsonResult ReplaceRequestHistoryFile(string filepath) 
        {
            try
            {
                System.Web.HttpPostedFileBase fileContent = Request.Files[0];                
                filepath = filepath.Replace("/Storage/", Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()));                
                if (System.IO.File.Exists(filepath))
                {   
                    System.IO.File.Delete(filepath);
                }
                fileContent.SaveAs(filepath);
                return Json("File Replaced");
            }            
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult getRFARequestByReferralID(int _referralID)
        {
            var referral = _intakeService.getRFARequestByReferralID(_referralID);
            return Json(referral, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult SaveNotes(Note _note)
        {
            var _Result = GlobalConst.ConstantChar.Zero;
            _note.UserID = MMCUser.UserId;
            _note.Date = DateTime.Now;
            try
            {
                if (_note.NoteID == 0)
                    _Result = _intakeService.addNotes(Mapper.Map<MMCService.IntakeService.Note>(_note));
                else
                {
                    _Result = _intakeService.updateNotes(Mapper.Map<MMCService.IntakeService.Note>(_note));
                    if (_Result > 0)
                        _Result = _note.NoteID;
                    else
                        _Result = GlobalConst.ConstantChar.Zero;
                }
            }
            catch
            {
                _Result = GlobalConst.ConstantChar.Zero;
            }
            return Json(_Result);
        }

        [NonAction]
        public void getNoOfReferralAccordingToProcessLevels()
        {
            var _NoOFReferral = _commonService.getNoOfReferralCountAccToProcessLevel();

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