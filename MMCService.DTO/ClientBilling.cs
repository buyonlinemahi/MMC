using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class ClientBilling
    {
        [DataMember]
        public int ClientBillingID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int ClientBillingRateTypeID { get; set; }
        [DataMember]
        public Boolean? ClientIsPrivateLabel { get; set; }
        [DataMember]
        public string ClientInvoiceNumber { get; set; }
        [DataMember]
        public int ClientAttentionToID { get; set; }
        [DataMember]
        public string ClientAttentionToFreeText { get; set; }
    }
}
