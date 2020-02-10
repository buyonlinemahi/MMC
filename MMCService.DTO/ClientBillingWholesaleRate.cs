using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientBillingWholesaleRate
    {
        [DataMember]
        public int ClientBillingWholesaleRateID { get; set; }
        [DataMember]
        public int ClientBillingID { get; set; }
        [DataMember]
        public decimal? ClientBillingWholesaleRateRN { get; set; }
        [DataMember]
        public decimal? ClientBillingWholesaleRateMD { get; set; }
        [DataMember]
        public decimal? ClientBillingWholesaleRateSpecialityReview { get; set; }
        [DataMember]
        public decimal? ClientBillingWholesaleRateAdminFee { get; set; }
        [DataMember]
        public decimal? ClientBillingWholesaleRateIMRPrep { get; set; }
        [DataMember]
        public decimal? ClientBillingWholesaleRateIMRMD { get; set; }
        [DataMember]
        public decimal? ClientBillingWholesaleRateIMRRush { get; set; }
        [DataMember]
        public decimal? ClientBillingWholesaleRateDeferral { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedOn { get; set; }
    }
}
