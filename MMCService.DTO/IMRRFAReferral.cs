using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class IMRRFAReferral
    {
        [DataMember]
        public int IMRRFAReferralID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int IMRRFAClaimPhysicianID { get; set; }
        [DataMember]
        public DateTime IMRApplicationReceivedDate { get; set; }
        [DataMember]
        public DateTime IMRNoticeInformationDate { get; set; }

        [DataMember]
        public DateTime? IMRCEReceivedDate { get; set; }
        [DataMember]
        public DateTime? IMRInternalReceivedDate { get; set; }
        [DataMember]
        public int? IMRReceivedVia { get; set; }
        [DataMember]
        public DateTime? IMRResponseDueDate { get; set; }
        [DataMember]
        public int? IMRPriority { get; set; }
        [DataMember]
        public int? IMRResponseType { get; set; }
        [DataMember]
        public int? IMRDecisionID { get; set; }
        [DataMember]
        public DateTime? IMRResponseDate { get; set; }
        [DataMember]
        public DateTime? IMRDecisionReceivedDate { get; set; }
       
    }
}
