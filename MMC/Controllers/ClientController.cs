using AutoMapper;
using MMCApp.Domain.Models.ClientBillingModel;
using MMCApp.Domain.Models.ClientModel;
using MMCApp.Domain.ViewModels.ClientBillingViewModel;
using MMCApp.Domain.ViewModels.ClientModelViewModel;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Infrastructure.ApplicationServices;
using MMCApp.Infrastructure.Base;
using MMCApp.Infrastructure.Global;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MMC.Controllers
{
    [AuthorizedUserCheck]
    public class ClientController : BaseController
    {
        MMCService.ClientService.ClientServiceClient _iClientService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public ClientController()
        {
            _iClientService = new MMCService.ClientService.ClientServiceClient();
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
        }
         [AuthorizedUserCheck("AllowManagement")]
        public ActionResult Index()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }
        public JsonResult getAllClaimAdministrator()
        {
            return Json(_iClientService.getAllClaimAdministrator(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getClientByName(string _searchText, int? _skip)
        {
            var _clientDetails = _iClientService.getClientByName(_searchText, _skip.Value, GlobalConst.Records.LandingTake);
            ClientViewModel objClient = new ClientViewModel();
            objClient.ClientDetails = Mapper.Map<IEnumerable<Client>>(_clientDetails.ClientDetails);
            objClient.TotalCount = _clientDetails.TotalCount;
            return Json(objClient, GlobalConst.ContentTypes.TextHtml);
        }
          [AuthorizedUserCheck("AllowManagement")]
        public ActionResult SaveClientDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            PatientClientDetailViewModel objPatientClientDetailViewModel = new PatientClientDetailViewModel();
            if (id != null)
            {
                var _ClientResult = _iClientService.getClientByID(id.Value);
                objPatientClientDetailViewModel.ClientDetail = Mapper.Map<Client>(_ClientResult);

                var _getClientInsurer = _iClientService.getClientInsurerByClientID(id.Value, 0, GlobalConst.Records.Take5);
                objPatientClientDetailViewModel.ClientInsurerDetails = Mapper.Map<IEnumerable<ClientInsurer>>(_getClientInsurer.ClientInsurerDetails);
                objPatientClientDetailViewModel.cinsTotalCount = _getClientInsurer.TotalCount;

                var _getClientEmployer = _iClientService.getClientEmployerByClientID(id.Value, 0, GlobalConst.Records.Take5);
                objPatientClientDetailViewModel.ClientEmployerDetails = Mapper.Map<IEnumerable<ClientEmployer>>(_getClientEmployer.ClientEmployerDetails);
                objPatientClientDetailViewModel.cEmpTotalCount = _getClientEmployer.TotalCount;

                var _getClientManagedCareCompany = _iClientService.getClientManagedCareCompanyByClientID(id.Value, 0, GlobalConst.Records.Take5);
                objPatientClientDetailViewModel.ClientManagedCareCompanyDetails = Mapper.Map<IEnumerable<ClientManagedCareCompany>>(_getClientManagedCareCompany.ClientManagedCareCompanyDetails);
                objPatientClientDetailViewModel.cMmcTotalCount = _getClientManagedCareCompany.TotalCount;

                var _getClientThirdPartyAdministrator = _iClientService.getClientThirdPartyAdministratorByClientID(id.Value, 0, GlobalConst.Records.Take5);
                objPatientClientDetailViewModel.ClientThirdPartyAdministratorDetails = Mapper.Map<IEnumerable<ClientThirdPartyAdministrator>>(_getClientThirdPartyAdministrator.ClientThirdPartyAdministratorDetails);
                objPatientClientDetailViewModel.cTpaTotalCount = _getClientThirdPartyAdministrator.TotalCount;

                var _getClaimAdministratorAllByClientID = _iClientService.getClaimAdministratorAllByClientID(id.Value);
                objPatientClientDetailViewModel.ClaimAdministratorAllByClientIDDetails = Mapper.Map<IEnumerable<ClaimAdministratorAllByClientID>>(_getClaimAdministratorAllByClientID);


            }
            return View(objPatientClientDetailViewModel);
        }
        [HttpPost]
        public ActionResult AddClient(Client _Client)
        {
            try
            {
                int _ClientID = _iClientService.addClient(Mapper.Map<MMC.MMCService.ClientService.Client>(_Client));
                return Json(_ClientID, GlobalConst.ContentTypes.TextHtml);
            }
            catch
            {
                return Json(0);
            }
        }
        [HttpPost]
        public JsonResult AddClientInsurer(ClientInsurer _ClientInsurer)
        {
            _ClientInsurer.ClientInsurerID = _iClientService.addClientInsurer(Mapper.Map<MMC.MMCService.ClientService.ClientInsurer>(_ClientInsurer));
            return Json(_ClientInsurer, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public JsonResult AddClientInsuranceBranch(ClientInsuranceBranch _clientInsuranceBranch)
        {
            int addClientInsuranceBranchID = _iClientService.addClientInsuranceBranch(Mapper.Map<MMC.MMCService.ClientService.ClientInsuranceBranch>(_clientInsuranceBranch));
            return Json(_clientInsuranceBranch.InsurerID, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult DeleteClientInsurerByID(int _ClientInsurerID, int _insurerID, string _insType)
        {
            try
            {
                if (_insType.ToLower() == GlobalConst.TableAbbreviation.Insb)
                {
                    _iClientService.deleteClientInsuranceBranch(_ClientInsurerID);
                }
                else
                {
                    _iClientService.deleteClientInsuranceBranchByInsurerID(_insurerID);
                    _iClientService.deleteClientInsurer(_ClientInsurerID);
                }
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }
        [HttpPost]
        public JsonResult AddClientEmployer(ClientEmployer _ClientEmployer)
        {
            int addClientInsurerID = _iClientService.addClientEmployer(Mapper.Map<MMC.MMCService.ClientService.ClientEmployer>(_ClientEmployer));
            return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult DeleteClientEmployerByID(int _ClientEmployerID)
        {
            try
            {
                _iClientService.deleteClientEmployer(_ClientEmployerID);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }
        [HttpPost]
        public JsonResult UpdateClientManagedCareCompany(int _ClientID, int _ManagedCareCompanyID)
        {
            ClientManagedCareCompany _ClientManagedCareCompany = new ClientManagedCareCompany();
            _ClientManagedCareCompany.ClientID = _ClientID;
            _ClientManagedCareCompany.CompanyID = _ManagedCareCompanyID;
            _ClientManagedCareCompany.ClientCompanyID = 0;
            _ClientManagedCareCompany.ClientName = null;
            int updateClientCompanyID = _iClientService.updateClientManagedCareCompanyByClientID(Mapper.Map<MMC.MMCService.ClientService.ClientManagedCareCompany>(_ClientManagedCareCompany));
            if (_ClientID == 0)
                return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);
            else
                return Json(GlobalConst.Message.SaveMessage, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult DeleteClientManagedCareCompanyByID(int _ClientCompanyID)
        {
            try
            {
                _iClientService.deleteClientManagedCareCompany(_ClientCompanyID);
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }
        [HttpPost]
        public JsonResult AddClientThirdPartyAdministrator(ClientThirdPartyAdministrator _ClientThirdPartyAdministrator)
        {
            _ClientThirdPartyAdministrator.ClientTPAID = _iClientService.addClientThirdPartyAdministrator(Mapper.Map<MMC.MMCService.ClientService.ClientThirdPartyAdministrator>(_ClientThirdPartyAdministrator));
            return Json(_ClientThirdPartyAdministrator, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult DeleteClientThirdPartyAdministratorByID(int _ClientTPAID, int _tpaID, string _tpaType)
        {
            try
            {
                if (_tpaType.ToLower() == GlobalConst.TableAbbreviation.Tpab)
                {
                    _iClientService.deleteClientThirdPartyAdministratorBranch(_ClientTPAID);
                }
                else
                {
                    _iClientService.deleteClientThirdPartyAdministratorBranchByTPAID(_tpaID);
                    _iClientService.deleteClientThirdPartyAdministrator(_ClientTPAID);
                }
                return Json(GlobalConst.Message.DeleteMessage);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage);
            }
        }
        [HttpPost]
        public JsonResult AddClientThirdPartyAdministratorBranch(ClientThirdPartyAdministratorBranch _clientThirdPartyAdministratorBranch)
        {
            int addClientTPABranchID = _iClientService.addClientThirdPartyAdministratorBranch(Mapper.Map<MMC.MMCService.ClientService.ClientThirdPartyAdministratorBranch>(_clientThirdPartyAdministratorBranch));
            return Json(_clientThirdPartyAdministratorBranch.TPAID, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public JsonResult GetClaimAdministratorAllByClientID(int ClientID)
        {
            var _getClaimAdministratorAllByClientID = _iClientService.getClaimAdministratorAllByClientID(ClientID);
            return Json(_getClaimAdministratorAllByClientID, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public JsonResult UpdateClaimAdministratorByClientID(int _ClientID, string _ClientName, string _ids)
        {
            Client _Client = new Client();
            _Client.ClientID = _ClientID;
            _Client.ClientName = _ClientName;
            string[] arr = _ids.Split('-');
            _Client.ClaimAdministratorID = int.Parse(arr[0]);
            _Client.ClaimAdministratorType = arr[1].ToString();

            int cnt = _iClientService.updateClient(Mapper.Map<MMC.MMCService.ClientService.Client>(_Client));
            return Json(cnt, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult GetClientInsuranceBranchesAllByInsurerID(int _clientID, int _insurerID)
        {
            var _InsuranceBranchDetails = _iClientService.getClientInsuranceBranchesAllByInsurerID(_clientID, _insurerID);
            return Json(_InsuranceBranchDetails, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetClientTPABranchesAllByTPAID(int _clientID, int _tpaID)
        {
            var _TPABranchDetails = _iClientService.getClientTPABranchesAllByTPAID(_clientID, _tpaID);
            return Json(_TPABranchDetails, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetClientInsurerByClientID(int _clientID, int skip)
        {
            PatientClientDetailViewModel objPatientClientDetailViewModel = new PatientClientDetailViewModel();
            var _getClientInsurer = _iClientService.getClientInsurerByClientID(_clientID, skip, GlobalConst.Records.Take5);
            objPatientClientDetailViewModel.ClientInsurerDetails = Mapper.Map<IEnumerable<ClientInsurer>>(_getClientInsurer.ClientInsurerDetails);
            objPatientClientDetailViewModel.cinsTotalCount = _getClientInsurer.TotalCount;
            return Json(objPatientClientDetailViewModel, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpPost]
        public ActionResult GetClientThirdPartyAdministratorByClientID(int _clientID, int skip)
        {
            PatientClientDetailViewModel objPatientClientDetailViewModel = new PatientClientDetailViewModel();
            var _getClientThirdPartyAdministrator = _iClientService.getClientThirdPartyAdministratorByClientID(_clientID, skip, GlobalConst.Records.Take5);
            objPatientClientDetailViewModel.ClientThirdPartyAdministratorDetails = Mapper.Map<IEnumerable<ClientThirdPartyAdministrator>>(_getClientThirdPartyAdministrator.ClientThirdPartyAdministratorDetails);
            objPatientClientDetailViewModel.cTpaTotalCount = _getClientThirdPartyAdministrator.TotalCount;
            return Json(objPatientClientDetailViewModel, GlobalConst.ContentTypes.TextHtml);
        }
        [HttpGet]
        public JsonResult GetClientBillingDetail(int _clientID)
        {
            ClientBillingDetailViewModel _ObjClientBillingDetailViewModel = new ClientBillingDetailViewModel();
            _ObjClientBillingDetailViewModel.ClientBillingDetail = Mapper.Map<ClientBilling>(_iClientService.getClientBillingDetailByClientID(_clientID));
            if (_ObjClientBillingDetailViewModel.ClientBillingDetail != null)
            {
                _ObjClientBillingDetailViewModel.ClientBillingRetailRateDetail = Mapper.Map<ClientBillingRetailRate>(_iClientService.getClientBillingRetailRateByClientBillingID(_ObjClientBillingDetailViewModel.ClientBillingDetail.ClientBillingID));
                _ObjClientBillingDetailViewModel.ClientBillingWholesaleRateDetail = Mapper.Map<ClientBillingWholesaleRate>(_iClientService.getClientBillingWholesaleRateByClientBillingID(_ObjClientBillingDetailViewModel.ClientBillingDetail.ClientBillingID));
            }
            return Json(_ObjClientBillingDetailViewModel, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetClientPrivateLabelByClientID(int _clientID)
        {
            ClientBillingDetailViewModel _ObjClientBillingDetailViewModel = new ClientBillingDetailViewModel();
            _ObjClientBillingDetailViewModel.ClientPrivateLabelDetail = Mapper.Map<ClientPrivateLabel>(_iClientService.getClientPrivateLabelDetailByClientID(_clientID));

            if (_ObjClientBillingDetailViewModel.ClientPrivateLabelDetail != null)
            {
                if (_ObjClientBillingDetailViewModel.ClientPrivateLabelDetail.ClientPrivateLabelLogoName != null)
                    _ObjClientBillingDetailViewModel.ClientPrivateLabelDetail.ClientPrivateLabelLogoName = GlobalConst.ConstantChar.ForwardSlash + GlobalConst.VirtualPathFolders.Storage + GlobalConst.ConstantChar.ForwardSlash + _clientID.ToString() + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.VirtualPathFolders.ClientPrivateLableLogo + GlobalConst.ConstantChar.ForwardSlash + _ObjClientBillingDetailViewModel.ClientPrivateLabelDetail.ClientPrivateLabelLogoName;
            }
            if (_ObjClientBillingDetailViewModel.ClientPrivateLabelDetail == null)
                _ObjClientBillingDetailViewModel.ClientPrivateLabelDetail = new ClientPrivateLabel();

            return Json(_ObjClientBillingDetailViewModel.ClientPrivateLabelDetail, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddClientBilling(ClientBilling _ClientBilling, ClientBillingRetailRate _ClientBillingRetailRate, ClientBillingWholesaleRate _ClientBillingWholesaleRate)
        {
            try
            {
                int _ClientBillingID = 0;
                string[] _Ids = new string[4];
                if (_ClientBilling.ClientBillingID > 0)
                {
                    _Ids[0] = GlobalConst.Mode.Update;

                    if (_ClientBilling.ClientAttentionToID != 3)
                        _ClientBilling.ClientAttentionToFreeText = null;

                    _ClientBillingID = _iClientService.updateClientBilling(Mapper.Map<MMC.MMCService.ClientService.ClientBilling>(_ClientBilling));

                    _Ids[1] = _ClientBilling.ClientBillingID.ToString(); ;

                    _ClientBillingRetailRate.ClientBillingID = _ClientBilling.ClientBillingID;
                    _ClientBillingRetailRate.CreatedBy = MMCUser.UserId;
                    _ClientBillingRetailRate.CreatedOn = System.DateTime.Now;

                    _Ids[2] = _ClientBillingRetailRate.ClientBillingRetailRateID.ToString();

                    _iClientService.updateClientBillingRetailRate(Mapper.Map<MMC.MMCService.ClientService.ClientBillingRetailRate>(_ClientBillingRetailRate));



                    if (_ClientBilling.ClientIsPrivateLabel == true)
                    {
                        _ClientBillingWholesaleRate.ClientBillingID = _ClientBilling.ClientBillingID;
                        _ClientBillingWholesaleRate.CreatedBy = MMCUser.UserId;
                        _ClientBillingWholesaleRate.CreatedOn = System.DateTime.Now;
                        _Ids[3] = _ClientBillingWholesaleRate.ClientBillingWholesaleRateID.ToString();
                        _iClientService.updateClientBillingWholesaleRate(Mapper.Map<MMC.MMCService.ClientService.ClientBillingWholesaleRate>(_ClientBillingWholesaleRate));
                    }
                    else
                    {
                        if (_ClientBillingWholesaleRate.ClientBillingWholesaleRateID > 0)
                            _iClientService.deleteClientBillingWholesaleRate(_ClientBillingWholesaleRate.ClientBillingWholesaleRateID);
                        // delete the Client Private Label 
                        _iClientService.deleteClientPrivateLabelByClientID(_ClientBilling.ClientID);
                    }
                }
                else
                {
                    _Ids[0] = GlobalConst.Mode.Add;

                    if (_ClientBilling.ClientAttentionToID != 3)
                        _ClientBilling.ClientAttentionToFreeText = null;

                    _ClientBilling.ClientBillingID = _iClientService.addClientBilling(Mapper.Map<MMC.MMCService.ClientService.ClientBilling>(_ClientBilling));


                    _Ids[1] = _ClientBilling.ClientBillingID.ToString();

                    _ClientBillingRetailRate.ClientBillingID = _ClientBilling.ClientBillingID;
                    _ClientBillingRetailRate.CreatedBy = MMCUser.UserId;
                    _ClientBillingRetailRate.CreatedOn = System.DateTime.Now;

                    _ClientBillingRetailRate.ClientBillingRetailRateID = _iClientService.addClientBillingRetailRate(Mapper.Map<MMC.MMCService.ClientService.ClientBillingRetailRate>(_ClientBillingRetailRate));

                    _Ids[2] = _ClientBillingRetailRate.ClientBillingRetailRateID.ToString();
                    if (_ClientBilling.ClientIsPrivateLabel == true)
                    {
                        _ClientBillingWholesaleRate.ClientBillingID = _ClientBilling.ClientBillingID;
                        _ClientBillingWholesaleRate.CreatedBy = MMCUser.UserId;
                        _ClientBillingWholesaleRate.CreatedOn = System.DateTime.Now;

                        _ClientBillingWholesaleRate.ClientBillingWholesaleRateID = _iClientService.addClientBillingWholesaleRate(Mapper.Map<MMC.MMCService.ClientService.ClientBillingWholesaleRate>(_ClientBillingWholesaleRate));
                        _Ids[3] = _ClientBillingWholesaleRate.ClientBillingWholesaleRateID.ToString();
                    }
                }
                return Json(_Ids, GlobalConst.ContentTypes.TextHtml);
            }
            catch
            {
                return Json(0);
            }
        }
        [HttpPost]
        public ActionResult AddClientPrivateLabel(ClientPrivateLabel _ClientPrivateLabel)
        {
            try
            {

                if (_ClientPrivateLabel.ClientPrivateLabelLogo != null)
                    _ClientPrivateLabel.ClientPrivateLabelLogoName = GlobalConst.VirtualPathFolders.ClientPrivateLableLogo + Path.GetExtension(_ClientPrivateLabel.ClientPrivateLabelLogo.FileName);

                string[] _ids = new string[2];
                if (_ClientPrivateLabel.ClientPrivateLabelID > 0)
                {
                    _ids[0] = "Update";

                    if (_ClientPrivateLabel.ClientPrivateLabelLogoName != null)
                    {
                        _ClientPrivateLabel.ClientPrivateLabelLogoName = Path.GetFileName(_ClientPrivateLabel.ClientPrivateLabelLogoName);
                    }

                    _iClientService.updateClientPrivateLabel(Mapper.Map<MMC.MMCService.ClientService.ClientPrivateLabel>(_ClientPrivateLabel));
                    _ids[1] = _ClientPrivateLabel.ClientPrivateLabelID.ToString();
                    if (_ClientPrivateLabel.ClientPrivateLabelLogo != null)
                    {
                        StorageService _storageService;
                        _storageService = new StorageService();
                        var path = _storageService.GeneateStorageByStoragePath(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()) + GlobalConst.ConstantChar.DoubleBackSlash + _ClientPrivateLabel.ClientID.ToString() + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.VirtualPathFolders.ClientPrivateLableLogo) + GlobalConst.ConstantChar.DoubleBackSlash + _ClientPrivateLabel.ClientPrivateLabelLogoName;
                        if ((System.IO.File.Exists(path)))
                            System.IO.File.Delete(path);
                        _ClientPrivateLabel.ClientPrivateLabelLogo.SaveAs(path);
                    }

                }
                else
                {
                    _ids[0] = "Add";
                    _ClientPrivateLabel.ClientPrivateLabelID = _iClientService.addClientPrivateLabel(Mapper.Map<MMC.MMCService.ClientService.ClientPrivateLabel>(_ClientPrivateLabel));
                    if (_ClientPrivateLabel.ClientPrivateLabelLogo != null)
                    {
                        StorageService _storageService;
                        _storageService = new StorageService();
                        var path = _storageService.GeneateStorageByStoragePath(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()) + GlobalConst.ConstantChar.DoubleBackSlash + _ClientPrivateLabel.ClientID.ToString() + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.VirtualPathFolders.ClientPrivateLableLogo) + GlobalConst.ConstantChar.DoubleBackSlash + _ClientPrivateLabel.ClientPrivateLabelLogoName;
                        _ClientPrivateLabel.ClientPrivateLabelLogo.SaveAs(path);
                    }

                    _ids[1] = _ClientPrivateLabel.ClientPrivateLabelID.ToString();
                }
                return Json(_ids, GlobalConst.ContentTypes.TextHtml, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("0");
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