using MMCApp.Domain.Models.MedicalRecordReview;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.MedicalRecordReviewViewModel
{
    public class RFAPatMedicalRecordReviewViewModel
    {
        public int TotalCount { get; set; }
        public IEnumerable<RFAPatMedicalRecordReviewDetail> RFAPatMedicalRecordReviewDetails { get; set; }
    }
}
