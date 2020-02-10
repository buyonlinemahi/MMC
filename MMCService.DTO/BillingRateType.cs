using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class BillingRateType
    {
        [DataMember]
        public int BillingRateTypeID { get; set; }
        [DataMember]
        public string BillingRateTypeName { get; set; }
    }
}
