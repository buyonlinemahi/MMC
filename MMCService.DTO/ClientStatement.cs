using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientStatement
    {
        [DataMember]
        public int ClientStatementID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public string ClientStatementNumber { get; set; }
        [DataMember]
        public DateTime? CreationDate { get; set; }
        [DataMember]
        public string ClientStatementFileName { get; set; }
        [DataMember]
        public int UserID { get; set; }
    }
}
