
namespace MMCApp.Domain.Models.StorageModel
{
    public class StorageModel
    {
        public string path { get; set; }
        public int ClientID { get; set; }
        public int PatientID { get; set; }
        public int ClaimID { get; set; }
        public int ReferralID { get; set; }
        public string FolderName { get; set; }
    }
}
