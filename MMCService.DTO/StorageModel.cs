using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class StorageModel
    {
        [DataMember]
        public string path { get; set; }
        [DataMember]
        public int ClientID { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public int ClaimID { get; set; }
        [DataMember]
        public int ReferralID { get; set; }
        [DataMember]
        public string FolderName { get; set; }
    }
}
