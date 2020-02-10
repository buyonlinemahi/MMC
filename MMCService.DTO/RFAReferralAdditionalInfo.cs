using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class RFAReferralAdditionalInfo
    {
        [DataMember]
        public int RFAReferralAdditionalInfoID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int OriginalRFAReferralID { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }

    }
}
