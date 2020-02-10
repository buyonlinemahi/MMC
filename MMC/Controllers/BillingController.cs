using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMCApp.Infrastructure.Global;
using MMCApp.Domain.ViewModels.BillingViewModel;
using MMCApp.Domain.Models.Billing;
using AutoMapper;
using MMCApp.Domain.Models.ClinicalTriage;
using MMCApp.Domain.Models.StorageModel;
using System.Net;
using System.Configuration;
using MMCApp.Infrastructure.ApplicationServices;
using MMCApp.Domain.ViewModels.Billing;
using MMCApp.Domain.Models.IntakeModel;
using MMCApp.Infrastructure.Base;
using MMC.MMCService.ClientService;
using MMCApp.Infrastructure.ApplicationFilters;

namespace MMC.Controllers
{
     [AuthorizedUserCheck]
    public class BillingController : BaseController
    {
        MMCService.IntakeService.IntakeServiceClient _intakeService;
        MMCService.BillingService.BillingServiceClient _iBillingService;
        MMCService.ClientService.ClientServiceClient _iClientService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        MMCService.PreparationService.PreparationServiceClient _iPreparationService;
        StorageService _storageService;
        public BillingController() 
        {
            _intakeService = new MMCService.IntakeService.IntakeServiceClient();
            _iPreparationService = new MMCService.PreparationService.PreparationServiceClient();
            _iBillingService = new MMCService.BillingService.BillingServiceClient();         
            _iClientService = new MMCService.ClientService.ClientServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
            _storageService = new StorageService();
        }
        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();

            return View(GetBillingDetailResult(GlobalConst.Records.Skip));
        }

        [HttpPost]
        public ActionResult UpdateRequestBillings(RFARequestBilling _RFARequestBilling)
        {
            if (_RFARequestBilling.RFARequestBillingID != 0)
            {
                _iBillingService.updateRFARequestBilling(Mapper.Map<MMCService.BillingService.RFARequestBilling>(_RFARequestBilling));
            }
            else
            {
                _iBillingService.addRFARequestBilling(Mapper.Map<MMCService.BillingService.RFARequestBilling>(_RFARequestBilling));
            }
            return Json(GlobalConst.Message.Updated);
        }

        [HttpPost]
        public ActionResult GetPageBillingResult(int _skip) 
        {
            return Json(GetBillingDetailResult(_skip));
        }

        [NonAction]
        public BillingViewModel GetBillingDetailResult(int _skip)
        {
            BillingViewModel _billingDetails = new BillingViewModel();
            var BillingDetails = _iBillingService.getDetailsForBilling(_skip, GlobalConst.Records.LandingTake);
            _billingDetails.BillingDetails = Mapper.Map<IEnumerable<Billing>>(BillingDetails.BillingDetails);
            _billingDetails.TotalCount = BillingDetails.TotalCount;
            return _billingDetails;
        }

