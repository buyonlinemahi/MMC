using MMCApp.Domain.Models.DurationTypeModel;
using MMCApp.Domain.Models.IntakeModel;
using MMCApp.Domain.Models.RequestTypeModel;
using MMCApp.Domain.Models.TreatmentCategoryModel;
using MMCApp.Domain.Models.TreatmentTypeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.IntakeViewModel
{
    public class RFARequestViewModel
    {
        public RFARequest rfaRequest { get; set; }
        public IEnumerable<RFARequestsDetails> rfaRequestsDetails { get; set; }
        public IEnumerable<DurationType> durationTypes { get; set; }
        public IEnumerable<RequestType> reqeustTypes { get; set; }
        public IEnumerable<TreatmentCategory> treatmentCategories { get; set; }
        public IEnumerable<TreatmentType> treatementTypes { get; set; }
        public IEnumerable<RFARequestCPTCode> rFARequestCPTCodes { get; set; }
    }
}