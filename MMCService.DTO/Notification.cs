using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class Notification
    {
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public string PatFirstName { get; set; }
        [DataMember]
        public string PatLastName { get; set; }
        [DataMember]
        public string PatClaimNumber { get; set; }
        [DataMember]
        public string RFARequestedTreatment { get; set; }
    }
}
