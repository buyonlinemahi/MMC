using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class InitialNotificationLetter
    {
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string ClaimNumber { get; set; }
        [DataMember]
        public DateTime DOI { get; set; }
        [DataMember]
        public string Adjuster { get; set; }
        [DataMember]
        public string ClaimAdministrator { get; set; }
        [DataMember]
        public DateTime CEReceivedDate { get; set; }
        [DataMember]
        public string Decision { get; set; }

        [DataMember]
        public string PhysFirstName { get; set; }
        [DataMember]
        public string PhysLastName { get; set; }
        [DataMember]
        public string PhysQualification { get; set; }
        [DataMember]
        public string PhysAddress1 { get; set; }
        [DataMember]
        public string PhysCity { get; set; }
        [DataMember]
        public string PhysStates { get; set; }
        [DataMember]
        public string PhysZip { get; set; }
        [DataMember]
        public string PhysFax { get; set; }
    }
}
