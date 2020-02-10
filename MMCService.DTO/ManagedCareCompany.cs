using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ManagedCareCompany
    {
        [DataMember]
        public int CompanyId { get; set; }
        [DataMember]
        public string CompName { get; set; }
        [DataMember]
        public string CompAddress { get; set; }
        [DataMember]
        public string CompAddress2 { get; set; }
        [DataMember]
        public string CompCity { get; set; }
        [DataMember]
        public int CompStateId { get; set; }
        [DataMember]
        public string CompZip { get; set; }
        [DataMember]
        public string CompStateName { get; set; }
    }
}
