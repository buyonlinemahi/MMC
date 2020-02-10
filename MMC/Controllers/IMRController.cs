using AutoMapper;
using MMCApp.Domain.Models.EmailRecordModel;
using MMCApp.Domain.Models.IMRModel;
using MMCApp.Domain.Models.IntakeModel;
using MMCApp.Domain.Models.StorageModel;
using MMCApp.Domain.Models.UserModel;
using MMCApp.Domain.ViewModels.ClinicalTriageViewModel;
using MMCApp.Domain.ViewModels.IMRViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.ApplicationServices;
using MMCApp.Infrastructure.Base;
using MMCApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class IMRController : BaseController
    {
        MMCService.IMRService.IMRServiceClient _iIMRService;
        MMCService.PreparationService.PreparationServiceClient _iPreparationService;
        MMCService.IntakeService.IntakeServiceClient _intakeService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public readonly PDFSplitterService _pdfSplitterService;
        StorageService _storageService;
        EMailService _mailService;
        MMCService.UserService.UserServiceClient _iUserService;
        MMCService.EmailRecordAttachmentService.EmailRecordAttachmentServiceClient _iEmailRecordAttachmentService;
        public IMRController()
        {
            _iIMRService = new MMCService.IMRService.IMRServiceClient();
            _iPreparationService = new MMCService.PreparationService.PreparationServiceClient();
            _intakeService = new MMCService.IntakeService.IntakeServiceClient();
            _storageService = new StorageService();
            _mailService = new EMailService();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
            _pdfSplitterService = new PDFSplitterService();
            _iUserService = new MMCService.UserService.UserServiceClient();
            _iEmailRecordAttachmentService = new MMCService.EmailRecordAttachmentService.EmailRecordAttachmentServiceClient();
        }
        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }

        public ActionResult SearchRequestIMRDetail()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }

        [HttpPost]
        public ActionResult GetRequestIMRRecordAll(int? _skip)
        {
            var _getRequestIMRRecordAll = _iIMRService.getRequestIMRRecordAll(_skip.Value, GlobalConst.Records.LandingTake);
            RequestIMRRecordViewModel objRequestIMRRecordViewModel = new RequestIMRRecordViewModel();
            objRequestIMRRecordViewModel.RequestIMRRecordDetails = Mapper.Map<IEnumerable<RequestIMRRecord>>(_getRequestIMRRecordAll.RequestIMRRecordDetails);
            objRequestIMRRecordViewModel.TotalCount = _getRequestIMRRecordAll.TotalCount;
            return Json(objRequestIMRRecordViewModel, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult GetRequestIMRRecordByPatientClaim(string _searchtext, int? _skip)
        {
            var _getRequestIMRRecordAll = _iIMRService.getRequestIMRRecordByPatientClaim(_searchtext, _skip.Value, GlobalConst.Records.LandingTake);
            RequestIMRRecordViewModel objRequestIMRRecordViewModel = new RequestIMRRecordViewModel();
            objRequestIMRRecordViewModel.RequestIMRRecordDetails = Mapper.Map<IEnumerable<RequestIMRRecord>>(_getRequestIMRRecordAll.RequestIMRRecordDetails);
            objRequestIMRRecordViewModel.TotalCount = _getRequestIMRRecordAll.TotalCount;
            return Json(objRequestIMRRecordViewModel, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult SaveRequestIMRRecord(string _rFARequestIDs)
        {
            string[] ArrRequestID = _rFARequestIDs.Substring(0, _rFARequestIDs.Length - 1).Split('#');
            _iIMRService.SaveRequestIMRRecord(ArrRequestID, MMCUser.UserId);
            return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpGet]
        public ActionResult IMRAction(int id)
        {
            getNoOfReferralAccordingToProcessLevels();
            Editor EditorNote = new Editor(System.Web.HttpContext.Current, GlobalConst.Richtexteditor.EditorNote);
            EditorNote.ClientFolder = GlobalConst.Richtexteditor.richtexteditor;
            EditorNote.Width = Unit.Pixel(1050);
            EditorNote.Height = Unit.Pixel(660);
            EditorNote.ResizeMode = RTEResizeMode.Disabled;
            EditorNote.DisabledItems = "save,help,insertgallery,insertimage,insertdocument,insertyoutube,googlemap,inserttemplate,insertmedia,insertvideo,syntaxhighlighter,html5";
            EditorNote.MvcInit();

            ViewData[GlobalConst.Richtexteditor.EditorNote] = EditorNote.MvcGetString();
            PatientAndRequestModel _patientAndRequestModel = new PatientAndRequestModel();
            _patientAndRequestModel = Mapper.Map<PatientAndRequestModel>(_iPreparationService.getPatientAndRequestByReferralId(id));
            _patientAndRequestModel.ReferralFileNotification = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIMRService.getMedicalAndLegalDocsByReferralID(id));
            _patientAndRequestModel.IMRLetters = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIMRService.GetIMRLetters(id));
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.ReferralID = id;
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();

            foreach (RFAReferralFile _rfaLetterFiledetail in _patientAndRequestModel.IMRLetters)
            {
                if (_rfaLetterFiledetail.RFAFileTypeID == 16)
                {
                    _rfaLetterFiledetail.RFAReferralFileFullPath = (_storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + id + "_IMRResponseLetter.pdf").Replace(toSearched, toReplace);
                }
                if (_rfaLetterFiledetail.RFAFileTypeID == 14)
                {
                    _rfaLetterFiledetail.RFAReferralFileFullPath = (_storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + id + "_IMRProofOfService.pdf").Replace(toSearched, toReplace);
                }
            }
            return View(_patientAndRequestModel);
        }

        [HttpPost]
        public ActionResult SaveIMRDecisionRecord(IEnumerable<IMRDecisionRequestDetails> _IMRDecisionRequestDetails, int DecisionID, int RFAReferralID, string _IMRDecisionReceivedDate)
        {
            _iIMRService.addIMRDecisionRequestDetail(Mapper.Map<MMC.MMCService.IMRService.IMRRFARequest[]>(_IMRDecisionRequestDetails));
            IMRRFAReferral _IMRRFAReferrals = new IMRRFAReferral();
            _IMRRFAReferrals.IMRDecisionID = DecisionID;
            _IMRRFAReferrals.RFAReferralID = RFAReferralID;
            _IMRRFAReferrals.IMRDecisionReceivedDate = Convert.ToDateTime(_IMRDecisionReceivedDate);
            _iIMRService.updateIMRRFAReferralByValues(Mapper.Map<MMCService.IMRService.IMRRFAReferral>(_IMRRFAReferrals));            
            var IMRRequestsDetails = Mapper.Map<IEnumerable<IMRDecisionRequestDetails>>(_iIMRService.getIMRDecisionPageRequestDetailsByReferralID(RFAReferralID));
            return Json(IMRRequestsDetails);
        }
        [HttpGet]
        public ActionResult IMRDecision(int id)
        {
            getNoOfReferralAccordingToProcessLevels();


            RequestIMRDecisionViewModel _RequestIMRDecisionViewModel = new RequestIMRDecisionViewModel();
            _RequestIMRDecisionViewModel.IMRReferralDetails = Mapper.Map<IMRDecisionReferralDetails>(_iIMRService.getIMRDecisionPageDetailsByReferralID(id));
            _RequestIMRDecisionViewModel.IMRRequestsDetails = Mapper.Map<IEnumerable<IMRDecisionRequestDetails>>(_iIMRService.getIMRDecisionPageRequestDetailsByReferralID(id));


            _RequestIMRDecisionViewModel.IMRLetters = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIMRService.GetIMRLetters(id));
            _RequestIMRDecisionViewModel.IMRDecision = Mapper.Map<IEnumerable<IMRDecision>>(_iIMRService.getIMRDecisionList());
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.ReferralID = id;
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            foreach (RFAReferralFile _rfaLetterFiledetail in _RequestIMRDecisionViewModel.IMRLetters)
            {
                if (_rfaLetterFiledetail.RFAFileTypeID == 17)
                {
                    _rfaLetterFiledetail.RFAReferralFileFullPath = (_storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.ReportName.IMRInitialNotification + GlobalConst.Extension.pdf).Replace(toSearched, toReplace);                    
                }
                if (_rfaLetterFiledetail.RFAFileTypeID == 18)
                {
                    _rfaLetterFiledetail.RFAReferralFileFullPath = (_storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + (_RequestIMRDecisionViewModel.IMRReferralDetails.IMRDecisionID == 2 ? GlobalConst.ReportName.IMRCertify : GlobalConst.ReportName.IMRModify) + GlobalConst.Extension.pdf).Replace(toSearched, toReplace);                    
                }
                if (_rfaLetterFiledetail.RFAFileTypeID == 19)
                {
                    _rfaLetterFiledetail.RFAReferralFileFullPath = (_storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.ReportName.IMRDecisionProofOfService + GlobalConst.Extension.pdf).Replace(toSearched, toReplace);                    
                }
            }
            return View(_RequestIMRDecisionViewModel);
        }

        [HttpPost]
        public JsonResult UploadIMRDecisionDoc(int id, int id2)
        {

            HttpPostedFileBase fileContent = Request.Files[0];
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(fileContent.FileName);
            StorageModel _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(id, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;
            string path = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + filename;

            RFAReferralFile _rfaFile = new RFAReferralFile();
            _rfaFile.RFAReferralID = id;
            _rfaFile.RFAReferralFileName = filename;
            _rfaFile.RFAFileTypeID = GlobalConst.FileType.IMRDecisionUpload;
            _rfaFile.RFAFileCreationDate = DateTime.Now;
            _rfaFile.RFAFileUserID = MMCUser.UserId;

            if (id2 != 0)
            {
                System.IO.File.Delete(path);
                fileContent.SaveAs(path);
                _rfaFile.RFAReferralFileID = id2;
                _intakeService.updateReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
            }
            else
            {
                fileContent.SaveAs(path);
                id2 = _intakeService.addReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
            }
            return Json(id2);
        }

        [HttpPost]
        public ActionResult SaveReferralIARecord(string _rFAReferralIDs, int _newReferral)
        {
            string[] ArrRequestID = _rFAReferralIDs.Substring(0, _rFAReferralIDs.Length - 1).Split('#');
            RFAReferralAdditionalInfo _rfaReferralAdditionalInfo = new RFAReferralAdditionalInfo();
            _rfaReferralAdditionalInfo.OriginalRFAReferralID = Convert.ToInt32(ArrRequestID[0]);
            _rfaReferralAdditionalInfo.RFAReferralID = _newReferral;
            _rfaReferralAdditionalInfo.UserId = MMCUser.UserId;
            _intakeService.addRFAReferralAdditionalInfo(Mapper.Map<MMCService.IntakeService.RFAReferralAdditionalInfo>(_rfaReferralAdditionalInfo));
            return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult GenerateIMRResponse(IEnumerable<RFAReferralFile> RFAReferralFile, int ReflID, int ImrRFAReferralFileID)
        {
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(ReflID, GlobalConst.ConstantChar.Char_R));

            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());

            string savePath;
            string AttachedDocument = "";
            foreach (RFAReferralFile __rfaReferralFiledetail in RFAReferralFile)
            {
                if (__rfaReferralFiledetail.IsChecked)
                {
                    AttachedDocument += __rfaReferralFiledetail.Mode + ",";
                }
            }
            AttachedDocument = AttachedDocument.Substring(0, AttachedDocument.Length - 1);
            string uClientPathURL = GlobalConst.Extension.http + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.ConstantChar.ForwardSlash;
            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptIMRResponse], ReflID, AttachedDocument, uClientPathURL, GlobalConst.Extension.PDF);
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
            _storageModel.ReferralID = ReflID;
            savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + ReflID + "_IMRResponseLetter.pdf";
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

            RFAReferralFile _rfaFile = new RFAReferralFile();
            _rfaFile.RFAReferralFileID = ImrRFAReferralFileID;
            _rfaFile.RFAReferralID = ReflID;
            _rfaFile.RFAReferralFileName = ReflID.ToString() + "_IMRResponseLetter.pdf";
            _rfaFile.RFAFileTypeID = GlobalConst.FileType.IMRResponse;
            _rfaFile.RFAFileCreationDate = DateTime.Now;
            _rfaFile.RFAFileUserID = MMCUser.UserId;
            if (ImrRFAReferralFileID != 0)
                _intakeService.updateReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
            else
                ImrRFAReferralFileID = _intakeService.addReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));

            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            String URL = savePath.Replace(toSearched, toReplace);
            Tuple<string, string, int> savePathWithFileName = new Tuple<string, string, int>(savePath, URL, ImrRFAReferralFileID);
            return Json(savePathWithFileName);
        }

        [HttpPost]
        public ActionResult AttachEmailFile(IEnumerable<RFAReferralFile> _rfaFile, int ReflID)
        {
            Tuple<string, string> t = DownloadPrintDocumnent(_rfaFile, ReflID);
            return Json(t);
        }

        [HttpPost]
        public ActionResult SendEmail(string EMailTo, string EMailCc, string subject, string EmailText, string DocumentFullPath, int ReflID)
        {
            var myList = new List<string>();
            myList.Add(DocumentFullPath);
            _mailService.SendEmail(EmailText, EMailTo, EMailCc, subject, myList.ToArray());
            EmailRecordStorage(EMailTo, EMailCc, subject, EmailText, myList, ReflID);

            IMRRFAReferral _IMRRFAReferrals = new IMRRFAReferral();
            _IMRRFAReferrals.IMRResponseDate = System.DateTime.Now;
            _IMRRFAReferrals.RFAReferralID = ReflID;
            _iIMRService.updateIMRRFAReferralByValues(Mapper.Map<MMCService.IMRService.IMRRFAReferral>(_IMRRFAReferrals));

            _iPreparationService.updateProcessLevel(ReflID, GlobalConst.ProcessLevel.IMRDecision);
            return Json(GlobalConst.Message.EmailSendMessage);
        }

        [HttpPost]
        public ActionResult SendEmailFromIMRDecision(string EMailTo, string EMailCc, string subject, string EmailText, string DocumentFullPath, int ReflID)
        {
            var myList = new List<string>();
            if(DocumentFullPath != null)
                myList.Add(DocumentFullPath);

            _mailService.SendEmail(EmailText, EMailTo, EMailCc, subject, myList.ToArray());
            EmailRecordStorage(EMailTo, EMailCc, subject, EmailText, myList, ReflID);
            _iPreparationService.updateProcessLevel(ReflID, GlobalConst.ProcessLevel.FileStorage);
            return Json(GlobalConst.Message.EmailSendMessage);
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
        public ActionResult GetMergedIMRDecisionDocuments(IEnumerable<RFAReferralFile> RFAReferralFile, int ReflID)
        {
            StorageModel _storageModel = new StorageModel();
            List<string> filesPath = new List<string>();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(ReflID, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;

            string pth1 = _storageService.GeneateStorage(_storageModel);
            foreach (RFAReferralFile _RFAReferralFile in RFAReferralFile)
            {
                filesPath.Add(_storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + _RFAReferralFile.RFAReferralFileName);
            }
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(ReflID, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.MergePDF;
            string FileName = _storageModel.ReferralID + GlobalConst.ReportName.IMRDecisionMergePdf;
            string sPath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + FileName;
            if (System.IO.File.Exists(sPath))
            {
                System.IO.File.Delete(sPath);
            }            
            _storageService.MergePdf(sPath, filesPath.ToArray());
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            String URL = sPath.Replace(toSearched, toReplace);
            Tuple<string, string> savePathWithFileName = new Tuple<string, string>(sPath, URL);
            return Json(savePathWithFileName);            
        }

        public Tuple<string, string> DownloadPrintDocumnent(IEnumerable<RFAReferralFile> RFAReferralFile, int ReflID)
        {   
            StorageModel _storageModel = new StorageModel();
            List<string> filesPath = new List<string>();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(ReflID, GlobalConst.ConstantChar.Char_R));
            _storageModel.ReferralID = 0;
            string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.path = path;
            _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;
            string splitRecordpath = _storageService.GeneateStorage(_storageModel);
            filesPath.Add(splitRecordpath + GlobalConst.ConstantChar.DoubleBackSlash + ReflID.ToString() + "_SplitBarcode.pdf");
            string savePath;

            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
            _storageModel.ReferralID = ReflID;
            savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + ReflID + "_IMRResponseLetter.pdf";            

            filesPath.Add(savePath);
            filesPath.Add(splitRecordpath + GlobalConst.ConstantChar.DoubleBackSlash + "Independent Medical Review Notice of Assignment and Request for Information Letter.pdf");
            string saveProofOfService = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + ReflID.ToString() + "_IMRProofOfService.pdf";
            foreach (RFAReferralFile __rfaReferralFiledetail in RFAReferralFile)
            {
                if (__rfaReferralFiledetail.IsChecked)
                {
                    if (__rfaReferralFiledetail.TableName == "RFAReferralFile")
                    {
                        _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(__rfaReferralFiledetail.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                        _storageModel.path = path;
                        _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                        savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + __rfaReferralFiledetail.FileTypeName;
                    }
                    else if (__rfaReferralFiledetail.TableName == "RecordSpliting")
                    {
                        _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(__rfaReferralFiledetail.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                        _storageModel.path = path;
                        _storageModel.ReferralID = 0;
                        _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;
                        savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + __rfaReferralFiledetail.FileTypeName;
                    }
                    else
                    {
                        savePath = path + GlobalConst.ConstantChar.DoubleBackSlash + "IntakeUploadFiles" + GlobalConst.ConstantChar.DoubleBackSlash + __rfaReferralFiledetail.FileTypeName;
                    }
                    filesPath.Add(savePath);
                }
            }
            filesPath.Add(saveProofOfService);
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(ReflID, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.MergePDF;
            string FileName = _storageModel.ReferralID + "_IMRMergeDocument.pdf";
            string sPath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + FileName;
            if (System.IO.File.Exists(sPath))
            {
                System.IO.File.Delete(sPath);
            }
            _storageService.MergePdf(sPath, filesPath.ToArray());
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            String URL = sPath.Replace(toSearched, toReplace);
            Tuple<string, string> savePathWithFileName = new Tuple<string, string>(sPath, URL);
            return savePathWithFileName;
        }

        [HttpPost]
        public ActionResult uploadIMRDoc(RFAReferralFile _rfaFile, IMRRFAReferral _imrReferral, RFAReferral rfa)
        {
            rfa.RFAReferralID = _imrReferral.RFAReferralID;
            _rfaFile.RFAReferralID = _imrReferral.RFAReferralID;
            int b = _iIMRService.UpdateRFAIMRReferenceNumberByReferralID(Mapper.Map<MMCService.IMRService.RFAReferral>(rfa));

            if (b == 0)
            {
                return Json("IMR Referrance Number already exists.");
            }
            else
            {
                if (_imrReferral.IMRRFAReferralID == 0)
                {
                    int a = _iIMRService.addIMRRFAReferral(Mapper.Map<MMCService.IMRService.IMRRFAReferral>(_imrReferral));
                }
                else
                {
                    int a = _iIMRService.updateIMRRFAReferral(Mapper.Map<MMCService.IMRService.IMRRFAReferral>(_imrReferral));
                }
                StorageModel _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_rfaFile.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                _storageModel.ReferralID = 0;
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;
                //Save file Temporary for Split
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(_rfaFile.rfaReferralFile.FileName);
                string path = _storageService.GeneateStorage(_storageModel);
                _rfaFile.rfaReferralFile.SaveAs(path + GlobalConst.ConstantChar.DoubleBackSlash + filename);
                string ContentFile = _pdfSplitterService.SplitPDFFilePatientMedicalRecord(1, 3, path + GlobalConst.ConstantChar.DoubleBackSlash + filename, _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash, "Independent Medical Review Notice of Assignment and Request for Information Letter");
                string BarcodeFile = _pdfSplitterService.SplitPDFFilePatientMedicalRecord(4, 4, path + GlobalConst.ConstantChar.DoubleBackSlash + filename, _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash, _rfaFile.RFAReferralID.ToString() + "_SplitBarcode");
                //Delete  Temporary file 
                System.IO.File.Delete(path + GlobalConst.ConstantChar.DoubleBackSlash + filename);
                _rfaFile.RFAReferralFileName = "Independent Medical Review Notice of Assignment and Request for Information Letter";
                _rfaFile.RFAFileTypeID = GlobalConst.FileType.IMRSplitContent;
                _rfaFile.RFAFileCreationDate = DateTime.Now;
                _rfaFile.RFAFileUserID = MMCUser.UserId;
                _intakeService.addReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
                _rfaFile.RFAFileTypeID = GlobalConst.FileType.IMRSplitBarcode;
                _rfaFile.RFAReferralFileName = _rfaFile.RFAReferralID.ToString() + "_SplitBarcode";
                _rfaFile.RFAFileCreationDate = DateTime.Now;
                _intakeService.addReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
                return Json(GlobalConst.Message.UploadedSucessfully);
            }
        }

        [HttpPost]
        public ActionResult getFileSplitedDetail(int _rfaID)
        {
            IEnumerable<RFAReferralFile> _rfaFileBarcode = Mapper.Map<IEnumerable<RFAReferralFile>>(_intakeService.getReferralFileByRFAReferralIDAndFileTypeID(_rfaID, GlobalConst.FileType.IMRSplitBarcode));
            return Json(_rfaFileBarcode);
        }

        [HttpPost]
        public ActionResult getIMRRefrenceNumber(int _rfaID)
        {
            RFAReferral _rfafile = Mapper.Map<RFAReferral>(_intakeService.getReferralByID(_rfaID));
            return Json(_rfafile);
        }
        [HttpPost]
        public ActionResult UpdateRFAIMRHistoryRationaleByReferralID(RFAReferral _rfaFile)
        {
            int a = _iIMRService.UpdateRFAIMRHistoryRationaleByReferralID(Mapper.Map<MMCService.IMRService.RFAReferral>(_rfaFile));
            return Json(a);
        }
        [HttpPost]
        public ActionResult GetIMRReferrals(int _rfaID)
        {
            return Json(_iIMRService.getIMRRFAReferralByRefID(_rfaID));
        }
        [HttpPost]
        public ActionResult GetPhysiciansbyReferralId(int _rfaID)
        {
            return Json(_iIMRService.getPhysicianByReferralID(_rfaID));
        }

        [HttpPost]
        public ActionResult GenerateProofOfService(int _rfaId, int POSRFAReferralFileID)
        {
            StorageModel _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_rfaId, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + _rfaId.ToString() + GlobalConst.ReportName.IMRProofOfService + GlobalConst.Extension.pdf;

            User objUserModel = new User();
            var _UserResult = _iUserService.getUserByID(MMCUser.UserId);
            objUserModel = Mapper.Map<User>(_UserResult);
            string uSignURL = GlobalConst.Extension.http + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.VirtualPathFolders.StorageUserPath + objUserModel.UserId + GlobalConst.ConstantChar.ForwardSlash + objUserModel.UserSignatureFileName;

            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptIMRResponseProofOfService], _rfaId, objUserModel.UserFirstName + ' ' + objUserModel.UserLastName, uSignURL, GlobalConst.Extension.PDF);
            using (WebClient client = new WebClient())
            {
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.DownloadFile(reportURL, savePath);
                client.Dispose();
            }

            RFAReferralFile _rfaFile = new RFAReferralFile();
            _rfaFile.RFAReferralFileID = POSRFAReferralFileID;
            _rfaFile.RFAReferralID = _rfaId;
            _rfaFile.RFAReferralFileName = _rfaId.ToString() + GlobalConst.ReportName.IMRProofOfService + GlobalConst.Extension.pdf;
            _rfaFile.RFAFileTypeID = GlobalConst.FileType.IMRProofofService;
            _rfaFile.RFAFileCreationDate = DateTime.Now;
            _rfaFile.RFAFileUserID = objUserModel.UserId;

            if (POSRFAReferralFileID != 0)
                _intakeService.updateReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
            else
                POSRFAReferralFileID = _intakeService.addReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));

            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            String URL = savePath.Replace(toSearched, toReplace);
            Tuple<string, string, int> savePathWithFileName = new Tuple<string, string, int>(GlobalConst.ReportName.IMRDecisionProofOfService, URL, POSRFAReferralFileID);
            return Json(savePathWithFileName);
        }

        [HttpPost]
        public ActionResult GetIMRProofOfService(int _rfaID)
        {
            IEnumerable<RFAReferralFile> _rfaProofOfService = Mapper.Map<IEnumerable<RFAReferralFile>>(_intakeService.getReferralFileByRFAReferralIDAndFileTypeID(_rfaID, GlobalConst.FileType.IMRProofofService));
            return Json(_rfaProofOfService);
        }


        [HttpPost]
        public ActionResult GenerateIMRDecisionProofOfService(int referralID, int RFAReferralFileID)
        {
            StorageModel _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(referralID, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
            string fileName = GlobalConst.ReportName.IMRDecisionProofOfService + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + fileName;            
            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptIMRDecisionProofOfService], referralID, GlobalConst.Extension.PDF);

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

            RFAReferralFile _rfaFile = new RFAReferralFile();
            _rfaFile.RFAReferralFileID = RFAReferralFileID;
            _rfaFile.RFAReferralID = referralID;
            _rfaFile.RFAReferralFileName = fileName;
            _rfaFile.RFAFileTypeID = GlobalConst.FileType.IMRDecisionProofOfService;
            _rfaFile.RFAFileCreationDate = DateTime.Now;
            _rfaFile.RFAFileUserID = MMCUser.UserId;

            if (RFAReferralFileID != 0)
                _intakeService.updateReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
            else
                RFAReferralFileID = _intakeService.addReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
            IEnumerable<RFAReferralFile> IMRLetters = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIMRService.GetIMRLetters(referralID));
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            String URL = savePath.Replace(toSearched, toReplace);
            Tuple<string, string, int, IEnumerable<RFAReferralFile>> savePathWithFileName = new Tuple<string, string, int, IEnumerable<RFAReferralFile>>(fileName, URL, RFAReferralFileID, IMRLetters);

            return Json(savePathWithFileName);
        }

        [HttpPost]
        public ActionResult GenerateIMRInitialNotification(int referralID, int RFAReferralFileID)
        {
            StorageModel _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(referralID, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
            string fileName = GlobalConst.ReportName.IMRInitialNotification + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + fileName;
            string uClientPathURL = GlobalConst.Extension.http + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.ConstantChar.ForwardSlash;
            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptIMRInitialNotification], referralID, uClientPathURL, GlobalConst.Extension.PDF);            
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

            RFAReferralFile _rfaFile = new RFAReferralFile();
            _rfaFile.RFAReferralFileID = RFAReferralFileID;
            _rfaFile.RFAReferralID = referralID;
            _rfaFile.RFAReferralFileName = fileName;
            _rfaFile.RFAFileTypeID = GlobalConst.FileType.IMRInitialNotification;
            _rfaFile.RFAFileCreationDate = DateTime.Now;
            _rfaFile.RFAFileUserID = MMCUser.UserId;

            if (RFAReferralFileID != 0)
                _intakeService.updateReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
            else
                RFAReferralFileID = _intakeService.addReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));

            IEnumerable<RFAReferralFile> IMRLetters = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIMRService.GetIMRLetters(referralID));
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            String URL = savePath.Replace(toSearched, toReplace);
            Tuple<string, string, int, IEnumerable<RFAReferralFile>> savePathWithFileName = new Tuple<string, string, int, IEnumerable<RFAReferralFile>>(fileName, URL, RFAReferralFileID, IMRLetters);
            return Json(savePathWithFileName);
        }

        [HttpPost]
        public ActionResult GenerateIMRDecisionLetter(int referralID, int RFAReferralFileID, int DecisionID)
        {
            StorageModel _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(referralID, GlobalConst.ConstantChar.Char_R));
            _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
            string fileName = (DecisionID == 2 ? GlobalConst.ReportName.IMRCertify : GlobalConst.ReportName.IMRModify) + GlobalConst.Extension.pdf;
            string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + fileName;
            string uClientPathURL = GlobalConst.Extension.http + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + GlobalConst.ConstantChar.ForwardSlash;

            string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptIMRDecisionLetter], referralID, uClientPathURL, GlobalConst.Extension.PDF);

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

            RFAReferralFile _rfaFile = new RFAReferralFile();
            _rfaFile.RFAReferralFileID = RFAReferralFileID;
            _rfaFile.RFAReferralID = referralID;
            _rfaFile.RFAReferralFileName = fileName;
            _rfaFile.RFAFileTypeID = GlobalConst.FileType.IMRDecisionLetter;
            _rfaFile.RFAFileCreationDate = DateTime.Now;
            _rfaFile.RFAFileUserID = MMCUser.UserId;

            if (RFAReferralFileID != 0)
                _intakeService.updateReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));
            else
                RFAReferralFileID = _intakeService.addReferralFile(Mapper.Map<MMC.MMCService.IntakeService.RFAReferralFile>(_rfaFile));

            IEnumerable<RFAReferralFile> IMRLetters = Mapper.Map<IEnumerable<RFAReferralFile>>(_iIMRService.GetIMRLetters(referralID));
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            String URL = savePath.Replace(toSearched, toReplace);
            Tuple<string, string, int, IEnumerable<RFAReferralFile>> savePathWithFileName = new Tuple<string, string, int, IEnumerable<RFAReferralFile>>(fileName, URL, RFAReferralFileID, IMRLetters);

            return Json(savePathWithFileName);
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