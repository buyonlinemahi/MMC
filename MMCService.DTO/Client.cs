using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class Client
    {
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public int ClaimAdministratorID { get; set; }
        [DataMember]
        public string ClaimAdministratorType { get; set; }
        [DataMember]
        public string ClaimAdministratorName { get; set; }
    }
}
