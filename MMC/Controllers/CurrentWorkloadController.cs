using AutoMapper;
using MMCApp.Domain.Models.CurrentWorkloadModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.ApplicationServices;
using MMCApp.Infrastructure.Base;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Globalization;
using System.Web;




namespace MMC.Controllers
{
    public class CurrentWorkloadController : Controller
    {

        MMCService.CurrentWorkloadService.CurrentWorkloadServiceClient _iCurrentWorkloadService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        StorageService _storageService;

        public CurrentWorkloadController()
        {
            _iCurrentWorkloadService = new MMCService.CurrentWorkloadService.CurrentWorkloadServiceClient();
            _storageService = new StorageService();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
        }

        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }

        [HttpPost]
        public JsonResult GetResultReport(string startDate, string endDate, string ClientId, string StatusId)
        {
            try
            {

                //adding currentworkload report name to database
                CurrentWorkloadReport _CurrentWorkloadReport = new CurrentWorkloadReport();
                _CurrentWorkloadReport.CurrentWorkloadReportName = GlobalConst.SSRSReportName.RptCurrentWorkload + DateTime.Now.ToString("ddMMyyyyss") + GlobalConst.Extension.xlsx;

                int _currentworkReportID = _iCurrentWorkloadService.AddCurrentWorkloadReport(Mapper.Map<MMC.MMCService.CurrentWorkloadService.CurrentWorkloadReport>(_CurrentWorkloadReport));

                List<CurrentWorkloadReportParameter> _CurrentWorkloadReportParameterList = new List<CurrentWorkloadReportParameter>();
                int[] clientIDArray = new int[0];
                int[] statusIDArray = new int[0];


                if (ClientId != "")
                    clientIDArray = Array.ConvertAll(ClientId.Split(','), int.Parse);
                if (StatusId != "")
                    statusIDArray = Array.ConvertAll(StatusId.Split(','), int.Parse);

                int loopCheck = (clientIDArray.Length > statusIDArray.Length) ? clientIDArray.Length : statusIDArray.Length;

                for (int i = 0; i < loopCheck; i++)
                {
                    CurrentWorkloadReportParameter currentworkloadReportParameter = new CurrentWorkloadReportParameter();
                    if (i < clientIDArray.Length)
                    {
                        currentworkloadReportParameter.ClientID = clientIDArray[i];
                    }
                    if (i < statusIDArray.Length)
                    {
                        currentworkloadReportParameter.StatusID = statusIDArray[i];
                    }

                    currentworkloadReportParameter.CurrentWorkloadReportID = _currentworkReportID;

                    _CurrentWorkloadReportParameterList.Add(currentworkloadReportParameter);
                }

                //adding Currentworkload report parameters


                _iCurrentWorkloadService.AddRptCurrentWorkloadReportParameters(Mapper.Map<List<MMC.MMCService.CurrentWorkloadService.CurrentWorkloadReportParameter>>(_CurrentWorkloadReportParameterList));

                string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptCurrentWorkload], _currentworkReportID, startDate, endDate, GlobalConst.Extension.EXCEL);

                string storagePath = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                string savePath = _storageService.getCurrentWokloadReportStorage(storagePath, GlobalConst.FolderName.CurrentWorkloadReport) + GlobalConst.ConstantChar.DoubleForwardSlash + _CurrentWorkloadReport.CurrentWorkloadReportName;

                using (WebClient client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.DownloadFile(reportURL, savePath);
                    client.Dispose();
                }

                string ReportLink = GlobalConst.ConstantChar.DoubleForwardSlash + GlobalConst.ConstantChar.Storage + GlobalConst.ConstantChar.DoubleForwardSlash
                    + GlobalConst.FolderName.CurrentWorkloadReport + GlobalConst.ConstantChar.DoubleBackwardSlash + _CurrentWorkloadReport.CurrentWorkloadReportName;

                return Json(ReportLink);
            }
            catch (Exception e)
            {
                return Json(e.Message);
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