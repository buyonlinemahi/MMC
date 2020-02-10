using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientBillingRetailRate
    {
        [DataMember]
        public int ClientBillingRetailRateID { get; set; }
        [DataMember]
        public int ClientBillingID { get; set; }
        [DataMember]
        public decimal? ClientBillingRetailRateRN { get; set; }
        [DataMember]
        public decimal? ClientBillingRetailRateMD { get; set; }
        [DataMember]
        public decimal? ClientBillingRetailRateSpecialityReview { get; set; }
        [DataMember]
        public decimal? ClientBillingRetailRateAdminFee { get; set; }
        [DataMember]
        public decimal? ClientBillingRetailRateIMRPrep { get; set; }
        [DataMember]
        public decimal? ClientBillingRetailRateIMRMD { get; set; }
        [DataMember]
        public decimal? ClientBillingRetailRateIMRRush { get; set; }
        [DataMember]
        public decimal? ClientBillingRetailRateDeferral { get; set; }
        
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedOn { get; set; }
    }
}
