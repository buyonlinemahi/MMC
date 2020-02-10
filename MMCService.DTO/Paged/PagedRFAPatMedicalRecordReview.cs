
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedRFAPatMedicalRecordReview
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<RFAPatMedicalRecordReviewDetail> RFAPatMedicalRecordReviewDetails { get; set; }
    }
}
