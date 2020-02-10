using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class RFARequestBilling
    {
        [DataMember]
        public int RFARequestBillingID { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public decimal RFARequestBillingRN { get; set; }
        [DataMember]
        public decimal RFARequestBillingMD { get; set; }
        [DataMember]
        public decimal RFARequestBillingSpeciality { get; set; }
        [DataMember]
        public decimal RFARequestBillingAdmin { get; set; }
        [DataMember]
        public decimal RFARequestBillingDeferral { get; set; }
    }
}
