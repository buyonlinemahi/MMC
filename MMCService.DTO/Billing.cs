using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class Billing
    {
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int PatClientID { get; set; }
        [DataMember]
        public string RFARequestedTreatment { get; set; }
        [DataMember]
        public int RFARequestBillingID { get; set; }
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
        [DataMember]
        public int ClientBillingRateTypeID { get; set; }
        [DataMember]
        public decimal BillingTotal { get; set; }        
        [DataMember]
        public int DecisionID { get; set; }
    }
}
