using MMCApp.Infrastructure.ApplicationServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMCApp.Domain.Models.UserModel;
using MMCApp.Infrastructure.Global;
using MMCApp.Infrastructure.Base;
using AutoMapper;
using MMCApp.Domain.ViewModels.UserViewModel;
using MMCApp.Infrastructure.ApplicationServices;
using System.IO;
using MMCApp.Infrastructure.ApplicationFilters;
using MMCApp.Domain.Models.CommonModel;

namespace MMC.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/
        public readonly IEncryption _encryptionService;

        MMCService.UserService.UserServiceClient _iUserService;
        MMCService.CommonService.CommonServiceClient _iCommonService;
        public UserController(IEncryption encryptionService)
        {
            _iUserService = new MMCService.UserService.UserServiceClient();
            _encryptionService = encryptionService;
            _iCommonService = new MMCService.CommonService.CommonServiceClient();
        }
         [AuthorizedUserCheck]
        public ActionResult Home()
        {
            getNoOfReferralAccordingToProcessLevels();
            return View();
        }
         [AuthorizedUserCheck]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            User user = new User();
            return View(user);
        }
         [AuthorizedUserCheck]
        [HttpPost]
        public ActionResult GetUserByName(string _searchText, int? _skip)
        {
            var _UserDetails = _iUserService.getUsersByName(_searchText.Trim(), _skip.Value, GlobalConst.Records.LandingTake);
            UserSearchViewModel objUserList = new UserSearchViewModel();
            objUserList.UserDetails = Mapper.Map<IEnumerable<User>>(_UserDetails.UserDetails);
            objUserList.TotalCount = _UserDetails.TotalCount;
            return Json(objUserList, GlobalConst.ContentTypes.TextHtml);
        }
         [AuthorizedUserCheck]
        [HttpGet]
        public ActionResult SaveUserDetail(int? id)
        {
            getNoOfReferralAccordingToProcessLevels();
            User objUserModel = new User();
            if (id != null)
            {
                StorageService _storageService;
                _storageService = new StorageService();
                var _UserResult = _iUserService.getUserByID(id.Value);
                objUserModel = Mapper.Map<User>(_UserResult);
                if (objUserModel.UserSignatureFileName != null)
                    objUserModel.UserSignatureFileName = GlobalConst.ConstantChar.ForwardSlash + GlobalConst.VirtualPathFolders.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.VirtualPathFolders.User + GlobalConst.ConstantChar.ForwardSlash + +objUserModel.UserId + GlobalConst.ConstantChar.ForwardSlash + objUserModel.UserSignatureFileName;

            }
            return View(objUserModel);
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return Redirect("/");
        }
         [AuthorizedUserCheck]
        [HttpPost]
        public ActionResult SaveUserDetail(User _objUser)
        {
            var _message = GlobalConst.ConstantChar.StringBlank;
            ModelState["UserPassword"].Errors.Clear();
            if (ModelState.IsValid)
            {
                if (_objUser.UserId == GlobalConst.ConstantChar.Zero)
                {
                    if (_objUser.UserSignature != null)
                        _objUser.UserSignatureFileName = GlobalConst.VirtualPathFolders.UserSignature + Path.GetExtension(_objUser.UserSignature.FileName);

                    _objUser.UserPassword = _encryptionService.HashPassword(_objUser.UserPassword);
                    int usrid = _iUserService.addUser(Mapper.Map<MMCService.UserService.User>(_objUser));
                    if (usrid > 0)
                    {
                        if (_objUser.UserSignature != null)
                        {
                            StorageService _storageService;
                            _storageService = new StorageService();
                            var path = _storageService.getUserSignatureStorage(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()), usrid) + GlobalConst.ConstantChar.DoubleBackSlash + _objUser.UserSignatureFileName;
                            _objUser.UserSignature.SaveAs(path);
                        }                        

                        _message = GlobalConst.Message.SaveMessage;
                    }
                    else if (usrid == 0)
                    {
                        _message = GlobalConst.Message.UserExist;
                    }
                    else
                        _message = GlobalConst.Message.ErrorMessage;
                }
                else
                {
                    if (_objUser.UserSignature != null)
                    {
                        StorageService _storageService;
                        _storageService = new StorageService();
                        var ToDeleteFilePath = _storageService.getUserSignatureStorage(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()), _objUser.UserId) + GlobalConst.ConstantChar.DoubleBackSlash + Path.GetFileName(_objUser.UserSignatureFileName);
                        if ((System.IO.File.Exists(ToDeleteFilePath)))
                        {
                            System.IO.File.Delete(ToDeleteFilePath);
                        }                                                
                        _objUser.UserSignatureFileName = GlobalConst.VirtualPathFolders.UserSignature + Path.GetExtension(_objUser.UserSignature.FileName);
                    }
                    else {
                        if (_objUser.UserSignatureFileName != null)
                        {
                            _objUser.UserSignatureFileName = Path.GetFileName(_objUser.UserSignatureFileName);
                        }
                    }
                    if (_iUserService.updateUser(Mapper.Map<MMCService.UserService.User>(_objUser)) > 0)
                    {
                        if (_objUser.UserSignature != null)
                        {
                            StorageService _storageService;
                            _storageService = new StorageService();
                            var path = _storageService.getUserSignatureStorage(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings[GlobalConst.VirtualDirectoryPath.VirtualPath].ToString()), _objUser.UserId) + GlobalConst.ConstantChar.DoubleBackSlash + _objUser.UserSignatureFileName;
                            _objUser.UserSignature.SaveAs(path);
                        }                        

                        _message = GlobalConst.Message.UpdateMessage;   
                    }
                        
                    else
                        _message = GlobalConst.Message.ErrorMessage;
                }
            }
            else
            {
                _message = GlobalConst.Message.ModelErrorMessage;
            }
            return Json(_message, GlobalConst.ContentTypes.TextHtml);
        }
         [AuthorizedUserCheck]
        [HttpPost]
        public ActionResult DeleteUserByID(int _userId)
        {
            try
            {
                _iUserService.deleteUser(_userId);
                return Json(GlobalConst.Message.DeleteMessage, GlobalConst.ContentTypes.TextHtml);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage, GlobalConst.ContentTypes.TextHtml);
            }
        }
         [AuthorizedUserCheck]
        [HttpPost]
        public ActionResult ChangePassword(int UserId, string UserPassword)
        {
            try
            {
                _iUserService.updateUserPassword(_encryptionService.HashPassword(UserPassword), UserId);
                return Json(GlobalConst.Message.PasswordChanged, GlobalConst.ContentTypes.TextHtml);
            }
            catch
            {
                return Json(GlobalConst.Message.ErrorMessage, GlobalConst.ContentTypes.TextHtml);
            }
        }

        [HttpPost]
        public ActionResult Login(User usr)
        {
            ModelState.Clear();
            var user = (_iUserService.GetUserByUserName(usr.UserName));
            if (user != null)
            {
                if (usr.UserName == user.UserName && _encryptionService.VerifyHashedPassword(usr.UserPassword, user.UserPassword))
                {

                    MMCUser = Mapper.Map<User>(user);
                    return RedirectToAction(GlobalConst.Actions.UserController.Home, GlobalConst.Controllers.User, new { area = "" });
                }
                else
                {
                    usr.Message = GlobalConst.Message.PasswordMessage;
                }
            }
            else
            {
                usr.Message = GlobalConst.Message.UserNameMessage;
            }


            return View(usr);
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
