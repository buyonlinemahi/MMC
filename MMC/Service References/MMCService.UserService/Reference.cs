﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMC.MMCService.UserService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/MMCService.DTO")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> UserDeletePermissionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserEmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserFaxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserFirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserLastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> UserManagementPermissionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserPasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserPhoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserSignatureFileNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> UserDeletePermission {
            get {
                return this.UserDeletePermissionField;
            }
            set {
                if ((this.UserDeletePermissionField.Equals(value) != true)) {
                    this.UserDeletePermissionField = value;
                    this.RaisePropertyChanged("UserDeletePermission");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserEmail {
            get {
                return this.UserEmailField;
            }
            set {
                if ((object.ReferenceEquals(this.UserEmailField, value) != true)) {
                    this.UserEmailField = value;
                    this.RaisePropertyChanged("UserEmail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserFax {
            get {
                return this.UserFaxField;
            }
            set {
                if ((object.ReferenceEquals(this.UserFaxField, value) != true)) {
                    this.UserFaxField = value;
                    this.RaisePropertyChanged("UserFax");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserFirstName {
            get {
                return this.UserFirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserFirstNameField, value) != true)) {
                    this.UserFirstNameField = value;
                    this.RaisePropertyChanged("UserFirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserLastName {
            get {
                return this.UserLastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserLastNameField, value) != true)) {
                    this.UserLastNameField = value;
                    this.RaisePropertyChanged("UserLastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> UserManagementPermission {
            get {
                return this.UserManagementPermissionField;
            }
            set {
                if ((this.UserManagementPermissionField.Equals(value) != true)) {
                    this.UserManagementPermissionField = value;
                    this.RaisePropertyChanged("UserManagementPermission");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserPassword {
            get {
                return this.UserPasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.UserPasswordField, value) != true)) {
                    this.UserPasswordField = value;
                    this.RaisePropertyChanged("UserPassword");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserPhone {
            get {
                return this.UserPhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.UserPhoneField, value) != true)) {
                    this.UserPhoneField = value;
                    this.RaisePropertyChanged("UserPhone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserSignatureFileName {
            get {
                return this.UserSignatureFileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserSignatureFileNameField, value) != true)) {
                    this.UserSignatureFileNameField = value;
                    this.RaisePropertyChanged("UserSignatureFileName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PagedUser", Namespace="http://schemas.datacontract.org/2004/07/MMCService.DTO.Paged")]
    [System.SerializableAttribute()]
    public partial class PagedUser : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalCountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MMC.MMCService.UserService.User[] UserDetailsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TotalCount {
            get {
                return this.TotalCountField;
            }
            set {
                if ((this.TotalCountField.Equals(value) != true)) {
                    this.TotalCountField = value;
                    this.RaisePropertyChanged("TotalCount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MMC.MMCService.UserService.User[] UserDetails {
            get {
                return this.UserDetailsField;
            }
            set {
                if ((object.ReferenceEquals(this.UserDetailsField, value) != true)) {
                    this.UserDetailsField = value;
                    this.RaisePropertyChanged("UserDetails");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MMCService.UserService.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/addUser", ReplyAction="http://tempuri.org/IUserService/addUserResponse")]
        int addUser(MMC.MMCService.UserService.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/addUser", ReplyAction="http://tempuri.org/IUserService/addUserResponse")]
        System.Threading.Tasks.Task<int> addUserAsync(MMC.MMCService.UserService.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateUser", ReplyAction="http://tempuri.org/IUserService/updateUserResponse")]
        int updateUser(MMC.MMCService.UserService.User users);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateUser", ReplyAction="http://tempuri.org/IUserService/updateUserResponse")]
        System.Threading.Tasks.Task<int> updateUserAsync(MMC.MMCService.UserService.User users);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/deleteUser", ReplyAction="http://tempuri.org/IUserService/deleteUserResponse")]
        void deleteUser(int _userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/deleteUser", ReplyAction="http://tempuri.org/IUserService/deleteUserResponse")]
        System.Threading.Tasks.Task deleteUserAsync(int _userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUserByID", ReplyAction="http://tempuri.org/IUserService/getUserByIDResponse")]
        MMC.MMCService.UserService.User getUserByID(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUserByID", ReplyAction="http://tempuri.org/IUserService/getUserByIDResponse")]
        System.Threading.Tasks.Task<MMC.MMCService.UserService.User> getUserByIDAsync(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByUserName", ReplyAction="http://tempuri.org/IUserService/GetUserByUserNameResponse")]
        MMC.MMCService.UserService.User GetUserByUserName(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByUserName", ReplyAction="http://tempuri.org/IUserService/GetUserByUserNameResponse")]
        System.Threading.Tasks.Task<MMC.MMCService.UserService.User> GetUserByUserNameAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUsersByName", ReplyAction="http://tempuri.org/IUserService/getUsersByNameResponse")]
        MMC.MMCService.UserService.PagedUser getUsersByName(string SearchText, int _skip, int _take);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/getUsersByName", ReplyAction="http://tempuri.org/IUserService/getUsersByNameResponse")]
        System.Threading.Tasks.Task<MMC.MMCService.UserService.PagedUser> getUsersByNameAsync(string SearchText, int _skip, int _take);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateUserPassword", ReplyAction="http://tempuri.org/IUserService/updateUserPasswordResponse")]
        int updateUserPassword(string userpassword, int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/updateUserPassword", ReplyAction="http://tempuri.org/IUserService/updateUserPasswordResponse")]
        System.Threading.Tasks.Task<int> updateUserPasswordAsync(string userpassword, int userId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : MMC.MMCService.UserService.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<MMC.MMCService.UserService.IUserService>, MMC.MMCService.UserService.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int addUser(MMC.MMCService.UserService.User user) {
            return base.Channel.addUser(user);
        }
        
        public System.Threading.Tasks.Task<int> addUserAsync(MMC.MMCService.UserService.User user) {
            return base.Channel.addUserAsync(user);
        }
        
        public int updateUser(MMC.MMCService.UserService.User users) {
            return base.Channel.updateUser(users);
        }
        
        public System.Threading.Tasks.Task<int> updateUserAsync(MMC.MMCService.UserService.User users) {
            return base.Channel.updateUserAsync(users);
        }
        
        public void deleteUser(int _userID) {
            base.Channel.deleteUser(_userID);
        }
        
        public System.Threading.Tasks.Task deleteUserAsync(int _userID) {
            return base.Channel.deleteUserAsync(_userID);
        }
        
        public MMC.MMCService.UserService.User getUserByID(int Id) {
            return base.Channel.getUserByID(Id);
        }
        
        public System.Threading.Tasks.Task<MMC.MMCService.UserService.User> getUserByIDAsync(int Id) {
            return base.Channel.getUserByIDAsync(Id);
        }
        
        public MMC.MMCService.UserService.User GetUserByUserName(string userName) {
            return base.Channel.GetUserByUserName(userName);
        }
        
        public System.Threading.Tasks.Task<MMC.MMCService.UserService.User> GetUserByUserNameAsync(string userName) {
            return base.Channel.GetUserByUserNameAsync(userName);
        }
        
        public MMC.MMCService.UserService.PagedUser getUsersByName(string SearchText, int _skip, int _take) {
            return base.Channel.getUsersByName(SearchText, _skip, _take);
        }
        
        public System.Threading.Tasks.Task<MMC.MMCService.UserService.PagedUser> getUsersByNameAsync(string SearchText, int _skip, int _take) {
            return base.Channel.getUsersByNameAsync(SearchText, _skip, _take);
        }
        
        public int updateUserPassword(string userpassword, int userId) {
            return base.Channel.updateUserPassword(userpassword, userId);
        }
        
        public System.Threading.Tasks.Task<int> updateUserPasswordAsync(string userpassword, int userId) {
            return base.Channel.updateUserPasswordAsync(userpassword, userId);
        }
    }
}
