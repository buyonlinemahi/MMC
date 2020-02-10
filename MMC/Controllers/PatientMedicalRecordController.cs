using AutoMapper;
using MMCApp.Domain.Models.IntakeModel;
using MMCApp.Domain.Models.PatientModel;
using MMCApp.Domain.Models.StorageModel;
using MMCApp.Domain.ViewModels.IntakeViewModel;
using MMCApp.Domain.ViewModels.PatientViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.ApplicationServices;
using MMCApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppModel = MMCApp.Domain.Models;
using serviceModel = MMC.MMCService;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class PatientMedicalRecordController : Controller
    {
        MMCService.CommonService.CommonServiceClient _iCommonService;
        MMCService.PatientService.PatientServiceClient _iPatientService;
        MMCService.IntakeService.IntakeServiceClient _intakeService;
        public readonly PDFSplitterService _pdfSplitterService;
        public readonly StorageService _storageService;

        // GET: PatientMedicalRecord

        public PatientMedicalRecordController()
        {
            _iPatientService = new MMCService.PatientService.PatientServiceClient();
            _pdfSplitterService = new PDFSplitterService();
            _storageService = new StorageService();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
            _intakeService = new MMCService.IntakeService.IntakeServiceClient();
        }
        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }
        public ActionResult GetPatientMedicalRecordByPatientID(int _patientID, int _skip, string _sortBy, string _order)
        {
            var _patientMedicalRecordByPatientID = _iPatientService.getPatientMedicalRecordByPatientID(_patientID, _skip, GlobalConst.Records.LandingTake, _sortBy, _order);
            PatientMedicalRecordViewModel objPatientMedicalRecordViewModel = new PatientMedicalRecordViewModel();
            objPatientMedicalRecordViewModel.PatientMedicalRecordByPatientIDDetails = Mapper.Map<IEnumerable<PatientMedicalRecordByPatientID>>(_patientMedicalRecordByPatientID.PatientMedicalRecordByPatientIDDetails);
            objPatientMedicalRecordViewModel.TotalCount = _patientMedicalRecordByPatientID.TotalCount;

            objPatientMedicalRecordViewModel.PatientMedicalRecordByPatientIDDetails.ToList().ForEach(
            hp =>
            {
                hp.DocumentURL = createURLforViewFile(hp.PatientMedicalRecordID);
            });


            return Json(objPatientMedicalRecordViewModel, GlobalConst.ContentTypes.TextHtml);
        }

        [NonAction]
        private string createURLforViewFile(int patientMedicalRecordID)
        {
            return Url.Action(GlobalConst.Actions.PatientMedicalRecordController.updatePatientMedicalRecord, GlobalConst.Controllers.PatientMedicalRecord,
                         new { Id = patientMedicalRecordID });
        }

        public FileResult GeneratePatientMedRecordTemplateByPatientID(int _patientID, int _patientClaimID)
        {
            var _PatientMedicalRecordModels = _iPatientService.getPatientMedicalRecordTemplateByPatientID(_patientID);
            var _PatientMedicalRecordMultipleModels = _iPatientService.GetPatientMedicalRecordMultipleTemplateByPatientID(_patientID).ToList();
            string savePath = "";
            string Filename = "";
            if (_PatientMedicalRecordModels != null)
            {

                Filename = GlobalConst.VirtualPathFolders.MedicalRecordPrintTemplate + System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_").Trim() + GlobalConst.Extension.pdf;
                StorageModel _storageModel = new StorageModel();
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_patientClaimID, 'C'));
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;
                savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + Filename;

                WebClient client = new WebClient();
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                string reportURL = string.Format(ConfigurationManager.AppSettings[GlobalConst.SSRSReportName.RptPatientMediclRecord], _patientID, GlobalConst.Extension.PDF);
                client.DownloadFile(reportURL, savePath);

                #region PATIENT MEDICAL RECORD XML FORMAT
                /*
                System.IO.File.Copy(Server.MapPath(GlobalConst.MMCTemplate.MedicalRecordPrintTemplate), savePath, true);
                System.IO.File.SetAttributes(savePath, FileAttributes.Normal);
                FileStream fs2 = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamReader d = new StreamReader(fs2);
                string swrtarget;
                string str;
                d.BaseStream.Seek(0, SeekOrigin.Begin);
                swrtarget = d.ReadToEnd();
                d.Close();
                FileStream fs1 = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter s = new StreamWriter(fs1);
                string strstring = @"<w:tbl>
                        <w:tblPr>
                        <w:tblStyle w:val='TableGrid'/>
                        <w:tblW w:w='11340' w:type='dxa'/>
                        <w:tblInd w:w='-905' w:type='dxa'/>
                        <w:tblLook w:val='04A0' w:firstRow='1' w:lastRow='0' w:firstColumn='1' w:lastColumn='0' w:noHBand='0' w:noVBand='1'/>
                        </w:tblPr>
                        <w:tblGrid>
                        <w:gridCol w:w='2155'/>
                        <w:gridCol w:w='9185'/>
                        </w:tblGrid>
                        <w:tr w:rsidR='003A361F' w:rsidTr='007958F8'>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='2155' w:type='dxa'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='003A361F' w:rsidP='003A361F'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr='007D204C'>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t>Patient</w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='9185' w:type='dxa'/> 
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='004114E6' w:rsidP='004114E6'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t><![CDATA[" + _PatientMedicalRecordModels.PatientName.ToString().Trim() + @"]]></w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        </w:tr>
                        <w:tr w:rsidR='003A361F' w:rsidTr='007958F8'>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='2155' w:type='dxa'/>
                        <w:shd w:val='clear' w:color='auto' w:fill='F2F2F2' w:themeFill='background1' w:themeFillShade='F2'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='003A361F'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr='007D204C'>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t>Claim</w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='9185' w:type='dxa'/>
                        <w:shd w:val='clear' w:color='auto' w:fill='F2F2F2' w:themeFill='background1' w:themeFillShade='F2'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='004114E6' w:rsidP='004114E6'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t><![CDATA[" + _PatientMedicalRecordModels.Claims.ToString().Trim() + @"]]></w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        </w:tr>
                        <w:tr w:rsidR='003A361F' w:rsidTr='007958F8'>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='2155' w:type='dxa'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='003A361F'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr='007D204C'>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t>Diagnosis</w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='9185' w:type='dxa'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='004114E6' w:rsidP='004114E6'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t><![CDATA[" + _PatientMedicalRecordModels.Diagnosis.ToString().Trim() + @"]]></w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        </w:tr>
                        <w:tr w:rsidR='003A361F' w:rsidTr='007958F8'>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='2155' w:type='dxa'/>
                        <w:shd w:val='clear' w:color='auto' w:fill='F2F2F2' w:themeFill='background1' w:themeFillShade='F2'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='003A361F'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr='007D204C'>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t>Accepted Body Parts</w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='9185' w:type='dxa'/>
                        <w:shd w:val='clear' w:color='auto' w:fill='F2F2F2' w:themeFill='background1' w:themeFillShade='F2'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='004114E6' w:rsidP='004114E6'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t><![CDATA[" + _PatientMedicalRecordModels.AcceptedBodyParts.ToString().Trim() + @"]]></w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        </w:tr>
                        <w:tr w:rsidR='003A361F' w:rsidTr='007958F8'>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='2155' w:type='dxa'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='003A361F'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr='007D204C'>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t>Denied Body Parts</w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='9185' w:type='dxa'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='004114E6' w:rsidP='004114E6'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t><![CDATA[" + _PatientMedicalRecordModels.DeniedBodyParts.ToString().Trim() + @"]]></w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        </w:tr>
                        <w:tr w:rsidR='003A361F' w:rsidTr='007958F8'>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='2155' w:type='dxa'/>
                        <w:shd w:val='clear' w:color='auto' w:fill='F2F2F2' w:themeFill='background1' w:themeFillShade='F2'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='003A361F'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr='007D204C'>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t>Discovery Body Parts</w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        <w:tc>
                        <w:tcPr>
                        <w:tcW w:w='9185' w:type='dxa'/>
                        <w:shd w:val='clear' w:color='auto' w:fill='F2F2F2' w:themeFill='background1' w:themeFillShade='F2'/>
                        </w:tcPr>
                        <w:p w:rsidR='003A361F' w:rsidRPr='007D204C' w:rsidRDefault='004114E6' w:rsidP='004114E6'>
                        <w:pPr>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        </w:pPr>
                        <w:r>
                        <w:rPr>
                        <w:sz w:val='20'/>
                        <w:szCs w:val='20'/>
                        </w:rPr>
                        <w:t><![CDATA[" + _PatientMedicalRecordModels.DiscoveryBodyParts.ToString().Trim() + @"]]></w:t>
                        </w:r>
                        </w:p>
                        </w:tc>
                        </w:tr>
                        </w:tbl>";
                str = swrtarget.Replace("#PatientMedRect#", strstring.ToString().Trim());
                string str1 = "<w:tbl>";
                for (int i = 0; i < _PatientMedicalRecordMultipleModels.Count; i++)
                {
                    #region
                    str1 += @"
                                <w:tblPr>
                                <w:tblStyle w:val='TableGrid'/>
                                <w:tblW w:w='11340' w:type='dxa'/>
                                <w:tblInd w:w='-905' w:type='dxa'/>
                                <w:tblLook w:val='04A0' w:firstRow='1' w:lastRow='0' w:firstColumn='1' w:lastColumn='0' w:noHBand='0' w:noVBand='1'/>
                                </w:tblPr>
                                <w:tblGrid>
                                <w:gridCol w:w='1611'/>
                                <w:gridCol w:w='1845'/>
                                <w:gridCol w:w='7884'/>
                                </w:tblGrid>
                                <w:tr w:rsidR='007D204C' w:rsidRPr='007958F8' w:rsidTr='00502C2C'>
                                <w:tc>
                                <w:tcPr>
                                <w:tcW w:w='1611' w:type='dxa'/>
                                <w:shd w:val='clear' w:color='auto' w:fill='7030A0'/>
                                </w:tcPr>
                                <w:p w:rsidR='004114E6' w:rsidRPr='007958F8' w:rsidRDefault='007D204C' w:rsidP='00177542'>
                                <w:pPr>
                                <w:rPr>
                                <w:b/>
                                <w:color w:val='BFBFBF' w:themeColor='background1' w:themeShade='BF'/>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                </w:pPr>
                                <w:r w:rsidRPr='007958F8'>
                                <w:rPr>
                                <w:b/>
                                <w:color w:val='BFBFBF' w:themeColor='background1' w:themeShade='BF'/>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                <w:t>Name</w:t>
                                </w:r>
                                </w:p>
                                </w:tc>
                                <w:tc>
                                <w:tcPr>
                                <w:tcW w:w='1845' w:type='dxa'/>
                                <w:shd w:val='clear' w:color='auto' w:fill='7030A0'/>
                                </w:tcPr>
                                <w:p w:rsidR='007D204C' w:rsidRPr='007958F8' w:rsidRDefault='007D204C' w:rsidP='00177542'>
                                <w:pPr>
                                <w:rPr>
                                <w:b/>
                                <w:color w:val='BFBFBF' w:themeColor='background1' w:themeShade='BF'/>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                </w:pPr>
                                <w:r w:rsidRPr='007958F8'>
                                <w:rPr>
                                <w:b/>
                                <w:color w:val='BFBFBF' w:themeColor='background1' w:themeShade='BF'/>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                <w:t>Date</w:t>
                                </w:r>
                                </w:p>
                                </w:tc>
                                <w:tc>
                                <w:tcPr>
                                <w:tcW w:w='7884' w:type='dxa'/>
                                <w:shd w:val='clear' w:color='auto' w:fill='7030A0'/>
                                </w:tcPr>
                                <w:p w:rsidR='007D204C' w:rsidRPr='007958F8' w:rsidRDefault='007D204C' w:rsidP='00177542'>
                                <w:pPr>
                                <w:rPr>
                                <w:b/>
                                <w:color w:val='BFBFBF' w:themeColor='background1' w:themeShade='BF'/>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                </w:pPr>
                                <w:r w:rsidRPr='007958F8'>
                                <w:rPr>
                                <w:b/>
                                <w:color w:val='BFBFBF' w:themeColor='background1' w:themeShade='BF'/>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                <w:t>Physician</w:t>
                                </w:r>
                                </w:p>
                                </w:tc>
                                </w:tr>
                                <w:tr w:rsidR='007D204C' w:rsidRPr='007958F8' w:rsidTr='007958F8'>
                                <w:tc>
                                <w:tcPr>
                                <w:tcW w:w='1611' w:type='dxa'/>
                                </w:tcPr>
                                <w:p w:rsidR='007D204C' w:rsidRPr='00F601AA' w:rsidRDefault='00DD6770' w:rsidP='00177542'>
                                <w:pPr>
                                <w:rPr>
                                <w:color w:val='000000' w:themeColor='text1'/>
                                <w:sz w:val='18'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                </w:pPr>
                                <w:r w:rsidRPr='00F601AA'>
                                <w:rPr>
                                <w:color w:val='000000' w:themeColor='text1'/>
                                <w:sz w:val='18'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                    <w:t><![CDATA[" + _PatientMedicalRecordMultipleModels[i].PatMRDocumentName.ToString().Replace(".pdf", "").Trim() + @" ]]></w:t>
                                </w:r>
                                </w:p>
                                </w:tc>
                                <w:tc>
                                <w:tcPr>
                                <w:tcW w:w='1845' w:type='dxa'/>
                                </w:tcPr>
                                <w:p w:rsidR='007D204C' w:rsidRPr='00F601AA' w:rsidRDefault='00DD6770' w:rsidP='00177542'>
                                <w:pPr>
                                <w:rPr>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                </w:pPr>
                                <w:r w:rsidRPr='00F601AA'>
                                <w:rPr>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                    <w:t><![CDATA[" + String.Format("{0:MM/dd/yyyy}", _PatientMedicalRecordMultipleModels[i].PatMRDocumentDate) + @"]]></w:t>
                                </w:r>
                                </w:p>
                                </w:tc>
                                <w:tc>
                                <w:tcPr>
                                <w:tcW w:w='7884' w:type='dxa'/>
                                </w:tcPr>
                                <w:p w:rsidR='007D204C' w:rsidRPr='00F601AA' w:rsidRDefault='00DD6770' w:rsidP='00177542'>
                                <w:pPr>
                                <w:rPr>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                </w:pPr>
                                <w:r w:rsidRPr='00F601AA'>
                                <w:rPr>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                    <w:t><![CDATA[" + _PatientMedicalRecordMultipleModels[i].PhysicianName.ToString().Trim() + @"]]></w:t>
                                </w:r>
                                </w:p>
                                </w:tc>
                                </w:tr>
                                <w:tr w:rsidR='007D204C' w:rsidRPr='007958F8' w:rsidTr='007958F8'>
                                <w:tc>
                                <w:tcPr>
                                <w:tcW w:w='1611' w:type='dxa'/>
                                </w:tcPr>
                                <w:p w:rsidR='007D204C' w:rsidRPr='00F601AA' w:rsidRDefault='00DD6770' w:rsidP='00177542'>
                                <w:pPr>
                                <w:rPr>
                                <w:sz w:val='18'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                </w:pPr>
                                <w:r w:rsidRPr='00F601AA'>
                                <w:rPr>
                                <w:sz w:val='18'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                <w:t>Diagnosis</w:t>
                                </w:r>
                                </w:p>
                                </w:tc>
                                <w:tc>
                                <w:tcPr>
                                <w:tcW w:w='9729' w:type='dxa'/>
                                <w:gridSpan w:val='2'/>
                                </w:tcPr>
                                <w:p w:rsidR='007D204C' w:rsidRPr='00F601AA' w:rsidRDefault='00DD6770' w:rsidP='00177542'>
                                <w:pPr>
                                <w:rPr>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                </w:pPr>
                                <w:r w:rsidRPr='00F601AA'>
                                <w:rPr>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                    <w:t><![CDATA[" + _PatientMedicalRecordMultipleModels[i].Diagnosis.ToString().Trim() + @"]]></w:t>
                                </w:r>
                                </w:p>
                                </w:tc>
                                </w:tr>
                                <w:tr w:rsidR='007D204C' w:rsidRPr='007958F8' w:rsidTr='007958F8'>
                                <w:tc>
                                <w:tcPr>
                                <w:tcW w:w='11340' w:type='dxa'/>
                                <w:gridSpan w:val='3'/>
                                </w:tcPr>
                                <w:p w:rsidR='007D204C' w:rsidRPr='00F601AA' w:rsidRDefault='00DD6770' w:rsidP='00177542'>
                                <w:pPr>
                                <w:rPr>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                </w:pPr>
                                <w:r w:rsidRPr='00F601AA'>
                                <w:rPr>
                                <w:sz w:val='20'/>
                                <w:szCs w:val='20'/>
                                </w:rPr>
                                    <w:t><![CDATA[" + _PatientMedicalRecordMultipleModels[i].Summary.ToString().Trim() + @"]]></w:t>
                                </w:r>
                                </w:p>
                                </w:tc>
                                </w:tr>
                                ";
                }
                    #endregion
                str1 += " </w:tbl>";
                str = str.Replace("#RecordDesc#", str1.ToString().Trim());
                s.WriteLine(str);
                s.Flush();
                s.Close();
                fs1.Close();
                fs2.Close();
                */
                #endregion
            }
            return File(savePath, GlobalConst.FileDownloadExtension.Application_Pdf, Filename);
        }

        [HttpPost]
        public FileResult PrintURHistoryDocumnent(IEnumerable<RequestHistory> RequestHistory)
        {
            Tuple<string, string> t = URHistoryDetailByRFARequestID(RequestHistory);
            return File(t.Item1, GlobalConst.FileDownloadExtension.Application_Pdf, t.Item2);
        }

        public Tuple<string, string> URHistoryDetailByRFARequestID(IEnumerable<RequestHistory> _RequestHistory)
        {
            var myList = new List<string>();
            StorageModel _storageModel = new StorageModel();
            List<string> filesPath = new List<string>();
            string savePath;
            foreach (RequestHistory __rfaReferralFiledetail in _RequestHistory)
            {
                if ((__rfaReferralFiledetail.IsChecked) || (__rfaReferralFiledetail.IsCheckedAll))
                {

                    if (__rfaReferralFiledetail.filename.Contains(GlobalConst.FolderName.IntakeUpload))
                    {
                        _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                        _storageModel.FolderName = GlobalConst.FolderName.IntakeUploadFiles;
                        savePath = Path.Combine(_storageModel.path, _storageModel.FolderName) + GlobalConst.ConstantChar.DoubleBackSlash + __rfaReferralFiledetail.filename;
                        
                    }
                    else
                    {
                        _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(__rfaReferralFiledetail.RFAReferralID, GlobalConst.ConstantChar.Char_R));
                        _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                        _storageModel.FolderName = GlobalConst.FolderName.LegalDocs;
                        savePath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + __rfaReferralFiledetail.filename;
                    }
                    filesPath.Add(savePath);
                }
            }
            _storageModel.FolderName = GlobalConst.FolderName.MergePDF;
            string FileName = _storageModel.ReferralID + GlobalConst.ReportName.MergePatientURHistoryDocumnentpdf;
            string sPath = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + FileName;
            if (System.IO.File.Exists(sPath))
            {
                System.IO.File.Delete(sPath);
            }
            _storageService.MergePdf(sPath, filesPath.ToArray());
            Tuple<string, string> savePathWithFileName = new Tuple<string, string>(sPath, FileName);
            return savePathWithFileName;
        }


        [HttpGet]
        public JsonResult getRFARequestRecordsByPatientClaim(string _patiantClaim)
        {
            return Json((_iPatientService.getRFARequestRecordsByPatientClaim(_patiantClaim)), GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }

        #region Patient(s) Medical Upload File Splitting........MS
        [HttpPost]
        public ActionResult UploadPatientMedicalRecord(RFAReferralFile _rfaReferralFile)
        {
            string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString() + GlobalConst.VirtualPathFolders.Patients);
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(_rfaReferralFile.rfaReferralFile.FileName);
            _rfaReferralFile.rfaReferralFile.SaveAs(path + "\\" + filename);
            _rfaReferralFile.RFAReferralFileName = filename;
            _rfaReferralFile.rfaReferralFile = null;
            _rfaReferralFile.RFAReferralFileFullPath = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString() + GlobalConst.VirtualPathFolders.Patients + "\\" + filename;
            _rfaReferralFile.TotalPages = _pdfSplitterService.getTotalPageofFilePatientMedicalRecord(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath]), filename);
            return Json(_rfaReferralFile, GlobalConst.ContentTypes.TextHtml);
        }


        [HttpPost]
        public ActionResult SavePatientMedicalRecordSplitting(RFARecordSplittingViewModel _rfaRecSplit)
        {
            var _result = 0;
            try
            {
                var lstpatMedicalSplitDetails = _rfaRecSplit.rfaRecordSplitingDetails.ToList();

                //create path...
                StorageModel _storageModel = new StorageModel();
                _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(lstpatMedicalSplitDetails.FirstOrDefault().PatientClaimID, 'C'));
                _storageModel.path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString());
                _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;

                string saveToPath = _storageService.GeneateStorage(_storageModel);
                //end path..

                foreach (var MedicalSplitDetail in lstpatMedicalSplitDetails)
                {
                    MedicalSplitDetail.RFAFileName = MedicalSplitDetail.RFARecDocumentName + System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_").Replace(" ", "_").Trim();
                    MedicalSplitDetail.RFAFileName = _pdfSplitterService.splitPDFPatientMedicalRecord(MedicalSplitDetail.RFARecPageStart, MedicalSplitDetail.RFARecPageEnd, Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath]), saveToPath, MedicalSplitDetail.RFAReferralFileName, MedicalSplitDetail.RFAFileName);
                    MedicalSplitDetail.RFAReferralID = null;
                    MedicalSplitDetail.RFARecSpltID = _intakeService.addRFARecordSplitting(Mapper.Map<serviceModel.IntakeService.RFARecordSplitting>(MedicalSplitDetail));

                }
                _result = 1;
            }
            catch
            {
                _result = 0;
            }
            return Json(_result, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public ActionResult getPatientMedicalRecordSplit(int _patientID)
        {
            PatientMedicalRecordSplitViewModel _patMedicalRecordSplit = new PatientMedicalRecordSplitViewModel();
            _patMedicalRecordSplit.documentCategories = Mapper.Map<IEnumerable<AppModel.DocumentCategoryModel.DocumentCategory>>(_iCommonService.getDocumentCategoriesAll());
            _patMedicalRecordSplit.documentTypes = Mapper.Map<IEnumerable<AppModel.DocumentTypeModel.DocumentType>>(_iCommonService.getDocumentTypesAll());
            _patMedicalRecordSplit.patientMedicalRecordSplitingDetails = Mapper.Map<IEnumerable<PatientMedicalRecord>>(_iPatientService.getMedicalRecordSplittingByPatientID(_patientID));

            _patMedicalRecordSplit.patientMedicalRecordSplitingDetails.ToList().ForEach(
             mr =>
             {
                 mr.DocumentCategoryName = _patMedicalRecordSplit.documentCategories.ToList().Find(mr1 => mr1.DocumentCategoryID == mr.DocumentCategoryID).DocumentCategoryName;
                 mr.DocumentTypeDesc = _patMedicalRecordSplit.documentTypes.ToList().Find(mr1 => mr1.DocumentTypeID == mr.DocumentTypeID).DocumentTypeDesc;
             });

            return Json(_patMedicalRecordSplit, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpGet]
        public ActionResult updatePatientMedicalRecord(int id)
        {
            RFARecordSplittingViewModel _rfaRecordSplit = new RFARecordSplittingViewModel();
            //_rfaRecordSplit.documentTypes = Mapper.Map<IEnumerable<AppModel.DocumentTypeModel.DocumentType>>(_iCommonService.getDocumentTypesAll());
            _rfaRecordSplit.rfaRecordSpliting = Mapper.Map<RFARecordSpliting>(_intakeService.getRFARecordSplittingByID(id));
            _rfaRecordSplit.documentTypes = Mapper.Map<IEnumerable<AppModel.DocumentTypeModel.DocumentType>>(_iCommonService.getDocumentTypeByDocumentCategoryID(_rfaRecordSplit.rfaRecordSpliting.DocumentCategoryID));
            _rfaRecordSplit.rfaRecordSpliting.PatientID = _iPatientService.getPatientClaimByID(_rfaRecordSplit.rfaRecordSpliting.PatientClaimID).PatientID;

            ///create path...
            StorageModel _storageModel = new StorageModel();
            _storageModel = Mapper.Map<StorageModel>(_iCommonService.GetStorageStuctureByID(_rfaRecordSplit.rfaRecordSpliting.PatientClaimID, 'C'));
            _storageModel.path = System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString();
            _storageModel.FolderName = GlobalConst.FolderName.MedicalRecords;

            //end path..

            _rfaRecordSplit.rfaRecordSpliting.DocumentUrl = _storageService.GeneateStorage(_storageModel) + GlobalConst.ConstantChar.DoubleBackSlash + _rfaRecordSplit.rfaRecordSpliting.RFAFileName;

            List<string> diag = (from s in _iPatientService.getPatientClaimDiagnoseByPatientClaimIdAll(_rfaRecordSplit.rfaRecordSpliting.PatientClaimID).OrderByDescending(hp => hp.PatientClaimDiagnosisID).ToList()
                                 select s.icdICDNumber).ToList();

            diag.ForEach(hp => _rfaRecordSplit.diagnosisAll += hp + ",");

            if (_rfaRecordSplit.diagnosisAll != null)
                _rfaRecordSplit.diagnosisAll = _rfaRecordSplit.diagnosisAll.Substring(0, _rfaRecordSplit.diagnosisAll.Length - 1);


            return View(_rfaRecordSplit);
        }

        [HttpPost]
        public ActionResult updatePatientMedicalRecord(RFARecordSpliting rfaRecordSpliting)
        {
            _intakeService.udateRFARecordSplittingRecordForMedicalRec(Mapper.Map<serviceModel.IntakeService.RFARecordSplitting>(rfaRecordSpliting));
            rfaRecordSpliting.oldRFARecDocumentName = rfaRecordSpliting.RFARecDocumentName;
            return Json(rfaRecordSpliting, GlobalConst.ContentTypes.TextHtml);
        }

        [HttpPost]
        public JsonResult GetPatientClaimsByPatientIDOnDrop(int _patientID)
        {
            var _patientDetails = _iPatientService.getpatientClaimsByPatientID(_patientID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
            PatientClaimDropDownList _objPatient = new PatientClaimDropDownList();
            _objPatient.PatientClaimDropLists = Mapper.Map<IEnumerable<PatientClaimDropDown>>(_patientDetails.PatientClaimDetails);
            if (_patientDetails.TotalCount != 0)
                return Json(_objPatient.PatientClaimDropLists);
            else
                return Json(null);
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