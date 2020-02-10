using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class ThirdPartyAdministratorBranch
    {
        [DataMember]
        public int TPABranchID { get; set; }
        [DataMember]
        public int TPAID { get; set; }
        [DataMember]
        public string TPABranchName { get; set; }
        [DataMember]
        public string TPABranchAddress { get; set; }
        [DataMember]
        public string TPABranchAddress2 { get; set; }
        [DataMember]
        public string TPABranchCity { get; set; }
        [DataMember]
        public int TPABranchStateId { get; set; }
        [DataMember]
        public string TPABranchZip { get; set; }
        [DataMember]
        public string TPABranchPhone { get; set; }
        [DataMember]
        public string TPABranchFax { get; set; }
        [DataMember]
        public string StateName { get; set; }
    }
}