        [HttpPost]
        public ActionResult GetBillingDetailByClientNameResult(string ClientName, int _skip)
        {
            BillingViewModel _billingDetails = new BillingViewModel();
            var _BillingDetails = _iBillingService.getDetailsForBillingByClientName(ClientName, _skip, GlobalConst.Records.LandingTake);
            _billingDetails.BillingDetails = Mapper.Map<IEnumerable<Billing>>(_BillingDetails.BillingDetails);
            _billingDetails.TotalCount = _BillingDetails.TotalCount;
            return Json(_billingDetails, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpGet]
        public JsonResult getClientAll()
        {
            return Json(_iClientService.getClientAll(), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateInvoice(IEnumerable<Billing> BillingDetails)
        {
            string referralIDList = GlobalConst.ConstantChar.StringBlank;
            int _patClientID = 0;
            bool CreateStatement = false;
            List<int> _listInvoiceNum = new List<int>();
            List<RFAReferralInvoice> _InvoiceModelList = new List<RFAReferralInvoice>();            
             RFAReferralAdditionalLink objAdditionalLinkInvoice = new RFAReferralAdditionalLink();
            var _listPatientClaimID = BillingDetails.Where(claim => claim.IsChecked).Select(claim => claim.PatientClaimID).Distinct();

            //Check Whether to create Statement or not

            var _clientPrivateLabel = _iClientService.getClientPrivateLabelDetailByClientID(BillingDetails.FirstOrDefault().PatClientID);
            if (_clientPrivateLabel != null)
            {
                if (_clientPrivateLabel.ClientStatementStart != null)
                {
                    CreateStatement = true;
                }
            }
         

           //Optimized Removed Foreach....For adding referralID in List and generating the InvoiceTemplate according to InvoiceID

            foreach (var _patClaimID in _listPatientClaimID) // For adding referralID in List and generating the InvoiceTemplate according to InvoiceID
            {
              var   _referralIDList = BillingDetails.Where(claim => claim.IsChecked && claim.PatientClaimID == _patClaimID).Select(claim => new { claim.RFAReferralID, claim.PatClientID }).Distinct();

                foreach (var refId in _referralIDList)
                        {
                    referralIDList += refId.RFAReferralID.ToString() + ",";
                    _patClientID = refId.PatClientID;
                    }
                //Optimized Removed Foreach....For Adding PatientClaimID in List
                 StorageModel _storageModel = new StorageModel();
                MMCApp.Domain.Models.IntakeModel.RFAReferralFile _rfaReferralFile = new MMCApp.Domain.Models.IntakeModel.RFAReferralFile();
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_patClaimID, GlobalConst.ConstantChar.Char_C));
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                _storageModel.FolderName = GlobalConst.FolderName.Invoices;

                RFAReferralInvoice _InvoiceModel = new RFAReferralInvoice();
                _InvoiceModel.ClientID = _patClientID;
                _InvoiceModel.PatientClaimID = _patClaimID;
                _InvoiceModel.UserID = MMCUser.UserId;
                _InvoiceModel.CreatedDate = System.DateTime.Now;
                _InvoiceModel.RFAReferralInvoiceID = 0;

                string _invoiceNum = _iBillingService.addRFAReferralInvoice(Mapper.Map<MMCService.BillingService.RFAReferralInvoice>(_InvoiceModel));
                string Filename = _invoiceNum + GlobalConst.ReportName.Invoice + GlobalConst.Extension.pdf;
                string savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;
                string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptInvoice], referralIDList, _patClaimID, _invoiceNum, GlobalConst.Extension.PDF);

                if (System.IO.File.Exists(savePath))
                {
                    System.IO.File.Delete(savePath);
                }
                using (WebClient client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.DownloadFile(reportURL, savePath);
                    client.Dispose();
                    referralIDList = "";
                }

                _InvoiceModel.InvoiceNumber = _invoiceNum;
                _InvoiceModelList.Add(_InvoiceModel);

                int _invoiceID = 0;
                _invoiceID = _iBillingService.getInvoiceIDByInvoiceNumber(_invoiceNum);
                foreach (var _refID in _referralIDList)
                {
                    objAdditionalLinkInvoice.RFAReferralID = _refID.RFAReferralID;
                    objAdditionalLinkInvoice.RFAReferralInvoiceID = _invoiceID;
                    _intakeService.AddUpdateRFAReferralAdditionalLinkInvoiceIDbyRefID(Mapper.Map<MMCService.IntakeService.RFAReferralAdditionalLink>(objAdditionalLinkInvoice));
                    if (CreateStatement == false)
                    {
                        _iPreparationService.updateProcessLevel(_refID.RFAReferralID, GlobalConst.ProcessLevel.FileStorage);

                    }
                
                }            
            }         

            //..........Statement
            if (CreateStatement == true)
            {
                ClientStatement _ClientStatement = new ClientStatement();
                _ClientStatement.ClientID = BillingDetails.FirstOrDefault().PatClientID;               
                _ClientStatement.UserID = MMCUser.UserId;
                _ClientStatement.CreationDate = DateTime.Now;
                int _StatementId = _iBillingService.addClientStatement(Mapper.Map<MMCService.BillingService.ClientStatement>(_ClientStatement));
                string StmntNum = _iBillingService.getStatementNumberByStatementID(_StatementId);
                //Add statementID to 
                foreach (var _patClaimID in _listPatientClaimID) // For adding referralID in List and generating the InvoiceTemplate according to InvoiceID
                {
                    var _referralIDListStatement = BillingDetails.Where(claim => claim.IsChecked && claim.PatientClaimID == _patClaimID).Select(claim => new { claim.RFAReferralID, claim.PatClientID }).Distinct();
                    foreach (var _refID in _referralIDListStatement)
                    {
                        objAdditionalLinkInvoice.RFAReferralID = _refID.RFAReferralID;
                        objAdditionalLinkInvoice.ClientStatementID = _StatementId;
                        _intakeService.AddUpdateRFAReferralAdditionalLinkStatementIDbyRefID(Mapper.Map<MMCService.IntakeService.RFAReferralAdditionalLink>(objAdditionalLinkInvoice));
                        _iPreparationService.updateProcessLevel(_refID.RFAReferralID, GlobalConst.ProcessLevel.FileStorage);
                    }

                }

                string StateMentFilename = StmntNum +"_"+ GlobalConst.FileName.InvoiceStatement + GlobalConst.Extension.pdf;
                string StatementsavePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()); //+GlobalConst.ConstantChar.DoubleBackSlash + Filename;
                StatementsavePath += GlobalConst.ConstantChar.DoubleBackSlash + _ClientStatement.ClientID + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.InvoiceStatement;
             
