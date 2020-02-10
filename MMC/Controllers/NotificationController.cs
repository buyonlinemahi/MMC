using AutoMapper;
using MMCApp.Domain.Models.ClinicalTriageModel;
using MMCApp.Domain.Models.EmailRecordModel;
using MMCApp.Domain.Models.IntakeModel;
using MMCApp.Domain.Models.Notification;
using MMCApp.Domain.Models.StorageModel;
using MMCApp.Domain.Models.UserModel;
using MMCApp.Domain.ViewModels.ClinicalTriageViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.ApplicationServices;
using MMCApp.Infrastructure.Base;
using MMCApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using serviceModel = MMC.MMCService;


namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class NotificationController : BaseController
    {

        MMCService.NotificationService.NotificationServiceClient _iNotificationService;
        MMCService.PreparationService.PreparationServiceClient _iPreparationService;
        MMCService.IntakeService.IntakeServiceClient _intakeService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        MMCService.UserService.UserServiceClient _iUserService;
        MMCService.EmailRecordAttachmentService.EmailRecordAttachmentServiceClient _iEmailRecordAttachmentService;

        StorageService _storageService;
        EMailService _mailService;
        public NotificationController()
        {
            _intakeService = new MMCService.IntakeService.IntakeServiceClient();
            _iPreparationService = new MMCService.PreparationService.PreparationServiceClient();
            _iNotificationService = new MMCService.NotificationService.NotificationServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
            _iUserService = new MMCService.UserService.UserServiceClient();
            _storageService = new StorageService();
            _mailService = new EMailService();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
            _iEmailRecordAttachmentService = new MMCService.EmailRecordAttachmentService.EmailRecordAttachmentServiceClient();
        }
        public ActionResult Index(int? skip)
        {
            getNoOfReferralAccordingToProcessLevels();

            if (skip == null)
            {
                var _clinicalTriageDetails = _iPreparationService.getReferralByProcessLevel(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, GlobalConst.ProcessLevel.Notifications);
                ClinicalTriageViewModel clinicalTriage = new ClinicalTriageViewModel();
                clinicalTriage.ClinicalTriages = Mapper.Map<IEnumerable<ClinicalTriage>>(_clinicalTriageDetails.ClinicalTriages);
                clinicalTriage.TotalCount = _clinicalTriageDetails.TotalCount;
                return View(clinicalTriage);
            }
            else
            {
                var _clinicalTriageDetails = _iPreparationService.getReferralByProcessLevel(skip.Value, GlobalConst.Records.LandingTake, GlobalConst.ProcessLevel.Notifications);
                ClinicalTriageViewModel clinicalTriage = new ClinicalTriageViewModel();
                clinicalTriage.ClinicalTriages = Mapper.Map<IEnumerable<ClinicalTriage>>(_clinicalTriageDetails.ClinicalTriages);
                clinicalTriage.TotalCount = _clinicalTriageDetails.TotalCount;
                return Json(clinicalTriage);
            }
        }
        public ActionResult NotificationAction(int id)
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
            PatientAndRequestModel _patientAndRequestModel = new PatientAndRequestModel();
            //int[] filetypeId = { GlobalConst.FileType.UploadInitialNotifications };
            //int[] filetypeIdDoc = { GlobalConst.FileType.InitialNotification, GlobalConst.FileType.ProofofService, GlobalConst.FileType.DeterminationLetter, GlobalConst.FileType.IMRApplication };
            _patientAndRequestModel = Mapper.Map<PatientAndRequestModel>(_iPreparationService.getPatientAndRequestForNotificationByReferralId(id, GlobalConst.Records.Skip, GlobalConst.Records.Take5));
            _patientAndRequestModel.ReferralFile = Mapper.Map<RFAReferralFile>(_intakeService.getReferralFileByRFAReferralIDandFileType(id).FirstOrDefault());
            _patientAndRequestModel.ReferralFileNotification = Mapper.Map<IEnumerable<RFAReferralFile>>(_intakeService.getReferralFileByRFAReferralIDandFileType(id));
            int _order = 1;
            foreach (RFAReferralFile _refFile in  _patientAndRequestModel.ReferralFileNotification)
            {
                _refFile.Order = _order;
                _order++;
            }

            StorageModel _storageModel = new StorageModel();            
            
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            foreach (RFAReferralFile _refFile in _patientAndRequestModel.ReferralFileNotification)
            {
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_refFile.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                _refFile.RFAReferralFileFullPath = (_storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + _refFile.RFAReferralFileName).Replace(toSearched, toReplace);
            }
            
            //int duplicateRequest = _patientAndRequestModel.RequestDetail.Count(s => s.DecisionID == GlobalConst.Decision.Duplicate);
            //if (duplicateRequest > 0)
            //{
            //    _patientAndRequestModel.ReferralsForDeterminationLetter = Mapper.Map<IEnumerable<PreviousReferralFromCurrentReferral>>(_intakeService.getPreviousReferralIDFromCurrentReferralInDuplicate(id));
            //}
            return View(_patientAndRequestModel);
        }


        [HttpPost]
        public ActionResult GetReferralFileByRFAReferralIDandFileType(int _referralID)
        {
            //int[] filetypeIdDoc = { GlobalConst.FileType.InitialNotification, GlobalConst.FileType.ProofofService, GlobalConst.FileType.DeterminationLetter, GlobalConst.FileType.IMRApplication };
            PatientAndRequestModel _patientAndRequestModel = new PatientAndRequestModel();
            _patientAndRequestModel.ReferralFileNotification = Mapper.Map<IEnumerable<RFAReferralFile>>(_intakeService.getReferralFileByRFAReferralIDandFileType(_referralID));
            int _order = 1;

            StorageModel _storageModel = new StorageModel();     
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            foreach (RFAReferralFile _refFile in _patientAndRequestModel.ReferralFileNotification)
            {
                _refFile.Order = _order;
                _order++;

               
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_refFile.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                _refFile.RFAReferralFileFullPath = (_storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + _refFile.RFAReferralFileName).Replace(toSearched, toReplace);
            }
            return Json(_patientAndRequestModel, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult GetRequestRecordByReferralID(int _referralID, int _skip)
        {
            return Json(Mapper.Map<PatientAndRequestModel>(_iPreparationService.getPatientAndRequestForNotificationByReferralId(_referralID, _skip, GlobalConst.Records.Take5)), GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult uploadNotificationDoc(RFAReferralFile _rfaReferralFile)
        {
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_rfaReferralFile.RFAReferralID, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(_rfaReferralFile.rfaReferralFile.FileName);
            string path = _storageService.GeneateStorage(_storageModel);
            string fileToDelete = _rfaReferralFile.RFAReferralFileName;
            _rfaReferralFile.rfaReferralFile.SaveAs(path + GlobalConst.ConstantChar.DoubleBackSlash + filename);

            _rfaReferralFile.RFAReferralFileName = filename;
            _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.UploadInitialNotifications;
            _rfaReferralFile.RFAFileCreationDate = DateTime.Now;
            _rfaReferralFile.RFAFileUserID = MMCUser.UserId;
            if (_rfaReferralFile.RFAReferralFileID == 0)
                _rfaReferralFile.RFAReferralFileID = _intakeService.addReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_rfaReferralFile));
            else
            {
                System.IO.File.Delete(path + GlobalConst.ConstantChar.DoubleBackSlash + fileToDelete);
                _intakeService.updateReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_rfaReferralFile));
            }
            _rfaReferralFile.rfaReferralFile = null;
            return Json(_rfaReferralFile, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult EmailNotificationDoc(IEnumerable<RFAReferralFile> RFAReferralFile, string EMailTo, string EMailCc, string subject, string EmailText, int RFAReferralID, string documentPath)
        {

            var myList = new List<string>();
            string path = _storageService.getReferralLetterStorage(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()), RFAReferralFile.First().RFAReferralID);
            //foreach (RFAReferralFile __rfaReferralFiledetail in RFAReferralFile)
            //{
            //    if (__rfaReferralFiledetail.IsChecked)
            //    {
            //        myList.Add(path + GlobalConst.ConstantChar.DoubleBackSlash + __rfaReferralFiledetail.RFAReferralFileName);

            //    }
            //}
            if (documentPath != null)
            {
                Tuple<string, string> t = DownloadPrintDocumnent(RFAReferralFile);
                myList.Add(t.Item1);
            }

            _mailService.SendEmail(EmailText, EMailTo, EMailCc, subject, myList.ToArray());
            EmailRecordStorage(EMailTo, EMailCc, subject, EmailText, myList, RFAReferralID);
            return Json(GlobalConst.Message.EmailSendMessage, GlobalConst.ContentTypes.TextHtml);

        }
        public void EmailRecordStorage(string EMailTo, string EMailCc, string subject, string EmailText, List<string> myList, int RFAReferralID)
        {
            EmailRecord _emailRecord = new EmailRecord();
            _emailRecord.EmRecTo = EMailTo;
            _emailRecord.EmRecCC = EMailCc;
            _emailRecord.EmRecSubject = subject;
            _emailRecord.EmRecBody = EmailText;
            _emailRecord.EmailRecDate = DateTime.Now;
            _emailRecord.UserID = MMCUser.UserId;

            int _emailRecordID = _iEmailRecordAttachmentService.addEmailRecord(Mapper.Map<MMCService.EmailRecordAttachmentService.EmailRecord>(_emailRecord));
            _iEmailRecordAttachmentService.AddEmailRecordAndRFARequestLinkByRFAReferralID(RFAReferralID, _emailRecordID);

            foreach (var _list in myList)
            {
                EmailRecordAttachment _emailRecordAttachment = new EmailRecordAttachment();
                string URL = "";
                string urlPathData = _list.ToString();
                string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
                URL = urlPathData.Replace(toSearched, toReplace);
                Tuple<string, string> savePathWithDownloadPath = new Tuple<string, string>(urlPathData, URL);
                string _urlData = savePathWithDownloadPath.ToString();
                _urlData = _urlData.Replace("(", "");
                _urlData = _urlData.Replace(")", "");

                string _fileName = Path.GetFileName(urlPathData);
                _emailRecordAttachment.EmailRecordId = _emailRecordID;
                _emailRecordAttachment.DocumentName = _fileName;
                _emailRecordAttachment.DocumentPath = _urlData;
                _iEmailRecordAttachmentService.addEmailRecordAttachment(Mapper.Map<MMCService.EmailRecordAttachmentService.EmailRecordAttachment>(_emailRecordAttachment));
            }
        }

        [HttpPost]
        public ActionResult GetAdjAndPhyEmail(int _referralID)
        {
            return Json(Mapper.Map<IEnumerable<Notification>>(_iNotificationService.getAdjandPhyEmailByReferralID(_referralID)), GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult BillingActionProcess(int _rFAReferralID)
        {
            if (_iPreparationService.updateProcessLevel(_rFAReferralID, GlobalConst.ProcessLevel.Billing) > 0)
                return Json(GlobalConst.Message.SendProcessLevelBilling);
            else
                return Json(GlobalConst.Message.ErrorSendProcessLevelBilling);
        }

        [HttpPost]
        public ActionResult GenerateProofOfService(int id)
        {
            StorageModel _storageModel = new StorageModel();
            MMCApp.Domain.Models.IntakeModel.RFAReferralFile _rfaReferralFile = new MMCApp.Domain.Models.IntakeModel.RFAReferralFile();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string Filename = id.ToString() + GlobalConst.ReportName.ProofOfService + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;    
            User objUserModel = new User();
            var _UserResult = _iUserService.getUserByID(MMCUser.UserId);
            objUserModel = Mapper.Map<User>(_UserResult);
            string uSignURL = GlobalConst.Extension.http + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.VirtualPathFolders.StorageUserPath + objUserModel.UserId + GlobalConst.ConstantChar.ForwardSlash + objUserModel.UserSignatureFileName;

            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptProofOfService], id, objUserModel.UserFirstName + ' ' + objUserModel.UserLastName, uSignURL, GlobalConst.Extension.PDF);
            
            if (System.IO.File.Exists(savePath))
            {
                System.IO.File.Delete(savePath);
            }
            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.DownloadFile(reportURL, savePath);
                client.Dispose();
            }
            _rfaReferralFile.RFAReferralID = id;
            _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.ProofofService;
            _rfaReferralFile.RFAReferralFileName = Filename;
            _rfaReferralFile.RFAFileCreationDate = DateTime.Now;
            _rfaReferralFile.RFAFileUserID = MMCUser.UserId;
            _intakeService.addReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_rfaReferralFile));
            return Json("");
        }

        [HttpGet]
        public FileResult GetProofOfServiceFile(int id)
        {
            StorageModel _storageModel = new StorageModel();            
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string Filename = id.ToString() + GlobalConst.ReportName.ProofOfService + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;    
            return File(savePath, GlobalConst.FileDownloadExtension.Application_doc, Filename);
        }

        [HttpPost]
        public ActionResult GenerateIMRForm(int id)
        {
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

           
            string Filename = id.ToString() + GlobalConst.ReportName.IMRForm + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
            User objUserModel = new User();
            var _UserResult = _iUserService.getUserByID(MMCUser.UserId);
            objUserModel = Mapper.Map<User>(_UserResult);

            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptIMRForm], id, GlobalConst.Extension.PDF);
           
            if (System.IO.File.Exists(savePath))
            {
                System.IO.File.Delete(savePath);
            }
            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.DownloadFile(reportURL, savePath);
                client.Dispose();
            }
            MMCApp.Domain.Models.IntakeModel.RFAReferralFile _rfaReferralFile = new MMCApp.Domain.Models.IntakeModel.RFAReferralFile();
            _rfaReferralFile.RFAReferralID = id;
            _rfaReferralFile.RFAReferralFileName = Filename;
            _rfaReferralFile.RFAFileCreationDate = DateTime.Now;
            _rfaReferralFile.RFAFileUserID = MMCUser.UserId;
            _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.IMRApplication;
            _intakeService.addReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_rfaReferralFile));
            return Json("");
            //return File(savePath, GlobalConst.FileDownloadExtension.Application_Pdf, Filename);
        }

        [HttpGet]
        public FileResult GetIMRFile(int id)
        {
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string Filename = id.ToString() + GlobalConst.ReportName.IMRForm + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
            return File(savePath, GlobalConst.FileDownloadExtension.Application_Pdf, Filename);
        }

        [HttpPost]
        public FileResult PrintDocumnent(IEnumerable<RFAReferralFile> RFAReferralFile)
        {
            Tuple<string, string> t = DownloadPrintDocumnent(RFAReferralFile);
            return File(t.Item1, GlobalConst.FileDownloadExtension.Application_Pdf, t.Item2);
        }

        public Tuple<string, string> DownloadPrintDocumnent(IEnumerable<RFAReferralFile> RFAReferralFile)
        {
            var myList = new List<string>();
            StorageModel _storageModel = new StorageModel();
            List<string> filesPath = new List<string>();
            string savePath;
            foreach (RFAReferralFile __rfaReferralFiledetail in RFAReferralFile)
            {
                if (__rfaReferralFiledetail.IsChecked)
                {

                    _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(__rfaReferralFiledetail.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                    _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                    savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + __rfaReferralFiledetail.RFAReferralFileName;
                    filesPath.Add(savePath);
                }
            }
            _storageModel.FolderName = GlobalConst.FolderName.MergePDF;
            string FileName = _storageModel.ReferralID + GlobalConst.ReportName.Mergepdf;
            string sPath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + FileName;
            if (System.IO.File.Exists(sPath))
            {
                System.IO.File.Delete(sPath);
            }
            _storageService.MergePdf(sPath, filesPath.ToArray());
            Tuple<string, string> savePathWithFileName = new Tuple<string, string>(sPath, FileName);
            return savePathWithFileName;
        }
        [HttpPost]
        public ActionResult GetLinkForAttachement(IEnumerable<RFAReferralFile> RFAReferralFile)
        {
            Tuple<string, string> t = DownloadPrintDocumnent(RFAReferralFile);
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            String URL = t.Item1.Replace(toSearched, toReplace);
            return Json(URL);
        }

        [HttpPost]
        public ActionResult GenerateDeterminationLetter(int id)
        {
            StorageModel _storageModel = new StorageModel();
            MMCApp.Domain.Models.IntakeModel.RFAReferralFile _rfaReferralFile = new MMCApp.Domain.Models.IntakeModel.RFAReferralFile();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string Filename = id.ToString() + GlobalConst.ReportName.Determination + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
            string _saveSignaturePath = _intakeService.GetSignaturePathAndDescriptionByReferralID(id);            

            string uSignURL = GlobalConst.Extension.http + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.ConstantChar.ForwardSlash + _saveSignaturePath;
            string uClientPathURL = GlobalConst.Extension.http + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.ConstantChar.ForwardSlash;

            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptDetermination], id, uSignURL, uClientPathURL, GlobalConst.Extension.PDF);

            if (System.IO.File.Exists(savePath))
            {
                System.IO.File.Delete(savePath);
            }
            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.DownloadFile(reportURL, savePath);
                client.Dispose();
            }
            _rfaReferralFile.RFAReferralID = id;
            _rfaReferralFile.RFAFileTypeID = GlobalConst.FileType.DeterminationLetter;
            _rfaReferralFile.RFAReferralFileName = Filename;
            _rfaReferralFile.RFAFileCreationDate = DateTime.Now;
            _rfaReferralFile.RFAFileUserID = MMCUser.UserId;
            _intakeService.addReferralFile(Mapper.Map<serviceModel.IntakeService.RFAReferralFile>(_rfaReferralFile));
            return Json("");
            //return File(savePath, GlobalConst.FileDownloadExtension.Application_Pdf, Filename);
        }

        [HttpGet]
        public FileResult GetDeterminationFile(int id)
        {
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string Filename = id.ToString() + GlobalConst.ReportName.Determination + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
            return File(savePath, GlobalConst.FileDownloadExtension.Application_Pdf, Filename);
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