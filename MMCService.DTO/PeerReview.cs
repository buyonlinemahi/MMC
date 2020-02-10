using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class PeerReview
    {
        [DataMember]
        public int PeerReviewID { get; set; }
        [DataMember]
        public string PeerReviewEmail { get; set; }
        [DataMember]
        public string PeerReviewPhone { get; set; }
        [DataMember]
        public string PeerReviewFax { get; set; }
        [DataMember]
        public string PeerReviewName { get; set; }
    }
}
