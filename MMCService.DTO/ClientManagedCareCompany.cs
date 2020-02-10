using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientManagedCareCompany
    {
        [DataMember]
        public int ClientCompanyID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string CompName { get; set; }
        [DataMember]
        public string CompAddress { get; set; }
        [DataMember]
        public string CompCity { get; set; }
        [DataMember]
        public string CompZip { get; set; }
        [DataMember]
        public string CompState { get; set; }
    }
}
