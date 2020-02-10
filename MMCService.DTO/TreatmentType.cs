using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class TreatmentType
    {
        [DataMember]
        public int TreatmentTypeID { get; set; }
        [DataMember]
        public int TreatmentCategoryID { get; set; }
        [DataMember]
        public string TreatmentTypeDesc { get; set; }
        [DataMember]
        public decimal RFARequestBillingRN { get; set; }
        [DataMember]
        public decimal RFARequestBillingMD { get; set; }
        [DataMember]
        public decimal RFARequestBillingPR { get; set; }
        [DataMember]
        public decimal RFARequestBillingAdmin { get; set; }
        [DataMember]
        public decimal EstCost { get; set; }
        [DataMember]
        public bool TreatmentMDRequired { get; set; }
        [DataMember]
        public string TreatmentDescNumber { get; set; }
    }
}
