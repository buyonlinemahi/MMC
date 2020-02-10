using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class IMRDecisionRequestDetails
    {
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public string RFARequestedTreatment { get; set; }
        [DataMember]
        public int RFAQuantity { get; set; }
        [DataMember]
        public int IMRRFARequestID { get; set; }
        [DataMember]
        public int? IMRApprovedUnits { get; set; }
        [DataMember]
        public Boolean? Overturn { get; set; }
    }
}
