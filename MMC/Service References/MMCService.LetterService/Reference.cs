﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMC.MMCService.LetterService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InitialNotificationLetter", Namespace="http://schemas.datacontract.org/2004/07/MMCService.DTO")]
    [System.SerializableAttribute()]
    public partial class InitialNotificationLetter : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AdjusterField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CEReceivedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClaimAdministratorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClaimNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DOIField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DecisionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhysAddress1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhysCityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhysFaxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhysFirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhysLastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhysQualificationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhysStatesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhysZipField;
        
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
        public string Adjuster {
            get {
                return this.AdjusterField;
            }
            set {
                if ((object.ReferenceEquals(this.AdjusterField, value) != true)) {
                    this.AdjusterField = value;
                    this.RaisePropertyChanged("Adjuster");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CEReceivedDate {
            get {
                return this.CEReceivedDateField;
            }
            set {
                if ((this.CEReceivedDateField.Equals(value) != true)) {
                    this.CEReceivedDateField = value;
                    this.RaisePropertyChanged("CEReceivedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClaimAdministrator {
            get {
                return this.ClaimAdministratorField;
            }
            set {
                if ((object.ReferenceEquals(this.ClaimAdministratorField, value) != true)) {
                    this.ClaimAdministratorField = value;
                    this.RaisePropertyChanged("ClaimAdministrator");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClaimNumber {
            get {
                return this.ClaimNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.ClaimNumberField, value) != true)) {
                    this.ClaimNumberField = value;
                    this.RaisePropertyChanged("ClaimNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DOI {
            get {
                return this.DOIField;
            }
            set {
                if ((this.DOIField.Equals(value) != true)) {
                    this.DOIField = value;
                    this.RaisePropertyChanged("DOI");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Decision {
            get {
                return this.DecisionField;
            }
            set {
                if ((object.ReferenceEquals(this.DecisionField, value) != true)) {
                    this.DecisionField = value;
                    this.RaisePropertyChanged("Decision");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatientName {
            get {
                return this.PatientNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PatientNameField, value) != true)) {
                    this.PatientNameField = value;
                    this.RaisePropertyChanged("PatientName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhysAddress1 {
            get {
                return this.PhysAddress1Field;
            }
            set {
                if ((object.ReferenceEquals(this.PhysAddress1Field, value) != true)) {
                    this.PhysAddress1Field = value;
                    this.RaisePropertyChanged("PhysAddress1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhysCity {
            get {
                return this.PhysCityField;
            }
            set {
                if ((object.ReferenceEquals(this.PhysCityField, value) != true)) {
                    this.PhysCityField = value;
                    this.RaisePropertyChanged("PhysCity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhysFax {
            get {
                return this.PhysFaxField;
            }
            set {
                if ((object.ReferenceEquals(this.PhysFaxField, value) != true)) {
                    this.PhysFaxField = value;
                    this.RaisePropertyChanged("PhysFax");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhysFirstName {
            get {
                return this.PhysFirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PhysFirstNameField, value) != true)) {
                    this.PhysFirstNameField = value;
                    this.RaisePropertyChanged("PhysFirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhysLastName {
            get {
                return this.PhysLastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PhysLastNameField, value) != true)) {
                    this.PhysLastNameField = value;
                    this.RaisePropertyChanged("PhysLastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhysQualification {
            get {
                return this.PhysQualificationField;
            }
            set {
                if ((object.ReferenceEquals(this.PhysQualificationField, value) != true)) {
                    this.PhysQualificationField = value;
                    this.RaisePropertyChanged("PhysQualification");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhysStates {
            get {
                return this.PhysStatesField;
            }
            set {
                if ((object.ReferenceEquals(this.PhysStatesField, value) != true)) {
                    this.PhysStatesField = value;
                    this.RaisePropertyChanged("PhysStates");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhysZip {
            get {
                return this.PhysZipField;
            }
            set {
                if ((object.ReferenceEquals(this.PhysZipField, value) != true)) {
                    this.PhysZipField = value;
                    this.RaisePropertyChanged("PhysZip");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestInitialNotificationLetter", Namespace="http://schemas.datacontract.org/2004/07/MMCService.DTO")]
    [System.SerializableAttribute()]
    public partial class RequestInitialNotificationLetter : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int DurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DurationTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FrequencyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> QTYField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RequestIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RequestTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TreatmentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TreatmentTypeField;
        
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
        public int Duration {
            get {
                return this.DurationField;
            }
            set {
                if ((this.DurationField.Equals(value) != true)) {
                    this.DurationField = value;
                    this.RaisePropertyChanged("Duration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DurationType {
            get {
                return this.DurationTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.DurationTypeField, value) != true)) {
                    this.DurationTypeField = value;
                    this.RaisePropertyChanged("DurationType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Frequency {
            get {
                return this.FrequencyField;
            }
            set {
                if ((this.FrequencyField.Equals(value) != true)) {
                    this.FrequencyField = value;
                    this.RaisePropertyChanged("Frequency");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> QTY {
            get {
                return this.QTYField;
            }
            set {
                if ((this.QTYField.Equals(value) != true)) {
                    this.QTYField = value;
                    this.RaisePropertyChanged("QTY");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RequestID {
            get {
                return this.RequestIDField;
            }
            set {
                if ((this.RequestIDField.Equals(value) != true)) {
                    this.RequestIDField = value;
                    this.RaisePropertyChanged("RequestID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RequestType {
            get {
                return this.RequestTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.RequestTypeField, value) != true)) {
                    this.RequestTypeField = value;
                    this.RaisePropertyChanged("RequestType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Treatment {
            get {
                return this.TreatmentField;
            }
            set {
                if ((object.ReferenceEquals(this.TreatmentField, value) != true)) {
                    this.TreatmentField = value;
                    this.RaisePropertyChanged("Treatment");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TreatmentType {
            get {
                return this.TreatmentTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TreatmentTypeField, value) != true)) {
                    this.TreatmentTypeField = value;
                    this.RaisePropertyChanged("TreatmentType");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MMCService.LetterService.ILetterService")]
    public interface ILetterService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILetterService/getInitialNotificationLetterDetails", ReplyAction="http://tempuri.org/ILetterService/getInitialNotificationLetterDetailsResponse")]
        MMC.MMCService.LetterService.InitialNotificationLetter getInitialNotificationLetterDetails(int referralID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILetterService/getInitialNotificationLetterDetails", ReplyAction="http://tempuri.org/ILetterService/getInitialNotificationLetterDetailsResponse")]
        System.Threading.Tasks.Task<MMC.MMCService.LetterService.InitialNotificationLetter> getInitialNotificationLetterDetailsAsync(int referralID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILetterService/getRequestDetailsInitialNotificationLetter", ReplyAction="http://tempuri.org/ILetterService/getRequestDetailsInitialNotificationLetterRespo" +
            "nse")]
        MMC.MMCService.LetterService.RequestInitialNotificationLetter[] getRequestDetailsInitialNotificationLetter(int referralID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILetterService/getRequestDetailsInitialNotificationLetter", ReplyAction="http://tempuri.org/ILetterService/getRequestDetailsInitialNotificationLetterRespo" +
            "nse")]
        System.Threading.Tasks.Task<MMC.MMCService.LetterService.RequestInitialNotificationLetter[]> getRequestDetailsInitialNotificationLetterAsync(int referralID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILetterService/getCertifiedRequestDetailsInitialNotificationLe" +
            "tter", ReplyAction="http://tempuri.org/ILetterService/getCertifiedRequestDetailsInitialNotificationLe" +
            "tterResponse")]
        MMC.MMCService.LetterService.RequestInitialNotificationLetter[] getCertifiedRequestDetailsInitialNotificationLetter(int referralID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILetterService/getCertifiedRequestDetailsInitialNotificationLe" +
            "tter", ReplyAction="http://tempuri.org/ILetterService/getCertifiedRequestDetailsInitialNotificationLe" +
            "tterResponse")]
        System.Threading.Tasks.Task<MMC.MMCService.LetterService.RequestInitialNotificationLetter[]> getCertifiedRequestDetailsInitialNotificationLetterAsync(int referralID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILetterService/getDeniedRequestDetailsInitialNotificationLette" +
            "r", ReplyAction="http://tempuri.org/ILetterService/getDeniedRequestDetailsInitialNotificationLette" +
            "rResponse")]
        MMC.MMCService.LetterService.RequestInitialNotificationLetter[] getDeniedRequestDetailsInitialNotificationLetter(int referralID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILetterService/getDeniedRequestDetailsInitialNotificationLette" +
            "r", ReplyAction="http://tempuri.org/ILetterService/getDeniedRequestDetailsInitialNotificationLette" +
            "rResponse")]
        System.Threading.Tasks.Task<MMC.MMCService.LetterService.RequestInitialNotificationLetter[]> getDeniedRequestDetailsInitialNotificationLetterAsync(int referralID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILetterServiceChannel : MMC.MMCService.LetterService.ILetterService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LetterServiceClient : System.ServiceModel.ClientBase<MMC.MMCService.LetterService.ILetterService>, MMC.MMCService.LetterService.ILetterService {
        
        public LetterServiceClient() {
        }
        
        public LetterServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LetterServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LetterServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LetterServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MMC.MMCService.LetterService.InitialNotificationLetter getInitialNotificationLetterDetails(int referralID) {
            return base.Channel.getInitialNotificationLetterDetails(referralID);
        }
        
        public System.Threading.Tasks.Task<MMC.MMCService.LetterService.InitialNotificationLetter> getInitialNotificationLetterDetailsAsync(int referralID) {
            return base.Channel.getInitialNotificationLetterDetailsAsync(referralID);
        }
        
        public MMC.MMCService.LetterService.RequestInitialNotificationLetter[] getRequestDetailsInitialNotificationLetter(int referralID) {
            return base.Channel.getRequestDetailsInitialNotificationLetter(referralID);
        }
        
        public System.Threading.Tasks.Task<MMC.MMCService.LetterService.RequestInitialNotificationLetter[]> getRequestDetailsInitialNotificationLetterAsync(int referralID) {
            return base.Channel.getRequestDetailsInitialNotificationLetterAsync(referralID);
        }
        
        public MMC.MMCService.LetterService.RequestInitialNotificationLetter[] getCertifiedRequestDetailsInitialNotificationLetter(int referralID) {
            return base.Channel.getCertifiedRequestDetailsInitialNotificationLetter(referralID);
        }
        
        public System.Threading.Tasks.Task<MMC.MMCService.LetterService.RequestInitialNotificationLetter[]> getCertifiedRequestDetailsInitialNotificationLetterAsync(int referralID) {
            return base.Channel.getCertifiedRequestDetailsInitialNotificationLetterAsync(referralID);
        }
        
        public MMC.MMCService.LetterService.RequestInitialNotificationLetter[] getDeniedRequestDetailsInitialNotificationLetter(int referralID) {
            return base.Channel.getDeniedRequestDetailsInitialNotificationLetter(referralID);
        }
        
        public System.Threading.Tasks.Task<MMC.MMCService.LetterService.RequestInitialNotificationLetter[]> getDeniedRequestDetailsInitialNotificationLetterAsync(int referralID) {
            return base.Channel.getDeniedRequestDetailsInitialNotificationLetterAsync(referralID);
        }
    }
}
