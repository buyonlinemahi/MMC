using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class RFAPatMedicalRecordReview
    {
        [DataMember]
        public int RFAPatMedicalRecordReviewedID { get; set; }
        [DataMember]
        public int RFARecSpltID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
    }
}
