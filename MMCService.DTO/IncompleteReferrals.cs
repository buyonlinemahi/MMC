using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class IncompleteReferrals
    {
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public string PatClaimNumber { get; set; }
        [DataMember]
        public string PatFirstName { get; set; }
        [DataMember]
        public string PatLastName { get; set; }
    }
}
