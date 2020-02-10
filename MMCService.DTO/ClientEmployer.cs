using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientEmployer
    {
        [DataMember]
        public int ClientEmployerID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int EmployerID { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string EmpAddress1 { get; set; }
        [DataMember]
        public string EmpCity { get; set; }
        [DataMember]
        public string EmpZip { get; set; }
        [DataMember]
        public string EmpState { get; set; }
    }
}
