﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMC.MMCService.NotificationService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PagedNotification", Namespace="http://schemas.datacontract.org/2004/07/MMCService.DTO.Paged")]
    [System.SerializableAttribute()]
    public partial class PagedNotification : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MMC.MMCService.NotificationService.Notification[] NotificationDetailsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalCountField;
        
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
        public MMC.MMCService.NotificationService.Notification[] NotificationDetails {
            get {
                return this.NotificationDetailsField;
            }
            set {
                if ((object.ReferenceEquals(this.NotificationDetailsField, value) != true)) {
                    this.NotificationDetailsField = value;
                    this.RaisePropertyChanged("NotificationDetails");
                }
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Notification", Namespace="http://schemas.datacontract.org/2004/07/MMCService.DTO")]
    [System.SerializableAttribute()]
    public partial class Notification : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatClaimNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatFirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatLastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RFAReferralIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RFARequestIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RFARequestedTreatmentField;
        
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
        public string PatClaimNumber {
            get {
                return this.PatClaimNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PatClaimNumberField, value) != true)) {
                    this.PatClaimNumberField = value;
                    this.RaisePropertyChanged("PatClaimNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatFirstName {
            get {
                return this.PatFirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PatFirstNameField, value) != true)) {
                    this.PatFirstNameField = value;
                    this.RaisePropertyChanged("PatFirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatLastName {
            get {
                return this.PatLastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PatLastNameField, value) != true)) {
                    this.PatLastNameField = value;
                    this.RaisePropertyChanged("PatLastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RFAReferralID {
            get {
                return this.RFAReferralIDField;
            }
            set {
                if ((this.RFAReferralIDField.Equals(value) != true)) {
                    this.RFAReferralIDField = value;
                    this.RaisePropertyChanged("RFAReferralID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RFARequestID {
            get {
                return this.RFARequestIDField;
            }
            set {
                if ((this.RFARequestIDField.Equals(value) != true)) {
                    this.RFARequestIDField = value;
                    this.RaisePropertyChanged("RFARequestID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RFARequestedTreatment {
            get {
                return this.RFARequestedTreatmentField;
            }
            set {
                if ((object.ReferenceEquals(this.RFARequestedTreatmentField, value) != true)) {
                    this.RFARequestedTreatmentField = value;
                    this.RaisePropertyChanged("RFARequestedTreatment");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="NotificationEmailSend", Namespace="http://schemas.datacontract.org/2004/07/MMCService.DTO")]
    [System.SerializableAttribute()]
    public partial class NotificationEmailSend : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NotificationEmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RFAReferralIDField;
        
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
        public string NotificationEmail {
            get {
                return this.NotificationEmailField;
            }
            set {
                if ((object.ReferenceEquals(this.NotificationEmailField, value) != true)) {
                    this.NotificationEmailField = value;
                    this.RaisePropertyChanged("NotificationEmail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RFAReferralID {
            get {
                return this.RFAReferralIDField;
            }
            set {
                if ((this.RFAReferralIDField.Equals(value) != true)) {
                    this.RFAReferralIDField = value;
                    this.RaisePropertyChanged("RFAReferralID");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MMCService.NotificationService.INotificationService")]
    public interface INotificationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotificationService/getAllNotifications", ReplyAction="http://tempuri.org/INotificationService/getAllNotificationsResponse")]
        MMC.MMCService.NotificationService.PagedNotification getAllNotifications(int _processlevel, int _skip, int _take);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotificationService/getAllNotifications", ReplyAction="http://tempuri.org/INotificationService/getAllNotificationsResponse")]
        System.Threading.Tasks.Task<MMC.MMCService.NotificationService.PagedNotification> getAllNotificationsAsync(int _processlevel, int _skip, int _take);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotificationService/getAdjandPhyEmailByReferralID", ReplyAction="http://tempuri.org/INotificationService/getAdjandPhyEmailByReferralIDResponse")]
        MMC.MMCService.NotificationService.NotificationEmailSend[] getAdjandPhyEmailByReferralID(int _referralID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INotificationService/getAdjandPhyEmailByReferralID", ReplyAction="http://tempuri.org/INotificationService/getAdjandPhyEmailByReferralIDResponse")]
        System.Threading.Tasks.Task<MMC.MMCService.NotificationService.NotificationEmailSend[]> getAdjandPhyEmailByReferralIDAsync(int _referralID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INotificationServiceChannel : MMC.MMCService.NotificationService.INotificationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NotificationServiceClient : System.ServiceModel.ClientBase<MMC.MMCService.NotificationService.INotificationService>, MMC.MMCService.NotificationService.INotificationService {
        
        public NotificationServiceClient() {
        }
        
        public NotificationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NotificationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NotificationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NotificationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MMC.MMCService.NotificationService.PagedNotification getAllNotifications(int _processlevel, int _skip, int _take) {
            return base.Channel.getAllNotifications(_processlevel, _skip, _take);
        }
        
        public System.Threading.Tasks.Task<MMC.MMCService.NotificationService.PagedNotification> getAllNotificationsAsync(int _processlevel, int _skip, int _take) {
            return base.Channel.getAllNotificationsAsync(_processlevel, _skip, _take);
        }
        
        public MMC.MMCService.NotificationService.NotificationEmailSend[] getAdjandPhyEmailByReferralID(int _referralID) {
            return base.Channel.getAdjandPhyEmailByReferralID(_referralID);
        }
        
        public System.Threading.Tasks.Task<MMC.MMCService.NotificationService.NotificationEmailSend[]> getAdjandPhyEmailByReferralIDAsync(int _referralID) {
            return base.Channel.getAdjandPhyEmailByReferralIDAsync(_referralID);
        }
    }
}