using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClaimAdministratorAllByClientID
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Ids { get; set; }
        [DataMember]
        public int ClientID { get; set; }
    }
}
