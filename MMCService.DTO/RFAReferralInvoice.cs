using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RFAReferralInvoice
    {
        [DataMember]
        public int RFAReferralInvoiceID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public int? ClientID { get; set; }
        [DataMember]
        public string InvoiceFileName { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public int UserID { get; set; }
    }
}
