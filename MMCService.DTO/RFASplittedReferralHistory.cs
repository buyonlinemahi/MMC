using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
   [DataContract]
    public class RFASplittedReferralHistory
    {
        [DataMember]
        public int RFASplittedReferralID { get; set; }
        [DataMember]
        public int RFAOldReferralID { get; set; }
        [DataMember]
        public int RFANewReferralID { get; set; }
        [DataMember]
        public DateTime RFASplittedReferralDate { get; set; }
    }
}
