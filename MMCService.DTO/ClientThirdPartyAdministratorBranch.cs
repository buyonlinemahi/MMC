using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientThirdPartyAdministratorBranch
    {
        [DataMember]
        public int ClientTPABranchID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int TPAID { get; set; }
        [DataMember]
        public int TPABranchID { get; set; }
        [DataMember]
        public string TPABranchName { get; set; }
    }
}
