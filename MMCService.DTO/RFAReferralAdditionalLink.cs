using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RFAReferralAdditionalLink
    {
        [DataMember]
        public int RFAReferralAdditionalLinkID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int? RFAReferralInvoiceID { get; set; }
        [DataMember]
        public int? ClientStatementID { get; set; }
    }
}