                if (!System.IO.Directory.Exists(StatementsavePath))
                {
                    System.IO.Directory.CreateDirectory(StatementsavePath);
                }
                StatementsavePath += GlobalConst.ConstantChar.DoubleBackSlash + StateMentFilename;
                string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptClientInvoiceStatement], _StatementId, _ClientStatement.ClientID, GlobalConst.Extension.PDF);

                if (System.IO.File.Exists(StatementsavePath))
                {
                    System.IO.File.Delete(StatementsavePath);
                }
                using (WebClient client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.DownloadFile(reportURL, StatementsavePath);
                    client.Dispose();
                }
            }                
         //   Tuple<string, string> t = DownloadPrintDocumnent(_InvoiceModelList);
         //   return File(t.Item1, GlobalConst.FileDownloadExtension.Application_Pdf, t.Item2);
            return Json(GlobalConst.Message.InvoiceCreatedSuccessfully);
           
        }

        public Tuple<string, string> DownloadPrintDocumnent(List<RFAReferralInvoice> modelInvoice)
        {
            var myList = new List<string>();
            StorageModel _storageModel = new StorageModel();
            List<string> filesPath = new List<string>();
            string savePath;
            foreach (RFAReferralInvoice _RFAReferralInvoice in modelInvoice)
            {
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_RFAReferralInvoice.PatientClaimID, GlobalConst.ConstantChar.Char_C));
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                _storageModel.FolderName = GlobalConst.FolderName.Invoices;
                savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + _RFAReferralInvoice.InvoiceNumber + GlobalConst.ReportName.Invoice + GlobalConst.Extension.PDF;
                filesPath.Add(savePath);
            }
            _storageModel.FolderName = GlobalConst.FolderName.InvoiceMergePDF;
            string FileName =  GlobalConst.ReportName.InvoiceMergePDF;
            string sPath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + FileName;
            if (System.IO.File.Exists(sPath))
            {
                System.IO.File.Delete(sPath);
            }
            _storageService.MergePdf(sPath, filesPath.ToArray());
            Tuple<string, string> savePathWithFileName = new Tuple<string, string>(sPath, FileName);
            return savePathWithFileName;
        }

        [AuthorizedUserCheck("AllowManagement")]
        public ActionResult AccountReceivables()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }

        [HttpPost]
        public ActionResult GetBillingAccountReceivablesByClientNameResult(string ClientName, int _skip)
        {
            BillingAccountReceivablesViewModel _billingAccountReceivablesDetails = new BillingAccountReceivablesViewModel();
            StorageModel _storageModel = new StorageModel();
            var _BillingAccountReceivablesDetails = _iBillingService.getAccountReceivablesByClientName(ClientName, _skip, GlobalConst.Records.LandingTake);
            _billingAccountReceivablesDetails.AccountReceivablesDetails = Mapper.Map<IEnumerable<BillingAccountReceivables>>(_BillingAccountReceivablesDetails.AccountReceivableDetails);
            foreach (var accountReceivables in _billingAccountReceivablesDetails.AccountReceivablesDetails)
            {
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(accountReceivables.PatientClaimID, GlobalConst.ConstantChar.Char_C));
                _storageModel.path = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
                _storageModel.FolderName = GlobalConst.FolderName.Invoices;
                accountReceivables.FullPathInvoiceNumber = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + accountReceivables.InvoiceFileName;

                if (accountReceivables.ClientStatementID != null)
                {
                    string StatementsavePath = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
                    StatementsavePath += GlobalConst.ConstantChar.DoubleBackSlash + accountReceivables.ClientID + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.InvoiceStatement;
                    StatementsavePath += GlobalConst.ConstantChar.DoubleBackSlash + accountReceivables.ClientStatementFileName;
                    accountReceivables.FullPathClientStatementNumber = StatementsavePath;
                }
            }
            _billingAccountReceivablesDetails.TotalCount = _BillingAccountReceivablesDetails.TotalCount;
            return Json(_billingAccountReceivablesDetails, GlobalConst.ContentTypes.TextHtml);
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