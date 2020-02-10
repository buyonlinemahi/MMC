using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class BillingAccountReceivables
    {
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int RFAReferralInvoiceID { get; set; }
        [DataMember]
        public int? ClientStatementID { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatClaimNumber { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string InvoiceFileName { get; set; }
        [DataMember]
        public string ClientStatementNumber { get; set; }
        [DataMember]
        public string ClientStatementFileName { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
}
