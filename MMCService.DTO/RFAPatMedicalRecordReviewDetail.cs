using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RFAPatMedicalRecordReviewDetail
    {
        [DataMember]
        public System.DateTime PatMRDocumentDate { get; set; }
        [DataMember]
        public int? RFAReferralID { get; set; }
        [DataMember]
        public string PatMRDocumentName { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public string PhysicianName { get; set; }
    }

}
