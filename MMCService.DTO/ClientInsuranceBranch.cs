using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ClientInsuranceBranch
    {
        [DataMember]
        public int ClientInsuranceBranchID { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int InsurerID { get; set; }
        [DataMember]
        public int InsuranceBranchID { get; set; }
        [DataMember]
        public string InsBranchName { get; set; }
    }
}
