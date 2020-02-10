using System;
using System.IO;
using AutoMapper;
using System.Linq;
using System.Web;
using System.Net;
using MMCApp.Domain.Models.ICD9CodeModel;
using MMCApp.Domain.Models.ICDCodeModel;
using MMCApp.Domain.ViewModels.ICD9CodeViewModel;
using MMCApp.Domain.ViewModels.ICDCodeViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using MMCApp.Infrastructure.Base;
using MMCApp.Domain.ViewModels.AdditionalDocumentViewModel;
using MMCApp.Domain.Models.AdditionalDocumentModel;
using MMCApp.Domain.Models.StorageModel;
using MMCApp.Infrastructure.ApplicationServices;
using System.Text.RegularExpressions;
using MMCApp.Domain.Models.CommonModel;
using System.Web.UI;
using MMCApp.Domain.Models.EmailRecordModel;
using MMCApp.Domain.ViewModels.EmailRecordViewModel;
namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class CommonController : BaseController
    {
        //
        // GET: /Common/
        MMCService.CommonService.CommonServiceClient _iCommonService;
        MMCService.PatientService.PatientServiceClient _iPatientService;
        MMCService.EmailRecordAttachmentService.EmailRecordAttachmentServiceClient _iEmailRecordAttachmentService;
        public readonly StorageService _storageService;
        public readonly EMailService _mailService;
        public CommonController()
        {
            _iPatientService = new MMCService.PatientService.PatientServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
            _storageService = new StorageService();
            _mailService = new EMailService();
            _iEmailRecordAttachmentService = new MMCService.EmailRecordAttachmentService.EmailRecordAttachmentServiceClient();
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult getStates()
        {
            return Json(_iCommonService.getStateAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getSpecialtyAll()
        {
            return Json(_iCommonService.getSpecialtiesAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getLanguageAll()
        {
            return Json(_iCommonService.getLanguageAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getCurrentMedicalConditionAll()
        {
            return Json(_iCommonService.getCurrentMedicalConditionsAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getPatientClaimAddOnBodyPartsAll()
        {
            return Json(_iCommonService.getBodyPartsAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult getPatientClaimBodyPartStatusAll()
        {
            return Json(_iCommonService.getBodyPartStatusesAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getPatientClaimDiagnosesAll(string _searchName,string _icdTab, int _skip)
        {
            ICDCodeViewModel objICDCodeViewModel = new ICDCodeViewModel();
            if (_skip == GlobalConst.ConstantChar.Zero)
            {
                var _icdResults = _iCommonService.getICDCodesByNumberOrDescription(_searchName.Trim(), _icdTab, GlobalConst.Records.Skip, GlobalConst.Records.Take);
                objICDCodeViewModel.ICDCodeDetails = Mapper.Map<IEnumerable<ICDCode>>(_icdResults.ICDCodeDetails);
                objICDCodeViewModel.TotalCount = _icdResults.TotalCount;
            }
            else
            {

                var _icdResults = _iCommonService.getICDCodesByNumberOrDescription(_searchName.Trim(), _icdTab, _skip, GlobalConst.Records.Take);
                objICDCodeViewModel.ICDCodeDetails = Mapper.Map<IEnumerable<ICDCode>>(_icdResults.ICDCodeDetails);
                objICDCodeViewModel.TotalCount = _icdResults.TotalCount;
            }
            return Json(objICDCodeViewModel);
        }
        #region AdditionalDocuments
        public ActionResult GetAdditionalDocuments(int id,int id2,int id3,string emailPopupName)
        {
            AdditionalDocumentViewModel objAdditionalDocument = new AdditionalDocumentViewModel();
            List<PreviousLinks> objPreviousLinks = new List<PreviousLinks>();
            List<string> cookieNameLists = new List<string>(new string[] { GlobalConst.ConstantName.MMCDeferral, GlobalConst.ConstantName.MMCDuplicate, GlobalConst.ConstantName.MMCUnabletoReview, GlobalConst.ConstantName.MMCClientAuthorized, GlobalConst.ConstantName.MMCAudit, GlobalConst.ConstantName.MMCRfi, GlobalConst.ConstantName.MMCTimeExtensionPN, GlobalConst.ConstantName.MMCTimeExtension, GlobalConst.ConstantName.MMCProofOfService, GlobalConst.ConstantName.MMCNotification, GlobalConst.ConstantName.MMCIMR, GlobalConst.ConstantName.MMCIMRDecision });
            foreach (var _name in cookieNameLists)
            {
                if (Request.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][_name] != null)
                {
                    string _attach = Request.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][_name];
                    if (_attach != "" && _attach != null)
                    {
                        objPreviousLinks.Add(new PreviousLinks() { AttachmentLink = _attach });
                    }
                }
            }
            objAdditionalDocument.PreviousAttachmentLinks = objPreviousLinks; 
            var _patientDetails = _iPatientService.getPatientByID(id);
            var _patientClaimDetails = _iPatientService.getPatientClaimByID(id2);
            objAdditionalDocument.PatientID = id;
            objAdditionalDocument.PatientClaimID = id2;
            objAdditionalDocument.RFAReferralID = id3;
            objAdditionalDocument.emailPopupName = emailPopupName;           
            objAdditionalDocument.PatientFullName = _patientDetails.PatFirstName + " " + _patientDetails.PatLastName;
            objAdditionalDocument.PatClaimNumber = _patientClaimDetails.PatClaimNumber;
            var _additionalDoc = _iCommonService.getAdditionalDocumentsByPatientID(id, id2, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
            objAdditionalDocument.AdditionalDocumentDetails = Mapper.Map<IEnumerable<AdditionalDocument>>(_additionalDoc.AdditionalDocumentDetails);
            objAdditionalDocument.TotalCount = _additionalDoc.TotalCount;

            return View(GlobalConst.Views.CommonController.AdditionalDocument, objAdditionalDocument);
        }

        [HttpPost]
        public JsonResult GetAdditionalDocumentByPatientID(int _patientID, int _patientClaimID, int _skip)
        {
            AdditionalDocumentViewModel objAdditionalDocument = new AdditionalDocumentViewModel();
            var _additionalDoc = _iCommonService.getAdditionalDocumentsByPatientID(_patientID, _patientClaimID, _skip, GlobalConst.Records.LandingTake);
            objAdditionalDocument.AdditionalDocumentDetails = Mapper.Map<IEnumerable<AdditionalDocument>>(_additionalDoc.AdditionalDocumentDetails);
            objAdditionalDocument.TotalCount = _additionalDoc.TotalCount;
            return Json(objAdditionalDocument, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public JsonResult GetAdditionalDocumentByMedicalUploadPatientID(int _patientID, int _patientClaimID, int _take)
        {
            AdditionalDocumentViewModel objAdditionalDocument = new AdditionalDocumentViewModel();
            var _additionalDoc = _iCommonService.getAdditionalDocumentsByPatientID(_patientID, _patientClaimID, GlobalConst.Records.Skip, _take);
            objAdditionalDocument.AdditionalDocumentDetails = Mapper.Map<IEnumerable<AdditionalDocument>>(_additionalDoc.AdditionalDocumentDetails);
            objAdditionalDocument.TotalCount = _additionalDoc.TotalCount;
            return Json(objAdditionalDocument, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult AddLinkForSendingEmail(IEnumerable<AdditionalDocument> AdditionalDocumentDetails, IEnumerable<PreviousLinks> PreviousAttachmentLinks)
        {
            double totalMB = 0;
            List<string> _urlPathForEmailAttachment = new List<string>();
            var _previousAttachmentLinks = PreviousAttachmentLinks;
            var _listAdditionalRFAReferralIDList = AdditionalDocumentDetails.Where(doc => doc.IsChecked).Select(doc => new { doc.RFAReferralID, doc.PatientClaimID, doc.DocumentName, doc.RFAFileName, doc.TypeName, doc.FileID }).Distinct();
            foreach (var _listDoc in _listAdditionalRFAReferralIDList)
            {
                string savePath = "";
                string saveFullPathWithFileName = "";

                StorageModel _storageModel = new StorageModel();
                if (_listDoc.TypeName == GlobalConst.ConstantName.URHistory)
                {
                    var _IsIntakeUpload = _listDoc.RFAFileName.Split('_');
                    if (_IsIntakeUpload[1].ToString() == GlobalConst.ConstantName.IntakeUploadpdf)
                    {
                        string _virtualPath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                        savePath = _virtualPath + GlobalConst.VirtualPathFolders.IntakeUploadFiles + GlobalConst.ConstantChar.DoubleBackSlash + _listDoc.RFAFileName;
                    }
                    else
                    {
                        _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_listDoc.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                        _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                        _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                         savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + _listDoc.RFAFileName;
                    }
                }
                else
                {
                    _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_listDoc.PatientClaimID, GlobalConst.ConstantChar.Char_C));
                    _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                    _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;
                    savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + _listDoc.RFAFileName;
                }
               
                Tuple<string, string, string> savePathWithFileName = CombinedLinkForAttachement(savePath, _listDoc.RFAFileName);
                saveFullPathWithFileName = savePathWithFileName.ToString();
                saveFullPathWithFileName = Regex.Replace(saveFullPathWithFileName, "[()|{}]", "");
                _urlPathForEmailAttachment.Add(saveFullPathWithFileName);

                totalMB += CalculateAttachmentInMB(savePath);
            }

            foreach (var _sessionWithName in _previousAttachmentLinks)
            {
                if (_sessionWithName != null)
                {
                    var fullPath = _sessionWithName.AttachmentLink.Split(',');
                    totalMB += CalculateAttachmentInMB(fullPath[0]);
                    _urlPathForEmailAttachment.Add(_sessionWithName.AttachmentLink);
                }

            }

            if (totalMB < GlobalConst.ConstantChar.TwenetyOne)
                 return Json(_urlPathForEmailAttachment,GlobalConst.ContentTypes.TextHtml); 
            else
                return Json(GlobalConst.ConstantName.ExceedLimit, GlobalConst.ContentTypes.TextHtml); 
        }

        private Tuple<string, string,string> CombinedLinkForAttachement(string urlPathData,string DocumentName)
        {
            string URL = "";
            string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
            string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            URL = urlPathData.Replace(toSearched, toReplace);
            Tuple<string, string, string> savePathWithFileName = new Tuple<string,string,string>(urlPathData,DocumentName, URL);
            return savePathWithFileName;
        }

        [HttpPost]
        public ActionResult EmailSendMultipleDoc(EmailMultipleAttachment objEmailAttached)
        {
            var myList = new List<string>();
            if(objEmailAttached.AttachmentForEmailArray != null)
            {
               
                foreach (var multipath in objEmailAttached.AttachmentForEmailArray)
                {
                    myList.Add(multipath.AttachmentLink);
                }
            }
            
            var myListReferralIDorFileID = GetRFAReferralFileIDOrReferralID(objEmailAttached.popUpName);
            _mailService.SendEmail(objEmailAttached.SendEmailText, objEmailAttached.SendEMailTo, objEmailAttached.SendEMailCc, objEmailAttached.Sendsubject, myList.ToArray());
            EmailRecordStorage(objEmailAttached.SendEMailTo, objEmailAttached.SendEMailCc, objEmailAttached.Sendsubject, objEmailAttached.SendEmailText, myList, objEmailAttached.popUpName, objEmailAttached.rflID, myListReferralIDorFileID);

            return Json(GlobalConst.Message.EmailSendMessage, GlobalConst.ContentTypes.TextHtml);

        }
        [HttpPost]
        public ActionResult EmailSendMultipleAttachment(EmailMultipleAttachment objEmailAttached)
        {
            //Only this method will not save email record because it is sending from Patient URHistory //
            var myList = new List<string>();
            if (objEmailAttached.AttachmentForEmailArray != null)
            {
                foreach (var multipath in objEmailAttached.AttachmentForEmailArray)
                {
                    myList.Add(multipath.AttachmentLink);
                }
            }
            if (Request.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks] != null)
            {
                var c = new HttpCookie(GlobalConst.ConstantName.PreviousAttachementLinks);
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            _mailService.SendEmail(objEmailAttached.SendEmailText, objEmailAttached.SendEMailTo, objEmailAttached.SendEMailCc, objEmailAttached.Sendsubject, myList.ToArray());
            return Json(GlobalConst.Message.EmailSendMessage, GlobalConst.ContentTypes.TextHtml);
        }

        public double CalculateAttachmentInMB(string savePath)
        {
            long bytes = 0;
            double kilobytes = 0;
            double megabytes = 0;
            

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(savePath);
            if (fileInfo.Exists)
            {
                bytes = fileInfo.Length;
                kilobytes = (double)bytes / 1024;
                megabytes = kilobytes / 1024;
            }
            return megabytes;
        }

        [HttpPost]
        public void AssignRFiInSessionVariable(string FullPath, string FileName, int referralFileID, string sessionVar)
        {
            HttpCookie cookie = new HttpCookie(GlobalConst.ConstantName.PreviousAttachementLinks);
            string path = FullPath.Replace(GlobalConst.ConstantName.Storage, GlobalConst.ConstantChar.StringBlank);
            string _fullPathName = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString() + path);
            string pathFullWithName = _fullPathName + GlobalConst.ConstantChar.Comma + FileName + GlobalConst.ConstantChar.Comma + FullPath;
            if (sessionVar ==  GlobalConst.ConstantName.SessionRFI)
            {
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCRfi] = pathFullWithName;
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.RFAReferralFileID] = referralFileID.ToString();  
            }
            else if (sessionVar == GlobalConst.ConstantName.SessionNotification)
            {
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCNotification] = pathFullWithName;
            }
            else if (sessionVar == GlobalConst.ConstantName.SessionIMR)
            {
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCIMR] = pathFullWithName;
            }
            else if (sessionVar == GlobalConst.ConstantName.SessionIMRDecision)
            {
                Response.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.MMCIMRDecision] = pathFullWithName;
            }                       
        }
        public void EmailRecordStorage(string EMailTo, string EMailCc, string subject, string EmailText, List<string> myList, string popUpName, int referralID, List<string> myListReferralIDorFileID)
        {
            EmailRecord _emailRecord = new EmailRecord();
            _emailRecord.EmRecTo = EMailTo;
            _emailRecord.EmRecCC = EMailCc;
            _emailRecord.EmRecSubject = subject;
            _emailRecord.EmRecBody = EmailText;
            _emailRecord.EmailRecDate = DateTime.Now;
            _emailRecord.UserID = MMCUser.UserId;

            int _emailRecordID = _iEmailRecordAttachmentService.addEmailRecord(Mapper.Map<MMCService.EmailRecordAttachmentService.EmailRecord>(_emailRecord));
            AddEmailRequestLinkByPopUpName(popUpName, _emailRecordID, referralID, myListReferralIDorFileID);
            foreach (var _list in myList)
            {
                EmailRecordAttachment _emailRecordAttachment = new EmailRecordAttachment();
                string URL = "";
                string urlPathData = _list.ToString();
                string toSearched = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                string toReplace = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
                URL = urlPathData.Replace(toSearched, toReplace);
                Tuple<string,string> savePathWithDownloadPath = new Tuple<string,string>(urlPathData,URL);
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
        public void AddEmailRequestLinkByPopUpName(string popupName, int emailRecordID, int rflID, List<string> myListReferralIDorFileID)
        {
            string _popName = popupName;          
            if (_popName == "Preparation" || _popName == "ClinicalTriage" || _popName == "ClinicalAudit")
            {                
                foreach (var _id in myListReferralIDorFileID)
                {
                    _iEmailRecordAttachmentService.AddEmailRecordAndRFARequestLinkByRFAReferralID(Convert.ToInt32(_id), emailRecordID);                       
                    
                }
            }           
            else if (_popName == "TimeExtensionPN")
            {
                _iEmailRecordAttachmentService.AddEmailRecordAndRFARequestLinkByRFITimeExtension(rflID,Convert.ToInt32(myListReferralIDorFileID[0]), emailRecordID);
            }
            else if (_popName == "RFI")
            {
                _iEmailRecordAttachmentService.AddEmailRecordAndRFARequestLinkByRFITimeExtension(0,Convert.ToInt32(myListReferralIDorFileID[0]), emailRecordID);
            }
            else if (_popName == "Notification")
            {
                _iEmailRecordAttachmentService.AddEmailRecordAndRFARequestLinkByRFAReferralID(rflID, emailRecordID);
            }
            else if (_popName == "IMR")
            {
                _iEmailRecordAttachmentService.AddEmailRecordAndRFARequestLinkByRFAReferralID(rflID, emailRecordID);
            }
        }
        public List<string> GetRFAReferralFileIDOrReferralID(string popUpName)
        {
            var myList = new List<string>();
            if (popUpName == "Preparation" || popUpName == "ClinicalTriage" || popUpName == "ClinicalAudit")
            {
                List<string> cookieNameLists = new List<string>(new string[] { GlobalConst.ConstantName.DeferralRFAReferralID, GlobalConst.ConstantName.DuplicateRFAReferralID, GlobalConst.ConstantName.UnableToReviewRFAReferralID, GlobalConst.ConstantName.ClientAuthorizedRFAReferralID, GlobalConst.ConstantName.AuditRFAReferralID });
                foreach (var _name in cookieNameLists)
                {
                    if (Request.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][_name] != null)
                    {
                        var _referralID = Request.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][_name];
                        myList.Add(_referralID);
                    }
                }
            }
            else if (popUpName == "TimeExtensionPN" || popUpName == "RFI")
            {
                var _referralID = Request.Cookies[GlobalConst.ConstantName.PreviousAttachementLinks][GlobalConst.ConstantName.RFAReferralFileID];
                myList.Add(_referralID);
            }
            return myList;
        }
        [HttpPost]
        public ActionResult GetEmailRecordAttachmentByEmailRecord(int emailRecordId)
        {
            //---- Only in case of Email Pop Up  emailRecordId act as RFARequestID-----------------//
            var _emailRecordAttachment = _iEmailRecordAttachmentService.getEmailRecordAttachmentByEmailRecordId(emailRecordId);
            EmailRecordAttachmentViewModel objEmailRecordAttachments = new EmailRecordAttachmentViewModel();
            objEmailRecordAttachments.EmailRecordAttachmentDetails = Mapper.Map<IEnumerable<EmailRecordAttachment>>(_emailRecordAttachment.EmailRecordAttachmentDetails);
            return Json(objEmailRecordAttachments);
        }
        #endregion
        public JsonResult getICD9CodesByCode(string _searchtext)
        {
            //return Json(_iCommonService.getICD9CodesByCode(_searchtext));
            return Json(_iCommonService.getICDCodesByNumber(_searchtext, "ICD9"));
        }

        [HttpGet]
        public JsonResult getPatientClaimStatusesAll()
        {
            return Json(_iCommonService.getClaimStatusesAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getDocumentCategoriesAll()
        {
            return Json(_iCommonService.getDocumentCategoriesAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult getDocumentTypeAll()
        {
            return Json(_iCommonService.getDocumentTypesAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDocumentTypeByDocumentCategoryID(int _documentCategoryID)
        {
            return Json(_iCommonService.getDocumentTypeByDocumentCategoryID(_documentCategoryID), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getPatientComorbidConditionsByPatientID(int PatientID)
        {
            return Json(_iCommonService.getPatientComorbidConditionsByPatientID(PatientID), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult getAttorneyFirmTypeAll()
        {
            return Json(_iCommonService.getAttorneyFirmTypeAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public void setScreenMode(string _mode)
        {
            MMCScreenMode = _mode;
        }

        [HttpPost]
        public string getScreenMode()
        {
            return MMCScreenMode;
        }

        [HttpGet]
        public JsonResult getStatus()
        {
            return Json(_iCommonService.getStatusAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
    }
}
 